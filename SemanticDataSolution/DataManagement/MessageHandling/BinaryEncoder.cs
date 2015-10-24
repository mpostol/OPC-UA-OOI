
using System;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryEncoder - wrapper of <see cref="UABinaryWriter"/> supporting OPC UA binary encoding.
  /// </summary>
  public abstract class BinaryEncoder : BinaryPackageEncoder, IDisposable
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryEncoder"/> class wrapper of <see cref="UABinaryWriter"/> supporting OPC UA binary encoding..
    /// </summary>
    public BinaryEncoder()
    {
      CreateUABinaryWriter();
    }
    #endregion

    #region IDisposable
    // Flag: Has Dispose already been called?
    bool disposed = false;
    // Public implementation of Dispose pattern callable by consumers.
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    // Protected implementation of Dispose pattern.
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
        return;
      if (disposing)
        DisposeWriter();
      disposed = true;
    }
    #endregion

    #region BinaryPackageEncoder

    #region BinaryWriter
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
    /// <summary>
    /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
    /// </summary>
    /// <param name="value">TThe unsigned <see cref="byte"/> to write./param>
    public override void Write(byte value)
    {
      m_BinaryWriter.Write(value);
    }
    /// <summary>
    /// Writes a <see cref="Guid"/> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    public override void Write(Guid value)
    {
      m_BinaryWriter.Write(value);
    }
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
    public override long Seek(int offset, SeekOrigin origin)
    {
      return m_BinaryWriter.Seek(offset, origin);
    }
    #endregion

    /// <summary>
    /// Begins sending the frame.
    /// </summary>
    protected override void SendFrame()
    {
      m_BinaryWriter.Close();
      SendFrame(m_Output.ToArray());
      DisposeWriter();
      CreateUABinaryWriter();
    }
    #endregion

    #region private
    //vars
    private MemoryStream m_Output;
    private UABinaryWriter m_BinaryWriter;
    //methods
    /// <summary>
    /// Sends the message.
    /// </summary>
    /// <param name="buffer">The buffer with the message content.</param>
    protected abstract void SendFrame(byte[] buffer);
    private void DisposeWriter()
    {
      m_BinaryWriter.Dispose();
      m_BinaryWriter = null;
    }
    private void CreateUABinaryWriter()
    {
      m_Output = new MemoryStream();
      m_BinaryWriter = new UABinaryWriter(m_Output);
      EncodePackageHeaders();
    }
    #endregion

  }
}
