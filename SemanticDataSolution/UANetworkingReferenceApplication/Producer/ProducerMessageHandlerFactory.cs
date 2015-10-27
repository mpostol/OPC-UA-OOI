using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
{
  internal class ProducerMessageHandlerFactory : IMessageHandlerFactory
  {
    public IMessageReader GetIMessageReader(string name, XmlElement configuration)
    {
      throw new NotImplementedException();
    }

    public IMessageWriter GetIMessageWriter(string name, XmlElement configuration)
    {
      throw new NotImplementedException();
    }
  }
}
