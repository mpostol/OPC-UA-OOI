
using CAS.UA.IServerConfiguration;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
      ComposeParts();
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

    #region MEF injection points
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
    /// <summary>
    /// Gets or sets the trace source - an access point to the external component.
    /// </summary>
    /// <value>The trace source.</value>
    [Import(typeof(ITraceSource))]
    public ITraceSource TraceSource
    {
      get { return b_TraceSource; }
      set { b_TraceSource = value; }
    }
    #endregion

    #region private
    private ITraceSource b_TraceSource;
    private IConfigurationEditor b_ConfigurationEditor;
    private CompositionContainer m_Container;
    private void ComposeParts()
    {
      //An aggregate catalog that combines multiple catalogs
      AggregateCatalog catalog = CreateAggregateCatalog();
      //Create the CompositionContainer with the parts in the catalog
      m_Container = new CompositionContainer(catalog);
      //Fill the imports of this object
      this.m_Container.ComposeParts(this);
    }
    /// <summary>
    /// Creates the aggregate catalog.
    /// </summary>
    /// <returns>AggregateCatalog.</returns>
    protected virtual AggregateCatalog CreateAggregateCatalog()
    {
      AggregateCatalog catalog = new AggregateCatalog();
      //Adds all the parts found in the same assembly as the UANetworkingConfigurationEditorUnitTest class
      catalog.Catalogs.Add(new AssemblyCatalog(typeof(UANetworkingConfigurationEditor).Assembly));
      //catalog.Catalogs.Add(new DirectoryCatalog(@".\Extensions"));
      return catalog;
    }
    #endregion

  }
}
