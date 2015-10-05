
using System.Xml;
namespace UAOOI.SemanticData.DataManagement
{
  public interface IMessageHandlerFactory
  {
    IMessageReader GetIMessageReader(string name, XmlElement configuration);
    IMessageWriter GetIMessageWriter(string name, XmlElement configuration);
  }
}
