
using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class CommonDefinition and extension functions.
  /// </summary>
  public static class CommonDefinitions
  {

    #region definitions needing further decisions.
    /// <summary>
    /// The protocol version used in the package header. move it to configuration.
    /// </summary>
    public static readonly byte ProtocolVersion = 110;
    #endregion
    internal static uint ToUInt32(Guid value)
    {
      int _intValue = value.ToString().GetHashCode();
      return _intValue < 0 ? Convert.ToUInt32(UInt32.MaxValue + _intValue) : Convert.ToUInt32(_intValue);
    }

  }
}
