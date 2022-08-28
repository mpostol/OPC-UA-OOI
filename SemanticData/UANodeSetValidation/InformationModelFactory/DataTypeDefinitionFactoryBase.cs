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
  internal class DataTypeDefinitionFactoryBase : IDataTypeDefinitionFactory
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
    public XmlQualifiedName Name { set { } }
    /// <summary>
    /// A symbolic name for the data type. It should only be specified if the Name cannot be used for this purpose.
    /// Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <value>The symbolic name of this entity.</value>
    public string SymbolicName { set { } }
    /// <summary>
    /// Sets a value indicating whether this instance is option set. This flag indicates that the data type defines the OptionSetValues Property.
    /// This field is optional.The default value is false.
    /// </summary>
    /// <value><c>true</c> if this instance is option set; otherwise, <c>false</c>.</value>
    public bool IsOptionSet { set { } }
    /// <summary>
    /// Sets a value indicating whether this instance is union.
    /// Only one of the Fields defined for the data type is encoded into a value.
    /// This field is optional.The default value is false. If this value is true, the first field is the switch value.
    /// </summary>
    /// <value><c>true</c> if this instance is union; otherwise, <c>false</c>.</value>
    public bool IsUnion { set { } }
  }
}
