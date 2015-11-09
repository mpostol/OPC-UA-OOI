
using CAS.UA.IServerConfiguration;
using System;
using System.Windows;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  public class ConfigurationEditorBase : IConfigurationEditor
  {
    public virtual void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, Action<bool> CancelWasPressed)
    {
      MessageBox.Show("CreateInstanceConfigurations is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
    }
    public void EditConfiguration(ConfigurationData configuration)
    {
      MessageBox.Show("CreateInstanceConfigurations is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
    }
  }
}
