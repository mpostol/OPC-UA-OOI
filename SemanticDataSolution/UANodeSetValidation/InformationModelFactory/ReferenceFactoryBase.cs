
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class ReferenceFactoryBase : IReferenceFactory
  {

    public XmlQualifiedName ReferenceType
    {
      set { }
    }
    public XmlQualifiedName TargetId
    {
      set { }
    }
    public bool IsInverse
    {
      set { }
    }

  }
}
