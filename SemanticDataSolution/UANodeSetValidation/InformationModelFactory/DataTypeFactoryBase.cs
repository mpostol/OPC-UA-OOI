
namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class DataTypeFactoryBase: TypeFactoryBase, IExportDataTypeFactory
  {
    public IExportDataTypeDefinitionFactory NewDefinition()
    {
      return new DataTypeDefinitionFactoryBase();
    }
  }
}
