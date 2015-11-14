
using CAS.UA.IServerConfiguration;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  /// <summary>
  /// Interface IInstanceConfigurationFactory - object implementing this interface should provide the user interface 
  /// </summary>
  public interface IInstanceConfigurationFactory
  {
    /// <summary>
    /// Gets an object providing <see cref="IInstanceConfiguration"/> interface which is to be displayed in the main editor window.
    /// </summary>
    /// <param name="dataSets">The data set to be edited.</param>
    /// <param name="availableHandlers">The available handlers.</param>
    /// <returns>IInstanceConfiguration.</returns>
    IInstanceConfiguration GetIInstanceConfiguration(DataSetConfiguration dataSets, MessageHandlerConfiguration[] availableHandlers);
  }
}
