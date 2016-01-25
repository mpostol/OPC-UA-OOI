using System;
using System.Globalization;
using System.Net;
using System.Windows.Controls;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Controls
{
  internal class IPAddressValidationRule : ValidationRule
  {

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      try
      {
        string _strValue = (string)value;
        if (String.IsNullOrEmpty(_strValue))
          return new ValidationResult(false, $"The string representing IP Address cannot be empty");
        IPAddress _address;
        if (!IPAddress.TryParse(_strValue, out _address))
          return new ValidationResult(false, $"The string doesn't represent IP Address");
        if (_address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
          return new ValidationResult(false, $"The address family {_address.AddressFamily} is not supported");
        byte[] _bytesArray = _address.GetAddressBytes();
        if (_bytesArray.Length != 4)
          return new ValidationResult(false, $"The address length {_bytesArray.Length} is not expected");
        if (_bytesArray[0] > 239 || _bytesArray[0] < 224)
          return new ValidationResult(false, $"The address {_address} is outside the IP V4 multicast range 224.0.0.0 - 239.255.255.255");
      }
      catch (Exception _ex)
      {
        return new ValidationResult(false, $"Validation error: {_ex.GetType().Name}: {_ex.Message}");
      }
      return ValidationResult.ValidResult;
    }

  }
}
