//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class AddressSpaceContext - responsible to manage all nodes in the OPC UA Address Space.
  /// </summary>
  internal class AddressSpaceContext : IAddressSpaceContext, IAddressSpaceBuildContext, IAddressSpaceValidationContext//, IAddressSpaceURIRecalculate
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSpaceContext" /> class.
    /// </summary>
    /// <param name="traceEvent">Encapsulates an action to trace the progress and validation issues.</param>
    /// <exception cref="ArgumentNullException">traceEvent - traceEvent - cannot be null</exception>
    public AddressSpaceContext(Action<TraceMessage> traceEvent)
    {
      m_TraceEvent = new ValidationBuildErrorsHandling(traceEvent);
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext creator - starting creation the OPC UA Address Space."));
      UANodeSet _standard = UANodeSet.ReadUADefinedTypes();
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage("Address Space - the OPC UA defined has been uploaded."));
      ImportNodeSet(_standard);
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage("Address Space - has bee created successfully."));
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
    /// Imports a part of the OPC UA Address Space contained in the <see cref="UANodeSet" /> object model.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <exception cref="System.ArgumentNullException">model;the model cannot be null</exception>
    Uri IAddressSpaceContext.ImportUANodeSet(UANodeSet model)
    {
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.ImportUANodeSet - importing from object model."));
      if (model == null)
        throw new ArgumentNullException("model", "the model cannot be null");
      return ImportNodeSet(model);
    }

    /// <summary>
    /// Imports a part of the OPC UA Address Space contained in the file <see cref="FileInfo" />.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <exception cref="System.IO.FileNotFoundException">The imported file does not exist</exception>
    Uri IAddressSpaceContext.ImportUANodeSet(FileInfo model)
    {
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.ImportUANodeSet - importing form file"));
      if (model == null)
        throw new ArgumentNullException("model", "the model cannot be null");
      if (!model.Exists)
        throw new FileNotFoundException("The imported file does not exist", model.FullName);
      UANodeSet _nodeSet = UANodeSet.ReadModelFile(model);
      return ImportNodeSet(_nodeSet);
    }

    /// <summary>
    /// Validates and exports the selected model for the default namespace at index 1 if defined or standard OPC UA.
    /// </summary>
    void IAddressSpaceContext.ValidateAndExportModel()
    {
      foreach (IModelTableEntry _nsi in m_NamespaceTable.Models)
      {
        int indes = m_NamespaceTable.GetURIIndex(_nsi.ModelUri);
        m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("Entering AddressSpaceContext.ValidateAndExportModel - starting for the {0} namespace.", _nsi.ModelUri)));
        ValidateAndExportModel(indes);
      }
    }

    /// <summary>
    /// Validates and exports the selected model.
    /// </summary>
    /// <param name="targetNamespace">The target namespace of the validated model.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">targetNamespace;Cannot find this namespace</exception>
    void IAddressSpaceContext.ValidateAndExportModel(Uri targetNamespace)
    {
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("Entering IAddressSpaceContext.ValidateAndExportModel - starting for the {0} namespace.", targetNamespace)));
      int _nsIndex = m_NamespaceTable.GetURIIndex(targetNamespace);
      if (_nsIndex == -1)
        throw new ArgumentOutOfRangeException("targetNamespace", "Cannot find this namespace");
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
        ValueRank = argument.ValueRank.GetValueRank(m_TraceEvent.TraceEvent)
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
      return m_NamespaceTable.GetURIatIndex(namespaceIndex).ModelUri.ToString();
    }

    /// <summary>
    /// Gets my references from the main collection.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>An instance of the <see cref="IEnumerable{UAReferenceContext}"/> containing references pointed out by index.</returns>
    IEnumerable<UAReferenceContext> IAddressSpaceBuildContext.GetMyReferences(IUANodeBase index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => (x.ParentNode == index));
    }

    /// <summary>
    /// Gets the references2 me.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>IEnumerable&lt;UAReferenceContext&gt;.</returns>
    IEnumerable<UAReferenceContext> IAddressSpaceBuildContext.GetReferences2Me(IUANodeContext index)
    {
      return m_References.Values.Where<UAReferenceContext>(x => x.TargetNode == index && x.ParentNode != index);
    }

    /// <summary>
    /// Gets the children nodes for the <paramref name="rootNode" />.
    /// </summary>
    /// <param name="rootNode">The root node of the requested children.</param>
    /// <returns>Return an instance of <see cref="IEnumerable" /> capturing all children of the selected node.</returns>
    public IEnumerable<IUANodeBase> GetChildren(IUANodeContext rootNode)
    {
      return m_References.Values.Where<UAReferenceContext>(x => x.SourceNode == rootNode).
                                                                  Where<UAReferenceContext>(x => (x.ReferenceKind == ReferenceKindEnum.HasProperty || x.ReferenceKind == ReferenceKindEnum.HasComponent)).
                                                                  Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
    }

    public Parameter ExportArgument(DataSerialization.Argument argument)
    {
      XmlQualifiedName _dataType = ExportBrowseName(NodeId.Parse(argument.DataType.Identifier), DataTypeIds.BaseDataType);
      return ExportArgument(argument, _dataType);
    }

    //TODO #40 remove commented functionality
    ///// <summary>
    ///// Gets an instance of the <see cref="IAddressSpaceBuildContext"/> representing selected by <paramref name="nodeClass"/> base type node if applicable, null otherwise.
    ///// </summary>
    ///// <param name="nodeClass">The node class selector.</param>
    ///// <returns>An  instance of <see cref="IUANodeBase"/> representing base type for selected node class.</returns>
    ///// <exception cref="ApplicationException"> If <paramref name="nodeClass"/> is equal <see cref="NodeClassEnum.Unknown"/></exception>
    //IUANodeBase IAddressSpaceBuildContext.GetBaseTypeNode(NodeClassEnum nodeClass)
    //{
    //  IUANodeContext _ret = null;
    //  switch (nodeClass)
    //  {
    //    case NodeClassEnum.UADataType:
    //      m_NodesDictionary.TryGetValue(DataTypeIds.BaseDataType.ToString(), out _ret);
    //      break;
    //    case NodeClassEnum.UAMethod:
    //      break;
    //    case NodeClassEnum.UAObjectType:
    //    case NodeClassEnum.UAObject:
    //      m_NodesDictionary.TryGetValue(ObjectTypeIds.BaseObjectType.ToString(), out _ret);
    //      break;
    //    case NodeClassEnum.UAReferenceType:
    //      m_NodesDictionary.TryGetValue(ReferenceTypeIds.References.ToString(), out _ret);
    //      break;
    //    case NodeClassEnum.UAVariable:
    //    case NodeClassEnum.UAVariableType:
    //      m_NodesDictionary.TryGetValue(VariableTypeIds.BaseVariableType.ToString(), out _ret);
    //      break;
    //    case NodeClassEnum.UAView:
    //      break;
    //    case NodeClassEnum.Unknown:
    //      throw new ApplicationException($"In {nameof(IAddressSpaceBuildContext.GetBaseTypeNode)} the {nameof(NodeClass)} must not be {nameof(NodeClassEnum.Unknown)}");
    //  }
    //  return _ret;
    //}

    #endregion IAddressSpaceBuildContext

    #region IAddressSpaceValidationContext

    /// <summary>
    /// Exports the current namespace table containing all namespaces that have been registered.
    /// </summary>
    /// <value>An instance of <see cref="IEnumerable{IModelTableEntry}" /> containing.</value>
    public IEnumerable<IModelTableEntry> ExportNamespaceTable => m_NamespaceTable.Models;

    #endregion IAddressSpaceValidationContext

    #region private

    //typeS

    //TODO Add a warning that the AS contains nodes orphaned and inaccessible for browsing starting from the Root node #529
    private class ValidationBuildErrorsHandling : IBuildErrorsHandling
    {
      public ValidationBuildErrorsHandling(Action<TraceMessage> traceEvent)
      {
        _TraceEvent = traceEvent ?? throw new ArgumentNullException("traceEvent", "traceEvent - cannot be null");
      }

      #region IBuildErrorsHandling

      public event Action<TraceMessage> TraceEventAction;

      public void TraceEvent(TraceMessage traceMessage)
      {
        _TraceEvent(traceMessage);
        if (traceMessage.BuildError.Focus != Focus.Diagnostic)
          Errors++;
      }

      #endregion IBuildErrorsHandling

      internal int Errors { get; private set; } = 0;

      private readonly Action<TraceMessage> _TraceEvent;
    }

    //vars

    private IModelFactory m_InformationModelFactory = new InformationModelFactoryBase();
    private Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
    private NamespaceTable m_NamespaceTable = new NamespaceTable();
    private Dictionary<string, IUANodeContext> m_NodesDictionary = new Dictionary<string, IUANodeContext>();
    private readonly ValidationBuildErrorsHandling m_TraceEvent = null;

    //methods
    private Uri ImportNodeSet(UANodeSet model)
    {
      IUAModelContext _modelContext = model.ParseUAModelContext(m_NamespaceTable, m_TraceEvent.TraceEvent);
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage($"Entering AddressSpaceContext.ImportNodeSet - starting import {_modelContext}."));
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext.ImportNodeSet - the context for the imported model is created and starting import nodes."));
      foreach (UANode _nd in model.Items)
        ImportUANode(_nd);
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage($"Finishing AddressSpaceContext.ImportNodeSet - imported {model.Items.Length} nodes."));
      return _modelContext.ModelUri;
    }

    private void ImportUANode(UANode node)
    {
      try
      {
        NodeId _nodeId = NodeId.Parse(node.NodeId);
        IUANodeContext _newNode = GetOrCreateNodeContext(_nodeId, x => new UANodeContext(_nodeId, this, m_TraceEvent.TraceEvent));
        _newNode.Update(node, _reference =>
              {
                if (!m_References.ContainsKey(_reference.Key))
                  m_References.Add(_reference.Key, _reference);
              });
      }
      catch (Exception _ex)
      {
        string _msg = string.Format("ImportUANode {1} is interrupted by exception {0}", _ex.Message, node.NodeId);
        m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
      }
    }

    private IUANodeContext TryGetUANodeContext(NodeId nodeId)
    {
      if (!m_NodesDictionary.TryGetValue(nodeId.ToString(), out IUANodeContext _ret))
      {
        m_TraceEvent.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, string.Format("References to node with NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      if (_ret.UANode == null)
      {
        m_TraceEvent.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, string.Format("NodeId: {0} is omitted during the import.", nodeId)));
        return null;
      }
      return _ret;
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
      IValidator validator = new Validator(this, m_TraceEvent);
      IEnumerable<IUANodeContext> _stubs = from _key in m_NodesDictionary.Values where _key.NodeIdContext.NamespaceIndex == nameSpaceIndex select _key;
      List<IUANodeContext> _nodes = (from _node in _stubs where _node.UANode != null && (_node.UANode is UAType) select _node).ToList();
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage($"Selected {_nodes.Count} types to be validated."));
      IUANodeBase _objects = TryGetUANodeContext(UAInformationModel.ObjectIds.ObjectsFolder);
      if (_objects is null)
        throw new ArgumentNullException("Cannot find ObjectsFolder in the standard information model");
      IEnumerable<IUANodeContext> _allInstances = m_References.Values.Where<UAReferenceContext>(x => (x.SourceNode.NodeIdContext == ObjectIds.ObjectsFolder) &&
                                                                                                     (x.TypeNode.NodeIdContext == ReferenceTypeIds.Organizes) &&
                                                                                                     (x.TargetNode.NodeIdContext.NamespaceIndex == nameSpaceIndex))
                                                                                                     .Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
      m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage($"Selected {_allInstances.Count<IUANodeContext>()} instances referenced by the ObjectsFolder to be validated."));
      _nodes.AddRange(_allInstances);
      foreach (IModelTableEntry _ns in ExportNamespaceTable)
      {
        string _publicationDate = _ns.PublicationDate.HasValue ? _ns.PublicationDate.Value.ToShortDateString() : DateTime.UtcNow.ToShortDateString();
        string _version = _ns.Version;
        InformationModelFactory.CreateNamespace(_ns.ModelUri.ToString(), _publicationDate, _version);
      }
      foreach (IUANodeBase _item in _nodes)
      {
        try
        {
          validator.ValidateExportNode(_item, InformationModelFactory);
        }
        catch (Exception _ex)
        {
          string msg = string.Format("Error caught while processing the node {0}. The message: {1} at {2}.", _item.UANode.NodeId, _ex.Message, _ex.StackTrace);
          m_TraceEvent.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, msg));
        }
      }
      string _msg = null;
      if (m_TraceEvent.Errors == 0)
      {
        _msg = $"Finishing Validator.ValidateExportModel - the model contains {_nodes.Count} nodes and no errors/warnings reported";
        m_TraceEvent.TraceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
      }
      else
      {
        _msg = $"Finishing Validator.ValidateExportModel - the model contains {_nodes.Count} nodes and {m_TraceEvent.Errors} errors reported.";
        m_TraceEvent.TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.ModelContainsErrors, _msg));
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