
using System;
using System.IO;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.MessageHandling
{

  /// <summary>
  /// Class BinaryMessageEncoder - provides message content binary encoding functionality.
  /// </summary>
  /// <remarks>
  /// <note>
  /// Implements only simple value types. Structural types must be implemented after more details will 
  /// be available in the spec.
  /// </note>
  /// </remarks>
  public abstract class BinaryMessageEncoder : MessageWriterBase, IBinaryHeaderEncoder
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryMessageEncoder" /> class.
    /// </summary>
    /// <param name="uaEncoder">The UA encoder.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="lengthFieldType">Type of the length field.</param>
    public BinaryMessageEncoder(IUAEncoder uaEncoder, MessageLengthFieldTypeEnum lengthFieldType) : base(uaEncoder)
    {
      m_lengthFieldType = lengthFieldType;
    }

    #region IBinaryHeaderWriter
    /// <summary>
    /// If implemented by the derived class sets the position within the wrapped stream.
    /// </summary>
    /// <param name="offset">
    /// A byte offset relative to origin.
    /// </param>
    /// <param name="origin">
    /// A field of <see cref="System.IO.SeekOrigin"/> indicating the reference point from which the new position is to be obtained..
    /// </param>
    /// <returns>The position with the current stream as <see cref="System.Int64"/>.</returns>
    public abstract long Seek(int offset, SeekOrigin origin);
    #endregion

    #region Header
    /// <summary>
    /// Gets or sets the message header.
    /// </summary>
    /// <value>The message header.</value>
    internal MessageHeader MessageHeader { get; set; }
    #endregion

    #region MessageWriterBase
    /// <summary>
    /// Creates the message.
    /// </summary>
    /// <param name="encoding">The selected encoding for the message.</param>
    /// <param name="dataSetWriterId">The data set writer identifier.</param>
    /// <param name="fieldCount">The field count.</param>
    /// <param name="sequenceNumber">The sequence number.</param>
    /// <param name="timeStamp">The time stamp.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    internal protected override void CreateMessage(FieldEncodingEnum encoding, ushort dataSetWriterId, ushort fieldCount, ushort sequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion)
    {
      OnMessageAdding(dataSetWriterId);
      MessageHeader = MessageHeader.GetProducerMessageHeader(this, encoding, m_lengthFieldType, MessageTypeEnum.DataKeyFrame, configurationVersion);
      //Create message header and placeholder for further header content.
      MessageHeader.FieldCount = fieldCount;
      MessageHeader.MessageSequenceNumber = sequenceNumber;
      MessageHeader.TimeStamp = timeStamp;
    }
    /// <summary>
    /// Sends the message - evaluates condition if send the package.
    /// </summary>
    /// <remarks>
    /// In current implementation one message per package is sent.
    /// </remarks>
    protected override void SendMessage()
    {
      MessageHeader.Synchronize();
      OnMessageAdded();
      //TODO sign and encrypt the message.
    }
    #endregion

    #region private
    private MessageLengthFieldTypeEnum m_lengthFieldType;

    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    protected abstract void OnMessageAdding(UInt16 dataSetWriterIds);
    /// <summary>
    /// Called when the current message has been added and is ready to be sent out.
    /// </summary>
    protected abstract void OnMessageAdded();

    #endregion    

  }

}
