
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportModelFactory
  {

    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    void CreateNamespace(string uri);
    NodeFactory NewExportNodeFFactory<NodeFactory>()
      where NodeFactory:IExportNodeFactory;

  }
}
