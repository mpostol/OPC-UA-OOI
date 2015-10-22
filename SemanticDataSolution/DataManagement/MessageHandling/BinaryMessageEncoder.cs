
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
  /// Implements only simple value types. Structural types must be implemented after more details  will 
  /// be available in the spec.
  /// </note>
  /// </remarks>
  public abstract class BinaryMessageEncoder : MessageWriterBase
  {

    #region MessageWriterBase

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

    protected override void CreateMessage(int length)
    {
      m_Output = new MemoryStream();
      m_BinaryWriter = new BinaryWriter(m_Output);
      EncodeHeaders();
    }
    protected override void SendMessage()
    {
      Debug.Assert(m_BinaryWriter != null);
      m_BinaryWriter.Close();
      DoUDPSend(m_Output.ToArray());
      DisposeWriter();
    }
    #endregion

    #region private
    //vars
    private MemoryStream m_Output;
    private BinaryWriter m_BinaryWriter;
    //methods
    /// <summary>
    /// Encodes the headers of the message.
    /// </summary>
    protected abstract void EncodeHeaders();
    private void DisposeWriter()
    {
      m_BinaryWriter.Dispose();
      m_BinaryWriter = null;
    }
    /// <summary>
    /// Sends the message.
    /// </summary>
    /// <param name="buffer">The buffer with the message content.</param>
    protected abstract void DoUDPSend(byte[] buffer);
    #endregion

  }

}
