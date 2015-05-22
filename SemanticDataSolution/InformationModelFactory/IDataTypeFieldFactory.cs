
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IExportDataTypeFieldFactory - The=is interface defines an abstract representation of a field within a 
  /// UADataType that can be used by design tools to automatically create serialization code.
  /// </summary>
  public interface IDataTypeFieldFactory
  {
    XmlQualifiedName DataType
    {
      set;
    }
    void AddDescription(string localeField, string valueField);
    IDataTypeDefinitionFactory NewDataTypeDefinitionFactory();
    int Identifier
    {
      set;
    }
    bool IdentifierSpecified
    {
      set;
    }
    string Name
    {
      set;
    }
    int ValueRank
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
    string SymbolicName
    {
      set;
    }
  }
}
