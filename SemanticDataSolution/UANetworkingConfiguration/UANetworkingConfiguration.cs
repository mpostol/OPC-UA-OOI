
using System;
using System.IO;
using CAS.UA.IServerConfiguration;
using UAOOI.DataBindings;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using System.Linq;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  public abstract class UANetworkingConfiguration<ConfigurationDataType> : ConfigurationBase<ConfigurationDataType>
    where ConfigurationDataType : ConfigurationData, new()
  {

    public UANetworkingConfiguration() :
      base(NewConfigurationData)
    { }
    #region ConfigurationBase
    public override void ReadConfiguration(FileInfo configurationFile)
    {
      CurrentConfiguration = ConfigurationData.Load<ConfigurationDataType>(() => DataBindings.Serializers.DataContractSerializers.Load<ConfigurationDataType>(configurationFile, (x, y, z) => Tracer?.Invoke(x, y, z)));
      m_ConfigurationFile = configurationFile;
    }
    public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
    {
      DataBindings.Serializers.DataContractSerializers.Save<ConfigurationData>(configurationFile, CurrentConfiguration, (x, y, z) => Tracer?.Invoke(x, y, z));
      if (m_ConfigurationFile != null && configurationFile.FullName.CompareTo(m_ConfigurationFile.FullName) == 0)
        return;
      m_ConfigurationFile = configurationFile;
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
    private FileInfo m_ConfigurationFile;
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
