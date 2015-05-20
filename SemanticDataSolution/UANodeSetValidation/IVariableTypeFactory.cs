using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IVariableTypeFactory : ITypeFactory
  {

    XmlElement DefaultValue
    {
      set;
    }
    XmlQualifiedName DataType
    {
      set;
    }
    int ValueRank
    {
      set;
    }
    bool ValueRankSpecified
    {
      set;
    }
    string ArrayDimensions
    {
      set;
    }

  }
}
