
using System.Xml;
namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class NodeFactoryBase : NodesContainer, IExportNodeFactory
  {

    public string BrowseName
    {
      set { }
    }
    public IExportReferenceFactory NewReference()
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
