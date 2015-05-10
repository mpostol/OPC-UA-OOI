
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportInstanceFactory : IExportNodeFactory
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
