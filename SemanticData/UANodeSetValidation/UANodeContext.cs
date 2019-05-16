//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
    /// <param name="addressSpaceContext">The address space context.</param>
    /// <param name="modelContext">The model context.</param>
    /// <param name="nodeId">An object of <see cref="NodeId"/> that stores an identifier for a node in a server's address space.</param>
    internal UANodeContext(IAddressSpaceBuildContext addressSpaceContext, IUAModelContext modelContext, NodeId nodeId)
    {
      NodeIdContext = nodeId;
      this.m_AddressSpaceContext = addressSpaceContext;
      this.UAModelContext = modelContext;
    }
    #endregion

    #region IUANodeContext
    /// <summary>
    /// Builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    /// <param name="traceEvent">A delegate <see cref="Action{TraceMessage}"/> encapsulates an action to report any errors and trace processing progress.</param>
    public void BuildSymbolicId(List<string> path)
    {
      if (this.UANode == null)
      {
        BuildErrorsHandling.Log.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, $"The target node NodeId={this.NodeIdContext}, current path {string.Join(", ", path)}"));
        return;
      }
      IEnumerable<UAReferenceContext> _parentConnector = m_AddressSpaceContext.GetReferences2Me(this).Where<UAReferenceContext>(x => x.ChildConnector);
      Debug.Assert(_parentConnector.Count<UAReferenceContext>() <= 1); //TODO #40; ValidateAndExportModel shall export also instances #40
      UAReferenceContext _connector = _parentConnector.FirstOrDefault<UAReferenceContext>();
      if (_connector != null)
        _connector.BuildSymbolicId(path);
      string _BranchName = string.IsNullOrEmpty(this.UANode.SymbolicName) ? this.BrowseName.Name : this.UANode.SymbolicName;
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
      if (UANode != null)
      {
        BuildErrorsHandling.Log.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdDuplicated, string.Format("The {0} is already defined and is removed from further processing.", node.NodeId.ToString())));
        return;
      }
      UANode = node;
      QualifiedName _broseName = node.BrowseName.Parse(BuildErrorsHandling.Log.TraceEvent);
      if (QualifiedName.IsNull(_broseName))
      {
        NodeId _id = NodeId.Parse(UANode.NodeId);
        _broseName = new QualifiedName(string.Format("EmptyBrowseName{0}", _id.IdentifierPart), _id.NamespaceIndex);
        BuildErrorsHandling.Log.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.EmptyBrowseName, string.Format("New identifier {0} is generated to proceed.", _broseName)));
      }
      BrowseName = UAModelContext.ImportQualifiedName(_broseName);
      foreach (Reference _reference in node.References)
      {
        UAReferenceContext _newReference = new UAReferenceContext(_reference, this.m_AddressSpaceContext, UAModelContext, this);
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
        if (m_BaseTypeNode == null)
          m_BaseTypeNode = m_AddressSpaceContext.GetBaseTypeNode(node.NodeClassEnum);
        addReference(_newReference);
      }
    }
    #endregion

    #region IUANodeBase
    /// <summary>
    /// Exports the browse name of the wrapped node by this instance.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the BrowseName of the node.</returns>
    public XmlQualifiedName ExportNodeBrowseName()
    {
      return UAModelContext.ExportQualifiedName(BrowseName);
    }
    /// <summary>
    /// Processes the node references to calculate all relevant properties. Must be called after finishing import of all the parent models.
    /// </summary>
    /// <param name="nodeFactory">The node container.</param>
    void IUANodeBase.CalculateNodeReferences(INodeFactory nodeFactory)
    {
      if (nodeFactory == null)
        throw new ArgumentNullException(nameof(nodeFactory), $"{nodeFactory} must not be null in {nameof(IUANodeBase.CalculateNodeReferences)}");
      List<UAReferenceContext> _children = new List<UAReferenceContext>();
      Dictionary<string, IUANodeBase> _derivedChildren = m_BaseTypeNode == null ? new Dictionary<string, IUANodeBase>() : m_BaseTypeNode.GetDerivedInstances();
      foreach (UAReferenceContext _rfx in m_AddressSpaceContext.GetMyReferences(this))
      {
        switch (_rfx.ReferenceKind)
        {
          case ReferenceKindEnum.Custom:
            XmlQualifiedName _ReferenceType = _rfx.GetReferenceTypeName();
            if (_ReferenceType == XmlQualifiedName.Empty)
            {
              BuildError _err = BuildError.DanglingReferenceTarget;
              BuildErrorsHandling.Log.TraceEvent(TraceMessage.BuildErrorTraceMessage(_err, "Information"));
            }
            IReferenceFactory _or = nodeFactory.NewReference();
            _or.IsInverse = !_rfx.Reference.IsForward;
            _or.ReferenceType = _ReferenceType;
            _or.TargetId = _rfx.BrowsePath();
            break;
          case ReferenceKindEnum.HasComponent:
            if (_rfx.SourceNode == this)
              _children.Add(_rfx);
            break;
          case ReferenceKindEnum.HasProperty:
            if ((_rfx.SourceNode == this) &&
              (!(_rfx.SourceNode.UANode.NodeClassEnum == NodeClassEnum.UADataType) || _rfx.TargetNode.UANode.BrowseName.CompareTo("EnumStrings") != 0))
              _children.Add(_rfx);
            break;
          case ReferenceKindEnum.HasModellingRule:
            break;
          case ReferenceKindEnum.HasSubtype:
            break;
          case ReferenceKindEnum.HasTypeDefinition: //Recognize problems with P3.7.13 HasTypeDefinition ReferenceType #39
            IsProperty = _rfx.TargetNode.IsPropertyVariableType;
            break;
        }
      }
      foreach (UAReferenceContext _rc in _children)
      {
        try
        {
          IUANodeBase _instanceDeclaration = null;
          if (!string.IsNullOrEmpty(_rc.TargetNode.BrowseName.Name))
            _instanceDeclaration = _derivedChildren.ContainsKey(_rc.TargetNode.BrowseName.Name) ? _derivedChildren[_rc.TargetNode.BrowseName.Name] : null;
          Validator.ValidateExportNode(_rc.TargetNode, _instanceDeclaration, nodeFactory, _rc, BuildErrorsHandling.Log.TraceEvent);
        }
        catch (Exception ex)
        {
          throw;
        }
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
    /// Exports the BrowseName of the BaseType.
    /// </summary>
    /// <param name="type">if set to <c>true</c> the source node represents type. <c>false</c> if it is an instance.</param>
    /// <returns>XmlQualifiedName.</returns>
    /// <value>An instance of <see cref="XmlQualifiedName" /> representing the base type.</value>
    public XmlQualifiedName ExportBaseTypeBrowseName(bool type)
    {
      return m_BaseTypeNode == null ? null : m_BaseTypeNode.ExportBrowseNameBaseType(x => TraceErrorUndefinedBaseType(x, type, BuildErrorsHandling.Log.TraceEvent));
    }
    /// <summary>
    /// Gets the modeling rule associated with this node.
    /// </summary>
    /// <value>The modeling rule. Null if valid modeling rule cannot be recognized.</value>
    public ModelingRules? ModelingRule { get; private set; } = new Nullable<ModelingRules>();
    /// <summary>
    /// Gets the parameters.
    /// </summary>
    /// <param name="arguments">The <see cref="XmlElement"/> encapsulates the arguments.</param>
    /// <returns>Parameter[].</returns>
    public Parameter[] GetParameters(XmlElement arguments)
    {
      List<Parameter> _parameters = new List<Parameter>();
      foreach (DataSerialization.Argument _item in arguments.GetParameters())
        _parameters.Add(UAModelContext.ExportArgument(_item));
      return _parameters.ToArray();
    }
    /// <summary>
    /// Converts the <paramref name="nodeId" /> representing instance of <see cref="NodeId" /> and returns <see cref="XmlQualifiedName" />
    /// representing the BrowseName name of the <see cref="UANode" /> pointed out by it.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the BrowseName of <see cref="UANode" /> of the node indexed by <paramref name="nodeId" /></returns>
    public XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue)
    {
      return UAModelContext.ExportBrowseName(nodeId, defaultValue);
    }
    /// <summary>
    /// Gets the instance of <see cref="UAModelContext" />containing definition of this node.
    /// </summary>
    /// <value>The model context for this node.</value>
    public IUAModelContext UAModelContext { get; } = null;
    /// <summary>
    /// Exports the browse name of the base type.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>XmlQualifiedName.</returns>
    public XmlQualifiedName ExportBrowseNameBaseType(Action<NodeId> traceEvent)
    {
      //TODO It cannot be the reference type
      if (this.NodeIdContext == ObjectTypeIds.BaseObjectType)
        return null;
      if (this.NodeIdContext == VariableTypeIds.BaseDataVariableType)
        return null;
      if (this.NodeIdContext == VariableTypeIds.PropertyType)
        return null;
      if (QualifiedName.IsNull(BrowseName))
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
    public Dictionary<string, IUANodeBase> GetDerivedInstances()
    {
      List<IUANodeBase> _derivedChildren = new List<IUANodeBase>();
      m_AddressSpaceContext.GetDerivedInstances(this, _derivedChildren);
      Dictionary<string, IUANodeBase> _ret = new Dictionary<string, IUANodeBase>();
      foreach (UANodeContext item in _derivedChildren)
      {
        //TODO break in case of loop
        string _key = item.BrowseName.Name;
        if (_ret.ContainsKey(_key))
          continue;
        _ret.Add(_key, item);
      }
      return _ret;
    }
    /// <summary>
    /// Gets or sets the name of the browse.
    /// </summary>
    /// <value>The name of the browse.</value>
    public QualifiedName BrowseName { get; set; } = QualifiedName.Null;
    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
    public bool Equals(IUANodeBase other)
    {
      if (Object.ReferenceEquals(other, null))
        return false;
      return this.BrowseName == other.BrowseName &&
        this.ModelingRule == other.ModelingRule &&
        this.UANode == other.InheritedUANode;
    }
    bool IUANodeBase.IsPropertyVariableType => this.NodeIdContext == VariableTypeIds.PropertyType;
    public UANode InheritedUANode
    {
      get
      {
        UANode _ret = null;
        if (!(this.m_BaseTypeNode is null))
          _ret = m_BaseTypeNode.InheritedUANode;
        else
          _ret = this.UANode?.Clone();
        return _ret;
      }
    }
    public void RemoveInheritedValues(IUANodeBase instanceDeclaration)
    {
      if (instanceDeclaration is null)
        return;
      this.UANode.RemoveInheritedValues(instanceDeclaration.UANode);
      if (this.ModelingRule == instanceDeclaration.ModelingRule)
        this.ModelingRule = null;
    }
    #endregion

    #region private
    private IUANodeBase m_BaseTypeNode;
    private readonly IAddressSpaceBuildContext m_AddressSpaceContext = null;
    //methods
    /// <summary>
    /// Gets or sets the name of the m browse.
    /// </summary>
    /// <value>The name of the m browse.</value>
    private void TraceErrorUndefinedBaseType(NodeId target, bool type, Action<TraceMessage> traceEvent)
    {
      if (type)
      {
        string _msg = string.Format("BaseType of Id={0} for node {1}", target, this.BrowseName);
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasSubtypeTarget, _msg));
      }
      else
      {
        string _msg = string.Format("TypeDefinition of Id={0} for node {1}", target, this.BrowseName);
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasTypeDefinition, _msg));
      }
    }
    #endregion

  }
}
