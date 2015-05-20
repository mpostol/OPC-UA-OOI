using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface IReferenceFactory
  {
    
    XmlQualifiedName ReferenceType
    {
      set;
    }
    XmlQualifiedName TargetId
    {
      set;
    }

    [System.ComponentModel.DefaultValueAttribute(false)]
    bool IsInverse
    {
      set;
    }

  }
}
