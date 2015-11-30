
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryPackageDecoder - OPC UA binary package decoder.
  /// </summary>
  public abstract class BinaryPackageDecoder : BinaryMessageDecoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPackageDecoder"/> class.
    /// </summary>
    public BinaryPackageDecoder(IUADecoder uaDecoder) : base(uaDecoder) { }
    #endregion

    #region API
    /// <summary>
    /// Gets or sets the header <see cref="PacketHeader"/> of the package. The header is retrieved from the message after arriving.
    /// </summary>
    /// <value>The header <see cref="PacketHeader"/>.</value>
    public PacketHeader Header { get; set; }
    #endregion

    #region private
    /// <summary>
    /// Called by the network handler to start analyzing new package by waking up all readers waiting for the messages by raising the event.
    /// </summary>
    protected void OnNewPackageArrived()
    {
      Header = PacketHeader.GetConsumerPackageHeader(this);
      for (int i = Header.MessageCount; i > 0; i--)
        OnNewMessageArrived();
    }
    #endregion

  }
}
