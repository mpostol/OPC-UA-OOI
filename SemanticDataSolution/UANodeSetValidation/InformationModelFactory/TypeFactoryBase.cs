
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class TypeFactoryBase : NodeFactoryBase, IExportTypeFactory
  {

    public XmlQualifiedName BaseType
    {
      set { }
    }
    public bool IsAbstract
    {
      set { }
    }

  }
}
