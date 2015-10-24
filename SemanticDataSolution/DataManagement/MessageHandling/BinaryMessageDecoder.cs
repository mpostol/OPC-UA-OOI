
using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class BinaryMessageDecoder - provides message content binary decoding functionality
  /// </summary>
  /// <remarks>
  /// <note>Implements only simple value types. Structural types must be implemented after more details will 
  /// be available in the spec.</note>
  /// </remarks>
  public abstract class BinaryMessageDecoder : MessageReaderBase, IBinaryHeaderReader
  {

    #region MessageReaderBase
    /// <summary>
    /// Gets the content mask. The content mast read from the message or provided by the writer.
    /// The order of the bits starting from the least significant bit matches the order of the data items
    /// within the data set.
    /// </summary>
    /// <value>The content mask is represented as unsigned number <see cref="UInt64" />.
    /// The value is provided by the message.
    /// The order of the bits starting from the least significant bit matches the order of the data items within the data set.</value>
    public override ulong ContentMask
    {
      get { return ulong.MaxValue; } //TODO must be implemented - get it from message.
    }
    #endregion

  }

}
