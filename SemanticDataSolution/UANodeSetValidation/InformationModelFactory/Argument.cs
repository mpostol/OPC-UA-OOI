
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
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
