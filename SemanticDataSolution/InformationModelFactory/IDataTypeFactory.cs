
namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IExportDataTypeFactory - 
  /// </summary>
  public interface IDataTypeFactory : ITypeFactory
  {
    /// <summary>
    /// Sets the fields.
    /// </summary>
    /// <value>The fields.</value>
    IDataTypeDefinitionFactory NewDefinition();
  }
}
