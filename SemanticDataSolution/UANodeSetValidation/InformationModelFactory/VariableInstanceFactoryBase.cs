using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{

  internal class VariableInstanceFactoryBase : InstanceFactoryBase, IVariableInstanceFactory
  {

    public byte? AccessLevel
    {
      set { }
    }
    public string ArrayDimensions
    {
      set { }
    }
    public XmlQualifiedName DataType
    {
      set { }
    }
    public XmlElement DefaultValue
    {
      set { }
    }
    public bool Historizing
    {
      set { }
    }
    public bool HistorizingSpecified
    {
      set { }
    }
    public int MinimumSamplingInterval
    {
      set { }
    }
    public bool MinimumSamplingIntervalSpecified
    {
      set { }
    }
    public byte? UserAccessLevel
    {
      set { }
    }
    public int? ValueRank
    {
      set { }
    }

  }
}
