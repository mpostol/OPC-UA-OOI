using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class VariableInstanceFactoryBase: InstanceFactoryBase, IExportVariableInstanceFactory
  {
    public byte AccessLevel
    {
      set {  }
    }

    public bool AccessLevelSpecified
    {
      set { }
    }

    public string ArrayDimensions
    {
      set {  }
    }

    public XmlQualifiedName DataType
    {
      set {  }
    }

    public XmlElement DefaultValue
    {
      set {  }
    }

    public bool Historizing
    {
      set {  }
    }

    public bool HistorizingSpecified
    {
      set {  }
    }

    public int MinimumSamplingInterval
    {
      set {  }
    }

    public bool MinimumSamplingIntervalSpecified
    {
      set {  }
    }

    public byte UserAccessLevel
    {
      set {  }
    }

    public bool UserAccessLevelSpecified
    {
      set {  }
    }

    public int ValueRank
    {
      set {  }
    }

    public bool ValueRankSpecified
    {
      set { }
    }
  }
}
