
using System;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class UABinaryReader - decodes OPC UA data types as binary values in a specific encoding from UA Binary encoded <see cref="Stream"/>.
  /// </summary>
  public class UABinaryReader : BinaryReader
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="UABinaryReader"/> class based on the specified <see cref="Stream"/> stream and using UTF-8 encoding.
    /// </summary>
    /// <param name="input">The input <see cref="Stream"/> containing UA Binary encoded values.</param>
    public UABinaryReader(Stream input) : base(input) { }
    #endregion

    #region OPC UA Decoder
    /// <summary>
    /// Reads the <see cref="Guid"/> from UA binary encoded as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <returns>The <see cref="Guid"/> decoded from the UA binary encoded <see cref="Stream"/>.</returns>
    public Guid ReadGuid()
    {
      byte[] bytes = ReadBytes(m_EncodedGuidLength);
      return new Guid(bytes);
    }
    /// <summary>
    /// Reads the <see cref="DateTime"/> from UA binary encoded as <see cref="Int64"/> that contains the value and advances the stream position by 8 bytes.
    /// </summary>
    /// <returns>The <see cref="DateTime "/> decoded from the UA binary encoded <see cref="Stream"/>.</returns>
    public DateTime ReadDateTime()
    {
      return CommonDefinitions.GetUADateTime(ReadInt64());
    }
    #endregion

    #region private
    private const int m_EncodedGuidLength = 16;
    #endregion

  }
}
