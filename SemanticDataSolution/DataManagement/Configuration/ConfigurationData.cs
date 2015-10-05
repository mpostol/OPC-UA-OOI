using System;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  [Serializable]
  public partial class ConfigurationData
  {

    public AssociationConfiguration[] Associations { get; set; }
    public MessageTransportConfiguration[] MessageTransport { get; set; }

  }
}
