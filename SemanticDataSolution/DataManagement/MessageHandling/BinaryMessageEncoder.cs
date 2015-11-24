
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

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryMessageEncoder"/> class.
    /// </summary>
    public BinaryMessageEncoder(IUAEncoder uaEncoder) : base(uaEncoder)
    {
      MessageHeader = MessageHeader.GetProducerMessageHeader(this);
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
    /// <summary>
    /// Creates and prepares new the message.
    /// </summary>
    /// <param name="length">The length.</param>
    /// <param name="dataSetId">The data set identifier.</param>
    protected override void CreateMessage(int length, Guid dataSetId)
    {
      OnMessageAdding();
      //Create message header and placeholder for further header content.
      MessageHeader.DataSetId = dataSetId;
      MessageHeader.Synchronize();
    }
    /// <summary>
    /// Sends the message - evaluates condition if send the package.
    /// </summary>
    /// <remarks>
    /// In current implementation one message per package is sent.
    /// </remarks>
    protected override void SendMessage()
    {
      OnMessageAdded();
      //TODO sign and encrypt the message.
    }
    #endregion

    #region private
    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    protected abstract void OnMessageAdding();
    /// <summary>
    /// Called when the current message has been added and is ready to be sent out.
    /// </summary>
    protected abstract void OnMessageAdded();
    #endregion    

  }

}
