
namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Delegate LocalizedTextFactory - encapsualtes a method that must be used to create <see cref="LocalizedTextFactory"/>
  /// </summary>
  /// <param name="localeField">The locale field.</param>
  /// <param name="valueField">The value field.</param>
  public delegate void LocalizedTextFactory(string localeField, string valueField);

  /// <summary>
  /// Interface IModelFactory defines a generalized implementation specyfic specific representation of th OPC UA Information Model.
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
