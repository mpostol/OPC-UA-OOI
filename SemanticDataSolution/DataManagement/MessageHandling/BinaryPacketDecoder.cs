
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryPacketDecoder - OPC UA binary packet decoder.
  /// </summary>
  public abstract class BinaryPacketDecoder : BinaryMessageDecoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPacketDecoder" /> class.
    /// </summary>
    /// <param name="uaDecoder">The UA decoder.</param>
    public BinaryPacketDecoder(IUADecoder uaDecoder) : base(uaDecoder) { }
    #endregion

    #region API
    /// <summary>
    /// Gets or sets the header <see cref="PacketHeader"/> of the packet. The header is retrieved from the message after arriving.
    /// </summary>
    /// <value>The header <see cref="PacketHeader"/>.</value>
    public PacketHeader Header { get; set; }
    #endregion

    #region private
    /// <summary>
    /// Called by the network handler to start analyzing new packet by waking up all readers waiting for the messages by raising the event.
    /// </summary>
    protected void OnNewPacketArrived()
    {
      Header = PacketHeader.GetConsumerPacketHeader(this);
      for (int i = 0; i < Header.MessageCount; i++)
        OnNewMessageArrived(Header.DataSetWriterIds[i]);
    }
    #endregion

  }
}
