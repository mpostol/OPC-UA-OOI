
namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IDataTypeFactory - 
  /// </summary>
  public interface IDataTypeFactory : ITypeFactory
  {
    /// <summary>
    /// Creates new definition of DataType
    /// </summary>
    /// <value>Returns new object of <see cref="IDataTypeDefinitionFactory"/> type encapsulating DataType Definition.</value>
    IDataTypeDefinitionFactory NewDefinition();

  }
}
