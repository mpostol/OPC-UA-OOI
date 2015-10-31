
using System;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  /// <summary>
  /// Interface IConfigurationFactory provides functionality to provide the configuration. 
  /// </summary>
  public interface IConfigurationFactory
  {
    
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <returns>Am object of <see cref="ConfigurationData"/> type capturing the communication configuration.</returns>
    ConfigurationData GetConfiguration();
    /// <summary>
    /// Occurs after the association configuration has been changed.
    /// </summary>
    event EventHandler<EventArgs> OnAssociationConfigurationChange;
    /// <summary>
    /// Occurs after the communication configuration has been changed.
    /// </summary>
    event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

  }
}
