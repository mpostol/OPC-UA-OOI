using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface INodeContainer
  {
    NodeFactory NewExportNodeFFactory<NodeFactory>()
      where NodeFactory : INodeFactory;
  }
}
