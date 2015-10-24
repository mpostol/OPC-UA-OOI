
using System;

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
    public BinaryPackageDecoder()
    {
      Header = PackageHeader.GetConsumerPackageHeader(this);
    }
    #endregion

    /// <summary>
    /// Gets or sets the header of the package.
    /// </summary>
    /// <value>The header <see cref="PackageHeader"/>.</value>
    public PackageHeader Header { get; set; }

    #region private

    /// <summary>
    /// Called by the network handler and start Analyzing new package by awaking all readers waiting for the messages by raising the event.
    /// </summary>
    protected void OnNewPackageArrived()
    {
      Header.Synchronize();
      for (int i = Header.MessageCount; i > 0; i--)
        RaiseReadMessageCompleted();
    }
    #endregion

  }
}
