//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{

  /// <summary>
  /// Class DataTypeFieldFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" />
  internal class DataTypeFieldFactoryBase : IDataTypeFieldFactory
  {

    /// <summary>
    /// Sets the DataType name.
    /// </summary>
    /// <value>The type of the data.</value>
    public XmlQualifiedName DataType
    {
      set { }
    }
    /// <summary>
    /// Sets the identifier the value associated with the field.
    /// </summary>
    /// <value>The identifier.</value>
    public int? Identifier
    {
      set { }
    }
    /// <summary>
    /// Sets the name for the field that is unique within the <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" />.
    /// </summary>
    /// <value>The name for the field.</value>
    public string Name
    {
      set { }
    }
    /// <summary>
    /// Sets the value rank. It shall be Scalar (-1) or a fixed rank Array (&gt;=1). This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value rank.</value>
    public int? ValueRank
    {
      set { }
    }
    /// <summary>
    /// Creates new object of <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" /> for anonymous definition of the DatType.
    /// The field is a structure with a layout specified by the <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" />.
    /// This field is optional.
    /// This field allows designers to create nested structures without defining a new DataType Node for each structure.
    /// This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <returns>IDataTypeDefinitionFactory.</returns>
    /// <value>A new instance of <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" /> encapsulating the DatType definition.</value>
    public IDataTypeDefinitionFactory NewDefinition()
    {
      return new DataTypeDefinitionFactoryBase();
    }
    /// <summary>
    /// The value associated with the field. This field is only specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value.</value>
    public int Value
    {
      set { }
    }
    /// <summary>
    /// Sets the symbolic name of the field. A symbolic name for the field that can be used in auto-generated code. It should only be
    /// specified if the Name cannot be used for this purpose. Only letters, digits or the underscore (‘_’) are permitted.
    /// This value is not exposed in the OPC UA Address Space
    /// </summary>
    /// <value>The symbolic name to be used by the tool.</value>
    public string SymbolicName
    {
      set { }
    }
    /// <summary>
    /// Adds the description for the field in multiple locales
    /// </summary>
    /// <param name="localeField">The locale field specified as a string that is composed of a language component and a country/region component as specified by RFC 3066.</param>
    /// <param name="valueField">The localized text.</param>
    public void AddDescription(string localeField, string valueField) { }
    /// <summary>
    /// Creates new datatypedefinitionfactory.
    /// </summary>
    /// <returns>IDataTypeDefinitionFactory.</returns>
    public IDataTypeDefinitionFactory NewDataTypeDefinitionFactory() { return new DataTypeDefinitionFactoryBase(); }

  }
}
