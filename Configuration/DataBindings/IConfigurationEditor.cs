
using CAS.UA.IServerConfiguration;
using System;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.DataBindings
{
  /// <summary>
  /// Interface IConfigurationEditor - describes an injection point to be used to compose en external editor. 
  /// </summary>
  public interface IConfigurationEditor
  {
    /// <summary>
    /// Creates the instance configurations.
    /// </summary>
    /// <param name="descriptors">The descriptors.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> if the opening configuration file should be skipped.</param>
    /// <param name="CancelWasPressed">The cancel was pressed.</param>
    void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, Action<bool> CancelWasPressed);
    /// <summary>
    /// Edits the configuration.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    void EditConfiguration(ConfigurationData configuration);

  }
}
