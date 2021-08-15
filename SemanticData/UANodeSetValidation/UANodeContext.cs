//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class UANodeContext - it wraps the <see cref="UANode"/> and provides functionality to analyze its semantic.
  /// </summary>
  internal class UANodeContext : IUANodeContext, IUANodeBase
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="UANodeContext" /> class.
    /// </summary>
    /// <param name="nodeId">An object of <see cref="NodeId" /> that stores an identifier for a node in a server's address space.</param>
    /// <param name="addressSpaceContext">The address space context.</param>
    /// <param name="traceMessageCallback">The trace message callback.</param>
    /// <exception cref="ArgumentNullException">traceMessageCallback</exception>
    internal UANodeContext(NodeId nodeId, IAddressSpaceBuildContext addressSpaceContext, Action<TraceMessage> traceMessageCallback)
    {
      _TraceEvent = traceMessageCallback ?? throw new ArgumentNullException(nameof(traceMessageCallback));
      NodeIdContext = nodeId;
      this.m_AddressSpaceContext = addressSpaceContext;
    }

    #endregion constructor

    #region IUANodeContext

    /// <summary>
    /// Builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    public void BuildSymbolicId(List<string> path)
    {
      if (this.UANode == null)
      {
        _TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, $"The target node NodeId={this.NodeIdContext}, current path {string.Join(", ", path)}"));
        return;
      }
      IEnumerable<UAReferenceContext> _parentConnector = m_AddressSpaceContext.GetReferences2Me(this).Where<UAReferenceContext>(x => x.ChildConnector);
      Debug.Assert(_parentConnector.Count<UAReferenceContext>() <= 1);
      UAReferenceContext _connector = _parentConnector.FirstOrDefault<UAReferenceContext>();
      if (_connector != null)
        _connector.BuildSymbolicId(path);
      string _BranchName = string.IsNullOrEmpty(this.UANode.SymbolicName) ? this.UANode.BrowseNameQualifiedName.Name : this.UANode.SymbolicName;
      path.Add(_BranchName);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the node is in recursion chain - selected for analysis second time.
    /// </summary>
    /// <value><c>true</c> if the node is in recursion chain; otherwise, <c>false</c>.</value>
    public bool InRecursionChain { get; set; } = false;

    /// <summary>
    /// Updates this instance in case the wrapped <see cref="UANode" /> is recognized in the model.
    /// </summary>
    /// <param name="node">The node <see cref="UANode" /> containing definition to be added to the model.</param>
    /// <param name="addReference">Used to add new reference to the common collection of references.</param>
    /// <exception cref="ArgumentException">node - Argument must not be null</exception>
    public void Update(UANode node, Action<UAReferenceContext> addReference)
    {
      if (node == null)
        throw new ArgumentException(nameof(node), $"Argument must not be null at {nameof(Update)} ");
      if (this.UANode != null)
      {
        _TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdDuplicated, string.Format("The {0} is already defined and is removed from further processing.", node.NodeId.ToString())));
        return;
      }
      UANode = node;
      if (node.References == null)
        return;
      foreach (Reference _reference in node.References)
      {
        UAReferenceContext _newReference = new UAReferenceContext(_reference, this.m_AddressSpaceContext, this);
        switch (_newReference.ReferenceKind)
        {
          case ReferenceKindEnum.Custom:
          case ReferenceKindEnum.HasComponent:
          case ReferenceKindEnum.HasProperty:
            break;

          case ReferenceKindEnum.HasModellingRule:
            ModelingRule = _newReference.GetModelingRule();
            break;

          case ReferenceKindEnum.HasSubtype: //TODO Part 3 7.10 HasSubtype - add test cases #35
            m_BaseTypeNode = _newReference.SourceNode;
            break;

          case ReferenceKindEnum.HasTypeDefinition: //Recognize problems with P3.7.13 HasTypeDefinition ReferenceType #39
            m_BaseTypeNode = _newReference.TargetNode;
            break;
        }
        addReference(_newReference);
      }
    }

    public IUANodeContext CreateUANodeContext(NodeId id)
    {
      return new UANodeContext(id, m_AddressSpaceContext, _TraceEvent);
    }

    #endregion IUANodeContext

    #region IUANodeBase

    /// <summary>
    /// Exports the browse name of the wrapped node by this instance.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the BrowseName of the node.</returns>
    public XmlQualifiedName ExportNodeBrowseName()
    {
      return new XmlQualifiedName(UANode.BrowseNameQualifiedName.Name, m_AddressSpaceContext.GetNamespace(UANode.BrowseNameQualifiedName.NamespaceIndex));
    }

    /// <summary>
    /// Processes the node references to calculate all relevant properties. Must be called after finishing import of all the parent models.
    /// </summary>
    /// <param name="nodeFactory">The node container.</param>
    /// <param name="validator">The validator.</param>
    /// <exception cref="ArgumentNullException"><paramref name="nodeFactory"/> must not be null.</exception>
    //TODO Import simple NodeSet2 file is incomplete #510
    void IUANodeBase.CalculateNodeReferences(INodeFactory nodeFactory, IValidator validator)
    {
      if (nodeFactory == null)
        throw new ArgumentNullException(nameof(nodeFactory), $"{nodeFactory} must not be null in {nameof(IUANodeBase.CalculateNodeReferences)}");
      if (validator is null)
        throw new ArgumentNullException(nameof(validator), $"{nameof(validator)} must not be null in {nameof(IUANodeBase.CalculateNodeReferences)}");
      List<UAReferenceContext> _children = new List<UAReferenceContext>();
      foreach (UAReferenceContext _rfx in m_AddressSpaceContext.GetMyReferences(this))
      {
        if (_rfx.TargetNode.UANode == null)
        {
          _TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, $"The Node {_rfx.TargetNode} has not been defined and is excluded from further model processing."));
          continue;
        }
        switch (_rfx.ReferenceKind)
        {
          case ReferenceKindEnum.Custom:
            //TODO NetworkIdentifier is missing in generated Model Design for DI model #51
            XmlQualifiedName _ReferenceType = _rfx.GetReferenceTypeName();
            if (_ReferenceType == XmlQualifiedName.Empty)
            {
              BuildError _err = BuildError.DanglingReferenceTarget;
              _TraceEvent(TraceMessage.BuildErrorTraceMessage(_err, "Information"));
            }
            else if (_ReferenceType == new XmlQualifiedName(BrowseNames.HasEncoding, Namespaces.OpcUa))
            {
              _TraceEvent(TraceMessage.DiagnosticTraceMessage($"Removed the graph of nodes at {_ReferenceType.ToString()} from the model"));
              return;
            }
            IReferenceFactory _or = nodeFactory.NewReference();
            _or.IsInverse = !_rfx.IsForward;
            _or.ReferenceType = _ReferenceType;
            _or.TargetId = _rfx.BrowsePath();
            break;

          case ReferenceKindEnum.HasComponent:
            _children.Add(_rfx);
            break;

          case ReferenceKindEnum.HasProperty:
            _children.Add(_rfx);
            break;

          case ReferenceKindEnum.HasModellingRule:
            break;

          case ReferenceKindEnum.HasSubtype:
            break;

          case ReferenceKindEnum.HasTypeDefinition: //TODO Recognize problems with P3.7.13 HasTypeDefinition ReferenceType #39
            IsProperty = _rfx.TargetNode.IsPropertyVariableType;
            break;
          case ReferenceKindEnum.HierarchicalReferences:
            throw new ArgumentOutOfRangeException($"the {nameof(ReferenceKindEnum.HierarchicalReferences)} is not handled");
        }
      }
      Dictionary<string, IUANodeBase> _derivedChildren = m_BaseTypeNode == null ? new Dictionary<string, IUANodeBase>() : m_BaseTypeNode.GetDerivedInstances();
      foreach (UAReferenceContext _rc in _children)
      {
        try
        {
          IUANodeBase _instanceDeclaration = null;
          if (!string.IsNullOrEmpty(_rc.TargetNode.UANode.BrowseNameQualifiedName.Name))
            _instanceDeclaration = _derivedChildren.ContainsKey(_rc.TargetNode.UANode.BrowseNameQualifiedName.Name) ? _derivedChildren[_rc.TargetNode.UANode.BrowseNameQualifiedName.Name] : null;
          if (_rc.TargetNode.Equals(_instanceDeclaration))
          {
            //_TraceEvent(TraceMessage.DiagnosticTraceMessage($"Removing instance declaration {_rc.TargetNode.ToString()}"));
            continue;
          }
          _rc.TargetNode.RemoveInheritedValues(_instanceDeclaration);
          validator.ValidateExportNode(_rc.TargetNode, nodeFactory, _rc);
        }
        catch (Exception) { throw; }
      }
    }

    /// <summary>
    /// Gets the instance of <see cref="UANode" /> of this context source
    /// </summary>
    /// <value>The source UA node from the model.</value>
    public UANode UANode { get; private set; } = null;

    /// <summary>
    /// Gets the node identifier.
    /// </summary>
    /// <value>The imported node identifier.</value>
    public NodeId NodeIdContext { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is a property.
    /// </summary>
    /// <value><c>true</c> if this instance is property; otherwise, <c>false</c>.</value>
    public bool IsProperty { get; private set; } = false;

    /// <summary>
    /// Exports the browse name of a node recognized as <see cref="ReferenceKindEnum.HasSubtype"/> or <see cref="ReferenceKindEnum.HasTypeDefinition"/> target.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> encapsulating the base type name.</returns>
    public XmlQualifiedName ExportBaseTypeBrowseName()
    {
      bool type = UANode is UAType;
      return m_BaseTypeNode == null ? null : m_BaseTypeNode.ExportBrowseNameBaseType(x => TraceErrorUndefinedBaseType(x, type, _TraceEvent));
    }

    /// <summary>
    /// Gets the modeling rule associated with this node.
    /// </summary>
    /// <value>The modeling rule. Null if valid modeling rule cannot be recognized.</value>
    public ModelingRules? ModelingRule { get; private set; } = new Nullable<ModelingRules>();

    /// <summary>
    /// Exports the browse name of this node recognized as <see cref="ReferenceKindEnum.HasSubtype" /> or <see cref="ReferenceKindEnum.HasTypeDefinition" /> target.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing subtype or type of an instance.</returns>
    public XmlQualifiedName ExportBrowseNameBaseType(Action<NodeId> traceEvent)
    {
      //TODO It cannot be the reference type
      if (this.NodeIdContext == ObjectTypeIds.BaseObjectType)
        return null;
      if (this.NodeIdContext == VariableTypeIds.BaseDataVariableType)
        return null;
      if (this.NodeIdContext == VariableTypeIds.PropertyType)
        return null;
      if (Object.ReferenceEquals(UANode, null))
      {
        traceEvent(this.NodeIdContext);
        return XmlQualifiedName.Empty;
      }
      return ExportNodeBrowseName();
    }

    /// <summary>
    /// Gets the derived instances.
    /// </summary>
    /// <returns>Dictionary&lt;System.String, IUANodeBase&gt;.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Circular loop in inheritance chain</exception>
    //TODO NetworkIdentifier is missing in generated Model Design for DI model #51
    public Dictionary<string, IUANodeBase> GetDerivedInstances()
    {
      if (m_InGetDerivedInstances)
        throw new ArgumentOutOfRangeException($"Circular loop in {nameof(GetDerivedInstances)}"); //TODO replace by the message - it is just model error.
      try
      {
        m_InGetDerivedInstances = true;
        IEnumerable<IUANodeBase> _myChildren = m_AddressSpaceContext.GetChildren(this);
        Dictionary<string, IUANodeBase> _instanceDeclarations = m_BaseTypeNode == null ? new Dictionary<string, IUANodeBase>() : m_BaseTypeNode.GetDerivedInstances();
        foreach (UANodeContext item in _myChildren)
        {
          string _key = item.UANode.BrowseNameQualifiedName.Name;
          if (_instanceDeclarations.ContainsKey(_key))
            _instanceDeclarations[_key] = item; //replace by current item that overrides the base one
          else
            _instanceDeclarations.Add(_key, item); //add derived item
        }
        return _instanceDeclarations;
      }
      finally
      {
        m_InGetDerivedInstances = false;
      }
    }

    bool IUANodeBase.IsPropertyVariableType => this.NodeIdContext == VariableTypeIds.PropertyType;

    void IUANodeBase.RemoveInheritedValues(IUANodeBase instanceDeclaration)
    {
      if (instanceDeclaration is null)
        return;
      this.UANode.RemoveInheritedValues(instanceDeclaration.UANode);
      if (this.ModelingRule == instanceDeclaration.ModelingRule)
        this.ModelingRule = null;
    }

    #endregion IUANodeBase

    #region IEquatable<IUANodeBase>

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
    public bool Equals(IUANodeBase other)
    {
      if (Object.ReferenceEquals(other, null))
        return false;
      return
        this.UANode.Equals(other.UANode);
    }

    #endregion IEquatable<IUANodeBase>

    #region object

    public override string ToString()
    {
      string browseName = this.UANode == null ? String.Empty : $", BrowseName = {this.UANode.BrowseName}";
      return $"NodeId={this.NodeIdContext}{browseName}";
    }

    #endregion object

    #region private

    private IUANodeBase m_BaseTypeNode;
    private readonly IAddressSpaceBuildContext m_AddressSpaceContext = null;
    private bool m_InGetDerivedInstances = false;

    //methods
    private void TraceErrorUndefinedBaseType(NodeId target, bool type, Action<TraceMessage> traceEvent)
    {
      if (type)
      {
        string _msg = string.Format("BaseType of Id={0} for node {1}", target, this.UANode.BrowseNameQualifiedName);
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasSubtypeTarget, _msg));
      }
      else
      {
        string _msg = string.Format("TypeDefinition of Id={0} for node {1}", target, this.UANode.BrowseNameQualifiedName);
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasTypeDefinition, _msg));
      }
    }

    private readonly Action<TraceMessage> _TraceEvent = null;

    #endregion private
  }
}