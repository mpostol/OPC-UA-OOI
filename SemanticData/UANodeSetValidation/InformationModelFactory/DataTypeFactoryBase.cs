//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  /// <summary>
  /// Class DataTypeFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.TypeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.TypeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFactory" />
  internal class DataTypeFactoryBase : TypeFactoryBase, IDataTypeFactory
  {

    /// <summary>
    /// Sets a value indicating whether this instance is option set. This flag indicates that the data type defines the OptionSetValues Property. 
    /// This field is optional.The default value is false.
    /// </summary>
    /// <value><c>true</c> if this instance is option set; otherwise, <c>false</c>.</value>
    public bool IsOptionSet { set; private get; }
    /// <summary>
    /// Creates new implementation dependent object implementing the <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" /> interface.
    /// The data type model is used to define simple and complex data types. Types are used to describe the structure of the Value attribute of variables and their types.
    /// Therefore each Variable and VariableType node is pointing with its DataType attribute to a node of the DataType node class.
    /// </summary>
    /// <returns>IDataTypeDefinitionFactory.</returns>
    /// <value>Returns new object of <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" /> type encapsulating DataType definition factory.</value>
    public IDataTypeDefinitionFactory NewDefinition()
    {
      return new DataTypeDefinitionFactoryBase();
    }
  }
}
