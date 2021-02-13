//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
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
    /// <param name="addressSpaceContext">The address space context represented by an instance of <see cref="IAddressSpaceBuildContext" />.</param>
    /// <param name="traceEvent">The trace event call back delegate.</param>
    /// <exception cref="ArgumentNullException">buildErrorsHandlingLog
    /// or
    /// addressSpaceContext</exception>
    internal static UAModelContext ParseUANodeSetModelHeader(IUANodeSetModelHeader modelHeader, IAddressSpaceURIRecalculate addressSpaceContext, Action<TraceMessage> traceEvent)
    {
      UAModelContext context2Return = new UAModelContext(addressSpaceContext, traceEvent, modelHeader);
      context2Return.Parse(modelHeader, addressSpaceContext);
      return context2Return;
    }

    #endregion API

    #region IUAModelContext

    public Uri ModelUri { get; private set; }

    public string ImportQualifiedName(string source)
    {
      QualifiedName _qn = QualifiedName.Parse(source);
      return new QualifiedName(_qn.Name, ImportNamespaceIndex(_qn.NamespaceIndex)).ToString();
    }

    /// <summary>
    /// Imports the node identifier if <paramref name="nodeId" /> is not empty.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <returns>An instance of the <see cref="NodeId" /> or null is the <paramref name="nodeId" /> is null or empty.</returns>
    public string ImportNodeId(string nodeId)
    {
      if (string.IsNullOrEmpty(nodeId))
        return string.Empty;
      nodeId = LookupAlias(nodeId);
      // parse the string.
      NodeId _nodeId = NodeId.Parse(nodeId);
      if (_nodeId.NamespaceIndex > 0)
      {
        ushort namespaceIndex = ImportNamespaceIndex(_nodeId.NamespaceIndex);
        _nodeId = new NodeId(_nodeId.IdentifierPart, namespaceIndex);
      }
      return _nodeId.ToString();
    }

    #endregion IUAModelContext

    #region private

    //var

    private readonly IUANodeSetModelHeader _modelHeader;
    private readonly Action<TraceMessage> _logTraceMessage;
    private readonly Dictionary<string, string> _aliasesDictionary = new Dictionary<string, string>();
    private List<string> _namespaceUris = new List<string>();
    private IAddressSpaceURIRecalculate _addressSpaceContext { get; }

    private static Random _randomNumber = new Random();

    //methods
    private UAModelContext(IAddressSpaceURIRecalculate addressSpaceContext, Action<TraceMessage> traceEvent, IUANodeSetModelHeader modelHeader)
    {
      _modelHeader = modelHeader ?? throw new ArgumentNullException(nameof(modelHeader));
      if (modelHeader.ServerUris != null && modelHeader.ServerUris.Length > 0)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "ServerUris is omitted during the import"));
      if (modelHeader.Extensions != null && modelHeader.Extensions.Length > 0)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Extensions is omitted during the import"));
      _logTraceMessage = traceEvent ?? throw new ArgumentNullException(nameof(traceEvent));
      _addressSpaceContext = addressSpaceContext ?? throw new ArgumentNullException(nameof(addressSpaceContext));
    }

    private void Parse(IUANodeSetModelHeader modelHeader, IAddressSpaceURIRecalculate addressSpaceContext)
    {
      _namespaceUris = Parse(modelHeader.NamespaceUris);
      ModelUri = Parse(modelHeader.Models, addressSpaceContext);
      Parse(modelHeader.Aliases);
    }

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
        _logTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NamespaceUrisCannotBeNull, $"Added a random URI { namespaceUris[0] } to NamespaceUris."));
      }
      for (int i = 0; i < namespaceUris.Length; i++)
        list2Return.Add(namespaceUris[i]);
      return list2Return;
    }

    private Uri Parse(ModelTableEntry[] models, IAddressSpaceURIRecalculate addressSpaceContext)
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
      else if (models.Length > 1)
        _logTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, $"Multi-model is not supported, only first model {models[0].ModelUri} is processed."));
      foreach (ModelTableEntry item in models)
        addressSpaceContext.UpadateModelOrAppend(item);
      return new UriBuilder(models[0].ModelUri).Uri;
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