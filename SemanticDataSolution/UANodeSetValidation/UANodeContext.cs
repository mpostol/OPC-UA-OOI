
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class UANodeContext.
  /// </summary>
  internal class UANodeContext : IUANodeContext
  {

    #region creators
    internal UANodeContext(AddressSpaceContext addressSpaceContext, UAModelContext modelContext, NodeId nodeId) : this(addressSpaceContext, modelContext, null, nodeId) { }
    internal UANodeContext(AddressSpaceContext addressSpaceContext, UAModelContext modelContext, UANode node, NodeId nodeId)
    {
      this.m_Context = addressSpaceContext;
      this.UANode = node;
      this.NodeIdContext = nodeId;
      this.m_UAModelContext = modelContext;
      this.InRecursionChain = false;
    }
    #endregion

    #region IUANodeContext
    /// <summary>
    /// Gets the instance of <see cref="UANode" /> of this context source
    /// </summary>
    /// <value>The source UA node from the model.</value>
    public UANode UANode { get; set; }
    /// <summary>
    /// Gets the instance of <see cref="IUAModelContext" />, which the node is defined in.
    /// </summary>
    /// <value>The model context.</value>
    public IUAModelContext UAModelContext
    {
      get { return m_UAModelContext; }
    }
    /// <summary>
    /// Gets the branch name to calculate the node path - it is name part of the browse name or symbolic name depending which one is not empty.
    /// </summary>
    /// <value>The name of the path.</value>
    public string BranchName
    {
      get { return String.IsNullOrEmpty(this.UANode.SymbolicName) ? this.UANode.NamePartOfBrowseName() : this.UANode.SymbolicName; }
    }
    /// <summary>
    /// Processes the node references to calculate all relevant properties. Must be called after finishing import of all the parent model <see cref="IAddressSpaceContext.ImportNodeSet" />
    /// </summary>
    /// <param name="createType">delegate function to create top level definition for children like methods.</param>
    /// <param name="traceEvent">A delegate action to report and error and trace processing progress.</param>
    void IUANodeContext.CalculateNodeReferences(IExportNodeContainer nodeContainer, Action<TraceMessage> traceEvent)
    {
      Errors = new List<BuildError>();
      m_ModelingRule = new Nullable<ModelingRules>();
      List<IUAReferenceContext> _references = new List<IUAReferenceContext>();
      List<UAReferenceContext> _children = new List<UAReferenceContext>();
      Dictionary<string, UANodeContext> _derivedChildren = null;
      foreach (UAReferenceContext _rfx in m_Context.GetMyReferences(this))
      {
        switch (_rfx.ReferenceKind)
        {
          case ReferenceKindEnum.Custom:
            XmlQualifiedName _ReferenceType = _rfx.GetReferenceTypeName(this.m_UAModelContext, traceEvent);
            if (_ReferenceType == XmlQualifiedName.Empty)
            {
              BuildError _err = BuildError.DanglingReferenceType;
              traceEvent(TraceMessage.BuildErrorTraceMessage(_err, "Compilation error"));
              Errors.Add(_err);
            }
            _references.Add(_rfx);
            //OldModel.Reference _or = new OldModel.Reference()
            //{
            //  IsInverse = !_rfx.Reference.IsForward,
            //  IsOneWay = false, //TODO no info provided; 
            //  ReferenceType = _ReferenceType,
            //  TargetId = _rfx.BrowsePath(x => { Errors.Add(x); traceEvent(TraceMessage.BuildErrorTraceMessage(x, "Compilation error")); }),
            //};
            break;
          case ReferenceKindEnum.HasComponent:
            if (_rfx.SourceNodeContext == this)
              _children.Add(_rfx);
            break;
          case ReferenceKindEnum.HasProperty:
            if ((_rfx.SourceNodeContext == this) && (!(_rfx.SourceNodeContext.UANode is UADataType) || _rfx.TargetNodeContext.UANode.BrowseName.CompareTo("EnumStrings") != 0))
              _children.Add(_rfx);
            break;
          case ReferenceKindEnum.HasModellingRule:
            m_ModelingRule = _rfx.GetModelingRule();
            break;
          case ReferenceKindEnum.HasSubtype:
            m_BaseTypeNode = _rfx.SourceNodeContext;
            break;
          case ReferenceKindEnum.HasTypeDefinition:
            m_BaseTypeNode = _rfx.TargetNodeContext;
            _derivedChildren = _rfx.TargetNodeContext.GetDerivedChildren();
            Debug.Assert(!m_IsProperty, "Has property ");
            m_IsProperty = _rfx.TargetNodeContext.IsPropertyVariableType;
            break;
        }
      }
      m_References = _references.Count == 0 ? null : _references.ToArray<IUAReferenceContext>();
      _children = _children.Where<UAReferenceContext>(x => _derivedChildren == null || !_derivedChildren.ContainsKey(x.TargetNodeContext.UANode.NamePartOfBrowseName())).ToList<UAReferenceContext>();
      foreach (UAReferenceContext _rc in _children)
        ModelDesignFactory.ValidateExportNode(_rc.TargetNodeContext, nodeContainer, _rc, traceEvent);
    }
    /// <summary>
    /// Converts the <paramref name="nodeId" /> representing instance of <see cref="Opc.Ua.NodeId" /> and returns <see cref="XmlQualifiedName" />
    /// representing the BrowseName name <see cref="UANode" /> of the node pointed out by it.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="traceEvent">A delegate action to report an error and trace processing progress.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the BrowseName of <see cref="UANode" /> of the node indexed by <paramref name="nodeId" /></returns>
    XmlQualifiedName IUANodeContext.ExportNodeId(string nodeId, NodeId defaultValue, Action<TraceMessage> traceEvent)
    {
      return m_Context.ExportNodeId(nodeId, defaultValue, m_UAModelContext, traceEvent);
    }
    /// <summary>
    /// Exports the browse name of the node.
    /// </summary>
    /// <param name="traceEvent">delegate action to report and error and trace processing progress.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the browse name of the node.</returns>
    XmlQualifiedName IUANodeContext.ExportNodeBrowseName(Action<TraceMessage> traceEvent)
    {
      Debug.Assert(UANode != null, "Processing of undefined node");
      string _broseName = UANode.BrowseName;
      if (String.IsNullOrEmpty(UANode.BrowseName))
      {
        NodeId _id = NodeId.Parse(UANode.NodeId);
        _broseName = string.Format("{1}:EmptyBrowseName{0}", _id.Identifier, _id.NamespaceIndex);
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.EmptyBrowseName, String.Format("New identifier {0} is generated to proceed.", _broseName)));
      }
      return m_Context.ExportQualifiedName(_broseName, m_UAModelContext);
    }
    /// <summary>
    /// Gets the the base type.
    /// </summary>
    /// <value>An instance of <see cref="XmlQualifiedName"/> representing the base type.</value>
    /// 
    XmlQualifiedName IUANodeContext.BaseType(Action<TraceMessage> traceEvent)
    {
      return m_BaseTypeNode == null ? null : m_BaseTypeNode.ExportBrowseName(traceEvent);
    }
    /// <summary>
    /// Gets a value indicating whether this instance is property.
    /// </summary>
    /// <value><c>true</c> if this instance is property; otherwise, <c>false</c>.</value>
    bool IUANodeContext.IsProperty { get { return m_IsProperty; } }
    /// <summary>
    /// Gets the modeling rule.
    /// </summary>
    /// <value>The modeling rule. Null if valid modeling rule cannot be recognized.</value>
    ModelingRules? IUANodeContext.ModelingRule { get { return m_ModelingRule; } }
    /// <summary>
    /// Gets the references.
    /// </summary>
    /// <value>The references of the node.</value>
    IUAReferenceContext[] IUANodeContext.References { get { return m_References; } }
    #endregion

    #region public API
    internal bool InRecursionChain { get; set; }
    internal void BuildSymbolicId(List<string> path, Action<BuildError> reportError)
    {
      if (this.UANode == null)
      {
        reportError(BuildError.DanglingReferenceTarget);
        return;
      }
      IEnumerable<UAReferenceContext> _parentConnector = m_Context.GetReferences2Me(this).Where<UAReferenceContext>(x => x.ChildConnector);
      Debug.Assert(_parentConnector.Count<UAReferenceContext>() <= 1);
      UAReferenceContext _connector = _parentConnector.FirstOrDefault<UAReferenceContext>();
      if (_connector != null)
        _connector.BuildSymbolicId(path, reportError);
      path.Add(BranchName);
    }
    internal NodeId NodeIdContext { get; private set; }
    #endregion

    #region IEquatable<IUANodeContext>
    public bool Equals(IUANodeContext other)
    {
      return this.NodeIdContext == ((UANodeContext)other).NodeIdContext;
    }
    #endregion

    #region private
    //vars
    private UAModelContext m_UAModelContext = null;
    private List<BuildError> Errors { get; set; }
    private IUAReferenceContext[] m_References = null;
    //private IExportInstanceFactory[] m_Children = null;
    private ModelingRules? m_ModelingRule;
    private UANodeContext m_BaseTypeNode;
    private bool m_IsProperty = false;
    //methods
    private XmlQualifiedName ExportBrowseName(Action<TraceMessage> traceEvent)
    {
      if (this.NodeIdContext == Opc.Ua.ObjectTypeIds.BaseObjectType)
        return null;
      if (this.NodeIdContext == Opc.Ua.VariableTypeIds.BaseDataVariableType)
        return null;
      if (this.NodeIdContext == Opc.Ua.VariableTypeIds.PropertyType)
        return null;
      return m_Context.ExportBrowseName(this.NodeIdContext, m_UAModelContext, traceEvent);
    }
    private AddressSpaceContext m_Context = default(AddressSpaceContext);
    private Dictionary<string, UANodeContext> GetDerivedChildren()
    {
      List<UANodeContext> _derivedChildren = new List<UANodeContext>();
      m_Context.GetDerivedInstances(this, _derivedChildren);
      Dictionary<string, UANodeContext> _ret = new Dictionary<string, UANodeContext>();
      foreach (UANodeContext item in _derivedChildren)
      {
        string _key = item.UANode.NamePartOfBrowseName();
        if (_ret.ContainsKey(_key))
          continue;
        _ret.Add(_key, item);
      }
      return _ret;
    }
    public bool IsPropertyVariableType
    {
      get { return this.NodeIdContext == Opc.Ua.VariableTypeIds.PropertyType; }
    }
    #endregion

  }
}
