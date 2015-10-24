
using System;
using System.Diagnostics;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class BinaryMessageEncoder - provides message content binary encoding functionality
  /// </summary>
  /// <remarks>
  /// <note>
  /// Implements only simple value types. Structural types must be implemented after more details will 
  /// be available in the spec.
  /// </note>
  /// </remarks>
  public abstract class BinaryMessageEncoder : MessageWriterBase, IBinaryHeaderWriter
  {

    #region MessageWriterBase

    #region IBinaryHeaderWriter
    /// <summary>
    /// Sets the position within the current stream.
    /// </summary>
    /// <param name="offset">
    /// A byte offset relative to origin.
    /// </param>
    /// <param name="origin">
    /// A field of <see cref="System.IO.SeekOrigin"/> indicating the reference point from which the new position is to be obtained..
    /// </param>
    /// <returns>The position with the current stream as <see cref="System.Int64"/>.</returns>
    public long Seek(int offset, SeekOrigin origin)
    {
      return m_BinaryWriter.Seek(offset, origin);
    }
    /// <summary>
    /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
    /// </summary>
    /// <param name="value">TThe unsigned <see cref="byte"/> to write./param>
    public void Write(byte value)
    {
      m_BinaryWriter.Write(value);
    }
    /// <summary>
    /// Writes a <see cref="Guid"/> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    public void Write(Guid value)
    {
      m_BinaryWriter.Write(value);
    }
    #endregion    

    #region Encoder
    protected override void WriteUInt64(ulong value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteUInt32(uint value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteUInt16(ushort value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteString(string value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteSingle(float value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteSByte(sbyte value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteInt64(long value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteInt32(int value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteInt16(short value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteDouble(double value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteDecimal(decimal value, object parameter)
    {
      m_BinaryWriter.Write(Convert.ToInt64(value));
    }
    protected override void WriteDateTime(DateTime value, object parameter)
    {
      m_BinaryWriter.Write(global::UAOOI.SemanticData.DataManagement.MessageHandling.CommonDefinitions.GetUADataTimeTicks(value));
    }
    protected override void WriteByte(byte value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteBool(bool value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    protected override void WriteChar(char value, object parameter)
    {
      m_BinaryWriter.Write(value);
    }
    #endregion

    /// <summary>
    /// Creates the message.
    /// </summary>
    /// <param name="length">The length.</param>
    protected override void CreateMessage(int length)
    {
      m_Output = new MemoryStream();
      m_BinaryWriter = new UABinaryWriter(m_Output);
      EncodePackageHeaders();
    }
    protected override void SendMessage()
    {
      Debug.Assert(m_BinaryWriter != null);
      SendPackage();

      #region To be moved to the package
      m_BinaryWriter.Close();
      SendMessage(m_Output.ToArray());
      DisposeWriter();
      #endregion
    }

    #endregion

    #region abstract
    /// <summary>
    /// Sends the message.
    /// </summary>
    /// <param name="buffer">The buffer with the message content.</param>
    protected abstract void SendMessage(byte[] buffer);
    /// <summary>
    /// Sends the package.
    /// </summary>
    protected abstract void SendPackage();
    /// <summary>
    /// If implemented in the derived class encodes the headers of the package.
    /// </summary>
    protected abstract void EncodePackageHeaders();
    #endregion    
    
    #region private
    //vars
    private MemoryStream m_Output;
    private UABinaryWriter m_BinaryWriter;  //TODO move to package encoder.
    //methods
    private void DisposeWriter()
    {
      m_BinaryWriter.Dispose();
      m_BinaryWriter = null;
    }
    #endregion

  }

}
