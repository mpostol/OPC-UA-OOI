
using CAS.UA.IServerConfiguration;
using System.IO;
using UAOOI.SemanticData.UANetworking.Configuration;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.DataBindings
{
  /// <summary>
  /// Class ConfigurationBase - Provides basic implementation of the <see cref="IConfiguration"/>.
  /// </summary>
  public abstract class ConfigurationBase<ConfigurationDataType> : UANetworkingConfiguration<ConfigurationDataType>, IConfiguration
    where ConfigurationDataType : ConfigurationData, new()
  {
    public abstract string DefaultFileName { get; }
    public abstract void CreateDefaultConfiguration();
    public abstract void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed);
    public abstract void EditConfiguration();
    public abstract IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor);
    public abstract void SaveConfiguration(string solutionFilePath, FileInfo configurationFile);

  }
}
