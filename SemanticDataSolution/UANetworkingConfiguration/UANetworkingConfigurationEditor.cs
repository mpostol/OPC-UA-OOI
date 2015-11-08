
using CAS.UA.IServerConfiguration;
using System;
using System.ComponentModel.Composition;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  [Export(typeof(IConfiguration))]
  public class UANetworkingConfigurationEditor : UANetworkingConfiguration<ConfigurationData>
  {

    public override void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
    {
      CancelWasPressed = false;
      if (ConfigurationEditor == null)
        throw new ArgumentNullException(nameof(ConfigurationEditor), "Configuration Editor is unavailable.");
      bool _CancelWasPressed = false;
      ConfigurationEditor.CreateInstanceConfigurations(descriptors, SkipOpeningConfigurationFile, x => _CancelWasPressed = x);
      CancelWasPressed = _CancelWasPressed;
    }
    public override void EditConfiguration()
    {
      if (ConfigurationEditor == null)
        throw new ArgumentNullException(nameof(ConfigurationEditor), "Configuration Editor is unavailable.");
      ConfigurationEditor.EditConfiguration(CurrentConfiguration);
    }
    [Import(typeof(IConfigurationEditor))]
    public IConfigurationEditor ConfigurationEditor
    {
      get { return b_ConfigurationEditor; }
      set { b_ConfigurationEditor = value; }
    }
    private IConfigurationEditor b_ConfigurationEditor;

  }
}
