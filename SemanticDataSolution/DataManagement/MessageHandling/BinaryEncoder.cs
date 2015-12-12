
using System;
using System.Collections.Generic;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryEncoder - wrapper of <see cref="BinaryWriter"/> supporting OPC UA binary encoding.
  /// </summary>
  public abstract class BinaryEncoder : BinaryPacketEncoder, IDisposable
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryEncoder" /> class wrapper of <see cref="BinaryWriter" /> supporting OPC UA binary encoding..
    /// </summary>
    /// <param name="uaEncoder">The ua encoder.</param>
    /// <param name="producerId">The producer identifier.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="lengthFieldType">Type of the length field.</param>
    public BinaryEncoder(IUAEncoder uaEncoder, Guid producerId, FieldEncodingEnum encoding, MessageLengthFieldTypeEnum lengthFieldType) : base(uaEncoder, encoding, lengthFieldType)
    {
      m_producerId = producerId;
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
      m_binaryWriter.Write(value);
    }
    public override void Write(uint value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(ushort value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(string value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(float value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(sbyte value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(long value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(int value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(short value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(double value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(bool value)
    {
      m_binaryWriter.Write(value);
    }
    /// <summary>
    /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
    /// </summary>
    /// <param name="value">TThe unsigned <see cref="byte"/> to write./param>
    public override void Write(byte value)
    {
      m_binaryWriter.Write(value);
    }
    public override void Write(byte[] value)
    {
      m_binaryWriter.Write(value);
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
      return m_binaryWriter.Seek(offset, origin);
    }
    #endregion

    /// <summary>
    /// Begins sending the frame.
    /// </summary>
    protected override void SendFrame()
    {
      m_binaryWriter.Close();
      SendFrame(m_output.ToArray());
      DisposeWriter();
    }
    #endregion

    #region private
    //vars
    private MemoryStream m_output;
    private BinaryWriter m_binaryWriter;
    private Guid m_producerId;

    //methods
    protected override void OnMessageAdding(uint dataSetWriterId)
    {
      CreateUABinaryWriter(new uint[] { dataSetWriterId });
      base.OnMessageAdding(dataSetWriterId);
    }
    /// <summary>
    /// Sends the message.
    /// </summary>
    /// <param name="buffer">The buffer with the message content.</param>
    protected abstract void SendFrame(byte[] buffer);
    private void DisposeWriter()
    {
      if (m_binaryWriter == null)
        return;
      m_binaryWriter.Dispose();
      m_output.Close();
      m_binaryWriter = null;
      m_output = null;
    }
    private void CreateUABinaryWriter(IList<uint> m_dataSetWriterIds)
    {
      m_output = new MemoryStream();
      m_binaryWriter = new BinaryWriter(m_output);
      EncodePacketHeaders(m_producerId, m_dataSetWriterIds);
    }
    #endregion

  }
}
