
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Interface IMessageHandlerFactory - creates objects supporting messages handling over the wire.
  /// </summary>
  public interface IMessageHandlerFactory
  {
    /// <summary>
    /// Gets the message reader.
    /// </summary>
    /// <param name="name">The name of the reader.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="IMessageReader"/>.</param>
    /// <returns>IMessageReader.</returns>
    IMessageReader GetIMessageReader(string name, XmlElement configuration);
    /// <summary>
    /// Gets the message writer.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="IMessageWriter"/>.</param>
    /// <returns>IMessageWriter.</returns>
    IMessageWriter GetIMessageWriter(string name, XmlElement configuration);
  }
}
