//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class AddressSpaceContext - responsible to manage all nodes in the OPC UA Address Space.
  /// </summary>
  // TODO Define independent Address Space API #645
  internal class AddressSpaceContext : IAddressSpaceContext, IAddressSpaceBuildContext
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSpaceContext" /> class.
    /// </summary>
    /// <param name="traceEvent">Encapsulates an action to trace the progress and validation issues.</param>
    /// <exception cref="ArgumentNullException">traceEvent - traceEvent - cannot be null</exception>
    public AddressSpaceContext(IBuildErrorsHandling traceEvent)
    {
      m_TraceEvent = traceEvent;
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext creator - starting creation the OPC UA Address Space."));
      UANodeSet _standard = UANodeSet.ReadUADefinedTypes();
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage("Address Space - the OPC UA defined has been uploaded."));
      ImportNodeSet(_standard);
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage("Address Space - has bee created successfully."));
    }

    #endregion constructor

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
    /// Imports all OPC UA Address Space models contained in the <see cref="UANodeSet" /> XML document, and populates internal OPC UA Address Space.
    /// </summary>
    /// <remarks>
    /// The input document must be compliant with the `UANodeSet` schema.
    /// </remarks>
    /// <param name="model">The model to be imported.</param>
    /// <returns>Return a default <see cref="Uri" /> for the model defined in <see cref="UANodeSet" />.</returns>
    /// <exception cref="ArgumentNullException">model - the model cannot be null</exception>
    //TODO IAddressSpaceContext.ImportUANodeSet(System.IO.FileInfo) returned result must be tested. #626
    Uri IAddressSpaceContext.ImportUANodeSet(UANodeSet model)
    {
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"{359517792}, Entering {nameof(IAddressSpaceContext.ImportUANodeSet)} - importing from object model."));
      if (model == null)
        throw new ArgumentNullException("model", "the model cannot be null");
      return ImportNodeSet(model);
    }

    /// <summary>
    /// Imports all OPC UA Address Space models contained in the file <paramref name="document"/> described by the <see cref="FileInfo"/>, and populates internal OPC UA Address Space.
    /// </summary>
    /// <remarks>
    /// The input document must be compliant with the `UANodeSet` schema.
    /// </remarks>
    /// <param name="document">The UANodeSet document to be imported, and described by the <see cref="FileInfo"/>.</param>
    /// <returns>Return a default <see cref="Uri"/> for the model defined in a file represented by <see cref="FileInfo"/></returns>
    /// <exception cref="ArgumentNullException">model - the model cannot be null</exception>
    /// <exception cref="FileNotFoundException">The imported file does not exist</exception>
    //TODO IAddressSpaceContext.ImportUANodeSet(System.IO.FileInfo) returned result must be tested. #626
    Uri IAddressSpaceContext.ImportUANodeSet(FileInfo document)
    {
      if (document == null)
        throw new ArgumentNullException("model", "the model cannot be null");
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"190380256, Entering {nameof(IAddressSpaceContext.ImportUANodeSet)} and starting model import form file {document.Name}"));
      if (!document.Exists)
        throw new FileNotFoundException("The imported file does not exist", document.FullName);
      UANodeSet _nodeSet = UANodeSet.ReadModelFile(document);
      return ImportNodeSet(_nodeSet);
    }

    /// <summary>
    /// Validates and exports the selected model.
    /// </summary>
    /// <param name="targetNamespace">The target namespace of the validated model.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">targetNamespace;Cannot find this namespace</exception>
    void IAddressSpaceContext.ValidateAndExportModel(Uri targetNamespace)
    {
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"{856488909}, Entering IAddressSpaceContext.ValidateAndExportModel - starting for the {targetNamespace} namespace."));
      List<Uri> undefinedUriLists = new List<Uri>();
      if (!m_NamespaceTable.ValidateNamesapceTable(x => undefinedUriLists.Add(x)))
        foreach (Uri item in undefinedUriLists)
          m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.LackOfRequiredModel, $"I cannot find definition of the required model {item}"));
      int _nsIndex = m_NamespaceTable.GetURIIndex(targetNamespace);
      //TODO This example doesn't work #583 - handle this exception
      if (_nsIndex == -1)
        throw new ArgumentOutOfRangeException("targetNamespace", $"Cannot find this {targetNamespace} namespace");
      ValidateAndExportModel(_nsIndex);
    }

    #endregion IAddressSpaceContext

    #region IAddressSpaceBuildContext

    /// <summary>
    /// Search the address space to find the node <paramref name="nodeId" /> and returns <see cref="XmlQualifiedName" />
    /// encapsulating the <see cref="UANode.BrowseName" /> of this node if exist. Returns<c>null</c> otherwise.
    /// </summary>
    /// <param name="nodeId">The identifier of the node to find.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the <see cref="UANode.BrowseName" /> of the node indexed by <paramref name="nodeId" /></returns>
    public XmlQualifiedName ExportBrowseName(NodeId nodeId, NodeId defaultValue)
    {
      if (nodeId == defaultValue)
        return null;
      IUANodeContext _context = TryGetUANodeContext(nodeId);
      if (_context == null)
        return null;
      return _context.ExportNodeBrowseName();
    }

    /// <summary>
    /// Exports the argument for a method.
    /// </summary>
    /// <param name="argument">The argument - it defines a Method input or output argument specification. It is for example used in the input and output argument Properties for Methods.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <returns>Parameter.</returns>
    public Parameter ExportArgument(DataSerialization.Argument argument, XmlQualifiedName dataType)
    {
      Parameter _ret = new Parameter()
      {
        ArrayDimensions = argument.ArrayDimensions.ArrayDimensionsToString(),
        DataType = dataType,
        Identifier = new Nullable<int>(),
        Name = argument.Name,
        ValueRank = argument.ValueRank.GetValueRank(m_TraceEvent.WriteTraceMessage)
      };
      if (argument.Description != null)
        _ret.AddDescription(argument.Description.Locale, argument.Description.Text);
      return _ret;
    }

    /// <summary>
    /// Gets the or create node context.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="createUAModelContext">Delegated capturing functionality to create ua model context.</param>
    /// <returns>Returns an instance of <see cref="IUANodeContext" />.</returns>
    public IUANodeContext GetOrCreateNodeContext(NodeId nodeId, Func<NodeId, IUANodeContext> createUAModelContext)
    {
      string _idKey = nodeId.ToString();
      if (!m_NodesDictionary.TryGetValue(_idKey, out IUANodeContext _ret))
      {
        _ret = createUAModelContext(nodeId);
        m_NodesDictionary.Add(_idKey, _ret);
      }
      return _ret;
    }

    /// <summary>
    /// Gets the namespace.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    public string GetNamespace(ushort namespaceIndex)
    {
      return m_NamespaceTable.GetModelTableEntry(namespaceIndex).ToString();
    }

    /// <summary>
    /// Gets my references.
    /// </summary>
    /// <param name="node">The source node</param>
    /// <returns>Returns <see cref="IEnumerable{UAReferenceContex}"/> containing references attached to the <paramref name="node"/>.</returns>
    IEnumerable<UAReferenceContext> IAddressSpaceBuildContext.GetMyReferences(IUANodeBase node)
    {
      return m_References.Values.Where<UAReferenceContext>(x => (Object.ReferenceEquals(x.SourceNode, node)));
    }

    /// <summary>
    /// Gets the references2 me.
    /// </summary>
    /// <param name="node">The index.</param>
    /// <returns>IEnumerable&lt;UAReferenceContext&gt;.</returns>
    IEnumerable<UAReferenceContext> IAddressSpaceBuildContext.GetReferences2Me(IUANodeBase node)
    {
      return m_References.Values.Where<UAReferenceContext>(x => Object.ReferenceEquals(x.TargetNode, node) && !Object.ReferenceEquals(x.ParentNode, node));
    }

    /// <summary>
    /// Gets the children nodes (<see cref="ReferenceKindEnum.HasProperty" /> or <see cref="ReferenceKindEnum.HasComponent" />) for the <paramref name="node" />.
    /// </summary>
    /// <param name="node">The root node of the requested children.</param>
    /// <returns>Return an instance of <see cref="IEnumerable{IUANodeBase}" /> capturing all children of the selected node.</returns>
    public IEnumerable<IUANodeBase> GetChildren(IUANodeBase node)
    {
      return m_References.Values.Where<UAReferenceContext>(x => Object.ReferenceEquals(x.SourceNode, node)).
                                                           Where<UAReferenceContext>(x => x.ChildConnector).
                                                           Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
    }

    public Parameter ExportArgument(DataSerialization.Argument argument)
    {
      XmlQualifiedName _dataType = ExportBrowseName(argument.DataType.Identifier.ParseNodeId(m_TraceEvent.WriteTraceMessage), DataTypeIds.BaseDataType);
      return ExportArgument(argument, _dataType);
    }

    public void GetBaseTypes(IUANodeContext rootNode, List<IUANodeContext> inheritanceChain)
    {
      if (rootNode == null)
        throw new ArgumentNullException("rootNode");
      inheritanceChain.Add(rootNode);
      if (rootNode.InRecursionChain)
        throw new ArgumentOutOfRangeException("Circular reference");
      rootNode.InRecursionChain = true;
      IEnumerable<IUANodeContext> _derived = m_References.Values.Where<UAReferenceContext>(x => (x.TypeNode.NodeIdContext == ReferenceTypeIds.HasSubtype) && (x.TargetNode == rootNode)).
                                                                 Select<UAReferenceContext, IUANodeContext>(x => x.SourceNode);
      if (_derived.Count<IUANodeContext>() > 1)
        throw new ArgumentOutOfRangeException("To many subtypes");
      else if (_derived.Count<IUANodeContext>() == 1)
        GetBaseTypes(_derived.First<IUANodeContext>(), inheritanceChain);
      rootNode.InRecursionChain = false;
    }

    #endregion IAddressSpaceBuildContext

    #region private

    //vars

    private IModelFactory m_InformationModelFactory = new InformationModelFactoryBase();
    private Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
    private NamespaceTable m_NamespaceTable = new NamespaceTable();
    private Dictionary<string, IUANodeContext> m_NodesDictionary = new Dictionary<string, IUANodeContext>();
    private readonly IBuildErrorsHandling m_TraceEvent = null;

    //methods

    //TODO Define independent Address Space API #645
    private Uri ImportNodeSet(UANodeSet model)
    {
      Uri defaultModelUri = model.ParseUAModelContext(m_NamespaceTable, m_TraceEvent.WriteTraceMessage);
      Dictionary<string, UANode> itemsDictionary = new Dictionary<string, UANode>();
      foreach (UANode node in model.Items)
      {
        if (itemsDictionary.ContainsKey(node.NodeId))
          m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdDuplicated, $"The {node.NodeId} is already defined in the imported model and is removed from further processing."));
        else
          ImportUANode(node);
      }
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"Finished import UANodeSet for {defaultModelUri}; Imported {model.Items.Length} nodes."));
      return defaultModelUri;
    }

    private void ImportUANode(UANode node)
    {
      try
      {
        NodeId _nodeId = node.NodeIdNodeId;
        IUANodeContext _newNode = GetOrCreateNodeContext(_nodeId, x => new UANodeContext(_nodeId, this, m_TraceEvent.WriteTraceMessage));
        _newNode.Update(node, _reference =>
              {
                if (!m_References.ContainsKey(_reference.Key))
                  m_References.Add(_reference.Key, _reference);
              });
      }
      catch (Exception _ex)
      {
        string _msg = string.Format("ImportUANode {1} is interrupted by exception {0}", _ex.Message, node.NodeId);
        m_TraceEvent.TraceData(TraceEventType.Critical, 298461119, _msg);
      }
    }

    private IUANodeContext TryGetUANodeContext(NodeId nodeId)
    {
      if (!m_NodesDictionary.TryGetValue(nodeId.ToString(), out IUANodeContext _ret))
      {
        m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, string.Format("References to node with NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      if (_ret.UANode == null)
      {
        m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, string.Format("NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      return _ret;
    }

    private void ValidateAndExportModel(int nameSpaceIndex)
    {
      IValidator validator = new Validator(this, m_TraceEvent);
      IEnumerable<IUANodeBase> stubs = from _key in m_NodesDictionary.Values where _key.NodeIdContext.NamespaceIndex == nameSpaceIndex select _key;
      IEnumerable<IUANodeBase> undefindNodes = from node in stubs
                                               where Object.ReferenceEquals(node.UANode, null)
                                               select node;
      foreach (IUANodeBase item in undefindNodes)
        m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NodeCannotBeNull, $"the node {item.ToString()} is not defined in the UANodeSet model"));
      List<IUANodeBase> allNodesInConcern = (from _node in stubs where _node.UANode != null select _node).ToList<IUANodeBase>();
      List<IUANodeBase> nodes = (from _node in stubs where _node.UANode != null && (_node.UANode is UAType) select _node).ToList<IUANodeBase>();
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"938023414, Selected {nodes.Count} types to be validated."));
      IUANodeBase _objects = TryGetUANodeContext(UAInformationModel.ObjectIds.ObjectsFolder);
      if (_objects is null)
        throw new ArgumentNullException("Cannot find ObjectsFolder in the standard information model");
      IEnumerable<IUANodeContext> _allInstances = m_References.Values.Where<UAReferenceContext>(x => (x.SourceNode.NodeIdContext == ObjectIds.ObjectsFolder) &&
                                                                                                     (x.TypeNode.NodeIdContext == ReferenceTypeIds.Organizes) &&
                                                                                                     (x.TargetNode.NodeIdContext.NamespaceIndex == nameSpaceIndex))
                                                                                                     .Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
      m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"863907859, Selected {_allInstances.Count<IUANodeContext>()} instances referenced by the ObjectsFolder to be validated."));
      nodes.AddRange(_allInstances);
      foreach (IModelTableEntry modelTableEntry in m_NamespaceTable.Models)
      {
        DateTime _publicationDate = modelTableEntry.PublicationDate.HasValue ? modelTableEntry.PublicationDate.Value : DateTime.UtcNow;
        Version _version = modelTableEntry.Version;
        InformationModelFactory.CreateNamespace(modelTableEntry.ModelUri, _publicationDate, _version);
      }
      int nodesCount = nodes.Count;
      do
      {
        string doMessage = $"1606585634, Do Validator.ValidateExportModel - now the model contains {nodesCount} nodes";
        m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(doMessage));
        NodesCollection embededNodes = new NodesCollection();
        foreach (IUANodeBase item in nodes)
        {
          try
          {
            validator.ValidateExportNode(item, allNodesInConcern, InformationModelFactory, y => { if (y.NodeIdContext.NamespaceIndex == nameSpaceIndex) embededNodes.AddOrReplace(y, false); });
          }
          catch (Exception ex)
          {
            string msg = string.Format("Error caught while processing the node {0}. The message: {1} at {2}.", item.UANode.NodeId, ex.Message, ex.StackTrace);
            m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, msg));
          }
        }
        List<IUANodeBase> notReferencedNodes = embededNodes.Values.ToList<IUANodeBase>(); //.ToList<IUANodeContext>();
        //foreach (IUANodeBase node in notReferencedNodes)
        //{
        //  string message = $"{514697109} the {node} is reported as orphan";
        //  m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, message));
        //}
        nodes.Clear();// = embededNodes.ToList();
        nodesCount += nodes.Count;
      } while (nodes.Count > 0);
      //TODO The exported model doesn't contain all nodes #653 - uncomment and check all UT
      //foreach (IUANodeBase node in allNodesInConcern)
      //{
      //  string message = $"{1594962400} the {node} is not added to the exported model";
      //  m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, message));
      //}
      if (m_TraceEvent.Errors == 0)
      {
        string message = $"711552454, Finishing Validator.ValidateExportModel - the model contains {nodesCount} nodes and no errors/warnings reported";
        m_TraceEvent.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(message));
      }
      else
      {
        string message = $"226242104, Finishing Validator.ValidateExportModel - the model contains {nodesCount} nodes and {m_TraceEvent.Errors} errors reported.";
        m_TraceEvent.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, message));
      }
    }

    #endregion private

    #region Unit Test

    [System.Diagnostics.Conditional("DEBUG")]
    internal void UTAddressSpaceCheckConsistency(Action<IUANodeContext> returnValue)
    {
      foreach (IUANodeContext _node in m_NodesDictionary.Values.Where<IUANodeBase>(x => x.UANode is null))
        returnValue(_node);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    internal void UTReferencesCheckConsistency(Action<IUANodeContext, IUANodeContext, IUANodeContext, IUANodeContext> returnValue)
    {
      foreach (UAReferenceContext _node in m_References.Values)
        if (_node.SourceNode is null || _node.ParentNode is null || _node.TargetNode is null || _node.TypeNode is null)
          returnValue(_node?.SourceNode, _node?.ParentNode, _node?.TargetNode, _node?.TargetNode);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    internal void UTTryGetUANodeContext(NodeId nodeId, Action<IUANodeContext> returnValue)
    {
      returnValue(TryGetUANodeContext(nodeId));
    }

    [System.Diagnostics.Conditional("DEBUG")]
    internal void UTGetReferences(NodeId source, Action<UAReferenceContext> returnValue)
    {
      foreach (UAReferenceContext _ref in m_References.Values.Where<UAReferenceContext>(x => (x.SourceNode.NodeIdContext == source)))
        returnValue(_ref);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    internal void UTValidateAndExportModel(int nameSpaceIndex, Action<IEnumerable<IUANodeContext>> returnValue)
    {
      returnValue((from _key in m_NodesDictionary.Values where _key.NodeIdContext.NamespaceIndex == nameSpaceIndex select _key));
    }

    #endregion Unit Test
  }
}