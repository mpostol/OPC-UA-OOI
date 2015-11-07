using System;
using CAS.UA.IServerConfiguration;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  public class UANetworkingConfigurationEditor : UANetworkingConfiguration<ConfigurationData>
  {
    public override void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
    {
      throw new NotImplementedException();
    }

    public override void EditConfiguration()
    {
      throw new NotImplementedException();
    }
  }
}
