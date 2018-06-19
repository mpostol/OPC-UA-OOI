
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class DataTypeDefinitionFactoryBase: IDataTypeDefinitionFactory
  {
    public IDataTypeFieldFactory NewField()
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
