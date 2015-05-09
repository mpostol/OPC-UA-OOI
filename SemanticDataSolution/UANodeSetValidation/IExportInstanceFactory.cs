using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportInstanceFactory : IExportNodeFactory
  {
    ModelingRules? ModelingRule
    {
      set;
    }
    System.Xml.XmlQualifiedName TypeDefinition
    {
      set;
    }
    System.Xml.XmlQualifiedName ReferenceType { set; }

  }
}
