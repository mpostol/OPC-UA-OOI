
using CAS.UA.IServerConfiguration;
using System;
using System.IO;
using System.Linq;
using UAOOI.DataBindings;
using UAOOI.DataBindings.Serializers;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  public abstract class UANetworkingConfiguration<ConfigurationDataType> : ConfigurationBase<ConfigurationDataType>
    where ConfigurationDataType : ConfigurationData, new()
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="UANetworkingConfiguration{ConfigurationDataType}"/> class.
    /// </summary>
    public UANetworkingConfiguration() :
      base(NewConfigurationData)
    { }
    #region ConfigurationBase
    /// <summary>
    /// Reads the configuration.
    /// </summary>
    /// <param name="configurationFile">The configuration <see cref="FileInfo"/> instance.</param>
    public override void ReadConfiguration(FileInfo configurationFile)
    {
      CurrentConfiguration = ConfigurationData.Load<ConfigurationDataType>(() => DataContractSerializers.Load<ConfigurationDataType>(configurationFile, (x, y, z) => Tracer?.Invoke(x, y, z)));
    }
    public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
    {
      ConfigurationData.Save<ConfigurationDataType>
        (CurrentConfiguration, configuration => DataContractSerializers.Save<ConfigurationData>(configurationFile, configuration, (x, y, z) => Tracer?.Invoke(x, y, z)));
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
