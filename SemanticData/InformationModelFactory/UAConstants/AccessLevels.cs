//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.InformationModelFactory.UAConstants
{
  /// <summary>
  /// Flags that can be set for the AccessLevel attribute.
  /// </summary>
  /// <remarks>
  /// Flags that can be set for the AccessLevel attribute.
  /// </remarks>
  public static class AccessLevels
  {
    /// <summary>
    /// The Variable value cannot be accessed and has no event history.
    /// </summary>
    public const byte None = 0x0;
    /// <summary>
    /// The current value of the Variable may be read.
    /// </summary>
    public const byte CurrentRead = 0x1;
    /// <summary>
    /// The current value of the Variable may be written.
    /// </summary>
    public const byte CurrentWrite = 0x2;
    /// <summary>
    /// The current value of the Variable may be read or written.
    /// </summary>
    public const byte CurrentReadOrWrite = 0x3;
    /// <summary>
    /// The history for the Variable may be read.
    /// </summary>
    public const byte HistoryRead = 0x4;
    /// <summary>
    /// The history for the Variable may be updated.
    /// </summary>
    public const byte HistoryWrite = 0x8;
    /// <summary>
    /// The history value of the Variable may be read or updated.
    /// </summary>
    public const byte HistoryReadOrWrite = 0xC;
    /// <summary>
    /// Indicates if the Variable generates SemanticChangeEvents when its value changes.
    /// </summary>
    public const byte SemanticChange = 0x10;
  }

}
