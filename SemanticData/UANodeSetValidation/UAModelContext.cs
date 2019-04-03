//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  internal class UAModelContext : IUAModelContext
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="UAModelContext" /> class.
    /// </summary>
    /// <param name="model">The imported model.</param>
    /// <param name="addressSpaceContext">The address space context.</param>
    /// <exception cref="ArgumentNullException">addressSpaceContext
    /// or
    /// model.Aliases</exception>
    internal UAModelContext(UANodeSet model, IAddressSpaceBuildContext addressSpaceContext)
    {
      AddressSpaceContext = addressSpaceContext ?? throw new ArgumentNullException(nameof(addressSpaceContext));
      m_UANodeSetModel = model ?? throw new ArgumentNullException(nameof(model));
      if (model.Aliases == null)
        throw new ArgumentNullException("nodeIdAlias");
      AddAlias(model.Aliases);
      model.NamespaceUris = model.NamespaceUris ?? new string[] { };
    }
    #endregion

    #region IUAModelContext
    /// <summary>
    /// Exports the node identifier.
    /// </summary>
    /// <param name="nodeId">The node identifier as the string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The identifier an object of <see cref="System.Xml.XmlQualifiedName" /> or null if <paramref name="nodeId" /> has default value.</returns>
    public XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue)
    {
      NodeId _id = ImportNodeId(nodeId, true);
      if (_id == NodeId.Null ||_id == defaultValue)
        return null;
      return AddressSpaceContext.ExportBrowseName(_id);
    }
    public Parameter ExportArgument(DataSerialization.Argument argument)
    {
      XmlQualifiedName _dataType = ExportBrowseName(argument.DataType.Identifier, DataTypeIds.BaseDataType);
      return AddressSpaceContext.ExportArgument(argument, _dataType);
    }
    public IUANodeContext GetOrCreateNodeContext(string nodeId, bool lookupAlias)
    {
      NodeId _id = ImportNodeId(nodeId, lookupAlias);
      return AddressSpaceContext.GetOrCreateNodeContext(_id, this);
    }
    public QualifiedName ImportQualifiedName(QualifiedName source)
    {
      return new QualifiedName(source.Name, ImportNamespaceIndex(source.NamespaceIndex));
    }
    /// <summary>
    /// Imports the node identifier if <paramref name="nodeId" /> is not empty.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="lookupAlias">if set to <c>true</c> lookup the aliases table .</param>
    /// <returns>An instance of the <see cref="NodeId" /> or null is the <paramref name="nodeId" /> is null or empty.</returns>
    public NodeId ImportNodeId(string nodeId, bool lookupAlias)
    {
      if (string.IsNullOrEmpty(nodeId))
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
    public XmlQualifiedName ExportQualifiedName(QualifiedName source)
    {
      return new XmlQualifiedName(source.Name, AddressSpaceContext.GetNamespace(source.NamespaceIndex));
    }
    #endregion

    #region private
    //var
    private Dictionary<string, string> m_AliasesDictionary = new Dictionary<string, string>();
    private readonly UANodeSet m_UANodeSetModel;
    private IAddressSpaceBuildContext AddressSpaceContext { get; }
    private static int m_NamespaceCount = 0; 
    //methods
    private void AddAlias(NodeIdAlias[] nodeIdAlias)
    {
      foreach (NodeIdAlias _alias in nodeIdAlias)
        m_AliasesDictionary.Add(_alias.Alias, _alias.Value);
    }
    private string LookupAlias(string id)
    {
      string _newId = string.Empty;
      return m_AliasesDictionary.TryGetValue(id, out _newId) ? _newId : id;
    }
    private ushort ImportNamespaceIndex(ushort namespaceIndex)
    {
      // nothing special required for indexes < 0.
      if (namespaceIndex == 0)
        return namespaceIndex;
      // return a new value if parameter is out of range.
      string _identifier = $"NameUnknown{m_NamespaceCount++}";
      if (m_UANodeSetModel.NamespaceUris.Length > namespaceIndex - 1)
        _identifier = m_UANodeSetModel.NamespaceUris[namespaceIndex - 1];
      else
        BuildErrorsHandling.Log.TraceEvent(
          TraceMessage.BuildErrorTraceMessage(BuildError.UndefinedNamespaceIndex, $"ImportNamespaceIndex failed - namespace index {namespaceIndex-1} is out of the NamespaceUris index. New namespace {_identifier} is created insted."));
      return AddressSpaceContext.GetIndexOrAppend(_identifier);
    }
    //TODO it is not used
    //private NodeId ImportExpandedNodeId(string nodeId, bool lookupAlias)
    //{
    //  if (string.IsNullOrEmpty(nodeId))
    //    return NodeId.Null;
    //  // lookup alias.
    //  if (lookupAlias)
    //    nodeId = LookupAlias(nodeId);
    //  ExpandedNodeId _expandedNodeId = ExpandedNodeId.Parse(nodeId);
    //  if (_expandedNodeId.IsAbsolute)
    //    throw new NotImplementedException();
    //  if (_expandedNodeId.ServerIndex > 0)
    //    throw new NotImplementedException();
    //  ushort namespaceIndex = _expandedNodeId.NamespaceIndex;
    //  if (_expandedNodeId.NamespaceIndex > 0)
    //    namespaceIndex = ImportNamespaceIndex(_expandedNodeId.NamespaceIndex);
    //  return new NodeId(_expandedNodeId.Identifier, namespaceIndex);
    //}
    #endregion

  }

}
