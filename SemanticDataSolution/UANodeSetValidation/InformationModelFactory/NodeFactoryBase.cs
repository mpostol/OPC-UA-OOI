
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class NodeFactoryBase : NodesContainer, INodeFactory
  {

    public string BrowseName
    {
      set { }
    }
    public IReferenceFactory NewReference()
    {
      return new ReferenceFactoryBase();
    }
    public XmlQualifiedName SymbolicName
    {
      set { }
    }
    public uint WriteAccess
    {
      set { }
    }
    public void AddDescription(string localeField, string valueField) { }
    public void AddDisplayName(string localeField, string valueField) { }

  }
}
