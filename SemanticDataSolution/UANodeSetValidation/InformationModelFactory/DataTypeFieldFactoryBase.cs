using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{

  internal class DataTypeFieldFactoryBase : IDataTypeFieldFactory
  {

    public XmlQualifiedName DataType
    {
      set { }
    }
    public int Identifier
    {
      set { }
    }
    public bool IdentifierSpecified
    {
      set { }
    }
    public string Name
    {
      set { }
    }
    public int ValueRank
    {
      set { }
    }
    public IDataTypeDefinitionFactory NewDefinition()
    {
      return new DataTypeDefinitionFactoryBase();
    }
    public int Value
    {
      set { }
    }
    public string SymbolicName
    {
      set { }
    }
    public void AddDescription(string localeField, string valueField) { }

  }
}
