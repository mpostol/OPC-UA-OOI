
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class AddressSpaceContext - responsible to manage all nodes in the OPC UA Address Space.
  /// </summary>
  sealed public class AddressSpaceContext : IAddressSpaceContext
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSpaceContext" /> class.
    /// </summary>
    /// <param name="traceEvent">Encapsulates an action to trace the progress and validation issues.</param>
    /// <exception cref="System.ArgumentNullException">traceEvent - cannot be null.</exception>
    public AddressSpaceContext(Action<TraceMessage> traceEvent)
    {
      if (traceEvent == null)
        throw new ArgumentNullException("traceEvent");
      m_NamespaceTable = new NamespaceTable(traceEvent);
      m_TraceEvent = x => { };
      traceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext creator - starting address space validation."));
      UANodeSet _standard = UANodeSet.ReadUADefinedTypes();
      Debug.Assert(_standard != null);
      traceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext - uploading the OPC UA defined types."));
      ImportNodeSet(_standard);
      m_TraceEvent = traceEvent;
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext - has bee created successfully."));
    }
    #endregion

    #region IAddressSpaceContext
    /// <summary>
    /// Sets the information model factory, which can be used to export a part of the OPC UA Address Space. If not set or set null an internal stub implementation will be used.
    /// </summary>
    /// <value>The information model factory.</value>
    /// <remarks>It is defined to handle dependency injection.</remarks>
    public IModelFactory InformationModelFactory
    {
      set
      {
        if (value == null)
          m_InformationModelFactory = new InformationModelFactoryBase();
        else
          m_InformationModelFactory = value;
      }
      private get { return m_InformationModelFactory; }
    }
    /// <summary>
    /// Imports a part of the OPC UA Address Space contained in the <see cref="UANodeSet" /> object model.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <exception cref="System.ArgumentNullException">model;the model cannot be null</exception>
    void IAddressSpaceContext.ImportUANodeSet(UANodeSet model)
    {
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.ImportUANodeSet - importing from object model."));
      if (model == null)
        throw new ArgumentNullException("model", "the model cannot be null");
      ImportNodeSet(model);
    }
    /// <summary>
    /// Imports a part of the OPC UA Address Space contained in the file <see cref="FileInfo" />.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <exception cref="System.IO.FileNotFoundException">The imported file does not exist</exception>
    void IAddressSpaceContext.ImportUANodeSet(FileInfo model)
    {
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.ImportUANodeSet - importing form file"));
      if (model == null)
        throw new ArgumentNullException("model", "the model cannot be null");
      if (!model.Exists)
        throw new FileNotFoundException("The imported file does not exist", model.FullName);
      UANodeSet _nodeSet = UANodeSet.ReadModellFile(model);
      ImportNodeSet(_nodeSet);
    }
    void IAddressSpaceContext.ValidateAndExportModel()
    {
      int _nsi = Math.Max(m_NamespaceTable.Count - 1, 0);
      string _namespace = m_NamespaceTable.GetString((uint)_nsi);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Entering IAddressSpaceContext.ValidateAndExportModel - starting for the {0} namespace.", _namespace)));
      ValidateAndExportModel(_nsi);
    }
    /// <summary>
    /// Validates and exports the selected model.
    /// </summary>
    /// <param name="targetNamespace">The target namespace of the validated model.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">targetNamespace;Cannot find this namespace</exception>
    void IAddressSpaceContext.ValidateAndExportModel(string targetNamespace)
    {
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("Entering IAddressSpaceContext.ValidateAndExportModel - starting for the {0} namespace.", targetNamespace)));
      int _nsIndex = m_NamespaceTable.GetIndex(targetNamespace);
      if (_nsIndex == -1)
        throw new ArgumentOutOfRangeException("targetNamespace", "Cannot find this namespace");
      ValidateAndExportModel(_nsIndex);
    }
    #endregion

    #region internal API of this service
    /// <summary>
    /// Converts the <paramref name="nodeId" /> representing instance of <see cref="NodeId" /> and returns <see cref="XmlQualifiedName" />
    /// representing the <see cref="UANode.BrowseName" /> of the node pointed out by it.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="modelContext">The model context for NodeSet.</param>
    /// <param name="traceEvent">Encapsulates an action used to trace events.</param>
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
    /// Converts the <paramref name="browseName" /> representing <see cref="QualifiedName" /> to instance of <see cref="XmlQualifiedName" />.
    /// </summary>
    /// <param name="browseName">Name of the browse.</param>
    /// <param name="modelContext">The model context.</param>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing <see cref="UANode.BrowseName" />.</returns>
    internal XmlQualifiedName ExportQualifiedName(string browseName, UAModelContext modelContext)
    {
      if (String.IsNullOrEmpty(browseName))
        return null;
      QualifiedName _qn = modelContext.ImportQualifiedName(browseName, m_NamespaceTable);
      return new XmlQualifiedName(_qn.Name, m_NamespaceTable.GetString(_qn.NamespaceIndex));
    }
    /// <summary>
    /// Gets the namespace.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    internal string GetNamespace(ushort namespaceIndex)
    {
      return m_NamespaceTable.GetString(namespaceIndex);
    }
    /// <summary>
    /// Exports the current namespace table containing all namespaces relevant for exported model.
    /// </summary>
    /// <returns>Array of relevant namespaces as the <see cref="System.String"/>.</returns>
    internal string[] ExportNamespaceTable()
    {
      return m_NamespaceTable.ToArray();
    }
    internal UANodeContext ImportNodeId(string nodeId, UAModelContext modelContext, bool lookupAlias, Action<TraceMessage> traceEvent)
    {
      NodeId _id = modelContext.ImportNodeId(nodeId, m_NamespaceTable, lookupAlias, traceEvent);
      UANodeContext _ret;
      string _idKey = _id.ToString();
      if (!m_NodesDictionary.TryGetValue(_idKey, out _ret))
      {
        _ret = new UANodeContext(this, modelContext, _id);
        m_NodesDictionary.Add(_idKey, _ret);
      }
      return _ret;
    }
    internal XmlQualifiedName ExportBrowseName(NodeId nodeId, UAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      UANodeContext _ret = TryGetUANodeContext(nodeId, traceEvent);
      if (_ret == null)
        return null;
      return ExportQualifiedName(_ret.UANode.BrowseName, modelContext);
    }
    internal IEnumerable<UAReferenceContext> GetReferences2Me(UANodeContext index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => x.TargetNode == index && x.ParentNode != index);
    }
    internal IEnumerable<UAReferenceContext> GetMyReferences(UANodeContext index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => (x.ParentNode == index));
    }
    internal void GetDerivedInstances(UANodeContext rootNode, List<UANodeContext> list)
    {
      List<UANodeContext> _col = new List<UANodeContext>();
      _col.Add(rootNode);
      GetBaseTypes(rootNode, _col);
      foreach (UANodeContext _type in _col)
        GetChildren(_type, list);
    }
    #endregion

    #region private
    //vars
    private IModelFactory m_InformationModelFactory = new InformationModelFactoryBase();
    private Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
    private NamespaceTable m_NamespaceTable = null;
    private Dictionary<string, UANodeContext> m_NodesDictionary = new Dictionary<string, UANodeContext>();
    private Action<TraceMessage> m_TraceEvent = x => { };
    //methods
    private void ImportNodeSet(UANodeSet model)
    {
      if (model.ServerUris != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "ServerUris is omitted during the import"));
      if (model.Extensions != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Extensions is omitted during the import"));
      string _namespace = model.NamespaceUris == null ? m_NamespaceTable.GetString(0) : model.NamespaceUris[0];
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Entering AddressSpaceContext.ImportNodeSet - starting import {0}.", _namespace)));
      UAModelContext _modelContext = new UAModelContext(model.Aliases, model.NamespaceUris, this);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext.ImportNodeSet - context for imported model is created and starting import nodes."));
      foreach (UANode _nd in model.Items)
        this.ImportUANode(_nd, _modelContext);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Finishing AddressSpaceContext.ImportNodeSet - imported {0} nodes.", model.Items.Length)));
    }
    private void ImportUANode(UANode node, UAModelContext modelContext)
    {
      try
      {
        if (node == null)
          m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeCannotBeNull, "At Importing UANode."));
        NodeId nodeId = modelContext.ImportNodeId(node.NodeId, m_NamespaceTable, false, m_TraceEvent);
        UANodeContext _newNode = null;
        string nodeIdKey = nodeId.ToString();
        if (!m_NodesDictionary.TryGetValue(nodeIdKey, out _newNode))
        {
          _newNode = new UANodeContext(this, modelContext, node, nodeId);
          m_NodesDictionary.Add(nodeIdKey, _newNode);
        }
        else
        {
          if (_newNode.UANode != null)
            m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdDuplicated, String.Format("The {0} is already defined.", node.NodeId.ToString())));
          _newNode.UANode = node;
        }
        foreach (Reference _rf in node.References)
        {
          UAReferenceContext _rs = UAReferenceContext.NewReferenceStub(_rf, this, modelContext, _newNode, m_TraceEvent);
          if (!m_References.ContainsKey(_rs.Key))
            m_References.Add(_rs.Key, _rs);
        }
      }
      catch (Exception _ex)
      {
        string _msg = String.Format("ImportUANode {1} is interrupted by exception {0}", _ex.Message, node.NodeId);
        m_TraceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
      }
    }
    private UANodeContext TryGetUANodeContext(NodeId nodeId, Action<TraceMessage> traceEvent)
    {
      UANodeContext _ret;
      if (!m_NodesDictionary.TryGetValue(nodeId.ToString(), out _ret))
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
      IEnumerable<UANodeContext> _derived = m_References.Values.Where<UAReferenceContext>(x => (x.TypeNode.NodeIdContext == ReferenceTypeIds.HasSubtype) && (x.TargetNode == rootNode)).
                                                                Select<UAReferenceContext, UANodeContext>(x => x.SourceNode);
      inheritanceChain.AddRange(_derived);
      if (_derived.Count<UANodeContext>() > 1)
        throw new ArgumentOutOfRangeException("To many subtypes");
      else if (_derived.Count<UANodeContext>() == 1)
        GetBaseTypes(_derived.First<UANodeContext>(), inheritanceChain);
      rootNode.InRecursionChain = false;
    }
    private void ValidateAndExportModel(int nameSpaceIndex)
    {
      IEnumerable<UANodeContext> _stubs = from _key in m_NodesDictionary.Values where _key.NodeIdContext.NamespaceIndex == nameSpaceIndex select _key;
      List<UANodeContext> _nodes = (from _node in _stubs where _node.UANode != null && (_node.UANode is UAType) select _node).ToList();
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("AddressSpaceContext.CreateModelDesign - selected {0} nodes to be added to the model.", _nodes.Count)));
      Validator.ValidateExportModel(_nodes, InformationModelFactory, this, m_TraceEvent);
    }
    #endregion

    #region UnitTestd
#if DEBUG
    internal UANodeContext UTTryGetUANodeContext(NodeId nodeId)
    {
      return TryGetUANodeContext(nodeId, x => { });
    }
#endif
    #endregion

  }

}
