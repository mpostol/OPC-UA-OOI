using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryPackageEncoder - OPC UA binary package encoder.
  /// </summary>
  public abstract class BinaryPackageEncoder : BinaryMessageEncoder
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPackageEncoder"/> class.
    /// </summary>
    public BinaryPackageEncoder()
    {
      Header = PackageHeader.GetProducerPackageHeader(this);
    }
    /// <summary>
    /// Gets or sets the header of the package.
    /// </summary>
    /// <value>The header <see cref="PackageHeader"/>.</value>
    public PackageHeader Header { get; set; }

    #region BinaryMessageEncoder
    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    protected override void OnMessageAdding()
    {
      m_NumberOfSentMessages++;
    }
    /// <summary>
    /// Called when the current message has been added and is ready to be sent out.
    /// </summary>
    protected override void OnMessageAdded()
    {
      this.Header.MessageCount = Convert.ToByte(m_NumberOfSentMessages);
      SendFrame();
    }
    #endregion

    #region private
    //vars
    /// <summary>
    /// Begins sending the frame.
    /// </summary>
    protected abstract void SendFrame();
    private int m_NumberOfSentMessages = 0;
    //methods
    /// <summary>
    /// Encodes the headers.
    /// </summary>
    protected void EncodePackageHeaders()
    {
      m_NumberOfSentMessages = 0;
      Header.Synchronize();
    }
    #endregion

  }
}
