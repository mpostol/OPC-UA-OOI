using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class ExportReferenceTypeFactory : TypeFactoryBase, IExportReferenceTypeFactory
  {

    public XML.LocalizedText[] InverseName
    {
      set { }
    }

    public bool Symmetric
    {
      set { }
    }

  }
}
