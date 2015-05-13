using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  public abstract class NodesFactoryBase : IExportNodeContainer
  {
    public NodeFactory NewExportNodeFFactory<NodeFactory>()
  where NodeFactory : IExportNodeFactory
    {
      IExportNodeFactory _df = default(NodeFactory);
      if (typeof(NodeFactory).Name == typeof(IExportReferenceTypeFactory).Name)
        _df = new ExportReferenceTypeFactory();
      else
        throw new NotImplementedException();
      return (NodeFactory)_df;
    }

  }
}
