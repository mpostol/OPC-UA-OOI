
using System;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.Encoding
{
  /// <summary>
  /// Interface IMatrix - if implemented wraps a multi-dimensional array for use within the <see cref="IVariant"/> .
  /// </summary>
  public interface IMatrix
  {

    /// <summary>
    /// Gets the elements of the matrix.
    /// </summary>
    /// <value>The elements of the matrix.</value>
    Array Elements { get; }
    /// <summary>
    /// Gets the dimensions of the matrix.
    /// </summary>
    /// <value>The dimensions of the matrix.</value>
    int[] Dimensions { get; }
    /// <summary>
    /// Gets the type information.
    /// </summary>
    /// <value>The type information.</value>
    UATypeInfo TypeInfo { get; }

  }
}
