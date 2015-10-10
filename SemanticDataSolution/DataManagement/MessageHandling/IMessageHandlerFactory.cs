
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  public interface IMessageHandlerFactory
  {
    /// <summary>
    /// Gets the message reader.
    /// </summary>
    /// <param name="name">The name of the reader.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IMessageReader.</returns>
    IMessageReader GetIMessageReader(string name, XmlElement configuration);
    /// <summary>
    /// Gets the message writer.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IMessageWriter.</returns>
    IMessageWriter GetIMessageWriter(string name, XmlElement configuration);
  }
}
