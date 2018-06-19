
using System.Collections.Generic;
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
      ReferenceFactoryBase _ret = new ReferenceFactoryBase();
      m_References.Add(_ret);
      return _ret;
    }
    public XmlQualifiedName SymbolicName
    {
      set;
      internal get;
    }
    public uint WriteAccess
    {
      set { }
    }
    public void AddDescription(string localeField, string valueField) { }
    public void AddDisplayName(string localeField, string valueField) { }

    protected List<ReferenceFactoryBase> m_References = new List<ReferenceFactoryBase>();
  }
}
