
using System;
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  
  [Serializable]
  public class MessageTransportConfiguration
  {
    
    public string Name { get; set; }
    public string[] AssociationNames { get; set; }
    public XmlElement Configuration { get; set; }
    public AssociationRole TransportRole { get; set; }

  }

}
