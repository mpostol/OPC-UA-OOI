
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  internal interface IConsumerViewModel
  {

    int ConsumerBytesReceived { get; set; }
    int ConsumerFramesReceived { get; set; }
    ICommand ConsumerUpdateConfiguration { get; set; }
    int UDPPort { get; set; }
    string ConsumerErrorMessage { get; set; }
    void Trace(string message);
    /// <summary>
    /// Gets the consumer binding.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>IConsumerBinding.</returns>
    IConsumerBinding GetConsumerBinding(string variableName, BuiltInType encoding);

  }
}