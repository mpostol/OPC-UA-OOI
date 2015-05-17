
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public delegate void LocalizedTextFactory(string localeField, string valueField);
  public interface IExportModelFactory : IExportNodeContainer
  {

    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    void CreateNamespace(string uri);

  }

}
