
namespace UAOOI.Networking.SemanticData.Encoding
{
  /// <summary>
  /// Class DiagnosticInfo - A <see cref="IDiagnosticInfo"/> structure is described in Part 4. 
  /// </summary>
  public interface IDiagnosticInfo
  {

    /// <summary>
    /// The index of the symbolic id in the string table.
    /// </summary>
    /// <remarks>
    /// The index of the symbolic id in the string table.
    /// </remarks>
    int? SymbolicId { get; }
    /// <summary>
    /// The index of the namespace uri in the string table.
    /// </summary>
    /// <remarks>
    /// The index of the namespace uri in the string table.
    /// </remarks>
    int? NamespaceUri { get; }
    /// <summary>
    /// The index of the locale associated with the localized text.
    /// </summary>
    int? Locale { get; }
    /// <summary>
    /// The index of the localized text in the string table.
    /// </summary>
    int? LocalizedText { get; }
    /// <summary>
    /// The additional debugging or trace information.
    /// </summary>
    /// <remarks>
    /// The additional debugging or trace information.
    /// </remarks>
    string AdditionalInfo { get; }
    /// <summary>
    /// The status code returned from an underlying system.
    /// </summary>
    /// <remarks>
    /// The status code returned from an underlying system.
    /// </remarks>
    IStatusCode InnerStatusCode { get; }
    /// <summary>
    /// The diagnostic info returned from a underlying system.
    /// </summary>
    /// <remarks>
    /// The diagnostic info returned from a underlying system.
    /// </remarks>
    IDiagnosticInfo InnerDiagnosticInfo { get; }

  }
}
