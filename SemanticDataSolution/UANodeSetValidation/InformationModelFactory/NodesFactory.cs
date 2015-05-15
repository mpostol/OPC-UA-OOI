
using System;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal abstract class NodesContainer : IExportNodeContainer
  {
    public NodeFactory NewExportNodeFFactory<NodeFactory>() where NodeFactory : IExportNodeFactory
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
