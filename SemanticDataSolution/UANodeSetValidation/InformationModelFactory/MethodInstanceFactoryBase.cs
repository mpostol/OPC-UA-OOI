using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class MethodInstanceFactoryBase: InstanceFactoryBase, IExportMethodInstanceFactory
  {
    public bool NonExecutable
    {
      set { }
    }

    public bool NonExecutableSpecified
    {
      set {  }
    }

  }
}
