
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportDataTypeFactory : IExportTypeFactory
  {
     IExportDataTypeDefinitionFactory[] Fields
    {
      set;
    }
     IExportDataTypeDefinitionFactory NewExportDataTypeDefinitionFactory();
  }
}
