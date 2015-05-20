
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface IInstanceFactory : INodeFactory
  {

    ModelingRules? ModelingRule
    {
      set;
    }
    XmlQualifiedName TypeDefinition
    {
      set;
    }
    XmlQualifiedName ReferenceType { set; }

  }
}
