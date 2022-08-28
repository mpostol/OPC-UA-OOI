//__________________________________________________________________________________________
//
//  Copyright 2022 Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and to get started
//  comment using the discussion panel at
//  https://github.com/mpostol/TP/discussions/182
//  with an introduction of yourself and tell us about what you do with this community.
//__________________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  internal class UAModelContext : IUAModelContext
  {
    #region API

    /// <summary>
    /// Initializes a new instance of the <see cref="UAModelContext" /> class.
    /// </summary>
    /// <param name="modelHeader">The model header of the <see cref="UANodeSet"/> represented as an instance of <see cref="IUANodeSetModelHeader"/>.</param>
    /// <param name="namespaceTable">The address space context represented by an instance of <see cref="IAddressSpaceBuildContext" />.</param>
    /// <param name="traceEvent">The trace event call back delegate.</param>
    /// <exception cref="ArgumentNullException">buildErrorsHandlingLog
    /// or
    /// addressSpaceContext</exception>
    internal static UAModelContext ParseUANodeSetModelHeader(IUANodeSetModelHeader modelHeader, INamespaceTable namespaceTable, Action<TraceMessage> traceEvent)
    {
      UAModelContext context2Return = new UAModelContext(modelHeader, namespaceTable, traceEvent);
      context2Return.Parse(modelHeader, namespaceTable);
      return context2Return;
    }

    internal Uri DefaultUri => new Uri(_namespaceUris[0]);

    #endregion API

    #region IUAModelContext

    //public Uri ModelUri { get; private set; }

    /// <summary>
    /// Imports the browse name <see cref="QualifiedName" /> and Node identifier as <see cref="NodeId" />. It recalculates the <see cref="QualifiedName.NamespaceIndex" /> and <see cref="NodeId.NamespaceIndex" /> against local namespace index table.
    /// </summary>
    /// <param name="browseNameText">The <see cref="QualifiedName" /> serialized as text to be imported.</param>
    /// <param name="nodeIdText">The <see cref="NodeId" /> serialized as text to be imported.</param>
    /// <param name="trace">Captures the functionality of trace.</param>
    /// <returns>A <see cref="ValueTuple{T1, T2}" /> value containing <see cref="QualifiedName" /> and <see cref="NodeId" /> with recalculated NamespaceIndex.</returns>
    public (QualifiedName browseName, NodeId nodeId) ImportBrowseName(string browseNameText, string nodeIdText, Action<TraceMessage> trace)
    {
      nodeIdText = LookupAlias(nodeIdText);
      NodeId nodeId = nodeIdText.ParseNodeId(trace);
      QualifiedName browseName = browseNameText.ParseBrowseName(nodeId, trace);
      nodeId.SetNamespaceIndex(ImportNamespaceIndex(nodeId.NamespaceIndex));
      browseName.NamespaceIndex = ImportNamespaceIndex(browseName.NamespaceIndex);
      browseName.NamespaceIndexSpecified = true;
      return (browseName, nodeId);
    }

    /// <summary>
    /// Imports the node identifier if <paramref name="nodeId" /> is not empty.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="trace">Captures the functionality of trace.</param>
    /// <returns>An instance of the <see cref="NodeId" /> or null is the <paramref name="nodeId" /> is null or empty.</returns>
    public NodeId ImportNodeId(string nodeId, Action<TraceMessage> trace)
    {
      if (string.IsNullOrEmpty(nodeId))
        return NodeId.Null;
      nodeId = LookupAlias(nodeId);
      NodeId _nodeId = nodeId.ParseNodeId(trace);
      ushort namespaceIndex = ImportNamespaceIndex(_nodeId.NamespaceIndex);
      return new NodeId(_nodeId.IdentifierPart, namespaceIndex);
    }

    public void RegisterUAReferenceType(QualifiedName browseName)
    {
      if (UAReferenceTypNames.Contains(browseName))
      {
        string message = $"The {nameof(UAReferenceType)} duplicated BrowseName={browseName}. It is not allowed that two different ReferenceTypes have the same BrowseName";
        _logTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.DuplicatedReferenceType, message));
      }
      else
        UAReferenceTypNames.Add(browseName);
    }

    #endregion IUAModelContext

    #region private

    //var

    private readonly IUANodeSetModelHeader _modelHeader;
    private readonly Action<TraceMessage> _logTraceMessage;
    private readonly Dictionary<string, string> _aliasesDictionary = new Dictionary<string, string>();
    private IList<string> _namespaceUris = new List<string>();
    private INamespaceTable _addressSpaceContext { get; }

    private readonly List<QualifiedName> UAReferenceTypNames = new List<QualifiedName>();
    private static Random _randomNumber = new Random();

    //methods
    private UAModelContext(IUANodeSetModelHeader modelHeader, INamespaceTable addressSpaceContext, Action<TraceMessage> traceEvent)
    {
      _modelHeader = modelHeader ?? throw new ArgumentNullException(nameof(modelHeader));
      if (modelHeader.ServerUris != null && modelHeader.ServerUris.Length > 0)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "ServerUris is omitted during the import"));
      if (modelHeader.Extensions != null && modelHeader.Extensions.Length > 0)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Extensions are omitted during the import"));
      _logTraceMessage = traceEvent ?? throw new ArgumentNullException(nameof(traceEvent));
      _addressSpaceContext = addressSpaceContext ?? throw new ArgumentNullException(nameof(addressSpaceContext));
    }

    private void Parse(IUANodeSetModelHeader modelHeader, INamespaceTable namespaceTable)
    {
      _namespaceUris = Parse(modelHeader.NamespaceUris);
      Parse(modelHeader.Models, namespaceTable);
      Parse(modelHeader.Aliases);
    }

    //TODO Enhance/Improve NodeIdAlias array parser #557
    private void Parse(NodeIdAlias[] nodeIdAlias)
    {
      if (nodeIdAlias is null)
        return;
      foreach (NodeIdAlias alias in nodeIdAlias)
        _aliasesDictionary.Add(alias.Alias.Trim(), alias.Value);
    }

    private List<string> Parse(string[] namespaceUris)
    {
      List<string> list2Return = new List<string>();
      if (namespaceUris is null || namespaceUris.Length == 0)
      {
        namespaceUris = new string[] { RandomUri().ToString() };
        _logTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NamespaceUrisCannotBeNull, $"Added a random URI {namespaceUris[0]} to NamespaceUris."));
      }
      for (int i = 0; i < namespaceUris.Length; i++)
        list2Return.Add(namespaceUris[i]);
      return list2Return;
    }

    private void Parse(ModelTableEntry[] models, INamespaceTable namespaceTable)
    {
      if (models == null || models.Length == 0)
      {
        models = new ModelTableEntry[] { new ModelTableEntry()
          {
            AccessRestrictions = 0,
            ModelUri = _namespaceUris[0],
            PublicationDate = DateTime.UtcNow,
            PublicationDateSpecified = true,
            RequiredModel = new ModelTableEntry[]{ },
            RolePermissions = new RolePermission[] { },
            Version = new Version().ToString()
          }
        };
        _logTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.ModelsCannotBeNull, $"Added default model {models[0].ModelUri}"));
      }
      foreach (ModelTableEntry item in models)
      {
        namespaceTable.RegisterModel(item);
        if (item.RequiredModel != null)
          foreach (ModelTableEntry requiredModel in item.RequiredModel)
            namespaceTable.RegisterDependency(requiredModel);
      }
    }

    private string LookupAlias(string id)
    {
      string _newId = string.Empty;
      return _aliasesDictionary.TryGetValue(id.Trim(), out _newId) ? _newId : id;
    }

    private ushort ImportNamespaceIndex(ushort namespaceIndex)
    {
      // nothing special required for indexes < 0.
      if (namespaceIndex == 0)
        return namespaceIndex;
      string uriString;
      if (_namespaceUris.Count > namespaceIndex - 1)
        uriString = _namespaceUris[namespaceIndex - 1];
      else
      {
        // return a random value if index is out of range.
        uriString = RandomUri().ToString();
        this._logTraceMessage(
          TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedNamespaceIndex, $"ImportNamespaceIndex failed - namespace index {namespaceIndex - 1} is out of the NamespaceUris index. New namespace {uriString} is created instead."));
        _namespaceUris.Add(uriString);
      }
      return _addressSpaceContext.GetURIIndexOrAppend(new Uri(uriString));
    }

    private static Uri RandomUri()
    {
      UriBuilder _builder = new UriBuilder()
      {
        Path = $@"github.com/mpostol/OPC-UA-OOI/NameUnknown{_randomNumber.Next(0, int.MaxValue)}",
        Scheme = Uri.UriSchemeHttp,
      };
      return _builder.Uri;
    }

    #endregion private
  }
}