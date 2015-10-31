
using System;
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{
  [Serializable]
  [DataContract]
  public partial class ConfigurationData
  {

    [DataMember]
    public DataSetConfiguration[] Associations { get; set; }
    [DataMember]
    public MessageTransportConfiguration[] MessageHandlers { get; set; }

  }
}
