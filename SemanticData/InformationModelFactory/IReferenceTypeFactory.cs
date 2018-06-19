
namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IReferenceTypeFactory - encapsulates a reference type definition.
  /// </summary>
  public interface IReferenceTypeFactory : ITypeFactory
  {

    /// <summary>
    /// Adds a new inverse name.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    void AddInverseName(string localeField, string valueField);
    /// <summary>
    /// Sets a value indicating whether this <see cref="IReferenceTypeFactory"/> is symmetric.
    /// </summary>
    /// <remarks>Default Value is <b>false</b></remarks>
    /// <value><c>true</c> if symmetric; otherwise, <c>false</c>.</value>
    bool Symmetric
    {
      set;
    }
  }
}
