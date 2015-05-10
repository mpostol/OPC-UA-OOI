using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportReferenceTypeFactory : IExportTypeFactory
  {
    LocalizedText[] InverseName
    {
      set;
    }
    /// <summary>
    /// Sets a value indicating whether this <see cref="IExportReferenceTypeFactory"/> is symmetric.
    /// </summary>
    /// <remarks>Default Value is <b>false</b></remarks>
    /// <value><c>true</c> if symmetric; otherwise, <c>false</c>.</value>
    bool Symmetric
    {
      set;
    }
  }
}
