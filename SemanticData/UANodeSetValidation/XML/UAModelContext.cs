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
    #region creator

    /// <summary>
    /// Initializes a new instance of the <see cref="UAModelContext" /> class.
    /// </summary>
    /// <param name="aliases">A list of Aliases used in the UANodeSet.</param>
    /// <param name="namespaceUris">A list of NamespaceUris used in the UANodeSet.</param>
    /// <param name="addressSpaceContext">The address space context represented by an instance of <see cref="IAddressSpaceBuildContext" />.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <exception cref="ArgumentNullException">buildErrorsHandlingLog
    /// or
    /// addressSpaceContext</exception>
    internal UAModelContext(NodeIdAlias[] aliases, string[] namespaceUris, IAddressSpaceBuildContext addressSpaceContext, Action<TraceMessage> traceEvent)
    {
      Log = traceEvent ?? throw new ArgumentNullException(nameof(traceEvent));
      AddressSpaceContext = addressSpaceContext ?? throw new ArgumentNullException(nameof(addressSpaceContext));
      AddNamespaceUriTable(namespaceUris);
      AddAliases(aliases);
    }

    #endregion creator

    #region IUAModelContext

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

    private Action<TraceMessage> Log;
    private readonly Dictionary<string, string> m_AliasesDictionary = new Dictionary<string, string>();
    private readonly List<string> m_NamespaceUris = new List<string>();
    private IAddressSpaceBuildContext AddressSpaceContext { get; }
    private static int m_NamespaceCount = 0;

    //methods
    private void AddAliases(NodeIdAlias[] nodeIdAlias)
    {
      if (nodeIdAlias is null)
        return;
      foreach (NodeIdAlias _alias in nodeIdAlias)
        m_AliasesDictionary.Add(_alias.Alias.Trim(), _alias.Value);
    }

    private void AddNamespaceUriTable(string[] namespaceUris)
    {
      if (namespaceUris is null)
        return;
      for (int i = 0; i < namespaceUris.Length; i++)
        m_NamespaceUris.Add(namespaceUris[i]);
    }

    private string LookupAlias(string id)
    {
      string _newId = string.Empty;
      return m_AliasesDictionary.TryGetValue(id.Trim(), out _newId) ? _newId : id;
    }

    private ushort ImportNamespaceIndex(ushort namespaceIndex)
    {
      // nothing special required for indexes < 0.
      if (namespaceIndex == 0)
        return namespaceIndex;
      // return a new value if parameter is out of range.
      string _identifier;
      if (m_NamespaceUris.Count > namespaceIndex - 1)
        _identifier = m_NamespaceUris[namespaceIndex - 1];
      else
      {
        _identifier = $@"http://tempuri.org/NameUnknown{m_NamespaceCount++}";
        this.Log(
          TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedNamespaceIndex, $"ImportNamespaceIndex failed - namespace index {namespaceIndex - 1} is out of the NamespaceUris index. New namespace {_identifier} is created instead."));
        m_NamespaceUris.Add(_identifier);
      }
      return AddressSpaceContext.GetIndexOrAppend(_identifier);
    }

    #endregion private
  }
}