
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class DataTypeFactoryBase: TypeFactoryBase, IDataTypeFactory
  {
    public IDataTypeDefinitionFactory NewDefinition()
    {
      return new DataTypeDefinitionFactoryBase();
    }
  }
}
