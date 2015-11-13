
using CAS.UA.IServerConfiguration;
using System;
using System.Diagnostics;
using System.IO;

namespace UAOOI.DataBindings
{
  /// <summary>
  /// Class ConfigurationBase - Provides basic implementation of the <see cref="IConfiguration"/>.
  /// </summary>
  public abstract class ConfigurationBase<ConfigurationDataType> : IConfiguration
  {

    #region API
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationBase{ConfigurationDataType}"/> class. 
    /// Default configuration is loader.
    /// </summary>
    public ConfigurationBase(Func<ConfigurationDataType> configurationLoader)
    {
      Tracer += (x, y, z) => { };
      DefaultConfigurationLoader = configurationLoader;
      CreateDefaultConfiguration();
    }
    /// <summary>
    /// Gets or sets the current configuration <typeparamref name="ConfigurationDataType"/>.
    /// </summary>
    /// <value>The current configuration as instance of <typeparamref name="ConfigurationDataType"/>.</value>
    public ConfigurationDataType CurrentConfiguration
    {
      get { return m_CurrentConfiguration; }
      set
      {
        if (Object.Equals(CurrentConfiguration, value))
          return;
        m_CurrentConfiguration = value;
        RaiseOnChangeEvent(true);
      }
    }
    /// <summary>
    /// Gets or sets the instnace of delegate capturing tracer functionality.
    /// </summary>
    /// <value>The delegaye of <see cref="Action{TraceEventType, Int32, String}"/>tracer.</value>
    public Action<TraceEventType, int, string> Tracer { get; set; }
    /// <summary>
    /// Gets or sets the default configuration loader.
    /// </summary>
    /// <value>The default configuration loader <see cref="Func{ConfigurationDataType}"/>.</value>
    public Func<ConfigurationDataType> DefaultConfigurationLoader { private get; set; }
    #endregion

    #region IConfiguration
    /// <summary>
    /// Occurs any time the configuration is modified.
    /// </summary>
    public event EventHandler<UAServerConfigurationEventArgs> OnModified;
    /// <summary>
    /// Creates the default configuration.
    /// </summary>
    public void CreateDefaultConfiguration()
    {
      CurrentConfiguration = DefaultConfigurationLoader();
    }
    /// <summary>
    /// Creates automatically the instance configurations on the best effort basis.
    /// </summary>
    /// <param name="descriptors">The descriptors of nodes.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">if set to <c>true</c> cancel was pressed.</param>
    public abstract void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed);
    /// <summary>
    /// Gets the default name of the file created from the name provided by the derived class and extension set in the assemply configuration file.
    /// </summary>
    /// <value>The default name of the file.</value>
    public virtual string DefaultFileName
    {
      get
      {
        return String.Format("{0}.{1}", DefaultConfigurationFileName, Properties.Settings.Default.DefaultConfigurationFileNametExtension);
      }
    }
    /// <summary>
    /// Gets the configuration editor - user interface to edit the plug-in configuration file.
    /// </summary>
    /// <returns>Represents a window or dialog box that makes up an application's user interface to be used to edit configuration file.</returns>
    public abstract void EditConfiguration();
    /// <summary>
    /// Gets the instance to be used by a user to configure the selected node.
    /// </summary>
    /// <param name="descriptor">Provides identifying description of the node to be configured.</param>
    /// <returns>Returned object provides access to the instance node configuration edition functionality.</returns>
    public abstract IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor);
    /// <summary>
    /// Reads the configuration.
    /// </summary>
    /// <param name="configurationFile">The configuration <see cref="FileInfo"/> instance.</param>
    public abstract void ReadConfiguration(FileInfo configurationFile);
    /// <summary>
    /// Saves the configuration file to a specified location.
    /// </summary>
    /// 
    /// <param name="solutionFilePath">The solution file path.</param>
    /// <param name="configurationFile">The configuration file.</param>
    /// <remarks>
    /// <paramref name="solutionFilePath" />It is to be used to create relative file path to configuration files used by the plug-in.</remarks>
    public abstract void SaveConfiguration(string solutionFilePath, FileInfo configurationFile);
    #endregion

    #region private
    private ConfigurationDataType m_CurrentConfiguration;
    /// <summary>
    /// Raises the on change event.
    /// </summary>
    /// <param name="configurationFileChanged">if set to <c>true</c> the configuration file changed, false if content changed.</param>
    protected void RaiseOnChangeEvent(bool configurationFileChanged)
    {
      OnModified?.Invoke(this, new UAServerConfigurationEventArgs(configurationFileChanged));
    }
    /// <summary>
    /// Gets the default name of the configuration file.
    /// </summary>
    /// <value>The default name of the configuration file.</value>
    protected abstract string DefaultConfigurationFileName { get; }
    #endregion

  }
}
