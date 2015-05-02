using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using OpcUa = Opc.Ua;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public class UAModelContext : IUAModelContext
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

    #region IAddressSpaceContext
    /// <summary>
    /// Exports the <see cref="Common.Types.Argument" />.
    /// </summary>
    /// <param name="argument">The argument to be exported.</param>
    /// <param name="traceEvent">An <see cref="Action" /> delegate is used to trace event as the <see cref="TraceMessage" />.</param>
    /// <returns>Returns an object of <see cref="Opc.Ua.ModelCompiler.Parameter" /> type derived from <see cref="Common.Types.Argument" />.</returns>
    public ParameterType ExportArgument<ParameterType>(DataSerialization.Argument argument, Action<TraceMessage> traceEvent, Func<Argument, XmlQualifiedName, ParameterType> createParameter)
    {
      return m_AddressSpaceContext.ExportArgument(argument, this, createParameter);
    }
    /// <summary>
    /// Exports the node identifier.
    /// </summary>
    /// <param name="nodeId">The node identifier as the string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="traceEvent">An <see cref="Action" /> delegate is used to trace event as the <see cref="TraceMessage" />.</param>
    /// <returns>The identifier an object of <see cref="System.Xml.XmlQualifiedName" /> or null if <paramref name="nodeId" /> has default value.</returns>
    public XmlQualifiedName ExportNodeId(string nodeId, uint defaultValue, Action<TraceMessage> traceEvent)
    {
      return m_AddressSpaceContext.ExportNodeId(nodeId, defaultValue, this, traceEvent);
    }
    #endregion

    #region public
    internal AddressSpaceContext AddressSpaceContext
    {
      get { return m_AddressSpaceContext; }
    }
    internal NodeId ImportNodeId(string source, NamespaceTable namespaceUris, bool lookupAlias, Action<TraceMessage> traceEvent)
    {
      if (String.IsNullOrEmpty(source))
        return NodeId.Null;
      // lookup alias.
      if (lookupAlias)
        source = LookupAlias(source);
      // parse the string.
      NodeId _nodeId = NodeId.Parse(LookupAlias(source));
      if (_nodeId.NamespaceIndex > 0)
      {
        ushort namespaceIndex = ImportNamespaceIndex(_nodeId.NamespaceIndex, namespaceUris);
        _nodeId = new NodeId(_nodeId.Identifier, namespaceIndex);
      }
      return _nodeId;
    }
    internal NodeId ImportExpandedNodeId(string source, NamespaceTable namespaceUris, bool lookupAlias)
    {
      if (string.IsNullOrEmpty(source))
        return NodeId.Null;
      // lookup alias.
      if (lookupAlias)
        source = LookupAlias(source);
      ExpandedNodeId _expandedNodeId = ExpandedNodeId.Parse(source);
      if (_expandedNodeId.IsAbsolute)
        throw new NotImplementedException();
      if (_expandedNodeId.ServerIndex > 0)
        throw new NotImplementedException();
      ushort namespaceIndex = _expandedNodeId.NamespaceIndex;
      if (_expandedNodeId.NamespaceIndex > 0)
        namespaceIndex = ImportNamespaceIndex(_expandedNodeId.NamespaceIndex, namespaceUris);
      return new NodeId(_expandedNodeId.Identifier, namespaceIndex);
    }
    /// <summary>
    /// Imports a QualifiedName
    /// </summary>
    internal QualifiedName ImportQualifiedName(string source, NamespaceTable namespaceUris)
    {
      if (String.IsNullOrEmpty(source))
        return QualifiedName.Null;
      QualifiedName _qn = QualifiedName.Parse(source);
      return new QualifiedName(_qn.Name, ImportNamespaceIndex(_qn.NamespaceIndex, namespaceUris));
    }
    internal void AddAlias(NodeIdAlias[] nodeIdAlias)
    {
      foreach (var _alias in nodeIdAlias)
      {
        NodeId _nd = NodeId.Parse(_alias.Value);
        m_AliasesDictionary.Add(_alias.Alias, _alias.Value);
      }
    }
    #endregion

    #region private
    //vars
    private AddressSpaceContext m_AddressSpaceContext;
    private Action<TraceMessage> m_TraceEvent = x => { };
    internal object GetAddressSpaceContext { get; set; }
    private string[] m_ModelNamespaceUris;
    private Dictionary<string, string> m_AliasesDictionary = new Dictionary<string, string>();
    //methods
    private string LookupAlias(string id)
    {
      string _newId = String.Empty;
      return m_AliasesDictionary.TryGetValue(id, out _newId) ? _newId : id;
    }
    /// <summary>
    ///  Imports a NodeId
    /// </summary>
    /// <summary>
    /// Get index of the namespace fro current yje table <paramref name="namespaceUris" />.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace in source file.</param>
    /// <param name="namespaceUris">The current namespace uris table.</param>
    /// <returns>Returns index from <paramref name="namespaceUris"/>.</returns>
    private ushort ImportNamespaceIndex(ushort namespaceIndex, NamespaceTable namespaceUris)
    {
      if (namespaceUris == null)
        throw new ArgumentNullException("namespaceUris");
      // nothing special required for indexes 0.
      if (namespaceIndex < 1)
        return namespaceIndex;
      // return a bad value if parameters are bad.
      string _identifier = "NameUnknown";
      if (m_ModelNamespaceUris != null || m_ModelNamespaceUris.Length < namespaceIndex - 1)
        _identifier = m_ModelNamespaceUris[namespaceIndex - 1];
      return namespaceUris.GetIndexOrAppend(_identifier, m_TraceEvent);
    }
    #endregion

  }

}
