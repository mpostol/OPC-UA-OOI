
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  internal interface IConsumerViewModel
  {

    /// <summary>
    /// Gets or sets the consumer received bytes.
    /// </summary>
    /// <value>The consumer received bytes.</value>
    int ConsumerReceivedBytes { get; set; }
    /// <summary>
    /// Gets or sets the number of consumer received frames .
    /// </summary>
    /// <value>The consumer frames received.</value>
    int ConsumerFramesReceived { get; set; }
    /// <summary>
    /// Gets or sets the consumer update configuration command.
    /// </summary>
    /// <value>The consumer update configuration <see cref="ICommand"/>.</value>
    ICommand ConsumerUpdateConfiguration { get; set; }
    /// <summary>
    /// Gets or sets the last consumer error message.
    /// </summary>
    /// <value>The consumer error message.</value>
    string ConsumerErrorMessage { get; set; }
    /// <summary>
    /// Traces the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    void Trace(string message);
    /// <summary>
    /// Gets the consumer binding.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>An object implementing <see cref="IConsumerBinding"/>.</returns>
    IConsumerBinding GetConsumerBinding(string variableName, UATypeInfo encoding);
    /// <summary>
    /// Saves the consumer user settings.
    /// </summary>
    void SaveConsumerUserSettings();

  }

}