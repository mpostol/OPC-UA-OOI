//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

//TODO Define independent Address Space API #645 - Remove dependency on InformationModelFactory
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  public interface IUADataType : IUADataTypeNodeClass, IUAType
  {

    //TODO Mantis - report error 
    /// <summary>
    /// Sets the data type purpose.
    /// </summary>
    /// <remarks>
    /// Not defined in the specification Part 2, 5, 6 and Errata Release 1.04.2 September 25, 2018
    /// This field is defined in the UADataType in the <c>UADataType</c> but in UA Model Design in the <c>NodeDesign</c>
    /// </remarks>
    /// <value>The data type purpose.</value>
    DataTypePurpose Purpose { get; }
  }
  /// <summary>
  /// DataTypes are defined using the DataType NodeClass. The DataType NodeClass describes the syntax of a Variable Value. 
  /// </summary>
  public interface IUADataTypeNodeClass
  {
    /// <summary>
    /// The DataTypeDefinition Attribute is used to provide the meta data and encoding information for custom DataTypes. The abstract DataTypeDefinition DataType is defined in 8.47.
    /// Structure and Union DataTypes
    /// The Attribute is mandatory for DataTypes derived from Structure and Union. For such DataTypes, the Attribute contains a structure of the DataType StructureDefinition. 
    /// The StructureDefinition DataType is defined in 8.48. It is a subtype of DataTypeDefinition.
    /// Enumeration and OptionSet DataTypes
    /// The Attribute is mandatory for DataTypes derived from Enumeration, OptionSet and subtypes of UInteger representing an OptionSet. For such DataTypes, the Attribute contains 
    /// a structure of the DataType EnumDefinition. The EnumDefinition DataType is defined in 8.49. It is a subtype of DataTypeDefinition.
    /// </summary>
    IDataTypeDefinition Definition { get; }
  }
}