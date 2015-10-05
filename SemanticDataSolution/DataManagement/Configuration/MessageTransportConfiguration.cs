
using System;
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  
  [Serializable]
  public class MessageTransportConfiguration
  {
    
    public string Name { get; set; }
    public string[] Associations { get; set; }
    public XmlElement Configuration { get; set; }
    public TransportRole TransportRole { get; set; }

  }

  public enum TransportRole
  {
    /// <summary>
    /// The message transport role is consumer 
    /// </summary>
    Consumer,
    /// <summary>
    /// The message transport role is publisher
    /// </summary>
    Publisher
  }
}
