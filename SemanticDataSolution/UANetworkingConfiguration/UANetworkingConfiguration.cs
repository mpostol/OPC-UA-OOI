
using CAS.UA.IServerConfiguration;
using System;
using System.ComponentModel.Composition;
using System.IO;
using UAOOI.DataBindings;
using UAOOI.DataBindings.Serializers;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  /// <summary>
  /// Class UANetworkingConfiguration - Provides implementation of the <see cref="ConfigurationBase{ConfigurationDataType}"/> for the UANetworking application.
  /// </summary>
  /// <typeparam name="ConfigurationDataType">The type of the configuration data type.</typeparam>
  public abstract class UANetworkingConfiguration<ConfigurationDataType> : ConfigurationBase<ConfigurationDataType>
      where ConfigurationDataType : ConfigurationData, new()
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="UANetworkingConfiguration{ConfigurationDataType}"/> class.
    /// </summary>
    public UANetworkingConfiguration() :
      base(NewConfigurationData)
    { }
    #endregion

    #region ConfigurationBase
    /// <summary>
    /// Reads the configuration from the <see cref="FileInfo"/>.
    /// </summary>
    /// <param name="configurationFile">The file <see cref="FileInfo"/> containing the configuration data of the UANetworking application.</param>
    public override void ReadConfiguration(FileInfo configurationFile)
    {
      CurrentConfiguration = ConfigurationData.Load<ConfigurationDataType>
        (Properties.Settings.Default.Serializer.ToUpper() == "XML" ? SerializerType.Xml : SerializerType.Json, configurationFile, (x, y, z) => TraceSource.TraceData(x, y, z), () => RaiseOnChangeEvent(true));
    }
    /// <summary>
    /// Saves the configuration.
    /// </summary>
    /// <param name="solutionFilePath">The solution file path.</param>
    /// <param name="configurationFile">The configuration file.</param>
    public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
    {
      ConfigurationData.Save<ConfigurationDataType>(CurrentConfiguration, Properties.Settings.Default.Serializer.ToUpper() == "XML" ? SerializerType.Xml : SerializerType.Json, configurationFile, (x, y, z) => TraceSource.TraceData(x, y, z));
    }
    /// <summary>
    /// Gets the instance configuration.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    /// <returns>An instance of <see cref="CAS.UA.IServerConfiguration.IInstanceConfiguration"/>.</returns>
    public override IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      if (descriptor == null)
        throw new ArgumentNullException(nameof(descriptor));
      if (CurrentConfiguration == null)
        return null;
      return InstanceConfigurationFactory.GetIInstanceConfiguration(CurrentConfiguration.GetInstanceConfiguration(descriptor), CurrentConfiguration.GetMessageHandlers(), TraceSource.TraceData, () => this.RaiseOnChangeEvent(false));
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
    /// <summary>
    /// Gets or sets the instance configuration factory.
    /// </summary>
    /// <value>The instance configuration factory.</value>
    [Import(typeof(IInstanceConfigurationFactory))]
    public IInstanceConfigurationFactory InstanceConfigurationFactory { get; set; }
    #endregion

    #region privat
    private ITraceSource b_TraceSource;
    private IConfigurationEditor b_ConfigurationEditor;
    private static ConfigurationDataType NewConfigurationData()
    {
      return new ConfigurationDataType() { DataSets = new DataSetConfiguration[] { }, MessageHandlers = new MessageHandlerConfiguration[] { } };
    }
    /// <summary>
    /// Gets the default name of the configuration file from the application settings.
    /// </summary>
    /// <value>The default name of the configuration file.</value>
    protected override string DefaultConfigurationFileName
    {
      get
      {
        return Properties.Settings.Default.Default_ConfigurationFileName;
      }
    }
    #endregion

  }

}

