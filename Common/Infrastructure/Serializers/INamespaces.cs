//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Collections.Generic;
using System.Xml;

namespace UAOOI.Common.Infrastructure.Serializers
{
  /// <summary>
  /// Interface INamespaces - define functionality necessary to manage namespaces for the XML serialization
  /// </summary>
  public interface INamespaces
  {
    /// <summary>
    /// Gets the namespaces that is to be used to parametrize XML document.
    /// </summary>
    /// <returns>An instance of IEnumerable[XmlQualifiedName] containing the XML namespaces and prefixes that a serializer uses to generate qualified names in an XML-document instance.</returns>
    IEnumerable<XmlQualifiedName> GetNamespaces();
  }
}