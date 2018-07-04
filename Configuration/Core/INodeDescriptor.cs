
using System.Xml;

namespace UAOOI.Configuration.Core
{
  /// <summary>
  /// Provides description of the node to be configured.
  /// </summary>
  public interface INodeDescriptor
  {

    /// <summary>
    /// Gets the node unique identifier, i.e. the browse path.
    /// </summary>
    /// <value>The node identifier.</value>
    XmlQualifiedName NodeIdentifier { get; }

    /// <summary>
    /// Gets the type of the node of of the Variable NodeClass
    /// </summary>
    /// <value>The type of the data.</value>
    XmlQualifiedName DataType { get; }

    /// <summary>
    /// Gets the node class.
    /// </summary>
    /// <value>The node class.</value>
    InstanceNodeClassesEnum NodeClass { get; }

    /// <summary>
    /// Gets a value indicating whether it is instance declaration - may have many instances in the created address space.
    /// </summary>
    /// <value><c>true</c> if the node is instance declaration; otherwise, <c>false</c>.</value>
    bool InstanceDeclaration { get; }

    /// <summary>
    /// Gets the binding description that allows the editor to create automatically bindings.
    /// </summary>
    /// <value>The binding description.</value>
    string BindingDescription { get; }

  }

}
