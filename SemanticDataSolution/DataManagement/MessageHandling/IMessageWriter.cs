
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Interface IMessageWriter - provides functionality supporting sending the messages over the wire.
  /// </summary>
  public interface IMessageWriter : IMessageHandler
  {

    /// <summary>
    /// Sends the data described by a data set collection to remote destination.
    /// </summary>
    /// <param name="producerBinding">Encapsulates functionality used by the <see cref="IMessageWriter" /> to collect all the data (data set items) required to prepare new message and send it over the network.</param>
    /// <param name="length">Number of items to be send used to calculate the length of the message.</param>
    void Send(Func<int, IProducerBinding> producerBinding, int length);

  }
}
