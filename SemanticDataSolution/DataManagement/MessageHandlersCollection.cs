
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Class MessageHandlersCollection - represents collection of communication channels involved in handling selected message centric transport providers.
  /// </summary>
  internal class MessageHandlersCollection : Dictionary<string, IMessageHandler>
  {
    /// <summary>
    /// Creates the message handlers.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="messageHandlerFactory">The message handler factory.</param>
    /// <param name="encodingFactory">The encoding factory that provides functionality to lookup a dictionary containing value converters..</param>
    /// <param name="addMessageHandler">The add message handler.</param>
    /// <returns>MessageHandlersCollection.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Name;Duplicated transport name</exception>
    internal static MessageHandlersCollection CreateMessageHandlers
      (MessageHandlerConfiguration[] configuration, IMessageHandlerFactory messageHandlerFactory, IEncodingFactory encodingFactory, Action<IMessageHandler, AssociationConfiguration> addMessageHandler)
    {
      MessageHandlersCollection _collection = new MessageHandlersCollection();
      foreach (MessageHandlerConfiguration _configuration in configuration)
      {
        if (_collection.ContainsKey(_configuration.Name))
          throw new ArgumentOutOfRangeException("Name", "Duplicated transport name");
        IMessageHandler _handler = null;
        switch (_configuration.TransportRole)
        {
          case AssociationRole.Consumer:
            MessageReaderConfiguration _readerConfiguration = (MessageReaderConfiguration)_configuration;
            _handler = messageHandlerFactory.GetIMessageReader(_configuration.Name, _configuration.Configuration, encodingFactory.UADecoder);
            foreach (ConsumerAssociationConfiguration _consumerAssociation in _readerConfiguration.ConsumerAssociationConfigurations)
              addMessageHandler(_handler, _consumerAssociation);
            break;
          case AssociationRole.Producer:
            MessageWriterConfiguration _writerConfiguration = (MessageWriterConfiguration)_configuration;
            _handler = messageHandlerFactory.GetIMessageWriter(_configuration.Name, _configuration.Configuration, encodingFactory.UAEncoder);
            foreach (ProducerAssociationConfiguration _producerAssociation in _writerConfiguration.ProducerAssociationConfigurations)
              addMessageHandler(_handler, _producerAssociation);
            break;
          default:
            break;
        }
        _collection.Add(_configuration.Name, _handler);
      }
      return _collection;
    }
    /// <summary>
    /// Handles the configuration modifications.
    /// </summary>
    /// <note>
    /// It is intentionally not implemented
    /// </note>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    /// <exception cref="System.NotImplementedException">It is intentionally not implemented</exception>
    internal void OnConfigurationChangeHandler(object sender, EventArgs e)
    {
      throw new NotImplementedException("It is intentionally not implemented");
    }
    /// <summary>
    /// Runs this instance. 
    /// It call <see cref="IMessageHandler.AttachToNetwork"/> and enables data pumping by enabling the the state of all <see cref="IMessageHandler"/> added to this collection.
    /// </summary>
    internal void Run()
    {
      foreach (IMessageHandler _mx in this.Values)
      {
        _mx.AttachToNetwork();
        _mx.State.Enable();
      }
    }
    private MessageHandlersCollection()
      : base()
    { }

  }
}
