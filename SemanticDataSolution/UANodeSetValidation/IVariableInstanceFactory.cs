
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  public interface IVariableInstanceFactory : IInstanceFactory
  {
    byte AccessLevel
    {
      set;
    }
    bool AccessLevelSpecified
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
    byte UserAccessLevel
    {
      set;
    }
    bool UserAccessLevelSpecified
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
  }
}
