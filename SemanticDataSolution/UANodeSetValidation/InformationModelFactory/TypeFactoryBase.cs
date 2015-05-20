
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class TypeFactoryBase : NodeFactoryBase, ITypeFactory
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
