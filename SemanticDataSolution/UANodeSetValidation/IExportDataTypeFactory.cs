
namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IExportDataTypeFactory - 
  /// </summary>
  public interface IExportDataTypeFactory : IExportTypeFactory
  {
    /// <summary>
    /// Sets the fields.
    /// </summary>
    /// <value>The fields.</value>
    IExportDataTypeDefinitionFactory NewDefinition();
  }
}
