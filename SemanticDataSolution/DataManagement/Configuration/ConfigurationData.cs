
using System;
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  [Serializable]
  [DataContract]
  public partial class ConfigurationData
  {

    [DataMember]
    public AssociationConfiguration[] Associations { get; set; }
    [DataMember]
    public MessageTransportConfiguration[] MessageTransport { get; set; }

  }
}
