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
  /// Interface <c>IDataTypeDefinitionFactory</c> - This interface is used to define subtypes of the Structure or Enumeration standard DataTypes.
  /// It defines an abstract representation of a <see cref="IDataTypeFactory"/> that can be used by design tools to automatically create 
  /// serialization code.
  /// </summary>
  public interface IDataTypeDefinitionFactory
  {

    /// <summary>
    /// Creates new field and provides an object of <see cref="IDataTypeFieldFactory"/> type encapsulating information about the field data type.
    /// </summary>
    /// <returns>Returns <see cref="IDataTypeFieldFactory"/> .</returns>
    IDataTypeFieldFactory NewField();
    /// <summary>
    /// Sets the name of the DataType.
    /// </summary>
    /// <value>The name represented as <see cref="XmlQualifiedName"/>.</value>
    XmlQualifiedName Name
    {
      set;
    }
    /// <summary>
    /// Sets the the base type name.
    /// </summary>
    /// <value>AN object <see cref="XmlQualifiedName"/> representing name of a base type.</value>
    XmlQualifiedName BaseType
    {
      set;
    }
    /// <summary>
    /// A symbolic name for the data type. It should only be specified if the Name cannot be used for this purpose. 
    /// Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <value>The symbolic name of thi entity.</value>
    string SymbolicName
    {
      set;
    }

  }
}
