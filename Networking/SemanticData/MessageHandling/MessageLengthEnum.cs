
using System;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Enum MessageLengthFieldTypeEnum - defines the type of the length field in the message header.
  /// </summary>
  public enum MessageLengthFieldTypeEnum : byte
  {

    /// <summary>
    /// The field type is byte
    /// </summary>
    OneByte = 0x0,
    /// <summary>
    /// The field type is <see cref="UInt16"/>
    /// </summary>
    TwoBytes = 0x1,
    /// <summary>
    /// The field type is <see cref="UInt32"/>
    /// </summary>
    FourBytes = 0x2,
    /// <summary>
    /// The value is reserved and not applicable for the current protocol version.
    /// </summary>
    Reserver = 0x3

  }
}
