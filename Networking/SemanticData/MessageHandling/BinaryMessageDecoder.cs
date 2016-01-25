
using System;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class BinaryMessageDecoder - provides message content binary decoding functionality.
  /// </summary>
  /// <remarks>
  /// <note>Implements only simple value types. Structural types must be implemented after more details will 
  /// be available in the spec.</note>
  /// </remarks>
  public abstract class BinaryMessageDecoder : MessageReaderBase
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryMessageDecoder" /> class.
    /// </summary>
    /// <param name="uaDecoder">The decoder that provides methods to be used to decode OPC UA Built-in types.</param>
    public BinaryMessageDecoder(IUADecoder uaDecoder) : base(uaDecoder)
    {
      b_MessageHeader = MessageHeader.GetConsumerMessageHeader(this);
    }
    #endregion

    #region MessageReaderBase
    /// <summary>
    /// Gets the content mask. The content mast read from the message or provided by the writer.
    /// The order of the bits starting from the least significant bit matches the order of the data items
    /// within the data set.
    /// </summary>
    /// <value>The content mask is represented as unsigned number <see cref="UInt64" />.
    /// The value is provided by the message.
    /// The order of the bits starting from the least significant bit matches the order of the data items within the data set.</value>
    public override ulong ContentMask //TODO must be implemented - get it from message.
    {
      get { return ulong.MaxValue; } 
    }
    /// <summary>
    /// Gets or sets the message header.
    /// </summary>
    /// <value>The message header.</value>
    protected override MessageHeader MessageHeader { get { return b_MessageHeader; } }
    #endregion

    #region private
    private MessageHeader b_MessageHeader;
    /// <summary>
    /// Called when there is a new message in the packet that is to be processed.
    /// </summary>
    protected void OnNewMessageArrived(UInt16 dataSetId)
    {
      MessageHeader.Synchronize();
      RaiseReadMessageCompleted(dataSetId);
    }
    #endregion

  }

}
