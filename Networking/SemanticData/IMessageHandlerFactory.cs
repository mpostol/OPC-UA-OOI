
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.SemanticData
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
    /// <param name="configuration">The configuration of the object implementing the <see cref="IMessageReader" />.</param>
    /// <param name="uaDecoder">The decoder that provides methods to be used to decode OPC UA Built-in types.</param>
    /// <returns>An object implementing <see cref="IMessageReader"/> that provides functionality supporting reading the messages from the wire.</returns>
    IMessageReader GetIMessageReader(string name, string configuration, IUADecoder uaDecoder);
    /// <summary>
    /// Gets the message writer.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="IMessageWriter"/>.</param>
    /// <param name="uaEncoder">The encoder that provides methods to be used to encode OPC UA Built-in types.</param>
    /// <returns>An object implementing <see cref="IMessageWriter"/> that provides functionality supporting sending the messages over the wire.</returns>
    IMessageWriter GetIMessageWriter(string name, string configuration, IUAEncoder uaEncoder);

  }
}
