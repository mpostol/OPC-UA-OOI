
using System;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  internal static class CommonDefinitions
  {
    
    public static readonly DateTime TimeBase = new DateTime(1601, 1, 1); //base DateTime to calculate ticks sent over wire.
    public static DateTime GetUADateTime(Int64 ticks)
    {
      if (ticks >= (Int64.MaxValue - CommonDefinitions.TimeBase.Ticks))
        return DateTime.MaxValue;
      ticks += TimeBase.Ticks;
      if (ticks >= DateTime.MaxValue.Ticks)
        return DateTime.MaxValue;
      if (ticks <= CommonDefinitions.TimeBase.Ticks)
        return DateTime.MinValue;
      return new DateTime(ticks, DateTimeKind.Utc);
    }
    public static Int64 GetUADataTimeTicks(DateTime value)
    {
      long _ticks = value.Ticks;
      if (_ticks >= DateTime.MaxValue.Ticks)
        _ticks = Int64.MaxValue;
      else
      {
        _ticks -= CommonDefinitions.TimeBase.Ticks;
        if (_ticks <= 0)
          _ticks = 0;
      }
      return _ticks;
    }

  }
}
