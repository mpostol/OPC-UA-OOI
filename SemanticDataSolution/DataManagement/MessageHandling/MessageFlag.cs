
using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Enum MessageFlag - the message control bits.
  /// </summary>
  /// <remarks>
  /// #98: PackageHeader - must be refined
  /// Because the specification is subject of further development this class must be refined according to further protocol modification.
  /// </remarks>
  [Flags]
  public enum MessageFlag
  {
    /// <summary>
    /// The metadata - not implemented 
    /// </summary>
    Metadata = 0x0,
    /// <summary>
    /// The periodic data
    /// </summary>
    PeriodicData = 0x1,
  }
}
