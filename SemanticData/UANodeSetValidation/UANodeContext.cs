//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

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
      TraceEvent = traceMessageCallback ?? throw new ArgumentNullException(nameof(traceMessageCallback));
      NodeIdContext = nodeId;
      this.m_AddressSpaceContext = addressSpaceContext;
    }

    #endregion constructor

    #region IUANodeContext

    /// <summary>
    /// Builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    //NetworkIdentifier is missing in generated Model Design for DI model #629
    public void BuildSymbolicId(List<string> path)
    {
      if (this.UANode == null)
      {
        TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, $"The target node NodeId={this.NodeIdContext}, current path {string.Join(", ", path)}"));
        return;
      }
      IEnumerable<UAReferenceContext> _parentConnector = m_AddressSpaceContext.GetReferences2Me(this).Where<UAReferenceContext>(x => x.ChildConnector);
      Debug.Assert(_parentConnector.Count<UAReferenceContext>() <= 1);
      UAReferenceContext _connector = _parentConnector.FirstOrDefault<UAReferenceContext>();
      if (_connector != null)
        _connector.BuildSymbolicId(path);
      string _BranchName = string.IsNullOrEmpty(this.UANode.SymbolicName) ? this.UANode.BrowseName.Name : this.UANode.SymbolicName;
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
    public void Update(IUANode node, Action<UAReferenceContext> addReference)
    {
      if (node == null)
        throw new ArgumentException(nameof(node), $"Argument must not be null at {nameof(Update)} ");
      if (this.UANode != null)
      {
        TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdDuplicated, string.Format("The {0} is already defined and is removed from further processing.", node.NodeId.ToString())));
        return;
      }
      UANode = node;
      if (node.References == null)
        return;
      foreach (IReference _reference in node.References)
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
      return new UANodeContext(id, m_AddressSpaceContext, TraceEvent);
    }

    #endregion IUANodeContext

    #region IUANodeBase

    /// <summary>
    /// Exports the browse name of the wrapped node by this instance.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the BrowseName of the node.</returns>
    public XmlQualifiedName ExportNodeBrowseName()
    {
      return new XmlQualifiedName(UANode.BrowseName.Name, m_AddressSpaceContext.GetNamespace(UANode.BrowseName.NamespaceIndex));
    }

    /// <summary>
    /// Calculates the node references.
    /// </summary>
    /// <param name="nodeFactory">The node factory.</param>
    /// <param name="allNodesInConcern">list of selected members to export.</param>
    /// <param name="validator">The validator.</param>
    /// <param name="validateExportNode2Model">It creates the node at the top level of the model. Called if the node has reference to another node that cannot be defined as a child.</param>
    //TODO Import simple NodeSet2 file is incomplete #510
    void IUANodeBase.CalculateNodeReferences(INodeFactory nodeFactory, List<IUANodeBase> allNodesInConcern, IValidator validator, Action<IUANodeContext> validateExportNode2Model)
    {
      if (nodeFactory == null)
        throw new ArgumentNullException(nameof(nodeFactory), $"{nodeFactory} must not be null in {nameof(IUANodeBase.CalculateNodeReferences)}");
      if (validator is null)
        throw new ArgumentNullException(nameof(validator), $"{nameof(validator)} must not be null in {nameof(IUANodeBase.CalculateNodeReferences)}");
      if (validateExportNode2Model == null)
        throw new ArgumentNullException(nameof(validateExportNode2Model), $"The parameter must not be null in {nameof(IUANodeBase.CalculateNodeReferences)}");
      List<UAReferenceContext> _children = new List<UAReferenceContext>();
      foreach (UAReferenceContext _rfx in m_AddressSpaceContext.GetMyReferences(this))
      {
        if (_rfx.TargetNode.UANode == null)
        {
          TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, $"The Node {_rfx.TargetNode} has not been defined and is excluded from further model processing."));
          continue;
        }
        switch (_rfx.ReferenceKind)
        {
          case ReferenceKindEnum.Custom:
            XmlQualifiedName _ReferenceType = _rfx.GetReferenceTypeName();
            if (_ReferenceType == XmlQualifiedName.Empty)
            {
              BuildError _err = BuildError.DanglingReferenceTarget;
              TraceEvent(TraceMessage.BuildErrorTraceMessage(_err, "Information"));
            }
            else if (_ReferenceType == new XmlQualifiedName(BrowseNames.HasEncoding, Namespaces.OpcUa))
            {
              TraceEvent(TraceMessage.DiagnosticTraceMessage($"Removed the graph of nodes at {_ReferenceType.ToString()} from the model"));
              return;
            }
            IReferenceFactory _or = nodeFactory.NewReference();
            _or.IsInverse = !_rfx.IsForward;
            _or.ReferenceType = _ReferenceType;
            //TODO The exported model doesn't contain all nodes #653
            //TODO NetworkIdentifier is missing in generated Model Design for DI model #629
            _or.TargetId = _rfx.BrowsePath();
            switch (_rfx.TargetNode.UANode.NodeClass)
            {
              case NodeClassEnum.UADataType:
              case NodeClassEnum.UAObjectType:
              case NodeClassEnum.UAReferenceType:
              case NodeClassEnum.UAVariableType:
                break;

              //TODO NetworkIdentifier is missing in generated Model Design for DI model #629
              //TODO The exported model doesn't contain all nodes #653
              case NodeClassEnum.UAObject:
              case NodeClassEnum.UAVariable:
              case NodeClassEnum.UAMethod:
                //validator.ValidateExportNode(_rfx.TargetNode, allNodesInConcern, nodeFactory, validateExportNode2Model, _rfx);
                validateExportNode2Model(_rfx.TargetNode);
                break;

              case NodeClassEnum.UAView:
                TraceEvent(TraceMessage.DiagnosticTraceMessage($"Removed the graph of nodes at {_rfx.TargetNode} from the model"));
                break;

              case NodeClassEnum.Unknown:
                TraceEvent(TraceMessage.DiagnosticTraceMessage($"Removed the graph of nodes at {_rfx.TargetNode} from the model"));
                break;

              default:
                throw new ArgumentOutOfRangeException(nameof(_rfx.TargetNode.UANode.NodeClass));
            }
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
        }
      }
      //TODO The exported model doesn't contain all nodes #653
      RemoveDerivedChildren(nodeFactory, allNodesInConcern, validator, validateExportNode2Model, _children);
    }

    private void RemoveDerivedChildren(INodeFactory nodeFactory, List<IUANodeBase> allNodesInConcern, IValidator validator, Action<IUANodeContext> validateExportNode2Model,
                                       List<UAReferenceContext> children)
    {
      Dictionary<IUANodeBase, UAReferenceContext> referencedChildren = children.ToDictionary<UAReferenceContext, IUANodeBase>(x => x.TargetNode);
      NodesCollection derivedChildren = m_BaseTypeNode == null ? new NodesCollection() : m_BaseTypeNode.GetDerivedInstances();
      foreach (var _rc in referencedChildren)
      {
        IUANodeBase _instanceDeclaration = null;
        string name = _rc.Key.UANode.BrowseName.Name;
        if (!string.IsNullOrEmpty(name))
          _instanceDeclaration = derivedChildren.ContainsKey(name) ? derivedChildren[name] : null;
        if (_rc.Key.Equals(_instanceDeclaration))
        {
          TraceEvent(TraceMessage.DiagnosticTraceMessage($"{2054200566} - Removing instance declaration {_rc.Key}"));
          if (!allNodesInConcern.Remove(_rc.Key))
            TraceEvent(TraceMessage.DiagnosticTraceMessage($"{2064801864} - Derived node {_rc.Key} doesn't exist in all nodes"));
          continue;
        }
        _rc.Key.RemoveInheritedValues(_instanceDeclaration);
        validator.ValidateExportNode(_rc.Key, allNodesInConcern, nodeFactory, validateExportNode2Model, _rc.Value);
      }
    }

    /// <summary>
    /// Gets the instance of <see cref="UANode" /> of this context source
    /// </summary>
    /// <value>The source UA node from the model.</value>
    public IUANode UANode { get; private set; } = null;

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
      bool type = UANode is IUAType;
      return m_BaseTypeNode == null ? null : m_BaseTypeNode.ExportBrowseNameBaseType(x => TraceErrorUndefinedBaseType(x, type));
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
    /// <returns>An instance of <see cref="NodesCollection"/> or null if there is nothing to return</returns>
    //TODO NetworkIdentifier is missing in generated Model Design for DI model #629
    //TODO The exported model doesn't contain all nodes #653
    public NodesCollection GetDerivedInstances()
    {
      if (m_InGetDerivedInstances)
      {
        TraceMessage errorToLog = TraceMessage.BuildErrorTraceMessage(BuildError.NotValidLoopingHierarchy, $"Circular loop in {nameof(GetDerivedInstances)}");
        TraceEvent(errorToLog);
        return null;
      }
      try
      {
        m_InGetDerivedInstances = true;
        IEnumerable<IUANodeBase> _myChildren = m_AddressSpaceContext.GetChildren(this);
        NodesCollection _instanceDeclarations = m_BaseTypeNode == null ? new NodesCollection() : m_BaseTypeNode.GetDerivedInstances();
        foreach (IUANodeBase item in _myChildren)
          _instanceDeclarations.AddOrReplace(item, true);
        return _instanceDeclarations;
      }
      finally
      {
        m_InGetDerivedInstances = false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is property variable type.
    /// </summary>
    /// <value><c>true</c> if this instance is property variable type; otherwise, <c>false</c>.</value>
    bool IUANodeBase.IsPropertyVariableType => this.NodeIdContext == VariableTypeIds.PropertyType;

    /// <summary>
    /// Removes the inherited values.
    /// </summary>
    /// <param name="instanceDeclaration">The instance declaration.</param>
    /// <remarks>If a member is overridden all inherited values of the node attributes must be removed.</remarks>
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
      return this.UANode.Equals(other.UANode);
    }

    #endregion IEquatable<IUANodeBase>

    #region object

    public override string ToString()
    {
      string browseName = this.UANode == null ? " ???? " : $"{this.UANode.BrowseName}";
      return $"NodeId=\"{this.NodeIdContext}\", BrowseName=\"{browseName}\", ModellingRule=\"{ModelingRule}\"";
    }

    #endregion object

    #region private

    private IUANodeBase m_BaseTypeNode;
    private readonly IAddressSpaceBuildContext m_AddressSpaceContext = null;
    private bool m_InGetDerivedInstances = false;
    private readonly Action<TraceMessage> TraceEvent = null;

    //methods
    private void TraceErrorUndefinedBaseType(NodeId target, bool type)
    {
      if (type)
      {
        string _msg = string.Format("BaseType of Id={0} for node {1}", target, this.UANode.BrowseName);
        TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasSubtypeTarget, _msg));
      }
      else
      {
        string _msg = string.Format("TypeDefinition of Id={0} for node {1}", target, this.UANode.BrowseName);
        TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasTypeDefinition, _msg));
      }
    }

    #endregion private
  }
}