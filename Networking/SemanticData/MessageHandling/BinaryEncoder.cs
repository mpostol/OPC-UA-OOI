//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.IO;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class BinaryEncoder - wrapper of <see cref="BinaryWriter"/> supporting OPC UA binary encoding.
  /// </summary>
  public sealed class BinaryEncoder : BinaryPacketEncoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryEncoder" /> class wrapper of <see cref="BinaryWriter" /> supporting OPC UA binary encoding..
    /// </summary>
    /// <param name="uaEncoder">The ua encoder.</param>
    /// <param name="lengthFieldType">Type of the length field.</param>
    public BinaryEncoder(IBinaryDataTransferGraphSender messageWriter, IUAEncoder uaEncoder, MessageLengthFieldTypeEnum lengthFieldType) : base(uaEncoder, lengthFieldType)
    {
      m_IBinaryDTGSender = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
    }
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
    /// <returns>The position with the current stream as <see cref="long"/>.</returns>
    public override long Seek(int offset, SeekOrigin origin)
    {
      return m_binaryWriter.Seek(offset, origin);
    }
    #endregion

    public override void AttachToNetwork()
    {
      m_IBinaryDTGSender.AttachToNetwork();
    }
    public override IAssociationState State { get => m_IBinaryDTGSender.State; set => m_IBinaryDTGSender.State = value; }
    #endregion

    #region private
    //vars
    private MemoryStream m_output;
    private BinaryWriter m_binaryWriter;
    private IBinaryDataTransferGraphSender m_IBinaryDTGSender;
    //methods
    /// <summary>
    /// Begins sending the frame.
    /// </summary>
    protected override void SendFrame()
    {
      m_binaryWriter.Flush();
      m_IBinaryDTGSender.SendFrame(m_output.ToArray());
      DisposeWriter();
    }
    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    /// <param name="producerId">The producer identifier.</param>
    /// <param name="dataSetWriterId">The data set writer identifier - must be unique in context of <paramref name="producerId" />.</param>
    protected override void OnMessageAdding(Guid producerId, ushort dataSetWriterId)
    {
      CreateUABinaryWriter(producerId, new ushort[] { dataSetWriterId });
      base.OnMessageAdding(producerId, dataSetWriterId);
    }
    private void DisposeWriter()
    {
      if (m_binaryWriter == null)
        return;
      m_binaryWriter.Dispose();
      m_output.Close();
      m_binaryWriter = null;
      m_output = null;
    }
    private void CreateUABinaryWriter(Guid producerId, IList<ushort> dataSetWriterIds)
    {
      m_output = new MemoryStream();
      m_binaryWriter = new BinaryWriter(m_output);
      EncodePacketHeaders(producerId, dataSetWriterIds);
    }
    #endregion

  }
}
