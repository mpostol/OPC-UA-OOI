
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{

  public interface IVariableInstanceFactory : IInstanceFactory
  {
    byte? AccessLevel
    {
      set;
    }
    string ArrayDimensions
    {
      set;
    }
    XmlQualifiedName DataType
    {
      set;
    }
    XmlElement DefaultValue
    {
      set;
    }
    bool Historizing
    {
      set;
    }
    bool HistorizingSpecified
    {
      set;
    }
    int MinimumSamplingInterval
    {
      set;
    }
    bool MinimumSamplingIntervalSpecified
    {
      set;
    }
    byte? UserAccessLevel
    {
      set;
    }
    int? ValueRank
    {
      set;
    }
  }
}
