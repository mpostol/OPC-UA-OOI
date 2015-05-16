using System;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class VariableTypeFactoryBase : TypeFactoryBase, IExportVariableTypeFactory
  {

    public System.Xml.XmlElement DefaultValue
    {
      set { }
    }
    public System.Xml.XmlQualifiedName DataType
    {
      set { }
    }
    public int ValueRank
    {
      set { }
    }
    public bool ValueRankSpecified
    {
      set { }
    }
    public string ArrayDimensions
    {
      set { }
    }
  }
}
