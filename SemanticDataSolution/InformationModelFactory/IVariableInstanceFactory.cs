
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{

  public interface IVariableInstanceFactory : IInstanceFactory, IDataDescriptor
  {
    byte? AccessLevel
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
  }
}
