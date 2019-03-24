//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
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
  public sealed class AddressSpaceContext : IAddressSpaceContext, IAddressSpaceBuildContext, IAddressSpaceValidationContext
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSpaceContext" /> class.
    /// </summary>
    /// <param name="traceEvent">Encapsulates an action to trace the progress and validation issues.</param>
    /// <exception cref="System.ArgumentNullException">traceEvent - cannot be null.</exception>
    public AddressSpaceContext(Action<TraceMessage> traceEvent)
    {
      m_TraceEvent = traceEvent ?? throw new ArgumentNullException("traceEvent");
      m_NamespaceTable = new NamespaceTable(m_TraceEvent);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext creator - starting creation the OPC UA Address Space."));
      UANodeSet _standard = UANodeSet.ReadUADefinedTypes();
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Address Space - the OPC UA defined has been uploaded."));
      ImportNodeSet(_standard);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Address Space - has bee created successfully."));
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
      private get => m_InformationModelFactory;
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
    /// <summary>
    /// Validates and exports the selected model for the default namespace at index 1 if defined or standard OPC UA.
    /// </summary>
    void IAddressSpaceContext.ValidateAndExportModel()
    {
      int _nsi = Math.Max(m_NamespaceTable.Count - 1, 0);
      string _namespace = m_NamespaceTable.GetString((uint)_nsi);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("Entering AddressSpaceContext.ValidateAndExportModel - starting for the {0} namespace.", _namespace)));
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

    #region IAddressSpaceBuildContext
    /// <summary>
    /// Search the address space to find the node <paramref name="nodeId" /> and returns <see cref="XmlQualifiedName" />
    /// encapsulating the <see cref="UANode.BrowseName" /> of this node if exist. Returns<c>null</c> otherwise.
    /// </summary>
    /// <param name="nodeId">The identifier of the node to find.</param>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the <see cref="UANode.BrowseName" /> of the node indexed by <paramref name="nodeId" /></returns>
    public XmlQualifiedName ExportBrowseName(NodeId nodeId)
    {
      IUANodeContext _context = TryGetUANodeContext(nodeId, m_TraceEvent);
      if (_context == null)
        return null;
      return _context.ExportNodeBrowseName();
    }
    public Parameter ExportArgument(DataSerialization.Argument argument, XmlQualifiedName dataType)
    {
      Parameter _ret = new Parameter()
      {
        DataType = dataType,
        Identifier = new Nullable<int>(),
        Name = argument.Name,
        ValueRank = argument.ValueRank.GetValueRank(m_TraceEvent)
      };
      if (argument.Description != null)
        _ret.AddDescription(argument.Description.Locale, argument.Description.Text);
      return _ret;
    }
    IUANodeContext IAddressSpaceBuildContext.GetOrCreateNodeContext(NodeId nodeId, IUAModelContext modelContext)
    {
      string _idKey = nodeId.ToString();
      if (!m_NodesDictionary.TryGetValue(_idKey, out IUANodeContext _ret))
      {
        _ret = new UANodeContext(this, modelContext, nodeId);
        m_NodesDictionary.Add(_idKey, _ret);
      }
      return _ret;
    }
    public ushort GetIndexOrAppend(string value)
    {
      return m_NamespaceTable.GetIndexOrAppend(value, m_TraceEvent);
    }
    /// <summary>
    /// Gets the namespace.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    public string GetNamespace(ushort namespaceIndex)
    {
      return m_NamespaceTable.GetString(namespaceIndex);
    }
    IEnumerable<UAReferenceContext> IAddressSpaceBuildContext.GetMyReferences(IUANodeContext index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => (x.ParentNode == index));
    }
    IEnumerable<UAReferenceContext> IAddressSpaceBuildContext.GetReferences2Me(IUANodeContext index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => x.TargetNode == index && x.ParentNode != index);
    }
    void IAddressSpaceBuildContext.GetDerivedInstances(IUANodeContext rootNode, List<IUANodeContext> list)
    {
      List<IUANodeContext> _col = new List<IUANodeContext>
      {
        rootNode
      };
      GetBaseTypes(rootNode, _col);
      foreach (IUANodeContext _type in _col)
        GetChildren(_type, list);
    }
    #endregion    

    #region IAddressSpaceValidationContext
    /// <summary>
    /// Exports the current namespace table containing all namespaces relevant for exported model.
    /// </summary>
    /// <returns>Array of relevant namespaces as the <see cref="System.String"/>.</returns>
    public string[] ExportNamespaceTable()
    {
      return m_NamespaceTable.ToArray();
    }
    #endregion

    #region private
    //vars
    private IModelFactory m_InformationModelFactory = new InformationModelFactoryBase();
    private Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
    private NamespaceTable m_NamespaceTable = null;
    private Dictionary<string, IUANodeContext> m_NodesDictionary = new Dictionary<string, IUANodeContext>();
    private readonly Action<TraceMessage> m_TraceEvent = BuildErrorsHandling.Log.TraceEvent;
    //methods
    private void ImportNodeSet(UANodeSet model)
    {
      if (model.ServerUris != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "ServerUris is omitted during the import"));
      if (model.Extensions != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Extensions is omitted during the import"));
      string _namespace = model.NamespaceUris == null ? m_NamespaceTable.GetString(0) : model.NamespaceUris[0];
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("Entering AddressSpaceContext.ImportNodeSet - starting import {0}.", _namespace)));
      UAModelContext _modelContext = new UAModelContext(model, this);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext.ImportNodeSet - context for imported model is created and starting import nodes."));
      foreach (UANode _nd in model.Items)
        this.ImportUANode(_nd, _modelContext, m_TraceEvent);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("Finishing AddressSpaceContext.ImportNodeSet - imported {0} nodes.", model.Items.Length)));
    }
    private void ImportUANode(UANode node, IUAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      try
      {
        if (node == null)
          m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeCannotBeNull, "At Importing UANode."));
        NodeId nodeId = modelContext.ImportNodeId(node.NodeId, false);
        string nodeIdKey = nodeId.ToString();
        if (!m_NodesDictionary.TryGetValue(nodeIdKey, out IUANodeContext _newNode))
        {
          _newNode = new UANodeContext(this, modelContext, nodeId);
          _newNode.Update(node);
          m_NodesDictionary.Add(nodeIdKey, _newNode);
        }
        else
        {
          if (_newNode.UANode != null)
            m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdDuplicated, string.Format("The {0} is already defined.", node.NodeId.ToString())));
          _newNode.Update(node);
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
        string _msg = string.Format("ImportUANode {1} is interrupted by exception {0}", _ex.Message, node.NodeId);
        m_TraceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
      }
    }
    private IUANodeContext TryGetUANodeContext(NodeId nodeId, Action<TraceMessage> traceEvent)
    {
      if (!m_NodesDictionary.TryGetValue(nodeId.ToString(), out IUANodeContext _ret))
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, string.Format("References to node with NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      if (_ret.UANode == null)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, string.Format("NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      return _ret;
    }
    private void GetChildren(IUANodeContext type, List<IUANodeContext> instances)
    {
      IEnumerable<IUANodeContext> _children = m_References.Values.Where<UAReferenceContext>(x => x.SourceNode == type).
                                                                  Where<UAReferenceContext>(x => (x.ReferenceKind == ReferenceKindEnum.HasProperty || x.ReferenceKind == ReferenceKindEnum.HasComponent)).
                                                                  Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
      instances.AddRange(_children);
    }
    private void GetBaseTypes(IUANodeContext rootNode, List<IUANodeContext> inheritanceChain)
    {
      if (rootNode == null)
        throw new ArgumentNullException("rootNode");
      if (rootNode.InRecursionChain)
        throw new ArgumentOutOfRangeException("Circular reference");
      rootNode.InRecursionChain = true;
      IEnumerable<IUANodeContext> _derived = m_References.Values.Where<UAReferenceContext>(x => (x.TypeNode.NodeIdContext == ReferenceTypeIds.HasSubtype) && (x.TargetNode == rootNode)).
                                                                Select<UAReferenceContext, IUANodeContext>(x => x.SourceNode);
      inheritanceChain.AddRange(_derived);
      if (_derived.Count<IUANodeContext>() > 1)
        throw new ArgumentOutOfRangeException("To many subtypes");
      else if (_derived.Count<IUANodeContext>() == 1)
        GetBaseTypes(_derived.First<IUANodeContext>(), inheritanceChain);
      rootNode.InRecursionChain = false;
    }
    private void ValidateAndExportModel(int nameSpaceIndex)
    {
      IEnumerable<IUANodeContext> _stubs = from _key in m_NodesDictionary.Values where _key.NodeIdContext.NamespaceIndex == nameSpaceIndex select _key;
      //TODO ValidateAndExportModel shall export also instances #40
      List<IUANodeContext> _nodes = (from _node in _stubs where _node.UANode != null && (_node.UANode is UAType) select _node).ToList();
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("AddressSpaceContext.ValidateAndExportModel - selected {0} nodes to be added to the model.", _nodes.Count)));
      Validator.ValidateExportModel(_nodes, InformationModelFactory, this, m_TraceEvent);
    }
    #endregion

    #region UnitTestd
    [System.Diagnostics.Conditional("DEBUG")]
    internal void UTTryGetUANodeContext(NodeId nodeId, Action<IUANodeContext> returnValue)
    {
      returnValue(TryGetUANodeContext(nodeId, x => { }));
    }
    [System.Diagnostics.Conditional("DEBUG")]
    internal void UTValidateAndExportModel(int nameSpaceIndex, Action<List<IUANodeContext>> returnValue)
    {
      returnValue((from _key in m_NodesDictionary.Values where _key.NodeIdContext.NamespaceIndex == nameSpaceIndex select _key).ToList<IUANodeContext>());
    }
    #endregion

  }

}
