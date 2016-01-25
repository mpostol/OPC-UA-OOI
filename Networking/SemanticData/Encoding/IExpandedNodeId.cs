namespace UAOOI.SemanticData.DataManagement.Encoding
{
  /// <summary>
  /// Interface ExpandedNodeId - extends a node id by adding a complete namespace URI.
  /// </summary>
  public interface IExpandedNodeId
  {
    /// <summary>
    /// Gets the node identifier.
    /// </summary>
    /// <value>The node identifier formatted as a URI.</value>
    string Identifier { get; }

  }
}
