
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class DataTypeDefinitionFactoryBase: IExportDataTypeDefinitionFactory
  {
    public IExportDataTypeFieldFactory NewField()
    {
      return new DataTypeFieldFactoryBase();
    }
    public XmlQualifiedName Name
    {
      set {  }
    }
    public XmlQualifiedName BaseType
    {
      set {  }
    }
    public string SymbolicName
    {
      set {  }
    }
  }
}
