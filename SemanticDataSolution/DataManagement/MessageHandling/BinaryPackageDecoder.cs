namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryPackageDecoder - OPC UA binary package decoder.
  /// </summary>
  public abstract class BinaryPackageDecoder : BinaryMessageDecoder
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPackageDecoder"/> class.
    /// </summary>
    public BinaryPackageDecoder()
    {
      Header = PackageHeader.GetConsumerPackageHeader(this);
    }

    /// <summary>
    /// Gets or sets the header of the package.
    /// </summary>
    /// <value>The header <see cref="PackageHeader"/>.</value>
    public PackageHeader Header { get; set; }

    #region private
    /// <summary>
    /// Reads the package headers.
    /// </summary>
    protected void ReadPackageHeaders()
    {
      Header.Synchronize();
    }
    #endregion

  }
}
