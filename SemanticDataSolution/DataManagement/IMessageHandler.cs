
using System;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Interface IMessageWriter - provides functionality supporting sending the messages over the wire.
  /// </summary>
  public interface IMessageWriter : IMessageHandler
  {

    /// <summary>
    /// Sends the specified message using association transport protocol.
    /// </summary>
    /// <param name="message">The message.</param>
    void Send(IPeriodicDataMessage message);

  }
  /// <summary>
  /// Interface IMessageReader - provides functionality supporting reading the messages from the wire.
  /// </summary>
  public interface IMessageReader : IMessageHandler
  {
    
    /// <summary>
    /// Occurs when an asynchronous operation to read a new message completes.
    /// </summary>
    event EventHandler<MessageEventArg> ReadMessageCompleted;

  }
  /// <summary>
  /// Interface IMessageHandler - provides basic functionality handling messages communication over the wire.
  /// </summary>
  public interface IMessageHandler
  {

    /// <summary>
    /// Gets the the state machine for the the <see cref="IMessageHandler"/> instance.
    /// </summary>
    /// <value>An object of <see cref="IAssociationState"/> providing implementation of the machine state governing this instance behavior.</value>
    IAssociationState State { get; }
    /// <summary>
    /// Attaches to network - initialize the underlying protocol stack and establish the connection with the broker is applicable.
    /// </summary>
    /// <remarks>
    /// Depending on the message transport layer type implementation of this function varies. 
    /// </remarks>
    void AttachToNetwork();

  }

}
