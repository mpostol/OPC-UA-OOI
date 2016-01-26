
using System;
using System.Collections.Generic;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class BinaryPacketEncoder - OPC UA binary packet encoder.
  /// </summary>
  public abstract class BinaryPacketEncoder : BinaryMessageEncoder
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPacketEncoder" /> class.
    /// </summary>
    /// <param name="uaEncoder">The ua encoder.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="lengthFieldType">Type of the length field in the the message header.</param>
    public BinaryPacketEncoder(IUAEncoder uaEncoder, MessageLengthFieldTypeEnum lengthFieldType) : base(uaEncoder, lengthFieldType) { }
    /// <summary>
    /// Gets or sets the header of the packet.
    /// </summary>
    /// <value>The header <see cref="PacketHeader"/>.</value>
    public PacketHeader Header { get; set; }

    #region BinaryMessageEncoder
    /// <summary>
    /// Called when new message is adding to the packet payload.
    /// </summary>
    protected override void OnMessageAdding(UInt16 dataSetWriterIds) { }
    /// <summary>
    /// Called when the current message has been added and is ready to be sent out.
    /// </summary>
    protected override void OnMessageAdded()
    {
      SendFrame();
    }
    #endregion

    #region private
    //vars
    /// <summary>
    /// Begins sending the frame.
    /// </summary>
    protected abstract void SendFrame();
    //methods
    /// <summary>
    /// Encodes the headers.
    /// </summary>
    protected void EncodePacketHeaders(Guid producerId, IList<UInt16> dataSetWriterIds)
    {
      Header = PacketHeader.GetProducerPacketHeader(this, producerId, dataSetWriterIds);
      Header.WritePacketHeader();
    }
    #endregion

  }
}
