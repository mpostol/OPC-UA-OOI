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
  /// Class DataTypeDefinitionFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" />
  internal class DataTypeDefinitionFactoryBase: IDataTypeDefinitionFactory
  {
    /// <summary>
    /// Creates new field and provides an object of <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" /> type encapsulating information about the field data type.
    /// </summary>
    /// <returns>Returns <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" /> .</returns>
    public IDataTypeFieldFactory NewField()
    {
      return new DataTypeFieldFactoryBase();
    }
    /// <summary>
    /// Sets the name of the DataType.
    /// </summary>
    /// <value>The name represented as <see cref="T:System.Xml.XmlQualifiedName" />.</value>
    public XmlQualifiedName Name
    {
      set {  }
    }
    /// <summary>
    /// Sets the the base type name.
    /// </summary>
    /// <value>AN object <see cref="T:System.Xml.XmlQualifiedName" /> representing name of a base type.</value>
    public XmlQualifiedName BaseType
    {
      set {  }
    }
    /// <summary>
    /// A symbolic name for the data type. It should only be specified if the Name cannot be used for this purpose.
    /// Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <value>The symbolic name of thi entity.</value>
    public string SymbolicName
    {
      set {  }
    }
  }
}
