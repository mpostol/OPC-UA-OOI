
using System;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class BinaryMessageDecoder - provides message content binary decoding functionality
  /// </summary>
  /// <remarks>
  /// <note>Implements only simple value types. Structural types must be implemented after more details will 
  /// be available in the spec.</note>
  /// </remarks>
  public abstract class BinaryMessageDecoder : MessageReaderBase
  {

    #region decoder
    /// <summary>
    /// Reads an 8-byte unsigned integer from the message and advances the position by eight bytes.
    /// </summary>
    /// <returns>An 8-byte unsigned integer <see cref="UInt64"/> read from this message. .</returns>
    protected override UInt64 ReadUInt64()
    {
      return m_Reader.ReadUInt64();
    }
    protected override UInt32 ReadUInt32()
    {
      return m_Reader.ReadUInt32();
    }
    protected override UInt16 ReadUInt16()
    {
      return m_Reader.ReadUInt16();
    }
    protected override String ReadString()
    {
      return m_Reader.ReadString();
    }
    protected override Single ReadSingle()
    {
      return m_Reader.ReadSingle();
    }
    protected override SByte ReadSByte()
    {
      return m_Reader.ReadSByte();
    }
    protected override Int64 ReadInt64()
    {
      return m_Reader.ReadInt64();
    }
    protected override Int32 ReadInt32()
    {
      return m_Reader.ReadInt32();
    }
    protected override Int16 ReadInt16()
    {
      return m_Reader.ReadInt16();
    }
    protected override Double ReadDouble()
    {
      return m_Reader.ReadDouble();
    }
    protected override Decimal ReadDecimal()
    {
      return Convert.ToDecimal(m_Reader.ReadInt64());
    }
    protected override char ReadChar()
    {
      return m_Reader.ReadChar();
    }
    protected override Byte ReadByte()
    {
      return m_Reader.ReadByte();
    }
    protected override Boolean ReadBoolean()
    {
      return m_Reader.ReadBoolean();
    }
    protected override DateTime ReadDateTime()
    {
      return CommonDefinitions.GetUADateTime(m_Reader.ReadInt64());
    }
    #endregion

    #region private
    private BinaryReader m_Reader = null;
    /// <summary>
    /// Initializes a new instance of the  <see cref="System.IO.BinaryReader"/> class based on the buffer and using UTF-8 encoding.
    /// </summary>
    /// <param name="buffer">The buffer with the frame content. </param>
    protected void CreateReader(byte[] buffer)
    {
      MemoryStream _stream = new MemoryStream(buffer, 0, buffer.Length);
      m_Reader = new BinaryReader(_stream);
    }
    /// <summary>
    /// Releases all resources used by the instance of the  <see cref="System.IO.BinaryReader"/> class used to manage content of the current message.
    /// </summary>
    protected void DisposeReader()
    {
      m_Reader.Dispose();
      m_Reader = null;
    }
    #endregion

  }

}
