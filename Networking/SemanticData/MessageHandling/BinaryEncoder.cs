
using System;
using System.Collections.Generic;
using System.IO;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class BinaryEncoder - wrapper of <see cref="BinaryWriter"/> supporting OPC UA binary encoding.
  /// </summary>
  public abstract class BinaryEncoder : BinaryPacketEncoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryEncoder" /> class wrapper of <see cref="BinaryWriter" /> supporting OPC UA binary encoding..
    /// </summary>
    /// <param name="uaEncoder">The ua encoder.</param>
    /// <param name="lengthFieldType">Type of the length field.</param>
    public BinaryEncoder(IUAEncoder uaEncoder, MessageLengthFieldTypeEnum lengthFieldType) : base(uaEncoder, lengthFieldType) { }
    #endregion

    #region IDisposable
    // Protected implementation of Dispose pattern.
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
        DisposeWriter();
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

    //methods
    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    /// <param name="producerId">The producer identifier.</param>
    /// <param name="dataSetWriterId">The data set writer identifier - must be unique in context of <paramref name="producerId" />.</param>
    protected override void OnMessageAdding(Guid producerId, ushort dataSetWriterId)
    {
      CreateUABinaryWriter(producerId, new UInt16[] { dataSetWriterId });
      base.OnMessageAdding(producerId, dataSetWriterId);
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
    private void CreateUABinaryWriter(Guid producerId, IList<UInt16> dataSetWriterIds)
    {
      m_output = new MemoryStream();
      m_binaryWriter = new BinaryWriter(m_output);
      EncodePacketHeaders(producerId, dataSetWriterIds);
    }
    #endregion

  }
}
