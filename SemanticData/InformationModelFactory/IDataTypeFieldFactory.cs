//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface <c>IDataTypeFieldFactory</c> - This interface defines a representation of a field within a structural DataType.
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
    /// <param name="localeField">The locale field specified as a string that is composed of a language component and a country/region component as specified by RFC 3066.</param>
    /// <param name="valueField">The localized text.</param>
    void AddDescription(string localeField, string valueField);
    /// <summary>
    /// Sets the identifier the value associated with the field.
    /// </summary>
    /// <value>The identifier.</value>
    //TODO IDataTypeFieldFactory.Identifier is not defined by the schema #46
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
    /// <summary>Creates new object of <see cref="IDataTypeDefinitionFactory"/> for anonymous definition of the DatType.
    /// The field is a structure with a layout specified by the <see cref="IDataTypeDefinitionFactory"/>.
    /// This field is optional.
    /// This field allows designers to create nested structures without defining a new DataType Node for each structure.
    /// This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <value>A new instance of <see cref="IDataTypeDefinitionFactory"/> encapsulating the DatType definition.</value>
    IDataTypeDefinitionFactory NewDefinition();
    /// <summary>
    /// The value associated with the field. This field is only specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value. </value>
    int Value
    {
      set;
    }
    /// <summary>
    /// Sets the symbolic name of the field. A symbolic name for the field that can be used in auto-generated code. It should only be 
    /// specified if the Name cannot be used for this purpose. Only letters, digits or the underscore (‘_’) are permitted.
    /// This value is not exposed in the OPC UA Address Space
    /// </summary>
    /// <value>The symbolic name to be used by the tool.</value>
    string SymbolicName
    {
      set;
    }
  }
}
