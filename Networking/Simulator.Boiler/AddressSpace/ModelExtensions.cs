
using System;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  /// <summary>
  /// <see cref="Range"/> helper functions.
  /// </summary>
  internal static class ModelExtensions
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
    internal static double Magnitude(this Range value)
    {
      return Math.Abs(value.High - value.Low);
    }
    private static Tuple<double, double> CreateValue(double high, double low)
    {
      return high > low ? Tuple.Create<double, double>(low, high) : Tuple.Create(high, low);
    }
    internal static UATypeInfo GetUATypeInfo(this Type code)
    {
      switch (Type.GetTypeCode(code))
      {
        case TypeCode.Boolean:
          return new UATypeInfo(BuiltInType.Boolean);
        case TypeCode.SByte:
          return new UATypeInfo(BuiltInType.SByte);
        case TypeCode.Byte:
          return new UATypeInfo(BuiltInType.Byte);
        case TypeCode.Int16:
          return new UATypeInfo(BuiltInType.Int16);
        case TypeCode.UInt16:
          return new UATypeInfo(BuiltInType.UInt16);
        case TypeCode.Int32:
          return new UATypeInfo(BuiltInType.Int32);
        case TypeCode.UInt32:
          return new UATypeInfo(BuiltInType.UInt32);
        case TypeCode.Int64:
          return new UATypeInfo(BuiltInType.Int64);
        case TypeCode.UInt64:
          return new UATypeInfo(BuiltInType.UInt64);
        case TypeCode.Single:
          return new UATypeInfo(BuiltInType.Float);
        case TypeCode.Double:
          return new UATypeInfo(BuiltInType.Double);
        case TypeCode.DateTime:
          return new UATypeInfo(BuiltInType.DateTime);
        case TypeCode.String:
          return new UATypeInfo(BuiltInType.String);
        default:
          return new UATypeInfo( BuiltInType.Null);
          //throw new ArgumentOutOfRangeException(nameof(code), $"Cannot convert system type {code} to {nameof(UATypeInfo)}");
      }
    }

  }
}
