
using System;
using Serialization = UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.DataDiscovery.DiscoveryServices.Models
{
  internal static class Converters
  {
    internal static Serialization.BuiltInType ToBuiltInType (this BuiltInType value)
    {
      return (Serialization.BuiltInType)Convert.ToUInt16(value);
    }
  }
}
