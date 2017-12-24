
using System;
using System.Net;

namespace UAOOI.Networking.UDPMessageHandler.Configuration
{
  internal static class IPAddressValidationRule
  {

    internal static IPAddress ValidateIP (string value)
    {
      if (String.IsNullOrEmpty(value))
        throw new ArgumentNullException($"The string representing IP Address cannot be empty");
      IPAddress _address;
      if (!IPAddress.TryParse(value, out _address))
        throw new ArgumentOutOfRangeException($"The string <{value}> doesn't represent IP Address");
      if (_address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
        throw new ArgumentOutOfRangeException($"The address family {_address.AddressFamily} is not supported");
      return _address;
    }
    internal static void ValidateMulticast(IPAddress address)
    {
      try
      {
        byte[] _bytesArray = address.GetAddressBytes();
        if (_bytesArray.Length != 4)
          throw new ArgumentOutOfRangeException($"The address length {_bytesArray.Length} is not expected");
        if (_bytesArray[0] > 239 || _bytesArray[0] < 224)
          throw new ArgumentOutOfRangeException($"The address {address} is outside the IP V4 multicast range 224.0.0.0 - 239.255.255.255");
      }
      catch (Exception _ex)
      {
        throw new ArgumentOutOfRangeException($"Validation error: {_ex.GetType().Name}: {_ex.Message}");
      }
    }

  }
}
