
namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface <c>IDataTypeFactory</c> - it provides functionality to factory objects implementing the <see cref="IDataTypeDefinitionFactory"/> interface. 
  /// </summary>
  public interface IDataTypeFactory : ITypeFactory
  {
    /// <summary>
    /// Creates new implementation dependent object implementing the <see cref="IDataTypeDefinitionFactory"/> interface.
    /// The data type model is used to define simple and complex data types. Types are used to describe the structure of the Value attribute of variables and their types. 
    /// Therefore each Variable and VariableType node is pointing with its DataType attribute to a node of the DataType node class.
    /// </summary>
    /// <value>Returns new object of <see cref="IDataTypeDefinitionFactory"/> type encapsulating DataType definition factory.</value>
    IDataTypeDefinitionFactory NewDefinition();

  }
}
