
namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class DataTypeDefinitionFactoryBase: IExportDataTypeDefinitionFactory
  {
    public IExportDataTypeFieldFactory Field()
    {
      return new DataTypeFieldFactoryBase();
    }

    public System.Xml.XmlQualifiedName Name
    {
      set {  }
    }

    public System.Xml.XmlQualifiedName BaseType
    {
      set {  }
    }

    public string SymbolicName
    {
      set {  }
    }
  }
}
