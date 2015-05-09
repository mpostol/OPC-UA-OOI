using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportObjectnstanceFactory : IExportInstanceFactory
  {
    bool SupportsEvents { set; }

    bool SupportsEventsSpecified { set; }

  }
}
