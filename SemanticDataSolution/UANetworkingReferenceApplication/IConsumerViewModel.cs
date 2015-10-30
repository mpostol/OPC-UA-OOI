
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  internal interface IConsumerViewModel
  {

    /// <summary>
    /// Gets the consumer binding.
    /// </summary>
    /// <param name="propertyName">Name of the property to be binded.</param>
    /// <returns>IConsumerBinding.</returns>
    IConsumerBinding GetConsumerBinding(string propertyName);
    int ConsumerBytesReceived { get; set; }
    int ConsumerFramesReceived { get; set; }
    ICommand ConsumerUpdateConfiguration { get; set; }
    int UDPPort { get; set; }
    string ConsumerErrorMessage { get; set; }
    void Trace(string message);

  }
}