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
      }
      catch (Exception _ex)
      {
        return new ValidationResult(false, $"Validation error: {_ex.GetType().Name}: {_ex.Message}");
      }
      return ValidationResult.ValidResult;
    }
  }
}
