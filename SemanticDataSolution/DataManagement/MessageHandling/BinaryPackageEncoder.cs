
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryPackageEncoder - OPC UA binary package encoder.
  /// </summary>
  public abstract class BinaryPackageEncoder : BinaryMessageEncoder
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPackageEncoder" /> class.
    /// </summary>
    /// <param name="uaEncoder">The ua encoder.</param>
    public BinaryPackageEncoder(IUAEncoder uaEncoder) : base(uaEncoder) { }
    /// <summary>
    /// Gets or sets the header of the package.
    /// </summary>
    /// <value>The header <see cref="PacketHeader"/>.</value>
    public PacketHeader Header { get; set; }

    #region BinaryMessageEncoder
    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    protected override void OnMessageAdding(uint dataSetWriterIds)
    {
    }
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
    protected void EncodePackageHeaders(Guid producerId, IList<UInt32> dataSetWriterIds)
    {
      Header = PacketHeader.GetProducerPackageHeader(this, producerId, dataSetWriterIds);
      Header.WritePacketHeader();
    }
    #endregion

  }
}
