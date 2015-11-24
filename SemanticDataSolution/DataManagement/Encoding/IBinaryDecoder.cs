
using System;

namespace UAOOI.SemanticData.DataManagement.Encoding
{
  /// <summary>
  /// Interface IBinaryDecoder - instance of this interface is used to decode the message and package headers content using binary encoding.
  /// </summary>
  public interface IBinaryDecoder
  {
    /// <summary>
    /// Reads the next byte from the current stream and advances the current position of the stream by one byte.
    /// </summary>
    /// <returns>The next <see cref="System.Byte"/> read from the current stream.</returns>
    byte ReadByte();
    int ReadInt32();
    bool ReadBoolean();
    sbyte ReadSByte();
    short ReadInt16();
    ushort ReadUInt16();
    uint ReadUInt32();
    long ReadInt64();
    ulong ReadUInt64();
    float ReadSingle();
    double ReadDouble();
    string ReadString();
    //TODO - remove from this interface - only method supported by the platform shall be here.
    DateTime ReadDateTime();
    /// <summary>
    /// Reads the <see cref="Guid"/> from UA Binary encoded as a 16-element byte array that contains the value and advances the stream position by 16 bytes.<see cref="System.IO.Stream"/>.
    /// </summary>
    /// <returns>The <see cref="Guid"/> decoded from the <see cref="System.IO.Stream"/>.</returns>
    Guid ReadGuid();
    byte[] ReadBytes(int count);
  }
}
