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
  internal class DataTypeFactoryBase: TypeFactoryBase, IDataTypeFactory
  {
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
