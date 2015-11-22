namespace UAOOI.SemanticData.DataManagement.Encoding
{
  /// <summary>
  /// Interface QualifiedName - if implemented represents a name qualified with a namespace.
  /// </summary>
  public interface IQualifiedName
  {

    /// <summary>
    /// Gets the index of the namespace.
    /// </summary>
    /// <value>The index of the namespace that qualifies the name.</value>
    ushort? NamespaceIndex { get; }
    /// <summary>
    /// Gets the unqualified name.
    /// </summary>
    /// <value>The unqualified name.</value>
    string Name { get; }

  }
}
