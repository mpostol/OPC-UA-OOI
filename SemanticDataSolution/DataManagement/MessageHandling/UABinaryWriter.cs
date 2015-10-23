
using System;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class UABinaryWriter - encodes OPC UA data types as binary values in a specific encoding from UA Binary encoded <see cref="Stream"/>.
  /// </summary>
  public class UABinaryWriter : BinaryWriter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UABinaryWriter" /> class based on the specified <see cref="Stream"/> stream and using UTF-8 encoding.
    /// </summary>
    /// <param name="output">The output <see cref="Stream"/> containing UA Binary encoded values..</param>
    public UABinaryWriter(Stream output) : base(output) { }
    /// <summary>
    /// Writes a <see cref="Guid"/> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    public virtual void Write(Guid value)
    {
      Write(((Guid)value).ToByteArray());
    }
  }

}

