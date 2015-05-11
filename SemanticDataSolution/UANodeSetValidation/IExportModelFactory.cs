
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportModelFactory : IExportNodeContainer
  {

    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    void CreateNamespace(string uri);

  }

}
