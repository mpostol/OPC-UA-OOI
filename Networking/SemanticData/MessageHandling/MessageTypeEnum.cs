
namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Enum MessageTypeEnum - The type of the message.
  /// </summary>
  public enum MessageTypeEnum : byte
  {

    /// <summary>
    /// The data key frame
    /// </summary>
    DataKeyFrame = 0x1,
    /// <summary>
    /// The data delta frame
    /// </summary>
    DataDeltaFrame = 0x2,
    /// <summary>
    /// The event frame
    /// </summary>
    Event = 0x3,
    /// <summary>
    /// The keep alive frame
    /// </summary>
    KeepAlive = 0x4,
    /// <summary>
    /// The data set metadata frame
    /// </summary>
    DataSetMetadata = 0x5,

  }

}
