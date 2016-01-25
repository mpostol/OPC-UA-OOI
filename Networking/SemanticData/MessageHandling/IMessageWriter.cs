
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

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
    /// <param name="contentMask">The content mask represented as unsigned number <see cref="UInt64" />. The order of the bits starting from the least significant
    /// bit matches the order of the data items within the data set.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="dataSetWriterId">The data set identifier.</param>
    /// <param name="messageSequenceNumber">The message sequence number. A monotonically increasing sequence number assigned by the publisher to each message sent.</param>
    /// <param name="timeStamp">The time stamp - the time the Data was collected.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    void Send
      (Func<int, IProducerBinding> producerBinding, ushort length, ulong contentMask, FieldEncodingEnum encoding, UInt16 dataSetWriterId, ushort messageSequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion);

  }
}
