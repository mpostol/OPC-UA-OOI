
namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface INodeContainer - it represent container of Nodes.
  /// </summary>
  public interface INodeContainer
  {
    /// <summary>
    /// Creates and adds a new node instance of the <see cref="INodeFactory"/>.
    /// </summary>
    /// <typeparam name="NodeFactory">The type of the node factory must inherit from <see cref="INodeFactory"/>.</typeparam>
    /// <returns>Returns new object implementing <see cref="INodeFactory"/>.</returns>
    NodeFactory AddNodeFactory<NodeFactory>()
      where NodeFactory : INodeFactory;
  }
}
