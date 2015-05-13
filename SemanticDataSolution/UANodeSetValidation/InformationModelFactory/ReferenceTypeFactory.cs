using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  public class ExportReferenceTypeFactory : NodesFactoryBase, IExportReferenceTypeFactory
  {
    public XML.LocalizedText[] InverseName
    {
      set { }
    }

    public bool Symmetric
    {
      set { }
    }

    public XmlQualifiedName BaseType
    {
      set { }
    }

    public bool IsAbstract
    {
      set { }
    }

    public string BrowseName
    {
      set { }
    }

    public IExportInstanceFactory[] Children
    {
      get
      {
        return null;
      }
      set
      {

      }
    }

    public XML.LocalizedText[] Description
    {
      get
      {
        return null;
      }
      set
      {
      }
    }

    public XML.LocalizedText[] DisplayName
    {
      set { }
    }

    public IUAReferenceContext[] References
    {
      set { }
    }
    public XmlQualifiedName SymbolicName
    {
      get
      {
        return null;
      }
      set
      {
      }
    }
    public uint WriteAccess
    {
      set { }
    }

  }
}
