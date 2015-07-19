
namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface INodeContainer - it represent container of Nodes
  /// </summary>
  public interface INodeContainer
  {
    /// <summary>
    /// Add a new node factory.
    /// </summary>
    /// <typeparam name="NodeFactory">The type of the node factory must inherit from <see cref="INodeFactory"/>.</typeparam>
    /// <returns>Returns new object of NodeFactory type.</returns>
    NodeFactory AddNodeFactory<NodeFactory>()
      where NodeFactory : INodeFactory;
  }
}
