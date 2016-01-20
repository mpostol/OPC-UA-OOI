
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  /// <summary>
  /// Interface IConfigurationDataFactory - creates an instance of <see cref="ConfigurationData"/>
  /// </summary>
  public interface IConfigurationDataFactory
  {
    /// <summary>
    /// Gets and instance of <see cref="ConfigurationData"/>.
    /// </summary>
    /// <returns>Returns an instance of <see cref="ConfigurationData"/>.</returns>
    ConfigurationData GetConfigurationData();

  }
}
