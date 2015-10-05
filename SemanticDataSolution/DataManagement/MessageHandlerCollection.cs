
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{
  internal class MessageHandlerCollection : Dictionary<string, IMessageHandler>
  {
    internal static MessageHandlerCollection CreateMessageHandlers(Configuration.MessageTransportConfiguration[] configuration, IMessageHandlerFactory messageHandlerFactory, Action<string, IMessageHandler> addMessageHandler)
    {
      MessageHandlerCollection _collection = new MessageHandlerCollection();
      foreach (Configuration.MessageTransportConfiguration item in configuration)
      {
        if (_collection.ContainsKey(item.Name))
          throw new ArgumentOutOfRangeException("Name", "Duplicated transport name");
        IMessageHandler _handler = null;
        switch (item.TransportRole)
        {
          case TransportRole.Consumer:
            _handler = messageHandlerFactory.GetIMessageReader(item.Name, item.Configuration);
            break;
          case TransportRole.Publisher:
            _handler = messageHandlerFactory.GetIMessageWriter(item.Name, item.Configuration);
            break;
          default:
            break;
        }
        _collection.Add(item.Name, _handler);
        foreach (string _association in item.Associations)
          addMessageHandler(_association, _handler);
      }
      return _collection;
    }
    private MessageHandlerCollection()
      : base()
    { }
  }
}
