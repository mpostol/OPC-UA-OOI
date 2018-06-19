using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IReferenceFactory - represents nodes reference instance.
  /// </summary>
  public interface IReferenceFactory
  {

    /// <summary>
    /// Sets the type of the reference.
    /// </summary>
    /// <value>The type of the reference.</value>
    XmlQualifiedName ReferenceType
    {
      set;
    }
    /// <summary>
    /// Sets the target identifier.
    /// </summary>
    /// <value>The target identifier.</value>
    XmlQualifiedName TargetId
    {
      set;
    }

    /// <summary>
    /// Sets a value indicating whether this instance is inverse.
    /// </summary>
    /// <value><c>true</c> if this instance is inverse; otherwise, <c>false</c>.</value>
    [System.ComponentModel.DefaultValueAttribute(false)]
    bool IsInverse
    {
      set;
    }

  }
}
