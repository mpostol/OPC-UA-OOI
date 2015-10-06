
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{
  internal class MessageHandlersCollection : Dictionary<string, IMessageHandler>
  {
    internal static MessageHandlersCollection CreateMessageHandlers
      (Configuration.MessageTransportConfiguration[] configuration, IMessageHandlerFactory messageHandlerFactory, Action<string, IMessageHandler> addMessageHandler)
    {
      MessageHandlersCollection _collection = new MessageHandlersCollection();
      foreach (Configuration.MessageTransportConfiguration item in configuration)
      {
        if (_collection.ContainsKey(item.Name))
          throw new ArgumentOutOfRangeException("Name", "Duplicated transport name");
        IMessageHandler _handler = null;
        switch (item.TransportRole)
        {
          case AssociationRole.Consumer:
            _handler = messageHandlerFactory.GetIMessageReader(item.Name, item.Configuration);
            break;
          case AssociationRole.Producer:
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
    internal void OnConfigurationChangeHandler(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }
    private MessageHandlersCollection()
      : base()
    { }


    internal void Run()
    {
      throw new NotImplementedException();
    }
  }
}
