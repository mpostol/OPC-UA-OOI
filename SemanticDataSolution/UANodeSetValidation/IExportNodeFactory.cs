using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportNodeFactory
  {
    string BrowseName
    {
      set;
    }
    IExportInstanceFactory[] Children
    {
      set;
      get;
    }
    XML.LocalizedText[] Description
    {
      set;
      get;
    }
    XML.LocalizedText[] DisplayName
    {
      set;
    }

    IUAReferenceContext[] References
    {
      set;
    }
    System.Xml.XmlQualifiedName SymbolicName
    {
      get;
      set;
    }
    /// <remarks/>
    [System.ComponentModel.DefaultValueAttribute(typeof(uint), "0")]
    //TODO is not assigned
    uint WriteAccess
    {
      set;
    }

  }
}
