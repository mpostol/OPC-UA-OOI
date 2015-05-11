using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportNodeContainer
  {
    NodeFactory NewExportNodeFFactory<NodeFactory>()
      where NodeFactory : IExportNodeFactory;
  }
}
