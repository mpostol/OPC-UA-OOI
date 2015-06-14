using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  
  public interface IParameter: IDataDescriptor
  {

    /// <summary>
    /// Sets the name of the parameter.
    /// </summary>
    /// <value>The name.</value>
    string Name { set; }
    /// <summary>
    /// Sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    int? Identifier { set; }
    /// <summary>
    /// Adds the localized description of the argument
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    void AddDescription(string localeField, string valueField);

  }
}
