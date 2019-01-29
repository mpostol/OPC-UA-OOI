//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System;
using System.IO;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class BinaryDecoder - wrapper of <see cref="BinaryReader"/> supporting OPC UA binary encoding.
  /// </summary>
  public sealed class BinaryDecoder : BinaryPacketDecoder
  {
    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPacketDecoder" /> class is to be used by the packet level decoding.
    /// </summary>
    /// <param name="uaDecoder">The UA decoder to be used fo decode UA Built-in data types.</param>
    public BinaryDecoder(IBinaryDataTransferGraphReceiver messageReader, IUADecoder uaDecoder) : base(uaDecoder)
    {
      m_DTGReceiver = messageReader ?? throw new ArgumentNullException(nameof(messageReader));
      m_DTGReceiver.OnNewFrameArrived += OnNewFrameArrived;
    }
    #endregion

    #region IDisposable Support
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      BinaryReader _lc = m_UABinaryReader;
      if (_lc != null)
        _lc.Close();
      m_UABinaryReader = null;
      m_DTGReceiver.Dispose();
    }
    #endregion

    #region BinaryPacketDecoder
    /// <summary>
    /// Reads an 8-byte unsigned integer from the message and advances the position by eight bytes.
    /// </summary>
    /// <returns>An 8-byte unsigned integer <see cref="ulong"/> read from this message. .</returns>
    public override ulong ReadUInt64()
    {
      return m_UABinaryReader.ReadUInt64();
    }
    public override uint ReadUInt32()
    {
      return m_UABinaryReader.ReadUInt32();
    }
    public override ushort ReadUInt16()
    {
      return m_UABinaryReader.ReadUInt16();
    }
    public override string ReadString()
    {
      return m_UABinaryReader.ReadString();
    }
    public override float ReadSingle()
    {
      return m_UABinaryReader.ReadSingle();
    }
    public override sbyte ReadSByte()
    {
      return m_UABinaryReader.ReadSByte();
    }
    public override long ReadInt64()
    {
      return m_UABinaryReader.ReadInt64();
    }
    public override int ReadInt32()
    {
      return m_UABinaryReader.ReadInt32();
    }
    public override short ReadInt16()
    {
      return m_UABinaryReader.ReadInt16();
    }
    public override double ReadDouble()
    {
      return m_UABinaryReader.ReadDouble();
    }
    public override char ReadChar()
    {
      return m_UABinaryReader.ReadChar();
    }
    public override byte ReadByte()
    {
      return m_UABinaryReader.ReadByte();
    }
    public override bool ReadBoolean()
    {
      return m_UABinaryReader.ReadBoolean();
    }
    public override byte[] ReadBytes(int count)
    {
      return m_UABinaryReader.ReadBytes(count);
    }
    protected override bool EndOfMessage()
    {
      return m_UABinaryReader.BaseStream.Position == m_UABinaryReader.BaseStream.Length;
    }
    public override void AttachToNetwork()
    {
      m_DTGReceiver.AttachToNetwork();
    }
    public override IAssociationState State { get => m_DTGReceiver.State; set => m_DTGReceiver.State = value; }
    #endregion

    #region private
    private BinaryReader m_UABinaryReader;
    private readonly IBinaryDataTransferGraphReceiver m_DTGReceiver;
    /// <summary>
    /// Called when new frame has arrived.
    /// </summary>
    /// <param name="uaBinaryReader">
    /// The UA binary reader is an instance of <see cref="BinaryReader"/> created after new frame has been arrived.
    /// </param>
    /// <remarks>Just after processing the object is disposed.</remarks>
    private void OnNewFrameArrived(object source, byte[] _receiveBytes)
    {
      MemoryStream _stream = new MemoryStream(_receiveBytes, 0, _receiveBytes.Length);
      m_UABinaryReader = new BinaryReader(_stream, System.Text.Encoding.UTF8);
      OnNewPacketArrived();
      m_UABinaryReader.Dispose();
      m_UABinaryReader = null; ;
    }
    #endregion


  }

}
