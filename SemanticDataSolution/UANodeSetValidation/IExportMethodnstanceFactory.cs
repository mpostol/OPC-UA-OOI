using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportMethodnstanceFactory : IExportInstanceFactory
  {
     bool NonExecutable
    {
      set;
    }
     bool NonExecutableSpecified
    {
      set;
    }

     IExportMethodnstanceFactory Clone();
  }
}
