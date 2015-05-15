using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportReferenceFactory
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
