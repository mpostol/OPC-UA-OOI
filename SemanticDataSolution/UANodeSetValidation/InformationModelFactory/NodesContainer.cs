
using System;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal abstract class NodesContainer : IExportNodeContainer
  {
    public NodeFactory NewExportNodeFFactory<NodeFactory>() where NodeFactory : IExportNodeFactory
    {
      IExportNodeFactory _df = default(NodeFactory);
      if (typeof(NodeFactory) == typeof(IExportReferenceTypeFactory))
        _df = new ReferenceTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportObjectTypeFactory))
        _df = new ObjectTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportVariableTypeFactory))
        _df = new VariableTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportDataTypeFactory))
        _df = new DataTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportObjectInstanceFactory))
        _df = new ObjectInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportPropertyInstanceFactory))
        _df = new PropertyInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportVariableInstanceFactory))
        _df = new VariableInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportMethodInstanceFactory))
        _df = new MethodInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IExportViewInstanceFactory))
        _df = new ViewInstanceFactoryBase();
      else
        throw new NotImplementedException();
      return (NodeFactory)_df;
    }

  }
}
