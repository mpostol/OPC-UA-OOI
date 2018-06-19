
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class InstanceFactoryBase : NodeFactoryBase, IInstanceFactory
  {
    public ModelingRules? ModelingRule
    {
      set { }
    }
    public System.Xml.XmlQualifiedName TypeDefinition
    {
      set { }
    }
    public System.Xml.XmlQualifiedName ReferenceType
    {
      set { }
    }
  }
}
