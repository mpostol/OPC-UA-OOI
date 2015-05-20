
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IReferenceTypeFactory : ITypeFactory
  {
    
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
