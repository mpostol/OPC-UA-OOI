
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal abstract class NodesContainer : INodeContainer
  {
    public NodeFactory AddNodeFactory<NodeFactory>() where NodeFactory : INodeFactory
    {
      NodeFactoryBase _df = null;
      if (typeof(NodeFactory) == typeof(IReferenceTypeFactory))
        _df = new ReferenceTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IObjectTypeFactory))
        _df = new ObjectTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IVariableTypeFactory))
        _df = new VariableTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IDataTypeFactory))
        _df = new DataTypeFactoryBase();
      else if (typeof(NodeFactory) == typeof(IObjectInstanceFactory))
        _df = new ObjectInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IPropertyInstanceFactory))
        _df = new PropertyInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IVariableInstanceFactory))
        _df = new VariableInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IMethodInstanceFactory))
        _df = new MethodInstanceFactoryBase();
      else if (typeof(NodeFactory) == typeof(IViewInstanceFactory))
        _df = new ViewInstanceFactoryBase();
      else
        throw new NotImplementedException();
      m_Nodes.Add(_df);
      return (NodeFactory)(INodeFactory)_df;
    }
    protected List<NodeFactoryBase> m_Nodes = new List<NodeFactoryBase>();

  }
}
