using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class BaseInstanceState: NodeState
  {

    /// <summary>
    /// Initializes the instance with its default attribute values.
    /// </summary>
    protected BaseInstanceState(NodeClass nodeClass, NodeState parent) : base(nodeClass)
    {
      Parent = parent;
    }
    /// <summary>
    /// The parent node.
    /// </summary>
    public NodeState Parent { get; internal set; }
    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    /// <param name="namespaceUris">The namespace uris.</param>
    /// <returns></returns>
    protected virtual NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return null;
    }
  }
}