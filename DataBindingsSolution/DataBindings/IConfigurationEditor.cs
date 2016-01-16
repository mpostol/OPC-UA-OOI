
using CAS.UA.IServerConfiguration;
using System;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.DataBindings
{
  public interface IConfigurationEditor
  {
    void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, Action<bool> CancelWasPressed);

    void EditConfiguration(ConfigurationData configuration);

  }
}
