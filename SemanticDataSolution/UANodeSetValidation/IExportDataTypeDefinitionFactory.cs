
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  public interface IExportDataTypeDefinitionFactory
  {

    XmlQualifiedName DataType
    {
      set;
    }
    XML.LocalizedText[] Description
    {
      set;
    }
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
    /// <summary>The field is a structure with a layout specified by the definition. 
    /// This field is optional.
    /// This field allows designers to create nested structures without defining a new DataType Node for each structure.
    /// This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The definition.</value>
    DataTypeDefinition Definition
    {
      set;
    }
    /// <summary>
    ///A symbolic name for the field that can be used in auto-generated code. It should only be specified if the Name cannot be used for this purpose. 
    ///Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <value> The name of the symbolic.</value>
    string SymbolicName
    {
      set;
    }
    /// <summary>
    /// The value associated with the field. This field is only specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value.</value>
    int Value
    {
      set;
    }

  }
}
