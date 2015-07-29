
using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal class UAModelContext
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="UAModelContext" /> class.
    /// </summary>
    /// <param name="nodeIdAlias">The node identifier aliases table.</param>
    /// <param name="modelNamespaceUris">The model namespace uris table.</param>
    /// <param name="addressSpaceContext">The address space context.</param>
    internal UAModelContext(NodeIdAlias[] nodeIdAlias, string[] modelNamespaceUris, AddressSpaceContext addressSpaceContext)
    {
      if (nodeIdAlias == null)
        throw new ArgumentNullException("nodeIdAlias");
      if (modelNamespaceUris == null)
        modelNamespaceUris = new string[] { };
      if (addressSpaceContext == null)
        throw new ArgumentNullException("addressSpaceContext");
      AddAlias(nodeIdAlias);
      m_ModelNamespaceUris = modelNamespaceUris;
      m_AddressSpaceContext = addressSpaceContext;
    }
    #endregion

    #region public
    /// <summary>
    /// Exports the node identifier.
    /// </summary>
    /// <param name="nodeId">The node identifier as the string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="traceEvent">An <see cref="Action" /> delegate is used to trace event as the <see cref="TraceMessage" />.</param>
    /// <returns>The identifier an object of <see cref="System.Xml.XmlQualifiedName" /> or null if <paramref name="nodeId" /> has default value.</returns>
    internal XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue, Action<TraceMessage> traceEvent)
    {
      NodeId _id = ImportNodeId(nodeId, true, traceEvent);
      if (_id == defaultValue)
        return null;
      return m_AddressSpaceContext.ExportBrowseName(_id, traceEvent);
    }
    internal AddressSpaceContext AddressSpaceContext
    {
      get { return m_AddressSpaceContext; }
    }
    internal UANodeContext GetOrCreateNodeContext(string nodeId, bool lookupAlias, Action<TraceMessage> traceEvent)
    {
      NodeId _id = ImportNodeId(nodeId, lookupAlias, traceEvent);
      return m_AddressSpaceContext.GetOrCreateNodeContext(_id, this, traceEvent);
    }
    internal NodeId ImportNodeId(string nodeId, bool lookupAlias, Action<TraceMessage> traceEvent)
    {
      if (String.IsNullOrEmpty(nodeId))
        return NodeId.Null;
      // lookup alias.
      if (lookupAlias)
        nodeId = LookupAlias(nodeId);
      // parse the string.
      NodeId _nodeId = NodeId.Parse(nodeId);
      if (_nodeId.NamespaceIndex > 0)
      {
        ushort namespaceIndex = ImportNamespaceIndex(_nodeId.NamespaceIndex);
        _nodeId = new NodeId(_nodeId.IdentifierPart, namespaceIndex);
      }
      return _nodeId;
    }
    internal NodeId ImportExpandedNodeId(string nodeId, bool lookupAlias)
    {
      if (string.IsNullOrEmpty(nodeId))
        return NodeId.Null;
      // lookup alias.
      if (lookupAlias)
        nodeId = LookupAlias(nodeId);
      ExpandedNodeId _expandedNodeId = ExpandedNodeId.Parse(nodeId);
      if (_expandedNodeId.IsAbsolute)
        throw new NotImplementedException();
      if (_expandedNodeId.ServerIndex > 0)
        throw new NotImplementedException();
      ushort namespaceIndex = _expandedNodeId.NamespaceIndex;
      if (_expandedNodeId.NamespaceIndex > 0)
        namespaceIndex = ImportNamespaceIndex(_expandedNodeId.NamespaceIndex);
      return new NodeId(_expandedNodeId.Identifier, namespaceIndex);
    }
    internal QualifiedName ImportQualifiedName(QualifiedName source)
    {
      return new QualifiedName(source.Name, ImportNamespaceIndex(source.NamespaceIndex));
    }
    internal XmlQualifiedName ExportQualifiedName(QualifiedName source)
    {
      return new XmlQualifiedName(source.Name, m_AddressSpaceContext.GetNamespace(source.NamespaceIndex));
    }
    private void AddAlias(NodeIdAlias[] nodeIdAlias)
    {
      foreach (var _alias in nodeIdAlias)
        m_AliasesDictionary.Add(_alias.Alias, _alias.Value);
    }
    #endregion

    #region private
    //vars
    private AddressSpaceContext m_AddressSpaceContext;
    private Action<TraceMessage> m_TraceEvent = x => { };
    private string[] m_ModelNamespaceUris;
    private Dictionary<string, string> m_AliasesDictionary = new Dictionary<string, string>();
    //methods
    private string LookupAlias(string id)
    {
      string _newId = String.Empty;
      return m_AliasesDictionary.TryGetValue(id, out _newId) ? _newId : id;
    }
    private ushort ImportNamespaceIndex(ushort namespaceIndex)
    {
      // nothing special required for indexes < 0.
      if (namespaceIndex < 1)
        return namespaceIndex;
      // return a bad value if parameters are bad.
      string _identifier = "NameUnknown";
      if (m_ModelNamespaceUris != null || m_ModelNamespaceUris.Length < namespaceIndex - 1)
        _identifier = m_ModelNamespaceUris[namespaceIndex - 1];
      return m_AddressSpaceContext.GetIndexOrAppend(_identifier, m_TraceEvent);
    }
    #endregion



  }

}
