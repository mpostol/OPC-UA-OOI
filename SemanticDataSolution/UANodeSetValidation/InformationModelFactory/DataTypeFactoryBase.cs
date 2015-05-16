using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class DataTypeFactoryBase: TypeFactoryBase, IExportDataTypeFactory
  {
    public IExportDataTypeDefinitionFactory Definition()
    {
      return new DataTypeDefinitionFactoryBase();
    }
  }
}
