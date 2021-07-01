//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Collections.Generic;
using System.Xml;
using UAOOI.Common.Infrastructure.Serializers;

namespace UAOOI.SemanticData.UAModelDesignExport.XML
{
  public partial class ModelDesign : INamespaces
  {
    /// <summary>
    /// Gets the namespaces that is to be used to parametrize XML document.
    /// </summary>
    /// <returns>An instance of IEnumerable[XmlQualifiedName] containing the XML namespaces and prefixes that a serializer uses to generate qualified names in an XML-document instance.</returns>
    public IEnumerable<XmlQualifiedName> GetNamespaces()
    {
      List<XmlQualifiedName> ret = new List<XmlQualifiedName>
      {
        new XmlQualifiedName("xsd", "http://www.w3.org/2001/XMLSchema"),
        new XmlQualifiedName("xsi", "http://www.w3.org/2001/XMLSchema-instance"),
        new XmlQualifiedName("uax", "http://opcfoundation.org/UA/2008/02/Types.xsd")
      };
      foreach (Namespace item in Namespaces)
        ret.Add(new XmlQualifiedName(item.XmlPrefix, item.Value));
      return ret;
    }
  }
}