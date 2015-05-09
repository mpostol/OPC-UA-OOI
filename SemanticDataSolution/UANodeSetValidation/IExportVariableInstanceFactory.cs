
namespace UAOOI.SemanticData.UANodeSetValidation
{

  public interface IExportVariableInstanceFactory: IExportInstanceFactory
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
     System.Xml.XmlQualifiedName DataType
    {
      set;
    }
     System.Xml.XmlElement DefaultValue
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
