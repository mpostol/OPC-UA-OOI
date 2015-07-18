
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IDataTypeFieldFactory - This interface defines a representation of a field within a DataType.
  /// </summary>
  public interface IDataTypeFieldFactory
  {
    /// <summary>
    /// Sets the DataType name.
    /// </summary>
    /// <value>The type of the data.</value>
    XmlQualifiedName DataType
    {
      set;
    }
    /// <summary>
    /// Adds the description for the field in multiple locales
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    void AddDescription(string localeField, string valueField);
    /// <summary>
    /// Sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    int? Identifier
    {
      set;
    }
    /// <summary>
    /// Sets the name for the field that is unique within the <see cref="IDataTypeDefinitionFactory"/>.
    /// </summary>
    /// <value>The name for the field.</value>
    string Name
    {
      set;
    }
    /// <summary>
    /// Sets the value rank. It shall be Scalar (-1) or a fixed rank Array (>=1). This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value rank.</value>
    int? ValueRank
    {
      set;
    }
    /// <summary>The field is a structure with a layout specified by the <see cref="IDataTypeDefinitionFactory"/>. 
    /// This field is optional.
    /// This field allows designers to create nested structures without defining a new DataType Node for each structure.
    /// This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The definition.</value>
    IDataTypeDefinitionFactory NewDefinition();
    /// <summary>
    /// The value associated with the field. This field is only specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value.</value>
    int Value
    {
      set;
    }
    /// <summary>
    /// Sets the symbolic name of the field. A symbolic name for the field that can be used in auto-generated code. It should only be 
    /// specified if the Name cannot be used for this purpose. Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <value>The name of the symbolic.</value>
    string SymbolicName
    {
      set;
    }
  }
}
