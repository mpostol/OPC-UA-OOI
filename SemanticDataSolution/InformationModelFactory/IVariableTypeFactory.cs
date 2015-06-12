using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface IVariableTypeFactory : ITypeFactory, IDataDescriptor
  {

    XmlElement DefaultValue
    {
      set;
    }

  }
}
