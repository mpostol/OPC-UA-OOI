
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UANodeSet
  {
    internal static UANodeSet ReadUADefinedTypes()
    {
      return ReadXmlFile(m_UADefinedTypesName);
    }
    internal static UANodeSet ReadXmlFile(string resourcePath)
    {
      return resourcePath.LoadResource<UANodeSet>();
    }
#if DEBUG
    /// <summary>
    /// Gets the name of the xml file path containing the standard OPC UA NodeSet
    /// </summary>
    /// <value>The name of the resource.</value>
    internal static string UTUADefinedTypesName { get { return m_UADefinedTypesName; } }
#endif
    //OPC UA standard NodeSet model resource folder.
    private const string m_UADefinedTypesName = @"UAOOI.SemanticData.UANodeSetValidation.XML.Opc.Ua.NodeSet2.xml";

  }
}
