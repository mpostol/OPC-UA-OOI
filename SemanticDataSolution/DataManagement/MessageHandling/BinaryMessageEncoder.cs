
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
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
  public abstract class BinaryMessageEncoder : MessageWriterBase, IBinaryHeaderWriter
  {
    private MessageHeader.ConfigurationVersionDataType m_ConfigurationVersion;
    private byte m_MessageFlags;

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryMessageEncoder"/> class.
    /// </summary>
    public BinaryMessageEncoder(IUAEncoder uaEncoder) : base(uaEncoder)
    {
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
    public MessageHeader MessageHeader { get; set; }
    #endregion

    #region MessageWriterBase
    protected override void CreateMessage(UInt32 dataSetWriterId, ushort fieldCount, ushort messageSequenceNumber, DateTime timeStamp)
    {
      OnMessageAdding(dataSetWriterId);
      MessageHeader = MessageHeader.GetProducerMessageHeader(this);
      //Create message header and placeholder for further header content.
      MessageHeader.ConfigurationVersion = m_ConfigurationVersion;
      MessageHeader.FieldCount = fieldCount;
      MessageHeader.MessageFlags = m_MessageFlags;
      MessageHeader.MessageSequenceNumber = messageSequenceNumber;
      MessageHeader.MessageType = MessageHeader.MessageTypeEnum.DataKeyFrame;
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
    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    protected abstract void OnMessageAdding(UInt32 dataSetWriterIds);
    /// <summary>
    /// Called when the current message has been added and is ready to be sent out.
    /// </summary>
    protected abstract void OnMessageAdded();

    #endregion    

  }

}
