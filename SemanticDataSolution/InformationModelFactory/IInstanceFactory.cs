
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IInstanceFactory - It encapsulates definition of an instance.
  /// </summary>
  public interface IInstanceFactory : INodeFactory
  {

    /// <summary>
    /// Sets the modeling rule.
    /// </summary>
    /// <value>The modeling rule.</value>
    ModelingRules? ModelingRule
    {
      set;
    }
    /// <summary>
    /// Sets the type definition.
    /// </summary>
    /// <value>The type definition.</value>
    XmlQualifiedName TypeDefinition
    {
      set;
    }
    /// <summary>
    /// Sets the type of the reference.
    /// </summary>
    /// <value>The type of the reference.</value>
    XmlQualifiedName ReferenceType { set; }

  }
}
