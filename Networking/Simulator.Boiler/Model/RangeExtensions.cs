
using System;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
{
  /// <summary>
  /// <see cref="Range"/> helper functions.
  /// </summary>
  internal static class RangeExtensions
  {
    /// <summary>
    /// Initializes the object with the high and low limits.
    /// </summary>
    internal static Range CreateRange(double high, double low)
    {
      Tuple<double, double> _value = CreateValue(high, low);
      Range _ret = new Range()
      {
        Low = _value.Item1,
        High = _value.Item2,
      };
      return _ret;
    }
    /// <summary>
    /// Returns the difference between high and low of the <see cref="Range"/>.
    /// </summary>
    public static double Magnitude(this Range value)
    {
      return Math.Abs(value.High - value.Low); 
    }
    private static Tuple<double, double> CreateValue(double high, double low)
    {
      return high > low ? Tuple.Create<double, double>(low, high) : Tuple.Create(high, low);
    }
  }
}
