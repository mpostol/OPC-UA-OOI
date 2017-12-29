
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.UDPMessageHandler.Configuration;
using UAOOI.Networking.UDPMessageHandler.Diagnostic;

namespace UAOOI.Networking.UDPMessageHandler
{

  /// <summary>
  /// Class <see cref="MessageHandlerFactory"/> - implements <see cref="IMessageHandlerFactory"/> 
  /// </summary>
  [Export(typeof(IMessageHandlerFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MessageHandlerFactory : IMessageHandlerFactory
  {

    #region IMessageHandlerFactory
    /// <summary>
    /// Gets the message reader.
    /// </summary>
    /// <param name="name">The name of the reader.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="T:UAOOI.Networking.SemanticData.MessageHandling.IMessageReader" />.</param>
    /// <param name="uaDecoder">The decoder that provides methods to be used to decode OPC UA Built-in types.</param>
    /// <returns>An object implementing <see cref="T:UAOOI.Networking.SemanticData.MessageHandling.IMessageReader" /> that provides functionality supporting reading the messages from the wire.</returns>
    IMessageReader IMessageHandlerFactory.GetIMessageReader(string name, string configuration, IUADecoder uaDecoder)
    {
      UDPMessageHandlerSemanticEventSource.Log.GetIMessageHandler($"{nameof(IMessageHandlerFactory.GetIMessageReader)}{{ name = {name}, configuration= {configuration} }}");
      UDPReaderConfiguration _configuration = UDPReaderConfiguration.Parse(configuration);
      BinaryUDPPackageReader _ret = new BinaryUDPPackageReader(uaDecoder, _configuration);
      return _ret;
    }
    /// <summary>
    /// Gets the new instance of <see cref="IMessageWriter" />.
    /// </summary>
    /// <param name="name">The name of thw writer.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="T:UAOOI.Networking.SemanticData.MessageHandling.IMessageWriter" />.</param>
    /// <param name="uaEncoder">The encoder that provides methods to be used to encode OPC UA Built-in types.</param>
    /// <returns>An instance of <see cref="IMessageWriter" /> that provides functionality supporting writing the messages on the wire..</returns>
    IMessageWriter IMessageHandlerFactory.GetIMessageWriter(string name, string configuration, IUAEncoder uaEncoder)
    {
      UDPMessageHandlerSemanticEventSource.Log.GetIMessageHandler($"{nameof(IMessageHandlerFactory.GetIMessageWriter)}{{ name = {name}, configuration= {configuration} }}");
      UDPWriterConfiguration _configuration = UDPWriterConfiguration.Parse(configuration);
      BinaryUDPPackageWriter _ret = new BinaryUDPPackageWriter(_configuration.RemoteHostName, _configuration.UDPPortNumber, uaEncoder);
      return _ret;
    }
    #endregion

  }

}