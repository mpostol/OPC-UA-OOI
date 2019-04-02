//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{

  //TODO report  Mantis issues.
  /// <summary>
  /// Interface <c>IDataTypeDefinitionFactory</c> - This interface is used to define subtypes of the Structure or Enumeration standard DataTypes.
  /// It defines an abstract representation of a <see cref="IDataTypeFactory"/> that can be used by design tools to automatically create 
  /// serialization code.
  /// </summary>
  /// <remarks>
  /// See detailed description in the Part 6 F.12.
  /// 
  /// Note:
  /// 
  /// <see cref="IDataTypeDefinitionFactory.BaseType"/> is not defined in the spec.
  /// <c>IsUnion</c> is not defined in the .xsd. This flag indicates if the data type represents a union.  Only one of the Fields defined for the data type is encoded into a value.
  /// This field is optional.The default value is false. If this value is true, the first field is the switch value.
  /// </remarks>
  public interface IDataTypeDefinitionFactory
  {

    /// <summary>
    /// Creates new field and provides an object of <see cref="IDataTypeFieldFactory"/> type encapsulating information about the field data type.
    /// It is assumed that the structure has a sequential layout.For enumerations, the fields are simply a list of values.
    /// </summary>
    /// <returns>Returns new instance of the <see cref="IDataTypeFieldFactory"/>.</returns>
    IDataTypeFieldFactory NewField();
    /// <summary>
    /// Sets a unique name of the DataType. This field is only specified for nested DataTypeDefinitions. 
    /// The BrowseName of the DataType Node is used otherwise.
    /// This field is only specified for nested DataTypeDefinitions. The SymbolicName of the DataType Node is used otherwise.
    /// </summary>
    /// <value>The name represented as <see cref="XmlQualifiedName"/>.</value>
    XmlQualifiedName Name { set; }
    /// <summary>
    /// Sets the base type name.
    /// </summary>
    /// <value>AN object <see cref="XmlQualifiedName"/> representing name of a base type.</value>
    XmlQualifiedName BaseType { set; }
    /// <summary>
    /// A symbolic name for the data type. It should only be specified if the Name cannot be used for this purpose. 
    /// Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <value>The symbolic name of thi entity.</value>
    string SymbolicName { set; }
    /// <summary>
    /// Sets a value indicating whether this instance is option set. This flag indicates that the data type defines the OptionSetValues Property.
    /// This field is optional.The default value is false.
    /// </summary>
    /// <value><c>true</c> if this instance is option set; otherwise, <c>false</c>.</value>
    bool IsOptionSet { set; }
    /// <summary>
    /// Sets a value indicating whether this instance is union. 
    /// Only one of the Fields defined for the data type is encoded into a value.
    /// This field is optional.The default value is false. If this value is true, the first field is the switch value.
    /// </summary>
    /// <value><c>true</c> if this instance is union; otherwise, <c>false</c>.</value>
    bool IsUnion { set; }

  }
}
