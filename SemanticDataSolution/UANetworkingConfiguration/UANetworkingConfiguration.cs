
using System;
using System.Diagnostics;
using System.IO;
using CAS.UA.IServerConfiguration;
using UAOOI.DataBindings;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  public class UANetworkingConfiguration : ConfigurationBase
  {
    public ConfigurationData CurrentConfiguration { get; private set; }
    public Action<TraceEventType, int, string> Tracer { get; set; }

    #region ConfigurationBase
    public override void CreateDefaultConfiguration()
    {
      CurrentConfiguration = ConfigurationData.CreateDefault();
      RaiseOnChangeEvent(true);
    }
    public override void ReadConfiguration(FileInfo configurationFile)
    {
      CurrentConfiguration = ConfigurationData.Load<ConfigurationData>(() => DataBindings.Serializers.DataContractSerializers.Load<ConfigurationData>(configurationFile, (x, y, z) => Tracer?.Invoke(x, y, z)));
      m_ConfigurationFile = configurationFile;
      RaiseOnChangeEvent(true);
    }
    public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
    {
      DataBindings.Serializers.DataContractSerializers.Save<ConfigurationData>(configurationFile, CurrentConfiguration, (x, y, z) => Tracer?.Invoke(x, y, z));
      if (m_ConfigurationFile != null && configurationFile.FullName.CompareTo(m_ConfigurationFile.FullName) == 0)
        return;
      m_ConfigurationFile = configurationFile;
      RaiseOnChangeEvent(true);
    }
    public override void EditConfiguration()
    {
      throw new NotImplementedException();
    }
    public override IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      if (descriptor == null)
        throw new ArgumentNullException(nameof(descriptor)); 
      if (CurrentConfiguration == null)
        return null;
      return CurrentConfiguration.GetInstanceConfiguration(descriptor);
    }
    #endregion

    private FileInfo m_ConfigurationFile;

  }
}
