using System;
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public class UAModelContext<ModelDesignType> : IUAModelContext
  {
    
    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="UAModelContext" /> class.
    /// </summary>
    /// <param name="nodeIdAlias">The node identifier aliases table.</param>
    /// <param name="modelNamespaceUris">The model namespace uris table.</param>
    /// <param name="addressSpaceContext">The address space context.</param>
    internal UAModelContext(NodeIdAlias[] nodeIdAlias, string[] modelNamespaceUris, AddressSpaceContext<ModelDesignType> addressSpaceContext)
    {
      if (nodeIdAlias == null)
        throw new ArgumentNullException("nodeIdAlias");
      if (modelNamespaceUris == null)
        throw new ArgumentNullException("modelNamespaceUris");
      if (addressSpaceContext == null)
        throw new ArgumentNullException("addressSpaceContext");
      AddAlias(nodeIdAlias);
      m_ModelNamespaceUris = modelNamespaceUris;
      m_AddressSpaceContext = addressSpaceContext;
    }
    #endregion

    public ParameterType ExportArgument<ParameterType>(DataSerialization.Argument argument, Action<TraceMessage> traceEvent, IParameterFactory<ParameterType> factory)
    {
      throw new NotImplementedException();
    }
    public System.Xml.XmlQualifiedName ExportNodeId(string nodeId, uint defaultValue, Action<TraceMessage> traceEvent)
    {
    #region public
      throw new NotImplementedException();
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
    public object GetAddressSpaceContext { get; set; }
    private string[] m_ModelNamespaceUris;
    private Dictionary<string, string> m_AliasesDictionary = new Dictionary<string, string>();
    private AddressSpaceContext<ModelDesignType> m_AddressSpaceContext;
  }
}
