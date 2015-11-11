
using CAS.UA.IServerConfiguration;
using System;
using System.IO;
using System.Linq;
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
      CurrentConfiguration = ConfigurationData.Load<ConfigurationDataType>(Properties.Settings.Default.Serializer.ToUpper() == "XML" ? SerializerType.Xml : SerializerType.Json, configurationFile, (x, y, z) => Tracer?.Invoke(x, y, z));
    }
    public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
    {
      ConfigurationData.Save<ConfigurationDataType>(CurrentConfiguration, Properties.Settings.Default.Serializer.ToUpper() == "XML" ? SerializerType.Xml : SerializerType.Json, configurationFile, (x, y, z) => Tracer?.Invoke(x, y, z));
    }
    public override IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      if (descriptor == null)
        throw new ArgumentNullException(nameof(descriptor));
      if (CurrentConfiguration == null)
        return null;
      return CurrentConfiguration.GetInstanceConfiguration(descriptor).FirstOrDefault<IInstanceConfiguration>();
    }
    #endregion

    #region privat
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
