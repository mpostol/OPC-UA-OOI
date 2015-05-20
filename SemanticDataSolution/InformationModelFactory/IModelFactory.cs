
namespace UAOOI.SemanticData.InformationModelFactory
{
  public delegate void LocalizedTextFactory(string localeField, string valueField);
  
  public interface IModelFactory : INodeContainer
  {

    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    void CreateNamespace(string uri);

  }

}
