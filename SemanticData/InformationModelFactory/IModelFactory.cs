
namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Interface IModelFactory defines a generalized implementation of the specific representation of th OPC UA Information Model.
  /// </summary>
  public interface IModelFactory : INodeContainer
  {

    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    void CreateNamespace(string uri);

  }

}
