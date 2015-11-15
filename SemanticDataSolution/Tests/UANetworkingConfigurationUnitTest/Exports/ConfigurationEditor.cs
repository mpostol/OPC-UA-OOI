
using CAS.UA.IServerConfiguration;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest.Exports
{

  /// <summary>
  /// Class ConfigurationEditorBase - a simple implementation of the <see cref="IConfigurationEditor"/>.
  /// </summary>
  [Export(typeof(IConfigurationEditor))]
  public class ConfigurationEditorBase : IConfigurationEditor
  {
    /// <summary>
    /// Creates the instance configurations.
    /// </summary>
    /// <param name="descriptors">The descriptors.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">The cancel was pressed.</param>
    public virtual void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, Action<bool> CancelWasPressed)
    {
      MessageBox.Show("CreateInstanceConfigurations is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
    }
    /// <summary>
    /// Open configuration editor.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public virtual void EditConfiguration(ConfigurationData configuration)
    {
      MessageBox.Show("EditConfiguration is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
    }

  }

}
