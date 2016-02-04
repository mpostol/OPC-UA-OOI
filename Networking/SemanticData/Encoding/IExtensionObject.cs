
using System.Xml;

namespace UAOOI.Networking.SemanticData.Encoding
{
  /// <summary>
  /// Interface ExtensionObject - An object used to wrap data types that the receiver may not understand.
  /// </summary>
  public interface IExtensionObject
  {

    /// <summary>
    /// Gets the type identifier.
    /// </summary>
    /// <value>The type identifier.</value>
    IExpandedNodeId TypeId { get; }
    /// <summary>
    /// Gets the body object embedded in the extension object.
    /// </summary>
    /// <value>The body of the <see cref="IExtensionObject"/>.</value>
    XmlElement Body { get; }

  }
}
