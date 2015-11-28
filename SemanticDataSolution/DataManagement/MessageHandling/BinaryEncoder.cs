
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryEncoder - wrapper of <see cref="BinaryWriter"/> supporting OPC UA binary encoding.
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
    /// <summary>
    /// Flag: Has Dispose already been called?
    /// </summary>
    bool disposed = false;
    // 
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// Public implementation of Dispose pattern callable by consumers.
    /// <remarks>
    /// </remarks>
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
    public override void Write(ulong value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(uint value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(ushort value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(string value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(float value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(sbyte value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(long value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(int value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(short value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(double value)
    {
      m_BinaryWriter.Write(value);
    }
    public override void Write(bool value)
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
    public override void Write(byte[] value)
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
    /// A field of <see cref="SeekOrigin"/> indicating the reference point from which the new position is to be obtained..
    /// </param>
    /// <returns>The position with the current stream as <see cref="Int64"/>.</returns>
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
    private BinaryWriter m_BinaryWriter;
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
      m_BinaryWriter = new BinaryWriter(m_Output);
      EncodePackageHeaders();
    }
    #endregion

  }
}
