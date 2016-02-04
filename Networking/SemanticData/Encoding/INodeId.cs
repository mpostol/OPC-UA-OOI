namespace UAOOI.Networking.SemanticData.Encoding
{

  /// <summary>
  /// Interface NodeId - if implemented Stores an identifier for a node in a server's address space.
  /// </summary>
  public interface INodeId
  {
    /// <summary>
    /// The node identifier formatted as a URI.
    /// </summary>
    /// <remarks>
    /// The node identifier formatted as a URI.
    /// </remarks>
    string Identifier { get; }

  }
}
