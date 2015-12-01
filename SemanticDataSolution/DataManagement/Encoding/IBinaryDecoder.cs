
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
    byte[] ReadBytes(int count);
    Guid ReadGuid();
    DateTime ReadDateTime();

  }
}
