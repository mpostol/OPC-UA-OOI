using System;
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class DataTypeFieldFactoryBase : IExportDataTypeFieldFactory
  {

    public XmlQualifiedName DataType
    {
      set {  }
    }
    public XML.LocalizedText[] Description
    {
      set {  }
    }
    public int Identifier
    {
      set {  }
    }
    public bool IdentifierSpecified
    {
      set {  }
    }
    public string Name
    {
      set {  }
    }
    public int ValueRank
    {
      set { }
    }
    public IExportDataTypeDefinitionFactory Definition()
    {
      return new DataTypeDefinitionFactoryBase();
    }
    public int Value
    {
      set {  }
    }
    public string SymbolicName
    {
      set { }
    }

  }
}
