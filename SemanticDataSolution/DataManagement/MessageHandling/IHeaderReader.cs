
using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Interface IHeaderReader - instance of this interface is used to manage the message and package headers content by the reader.
  /// </summary>
  public interface IHeaderReader
  {

    /// <summary>
    /// Reads the <see cref="Guid"/> from UA Binary encoded as a 16-element byte array that contains the value and advances the stream position by 16 bytes.<see cref="System.IO.Stream"/>.
    /// </summary>
    /// <returns>The <see cref="Guid"/> decoded from the <see cref="System.IO.Stream"/>.</returns>
    Guid ReadGuid();
    /// <summary>
    /// Reads the next byte from the current stream and advances the current position of the stream by one byte.
    /// </summary>
    /// <returns>The next <see cref="System.Byte"/> read from the current stream.</returns>
    byte ReadByte();

  }
}
