
using System;

namespace UAOOI.Networking.SemanticData.Encoding
{

  /// <summary>
  /// Interface IBinaryDecoder - instance of this interface is used to decode the message and package headers content using binary encoding.
  /// </summary>
  public interface IBinaryEncoder
  {
    /// <summary>
    /// Writes the next byte from the current stream and advances the current position of the stream by one byte.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The next <see cref="System.Byte" /> Write from the current stream.</returns>
    void Write(byte value);
    /// <summary>
    ///  Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.
    /// </summary>
    /// <param name="value">he four-byte signed integer to write.</param>
    /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
    /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
    void Write(int value);
    void Write(bool value);
    void Write(sbyte value);
    void Write(short value);
    void Write(ushort value);
    void Write(uint value);
    void Write(long value);
    void Write(ulong value);
    void Write(float value);
    void Write(double value);
    /// <summary>
    /// Writes the <see cref="Guid"/> from UA Binary encoded as a 16-element byte array that contains the value and advances the stream position by 16 bytes.<see cref="System.IO.Stream"/>.
    /// </summary>
    /// <returns>The <see cref="Guid"/> object encoded from the message.</returns>
    void Write(Guid value);
    void Write(byte[] value);
    void Write(DateTime value);
  }
}
