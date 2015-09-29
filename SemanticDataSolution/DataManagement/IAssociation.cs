using System;
using System.Collections.Generic;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IAssociation - it is represents PubSubConnectionType and contains all information related to communication protocols.
  /// </summary>
  public interface IAssociation : IComparable, IEnumerable<string>
  {
    ISemanticData DataDescriptor { get; }
    /// <summary>
    /// Gets the current operational state of the association <see cref="IAssociationState"/>
    /// </summary>
    /// <value>The state.</value>
    IAssociationState State { get; }
    event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    /// <summary>
    /// Gets the address as the end point description.
    /// </summary>
    /// <value>The network end point description.</value>
    IEndPointConfiguration Address { get; }
    ISemanticDataItemConfiguration DefaultConfiguration { get; }
    ISemanticDataItemConfiguration this[string SymbolicName] { get; set; }
  }
  public interface IProducer : IAssociation
  {
    IProducerConfiguration Configuration { get; }
    void AddMessageWriter(IMessageWriter messageWriter, Func<IMessageHandler> messageHandler);
    void RemoveMessageWriter(IMessageHandler messageHandler);
  }
  public interface IConsumer : IAssociation
  {
    IProducerConfiguration Configuration { get; }
    void AddMessageReader(IMessageReader messageWriter, Func<IMessageHandler> messageHandler);
    void RemoveMessageReader(IMessageHandler messageHandler);
  }
  public interface ISemanticDataItemConfiguration
  {
    bool State { get; }
    /// <summary>
    /// Enables this instance.
    /// </summary>
    /// <remarks>It must be method because the operation may be executed asynchronously</remarks>
    void Enable();
    /// <summary>
    /// Disables this instance.
    /// </summary>
    /// <remarks>It must be method because the operation may be executed asynchronously</remarks>
    void Disable();
  }

  public interface IConfiguration  {}
  public interface IProducerConfiguration : IConfiguration
  {

  }
  public interface IConsumerConfiguration : IConfiguration
  {

  }
  /// <summary>
  /// Interface IEndPointConfiguration - Represents the current network attachment parameters.
  /// Depending on the role of the <see cref="IAssociation"/> it describes: remote or local end point.
  /// </summary>
  public interface IEndPointConfiguration
  {
  }
  public interface IAssociationState
  {
  }
  public interface IMessageWriter
  {
  }
  public interface IMessageReader
  {

  }
  public interface IMessageHandler
  {
    event EventHandler<MessageHandlerEventArgs> messageHandlerStatusChanged;
  }
  public class MessageHandlerEventArgs : EventArgs
  {
  }
  public class AssociationStateChangedEventArgs : EventArgs
  {

  }
}
