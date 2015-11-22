
using System.Collections.Generic;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class ValueRanks provides definition of constants defined for the ValueRank attribute.
  /// </summary>
  public static class ValueRanks
  {

    /// <summary>
    /// The variable may be a scalar or a one dimensional array.
    /// </summary>
    public const int ScalarOrOneDimension = -3;
    /// <summary>
    /// The variable may be a scalar or an array of any dimension.
    /// </summary>
    public const int Any = -2;
    /// <summary>
    /// The variable is always a scalar.
    /// </summary>
    public const int Scalar = -1;
    /// <summary>
    /// The variable is always an array with one or more dimensions.
    /// </summary>
    public const int OneOrMoreDimensions = 0;
    /// <summary>
    /// The variable is always one dimensional array.
    /// </summary>
    public const int OneDimension = 1;
    /// <summary>
    /// The variable is always an array with two or more dimensions.
    /// </summary>
    public const int TwoDimensions = 2;
    /// <summary>
    /// Checks if the actual value rank is compatible with the expected value rank.
    /// </summary>
    /// <param name="actualValueRank">The actual value rank.</param>
    /// <param name="expectedValueRank">The expected value rank.</param>
    /// <returns><c>true</c> if the specified actual value rank is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValid(int actualValueRank, int expectedValueRank)
    {
      if (actualValueRank == expectedValueRank)
        return true;
      switch (expectedValueRank)
      {
        case Any:
          return true;
        case OneOrMoreDimensions:
          if (actualValueRank < 0)
            return false;
          break;
        case ScalarOrOneDimension:
          if (actualValueRank != Scalar && actualValueRank != OneDimension)
            return false;
          break;
        default:
          return false;
      }
      return true;
    }
    /// <summary>
    /// Checks if the actual array dimensions is compatible with the expected value rank and array dimensions.
    /// </summary>
    /// <param name="actualArrayDimensions">The actual array dimensions.</param>
    /// <param name="valueRank">The value rank.</param>
    /// <param name="expectedArrayDimensions">The expected array dimensions.</param>
    /// <returns><c>true</c> if the specified actual array dimensions is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValid(IList<uint> actualArrayDimensions, int valueRank, IList<uint> expectedArrayDimensions)
    {
      // check if parameter omitted.
      if (actualArrayDimensions == null || actualArrayDimensions.Count == 0)
        return expectedArrayDimensions == null || expectedArrayDimensions.Count == 0;
      // no array dimensions allowed for scalars.
      if (valueRank == ValueRanks.Scalar)
        return false;
      // check if one dimension required.
      if ((valueRank == ValueRanks.OneDimension || valueRank == ValueRanks.ScalarOrOneDimension) && (actualArrayDimensions.Count != 1))
        return false;
      // check number of dimensions.
      if ((valueRank != ValueRanks.OneOrMoreDimensions) && (actualArrayDimensions.Count != valueRank))
        return false;
      // nothing more to do if expected dimensions omitted.
      if (expectedArrayDimensions == null || expectedArrayDimensions.Count == 0)
        return true;
      // check dimensions.
      if (expectedArrayDimensions.Count != actualArrayDimensions.Count)
        return false;
      // check length of each dimension.
      for (int ii = 0; ii < expectedArrayDimensions.Count; ii++)
        if (expectedArrayDimensions[ii] != actualArrayDimensions[ii] && expectedArrayDimensions[ii] != 0)
          return false;
      // everything ok.
      return true;
    }

  }

}
