
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using System.Linq;
using OpcUa = Opc.Ua;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class AddressSpaceContext - stub class - TBD
  /// </summary>
  public class AddressSpaceContext : IAddressSpaceContext
  {
    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSpaceContext{ModelDesignType}"/> class.
    /// </summary>
    /// <param name="traceEvent">An action to trace the compilation errors.</param>
    public AddressSpaceContext(Action<TraceMessage> traceEvent)
    {
      if (traceEvent != null)
        m_TraceEvent = traceEvent;
      m_NamespaceTable = new NamespaceTable(traceEvent);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext - starting address space compilation."));
      UANodeSet _standard = UANodeSet.ReadUADefinedTypes();
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext - uploading the UA defined types."));
      Debug.Assert(_standard != null);
      ImportNodeSet(_standard, false);
    }
    #endregion

    /// <summary>
    /// Creates the instance of the address space.
    /// </summary>
    /// <param name="targetNamespace">The target namespace.</param>
    /// <param name="getNodesFromModel">An action called to get nodes from the information model.</param>
    /// <returns>An instance of <see cref="ModelDesign.ModelDesign"/> containing the model.</returns>
    public void CreateInstance(string targetNamespace, Action<IAddressSpaceContext> getNodesFromModel, IExportModelFactory factory)
    {
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.CreateInstance"));
      m_NamespaceTable.Append(targetNamespace, m_TraceEvent);
      InternalCreateInstance(getNodesFromModel, factory);
    }
    /// <summary>
    /// Creates the instance.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">filePath;The imported file does not exist</exception>
    public void CreateInstance(FileInfo filePath, IExportModelFactory factory)
    {
      if (!filePath.Exists)
        throw new FileNotFoundException("The imported file does not exist", filePath.FullName);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.CreateInstance"));
      UANodeSet _nodeSet = UANodeSet.ReadXmlFile(filePath.FullName);
      if (_nodeSet.ServerUris != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "ServerUris is omitted during the import"));
      if (_nodeSet.Extensions != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Extensions is omitted during the import"));
      m_NamespaceTable.Append(_nodeSet.NamespaceUris == null ? Namespaces.OpcUa : _nodeSet.NamespaceUris[0], m_TraceEvent);
      InternalCreateInstance(context => context.ImportNodeSet(_nodeSet, true), factory);
    }

    #region IAddressSpaceContext
    /// <summary>
    /// Analyze and imports the <see cref="UANodeSet" /> model.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <param name="validation">If set to <c>true</c> the nodes are validated and progress is traced.</param>
    public void ImportNodeSet(UANodeSet model, bool validation)
    {
      string _namespace = model.NamespaceUris == null ? m_NamespaceTable.GetString(0) : model.NamespaceUris[0];
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Entering AddressSpaceContext.ImportNodeSet - starting import {0}.", _namespace)));
      UAModelContext _modelContext = new UAModelContext(model.Aliases, model.NamespaceUris, this);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext.ImportNodeSet - context for imported model is created and starting import nodes."));
      foreach (UANode _nd in model.Items)
        this.ImportUANode(_nd, _modelContext, validation ? m_TraceEvent : x => { });
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Finishing AddressSpaceContext.ImportNodeSet - imported {0} nodes.", model.Items.Length)));
    }
    /// <summary>
    /// Exports the current namespace table containing all namespaces relevant for exported model.
    /// </summary>
    /// <returns>System.String[].</returns>
    string[] IAddressSpaceContext.ExportNamespaceTable()
    {
      return m_NamespaceTable.ToArray();
    }
    #endregion

    #region public
    /// <summary>
    /// Converts the <paramref name="nodeId" /> representing instance of <see cref="Opc.Ua.NodeId" /> and returns <see cref="XmlQualifiedName" />
    /// representing the <see cref="UANode.BrowseName" /> of the node pointed out by it.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="modelContext">The model context for NodeSet.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the <see cref="UANode.BrowseName" /> of the node indexed by <paramref name="nodeId" /></returns>
    internal XmlQualifiedName ExportNodeId(string nodeId, NodeId defaultValue, UAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      NodeId _nd = modelContext.ImportNodeId(nodeId, m_NamespaceTable, true, traceEvent);
      if (_nd == defaultValue)
        return null;
      UANodeContext _context = TryGetUANodeContext(_nd, traceEvent);
      if (_context == null)
        return null;
      QualifiedName _qn = modelContext.ImportQualifiedName(_context.UANode.BrowseName, m_NamespaceTable);
      return new XmlQualifiedName(_qn.Name, m_NamespaceTable.GetString(_qn.NamespaceIndex));
    }
    /// <summary>
    /// Converts the <paramref name="browseName" /> representing <see cref="Opc.Ua.QualifiedName" /> to instance of <see cref="XmlQualifiedName" />.
    /// </summary>
    /// <param name="browseName">Name of the browse.</param>
    /// <param name="modelContext">The model context.</param>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing <see cref="UANode.BrowseName"/>.</returns>
    internal XmlQualifiedName ExportQualifiedName(string browseName, UAModelContext modelContext)
    {
      if (String.IsNullOrEmpty(browseName))
        return null;
      QualifiedName _qn = modelContext.ImportQualifiedName(browseName, m_NamespaceTable);
      return new XmlQualifiedName(_qn.Name, m_NamespaceTable.GetString(_qn.NamespaceIndex));
    }
    ///// <summary>
    ///// Exports the argument.
    ///// </summary>
    ///// <param name="argument">The argument to be exported.</param>
    ///// <param name="modelContext">The model context.</param>
    ///// <param name="traceEvent">The trace event.</param>
    ///// <returns>Returns an instance of <see cref="OldModel.Parameter" />.</returns>
    //internal ParameterType ExportArgument<ParameterType>(Argument argument, UAModelContext modelContext, Func<Argument, XmlQualifiedName, ParameterType> createParameter)
    //{
    //  XmlQualifiedName _dataType = ExportNodeId(argument.DataType.Identifier, Opc.Ua.DataTypeIds.BaseDataType, modelContext, m_TraceEvent);
    //  return createParameter(argument, _dataType);
    //  //bool _ValueRankSpecified = false;
    //  //OldModel.ValueRank _ValueRank = argument.ValueRank.GetValueRank(x => _ValueRankSpecified = x, traceEvent);
    //  //return new OldModel.Parameter()
    //  //{
    //  //  DataType = _dataType,
    //  //  Description = argument.Description == null ? null : new OldModel.LocalizedText() { Key = argument.Description.Locale, Value = argument.Description.Text },
    //  //  Identifier = 0,
    //  //  IdentifierSpecified = false,
    //  //  Name = argument.Name,
    //  //  ValueRank = _ValueRank,
    //  //};
    //}
    /// <summary>
    /// Gets the namespace.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    internal string GetNamespace(ushort namespaceIndex)
    {
      return m_NamespaceTable.GetString(namespaceIndex);
    }
    #endregion

    //#region AddressSpaceContextService
    //protected override OldModel.ModelDesign InternalCreateInstance(Action<IAddressSpaceContext> getNodesFromModel, Action<TraceMessage> traceEvent)
    //{
    //  traceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext.InternalCreateInstance - starting address space compilation."));
    //  m_TraceEvent = traceEvent;
    //  UANodeSet _standard = Extensions.LoadResource<UANodeSet>(Extensions.UADefinedTypesName);
    //  Debug.Assert(_standard != null);
    //  ImportNodeSet(_standard, traceEvent, false);
    //  getNodesFromModel(this);
    //  return CreateModelDesign(traceEvent);
    //}
    //#endregion

    #region private
    //vars
    private Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
    private NamespaceTable m_NamespaceTable = null;
    private Dictionary<NodeId, UANodeContext> m_NodesDictionary = new Dictionary<NodeId, UANodeContext>();
    private Action<TraceMessage> m_TraceEvent = x => { };
    //methods
    private void ImportUANode(UANode node, UAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      try
      {
        if (node == null)
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeCannotBeNil, "At Importing UANode."));
        NodeId nodeId = modelContext.ImportNodeId(node.NodeId, m_NamespaceTable, false, traceEvent);
        UANodeContext _newNode = null;
        if (!m_NodesDictionary.TryGetValue(nodeId, out _newNode))
        {
          _newNode = new UANodeContext(this, modelContext, node, nodeId);
          m_NodesDictionary.Add(nodeId, _newNode);
        }
        else
        {
          if (m_NodesDictionary[_newNode.NodeIdContext].UANode != null)
            traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdDuplicated, String.Format("The {0} is already defined.", node.NodeId)));
          _newNode.UANode = node;
        }
        foreach (Reference _rf in node.References)
        {
          UAReferenceContext _rs = UAReferenceContext.NewReferenceStub(_rf, this, modelContext, _newNode, traceEvent);
          if (!m_References.ContainsKey(_rs.Key))
            m_References.Add(_rs.Key, _rs);
        }
      }
      catch (Exception _ex)
      {
        string _msg = String.Format("ImportUANode {1} is interrupted by exception {0}", _ex.Message, node.NodeId);
        traceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
      }
    }
    internal UANodeContext ImportNodeId(string nodeId, UAModelContext modelContext, bool lookupAlias, Action<TraceMessage> traceEvent)
    {
      NodeId _id = modelContext.ImportNodeId(nodeId, m_NamespaceTable, lookupAlias, traceEvent);
      UANodeContext _ret;
      if (!m_NodesDictionary.TryGetValue(_id, out _ret))
      {
        _ret = new UANodeContext(this, modelContext, _id);
        m_NodesDictionary.Add(_id, _ret);
      }
      return _ret;
    }
    /// <summary>
    /// Create instance internally.
    /// </summary>
    /// <param name="getNodesFromModel">The action to get nodes from model.</param>
    /// <param name="traceEvent">The action to trace events.</param>
    /// <returns></returns>
    private void InternalCreateInstance(Action<IAddressSpaceContext> getNodesFromModel, IExportModelFactory factory)
    {
      getNodesFromModel(this);
      CreateModelDesign(factory);
    }
    internal XmlQualifiedName ExportBrowseName(NodeId nodeId, UAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      UANodeContext _ret = TryGetUANodeContext(nodeId, traceEvent);
      if (_ret == null)
        return null;
      return ExportQualifiedName(_ret.UANode.BrowseName, modelContext);
    }
    private UANodeContext TryGetUANodeContext(NodeId nodeId, Action<TraceMessage> traceEvent)
    {
      UANodeContext _ret;
      if (!m_NodesDictionary.TryGetValue(nodeId, out _ret))
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, String.Format("References to node with NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      if (_ret.UANode == null)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, String.Format("NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      return _ret;
    }
    internal IEnumerable<UAReferenceContext> GetReferences2Me(UANodeContext index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => x.TargetNode == index && x.ParentNode != index);
    }
    internal IEnumerable<UAReferenceContext> GetMyReferences(UANodeContext index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => (x.ParentNode == index));
    }
    private void CreateModelDesign(IExportModelFactory factory)
    {
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext.CreateModelDesign - starting creation of the ModelDesign for the address space."));
      IEnumerable<UANodeContext> _stubs = from _key in m_NodesDictionary.Keys where _key.NamespaceIndex == 1 select m_NodesDictionary[_key];
      List<IUANodeContext> _nodes = (from _node in _stubs where _node.UANode != null && (_node.UANode is UAType) select _node as IUANodeContext).ToList();
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("AddressSpaceContext.CreateModelDesign - selected {0} nodes to be added to the model.", _nodes.Count)));
      ModelDesignFactory.CreateModelDesign(_nodes, factory, this, m_TraceEvent);
    }
    internal void GetDerivedInstances(UANodeContext rootNode, List<UANodeContext> list)
    {
      List<UANodeContext> _col = new List<UANodeContext>();
      _col.Add(rootNode);
      GetBaseTypes(rootNode, _col);
      foreach (UANodeContext _type in _col)
        GetChildren(_type, list);
    }
    private void GetChildren(UANodeContext type, List<UANodeContext> instances)
    {
      IEnumerable<UANodeContext> _children = m_References.Values.Where<UAReferenceContext>(x => x.SourceNode == type).
                                                                 Where<UAReferenceContext>(x => (x.ReferenceKind == ReferenceKindEnum.HasProperty || x.ReferenceKind == ReferenceKindEnum.HasComponent)).
                                                                 Select<UAReferenceContext, UANodeContext>(x => x.TargetNode);
      instances.AddRange(_children);
    }
    private void GetBaseTypes(UANodeContext rootNode, List<UANodeContext> inheritanceChain)
    {
      if (rootNode == null)
        throw new ArgumentNullException("rootNode");
      if (rootNode.InRecursionChain)
        throw new ArgumentOutOfRangeException("Circular reference");
      rootNode.InRecursionChain = true;
      IEnumerable<UANodeContext> _derived = m_References.Values.Where<UAReferenceContext>(x => (x.TypeNode.NodeIdContext == OpcUa.ReferenceTypeIds.HasSubtype) && (x.TargetNode == rootNode)).
                                                      Select<UAReferenceContext, UANodeContext>(x => x.SourceNode);
      inheritanceChain.AddRange(_derived);
      if (_derived.Count<UANodeContext>() > 1)
        throw new ArgumentOutOfRangeException("To many subtypes");
      else if (_derived.Count<UANodeContext>() == 1)
        GetBaseTypes(_derived.First<UANodeContext>(), inheritanceChain);
      rootNode.InRecursionChain = false;
    }
    #endregion

  }
}
