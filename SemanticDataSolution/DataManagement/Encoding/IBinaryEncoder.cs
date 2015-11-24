
using System;

namespace UAOOI.SemanticData.DataManagement.Encoding
{
 
  /// <summary>
  /// Interface IBinaryDecoder - instance of this interface is used to decode the message and package headers content using binary encoding.
  /// </summary>
  public interface IBinaryEncoder
  {
    /// <summary>
    /// Writes the next byte from the current stream and advances the current position of the stream by one byte.
    /// </summary>
    /// <returns>The next <see cref="System.Byte"/> Write from the current stream.</returns>
    void WriteByte(byte value);
    void WriteInt32(int value);
    void WriteBoolean(bool value);
    void WriteSByte(sbyte value);
    void WriteInt16(short value);
    void WriteUInt16(ushort value);
    void WriteUInt32(uint value);
    void WriteInt64(long value);
    void WriteUInt64(ulong value);
    void WriteSingle(float value);
    void WriteDouble(double value);
    void WriteString(string value);
    /// <summary>
    /// Writes the <see cref="Guid"/> from UA Binary encoded as a 16-element byte array that contains the value and advances the stream position by 16 bytes.<see cref="System.IO.Stream"/>.
    /// </summary>
    /// <returns>The <see cref="Guid"/> object encoded from the message.</returns>
    void WriteGuid(Guid value);
    void WriteBytes(byte[]  value);
  }
}
