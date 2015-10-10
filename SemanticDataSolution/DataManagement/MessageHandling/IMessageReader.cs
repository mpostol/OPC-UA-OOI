
using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  
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

}
