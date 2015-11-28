using System;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  internal static class CommonDefinitions
  {
    /// <summary>
    /// The time base DateTime to calculate ticks sent over wire for UA binary representation.
    /// </summary>
    public static readonly DateTime TimeBase = new DateTime(1601, 1, 1); //
    public static readonly DateTime TimeBaseMaxValue = new DateTime(9999, 12, 31, 23, 59, 59); //
    /// <summary>
    /// Decode the UA date and time form ticks.
    /// </summary>
    /// <param name="ticks">The ticks as defined in <see cref="DateTime"/>.</param>
    /// <returns>Decoded from the stream <see cref="DateTime"/>.</returns>
    internal static DateTime GetUADateTime(this Int64 ticks)
    {
      if (ticks == Int64.MaxValue)
        return TimeBaseMaxValue;
      if (ticks >= (Int64.MaxValue - TimeBase.Ticks))
        return TimeBaseMaxValue;
      ticks += TimeBase.Ticks;
      if (ticks >= DateTime.MaxValue.Ticks)
        return DateTime.MaxValue;
      if (ticks < TimeBase.Ticks)
        return DateTime.MinValue;
      return new DateTime(ticks, DateTimeKind.Utc);
    }
    /// <summary>
    /// Encode the UA <see cref="DateTime"/> as ticks is relation to <see cref="TimeBase"/>.
    /// </summary>
    /// <param name="value">The value to be encoded.</param>
    /// <returns>Returns ticks as defined in <see cref="DateTime"/>.</returns>
    internal static Int64 GetUADataTimeTicks(this DateTime value)
    {
      if (value.Kind == DateTimeKind.Local)
        value = value.ToUniversalTime();
      long _ticks = value.Ticks;
      if (_ticks >= TimeBaseMaxValue.Ticks)
        _ticks = Int64.MaxValue;
      else
      {
        _ticks -= TimeBase.Ticks;
        if (_ticks <= 0)
          _ticks = 0;
      }
      return _ticks;
    }

  }

}
