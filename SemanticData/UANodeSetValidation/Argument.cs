//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class Argument.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.Parameter" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.Parameter" />
  internal class Argument: Parameter
  {

    /// <summary>
    /// Adds the localized description of the argument
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    internal void AddDescription(string localeField, string valueField)
    {
      Descriptions.Add(new Description() { Locale = localeField, Text = valueField });
    }

  }
}
