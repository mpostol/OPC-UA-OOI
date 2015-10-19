
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

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
    /// <summary>
    /// Updates my values using inverse of control pattern.
    /// </summary>
    /// <param name="update">Captures a delegated used to update the consumer variables using values decoded form the message.</param>
    /// <param name="length">Number of items in the data set.</param>
    void UpdateMyValues(Func<int, IConsumerBinding> update, int length);
    /// <summary>
    /// Check if the message destination is the data set described by the <paramref name="dataId"/> of type <see cref="ISemanticData"/>.
    /// </summary>
    /// <param name="dataId">The data identifier <see cref="ISemanticData"/>.</param>
    /// <returns><c>true</c> if <paramref name="dataId"/> is the destination of the message, <c>false</c> otherwise.</returns>
    bool IAmDestination(ISemanticData dataId);

  }

}
