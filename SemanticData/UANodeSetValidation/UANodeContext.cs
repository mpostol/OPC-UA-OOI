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
  internal class UANodeContext : IEquatable<UANodeContext>, IUANodeContext, IUANodeValidator
  {

    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="UANodeContext" /> class.
    /// </summary>
    /// <param name="addressSpaceContext">The address space context.</param>
    /// <param name="modelContext">The model context.</param>
    /// <param name="nodeId">An object of <see cref="NodeId"/> that stores an identifier for a node in a server's address space.</param>
    internal UANodeContext(IAddressSpaceBuildContext addressSpaceContext, IUAModelContext modelContext, NodeId nodeId)
    {
      this.m_AddressSpaceContext = addressSpaceContext;
      this.NodeIdContext = nodeId;
      this.m_ModelContext = modelContext;
      this.InRecursionChain = false;
    }
    #endregion

    #region IUANodeContext
    /// <summary>
    /// Gets the node identifier.
    /// </summary>
    /// <value>The imported node identifier.</value>
    public NodeId NodeIdContext { get; private set; }
    /// <summary>
    /// Builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    /// <param name="traceEvent">A delegate <see cref="Action{TraceMessage}"/> encapsulates an action to report any errors and trace processing progress.</param>
    public void BuildSymbolicId(List<string> path)
    {
      if (this.UANode == null)
      {
        BuildErrorsHandling.Log.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, ""));
        return;
      }
      IEnumerable<UAReferenceContext> _parentConnector = m_AddressSpaceContext.GetReferences2Me(this).Where<UAReferenceContext>(x => x.ChildConnector);
      Debug.Assert(_parentConnector.Count<UAReferenceContext>() <= 1);
      UAReferenceContext _connector = _parentConnector.FirstOrDefault<UAReferenceContext>();
      if (_connector != null)
        _connector.BuildSymbolicId(path);
      string _BranchName = string.IsNullOrEmpty(this.UANode.SymbolicName) ? this.m_BrowseName.Name : this.UANode.SymbolicName;
      path.Add(_BranchName);
    }
    /// <summary>
    /// Gets the instance of <see cref="UANode" /> of this context source
    /// </summary>
    /// <value>The source UA node from the model.</value>
    public UANode UANode { get; private set; } = null;
    /// <summary>
    /// Processes the node references to calculate all relevant properties. Must be called after finishing import of all the parent models.
    /// </summary>
    /// <param name="nodeFactory">The node container.</param>
    public void CalculateNodeReferences(INodeFactory nodeFactory)
    {
      m_ModelingRule = new Nullable<ModelingRules>();
      List<UAReferenceContext> _children = new List<UAReferenceContext>();
      Dictionary<string, IUANodeContext> _derivedChildren = null;
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
            if (_rfx.SourceNodeContext == this)
              _children.Add(_rfx);
            break;
          case ReferenceKindEnum.HasProperty:
            if ((_rfx.SourceNodeContext == this) && (!(_rfx.SourceNodeContext.UANode is UADataType) || _rfx.TargetNodeContext<IUANodeContext>().UANode.BrowseName.CompareTo("EnumStrings") != 0))
              _children.Add(_rfx);
            break;
          case ReferenceKindEnum.HasModellingRule:
            m_ModelingRule = _rfx.GetModelingRule();
            break;
          case ReferenceKindEnum.HasSubtype: //TODO Part 3 7.10 HasSubtype - add test cases #35
            m_BaseTypeNode = _rfx.SourceNodeContext;
            break;
          case ReferenceKindEnum.HasTypeDefinition: //Recognize problems with P3.7.13 HasTypeDefinition ReferenceType #39
            m_BaseTypeNode = _rfx.TargetNodeContext<IUANodeContext>();
            _derivedChildren = _rfx.TargetNodeContext<IUANodeContext>().GetDerivedChildren();
            Debug.Assert(!IsProperty, "Has property ");
            m_IsProperty = _rfx.TargetNodeContext<IUANodeContext>().IsPropertyVariableType;
            break;
        }
      }
      _children = _children.Where<UAReferenceContext>(x => _derivedChildren == null || !_derivedChildren.ContainsKey(x.TargetNodeContext<IUANodeContext>().m_BrowseName.Name)).ToList<UAReferenceContext>();
      foreach (UAReferenceContext _rc in _children)
        Validator.ValidateExportNode(_rc.TargetNodeContext<IUANodeValidator>(), nodeFactory, _rc, BuildErrorsHandling.Log.TraceEvent);
    }
    /// <summary>
    /// Gets or sets a value indicating whether the node is in recursion chain - selected for analysis second time.
    /// </summary>
    /// <value><c>true</c> if the node is in recursion chain; otherwise, <c>false</c>.</value>
    public bool InRecursionChain { get; set; }
    /// <summary>
    /// Exports the browse name of the wrapped node by this instance.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the BrowseName of the node.</returns>
    public XmlQualifiedName ExportNodeBrowseName()
    {
      return m_ModelContext.ExportQualifiedName(m_BrowseName);
    }
    public QualifiedName m_BrowseName { get; set; } = QualifiedName.Null;
    public XmlQualifiedName ExportBrowseNameBaseType(Action<NodeId> traceEvent)
    {
      //TODO It cannot be the reference type
      if (this.NodeIdContext == ObjectTypeIds.BaseObjectType)
        return null;
      if (this.NodeIdContext == VariableTypeIds.BaseDataVariableType)
        return null;
      if (this.NodeIdContext == VariableTypeIds.PropertyType)
        return null;
      if (QualifiedName.IsNull(m_BrowseName))
      {
        traceEvent(this.NodeIdContext);
        return XmlQualifiedName.Empty;
      }
      return ExportNodeBrowseName();
    }
    /// <summary>
    /// Updates this instance in case the wrapped <see cref="UANode"/> is recognized in the model.
    /// </summary>
    /// <param name="node">The node <see cref="UANode"/> containing definition to be added to the model.</param>
    public void Update(UANode node)
    {
      if (node == null)
        return;
      UANode = node;
      QualifiedName _broseName = node.BrowseName.Parse(BuildErrorsHandling.Log.TraceEvent);
      Debug.Assert(m_BrowseName != null);
      if (QualifiedName.IsNull(_broseName))
      {
        NodeId _id = NodeId.Parse(UANode.NodeId);
        _broseName = new QualifiedName(string.Format("EmptyBrowseName{0}", _id.IdentifierPart), _id.NamespaceIndex);
        BuildErrorsHandling.Log.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.EmptyBrowseName, string.Format("New identifier {0} is generated to proceed.", _broseName)));
      }
      m_BrowseName = m_ModelContext.ImportQualifiedName(_broseName);
    }

    #endregion

    #region IUANodeValidator
    /// <summary>
    /// Gets the instance of <see cref="UAModelContext" />containing definition of this node.
    /// </summary>
    /// <value>The model context for this node.</value>
    public IUAModelContext UAModelContext => m_ModelContext;    /// <summary>
                                                                /// Gets a value indicating whether this instance is a property.
                                                                /// </summary>
                                                                /// <value><c>true</c> if this instance is property; otherwise, <c>false</c>.</value>
    public bool IsProperty => m_IsProperty;
    /// <summary>
    /// Converts the <paramref name="nodeId" /> representing instance of <see cref="NodeId" /> and returns <see cref="XmlQualifiedName" />
    /// representing the BrowseName name of the <see cref="UANode" /> pointed out by it.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the BrowseName of <see cref="UANode" /> of the node indexed by <paramref name="nodeId" /></returns>
    public XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue)
    {
      return m_ModelContext.ExportBrowseName(nodeId, defaultValue);
    }
    /// <summary>
    /// Exports the BrowseName of the BaseType.
    /// </summary>
    /// <param name="type">if set to <c>true</c> the source node represents type. <c>false</c> if it is an instance.</param>
    /// <param name="traceEvent">A delegate encapsulates the action to report any error and trace processing progress.</param>
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
    public ModelingRules? ModelingRule => m_ModelingRule;
    /// <summary>
    /// Gets the parameters.
    /// </summary>
    /// <param name="arguments">The <see cref="XmlElement"/> encapsulates the arguments.</param>
    /// <returns>Parameter[].</returns>
    public Parameter[] GetParameters(XmlElement arguments)
    {
      List<Parameter> _parameters = new List<Parameter>();
      foreach (DataSerialization.Argument _item in arguments.GetParameters())
        _parameters.Add(m_ModelContext.ExportArgument(_item));
      return _parameters.ToArray();
    }

    #endregion

    #region public API
    public bool IsPropertyVariableType => this.NodeIdContext == VariableTypeIds.PropertyType;
    public Dictionary<string, IUANodeContext> GetDerivedChildren()
    {
      List<IUANodeContext> _derivedChildren = new List<IUANodeContext>();
      m_AddressSpaceContext.GetDerivedInstances(this, _derivedChildren);
      Dictionary<string, IUANodeContext> _ret = new Dictionary<string, IUANodeContext>();
      foreach (UANodeContext item in _derivedChildren)
      {
        //TODO break in case of loop
        string _key = item.m_BrowseName.Name;
        if (_ret.ContainsKey(_key))
          continue;
        _ret.Add(_key, item);
      }
      return _ret;
    }
    #endregion

    #region IEquatable<UANodeContext>
    public bool Equals(UANodeContext other)
    {
      return this.NodeIdContext.Equals(other.NodeIdContext);
    }

    #endregion
    #region private
    private IUAModelContext m_ModelContext = null;
    private ModelingRules? m_ModelingRule;
    private IUANodeContext m_BaseTypeNode;
    private bool m_IsProperty = false;
    private IAddressSpaceBuildContext m_AddressSpaceContext = null;
    //methods
    private void TraceErrorUndefinedBaseType(NodeId target, bool type, Action<TraceMessage> traceEvent)
    {
      if (type)
      {
        string _msg = string.Format("BaseType of Id={0} for node {1}", target, this.m_BrowseName);
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasSubtypeTarget, _msg));
      }
      else
      {
        string _msg = string.Format("TypeDefinition of Id={0} for node {1}", target, this.m_BrowseName);
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedHasTypeDefinition, _msg));
      }
    }
    #endregion


  }
}
