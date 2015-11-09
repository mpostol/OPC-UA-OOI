
using CAS.UA.IServerConfiguration;
using System;
using System.ComponentModel.Composition;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{

  /// <summary>
  /// Class UANetworkingConfigurationEditor - 
  /// </summary>
  [Export(typeof(IConfiguration))]
  public class UANetworkingConfigurationEditor : UANetworkingConfiguration<ConfigurationData>
  {

    public UANetworkingConfigurationEditor()
    {
      b_ConfigurationEditor = new ConfigurationEditorBase();
    }
    #region UANetworkingConfiguration<ConfigurationData>
    /// <summary>
    /// Creates automatically the instance configurations on the best effort basis.
    /// </summary>
    /// <param name="descriptors">The descriptors of nodes.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">if set to <c>true</c> cancel was pressed.</param>
    /// <exception cref="System.ArgumentNullException">Configuration Editor is unavailable.</exception>
    public override void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
    {
      CancelWasPressed = false;
      if (ConfigurationEditor == null)
        throw new ArgumentNullException(nameof(ConfigurationEditor), "Configuration Editor is unavailable.");
      bool _CancelWasPressed = false;
      ConfigurationEditor.CreateInstanceConfigurations(descriptors, SkipOpeningConfigurationFile, x => _CancelWasPressed = x);
      CancelWasPressed = _CancelWasPressed;
    }
    /// <summary>
    /// Gets the configuration editor - user interface to edit the plug-in configuration file.
    /// </summary>
    /// <returns>Represents a window or dialog box that makes up an application's user interface to be used to edit configuration file.</returns>
    /// <exception cref="System.ArgumentNullException">Configuration Editor is unavailable.</exception>
    public override void EditConfiguration()
    {
      if (ConfigurationEditor == null)
        throw new ArgumentNullException(nameof(ConfigurationEditor), "Configuration Editor is unavailable.");
      ConfigurationEditor.EditConfiguration(CurrentConfiguration);
    }
    #endregion    

    /// <summary>
    /// Gets or sets the configuration editor - an access point to the external component.
    /// </summary>
    /// <value>The configuration editor.</value>
    [Import(typeof(IConfigurationEditor))]
    public IConfigurationEditor ConfigurationEditor
    {
      get { return b_ConfigurationEditor; }
      set { b_ConfigurationEditor = value; }
    }

    private IConfigurationEditor b_ConfigurationEditor;

  }
}
