using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// Encodes the headers.
    /// </summary>
    protected override void EncodePackageHeaders()
    {
      Header.Synchronize();
    }
    #endregion

  }
}
