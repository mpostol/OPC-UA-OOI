using System;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IEndPointConfiguration - Represents the current network attachment parameters.
  /// Depending on the role of the <see cref="IAssociation"/> it describes: remote or local end point.
  /// </summary>
  public interface IEndPointConfiguration
  {
  }
  public interface IMessageWriter : IMessageHandler
  {
  }
  public interface IMessageReader : IMessageHandler
  {
    event EventHandler<MessageEventArg> messageHandlerStatusChanged;
  }
  public interface IMessageHandler
  {
    /// <summary>
    /// The property provides the current operational state of the <see cref="IMessageHandler"/> Object.
    /// </summary>
    /// <value>The handler state <see cref="HandlerState"/>.</value>
    HandlerState HandlerState { get; }
    /// <summary>
    /// This method is used to enable a configured <see cref="IMessageHandler"/> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational"/> state. 
    /// In the case of an error situation, the state changes into <see cref="HandlerState.Error"/>. The operation is rejected if the current <paramref name="HandlerState"/>  is not <see cref="HandlerState.Disabled"/>.
    /// </summary>
    void Enable();
    /// <summary>
    /// This method is used to disable a PubSub Object.
    /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled"/> or <see cref="HandlerState.NoConfiguration"/>.
    /// </summary>
    void Disable();
  }
}
