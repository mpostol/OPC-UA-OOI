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
  public interface IConfiguration { }
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
  public interface IMessageWriter : IMessageHandler
  {

  }
  public interface IMessageReader : IMessageHandler
  {

  }
  public interface IMessageHandler
  {
    
    event EventHandler<MessageHandlerEventArgs> messageHandlerStatusChanged;
    /// <summary>
    /// The property provides the current operational state of the <see cref="IMessageHandler"/> Object.
    /// </summary>
    /// <value>The handler state <see cref="HandlerStat"/>.</value>
    HandlerStat HandlerState { get; }
    /// <summary>
    /// This method is used to enable a configured <see cref="IMessageHandler"/> object. If a normal operation is possible, the state changes into <see cref="HandlerStat.Operational"/> state. 
    /// In the case of an error situation, the state changes into <see cref="HandlerStat.Error"/>. The operation is rejected if the current <paramref name="HandlerState"/>  is not <see cref="HandlerStat.Disabled"/>.
    /// </summary>
    void Enable();
    /// <summary>
    /// Disables this instance.
    /// </summary>
    void Disable();
  }
  public class MessageHandlerEventArgs : EventArgs
  {
  }
  public class AssociationStateChangedEventArgs : EventArgs
  {

  }
  public enum HandlerStat
  {
    /// <summary>
    /// The handler is not configured and cannot be enabled.
    /// </summary>
    NoConfiguration,
    /// <summary>
    /// The handler is configured but currently disabled.
    /// </summary>
    Disabled,
    /// <summary>
    /// The handler is operational.
    /// </summary>
    Operational,
    /// <summary>
    /// The handler is in an error state.
    /// </summary>
    Error
  }
}
