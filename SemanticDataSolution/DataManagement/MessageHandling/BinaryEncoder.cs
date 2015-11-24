
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;

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
    public BinaryEncoder(Guid producerId, IUAEncoder uaEncoder) : base(producerId, uaEncoder)
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
    public override void WriteUInt64(ulong value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteUInt32(uint value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteUInt16(ushort value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteString(string value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteSingle(float value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteSByte(sbyte value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteInt64(long value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteInt32(int value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteInt16(short value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteDouble(double value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteBoolean(bool value)
    {
      m_BinaryWriter.Write(value);
    }
    /// <summary>
    /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
    /// </summary>
    /// <param name="value">TThe unsigned <see cref="byte"/> to write./param>
    public override void WriteByte(byte value)
    {
      m_BinaryWriter.Write(value);
    }
    /// <summary>
    /// Writes a <see cref="Guid"/> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    public override void WriteGuid(Guid value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void WriteBytes(byte[] value)
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
