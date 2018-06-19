
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface ITypeFactory - instances implementing this interface supports type definition factoring. 
  /// </summary>
  public interface ITypeFactory : INodeFactory
  {

    /// <summary>
    /// Sets the base type of the node.
    /// </summary>
    /// <value>The base type represented by the <see cref="XmlQualifiedName"/>.</value>
    XmlQualifiedName BaseType { set; }
    /// <summary>
    /// Sets a value indicating whether this instance is abstract.
    /// </summary>
    /// <remarks>Default Value is false</remarks>
    /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
    bool IsAbstract{ set; }

  }
}
