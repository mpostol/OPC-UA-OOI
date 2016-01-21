
using System;
using System.Runtime.Serialization;
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
    /// <summary>
    /// Gets or sets the the delegate capturing functionality tha is executed when the configuration is changing.
    /// </summary>
    /// <value>The m_ on changed.</value>
    Action OnChanged { get; set; }
    /// <summary>
    /// Called when the configuration is loaded.
    /// </summary>
    void OnLoaded();
    /// <summary>
    /// Called before the saving the configuration.
    /// </summary>
    void OnSaving();

  }
}
