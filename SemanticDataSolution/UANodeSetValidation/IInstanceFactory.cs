
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
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
