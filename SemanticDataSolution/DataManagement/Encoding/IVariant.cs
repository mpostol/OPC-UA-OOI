
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.Encoding
{
  /// <summary>
  /// Interface IVariant - if implemented A structure that could contain value with any of the UA built-in data types.
  /// </summary>
  public interface IVariant
  {

    /// <summary>
    /// Gets the value stored in the object.
    /// </summary>
    /// <value>The value stored in the object.</value>
    object Value { get; }
    /// <summary>
    /// Gets the type information about the type of stored in the object.
    /// </summary>
    /// <value>The <see cref="UATypeInfo"/> capturing information about the type of stored in the object.</value>
    UATypeInfo UATypeInfo { get; }

  }

}
