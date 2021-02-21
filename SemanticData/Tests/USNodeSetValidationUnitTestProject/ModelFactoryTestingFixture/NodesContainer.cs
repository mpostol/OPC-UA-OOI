//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.ModelFactoryTestingFixture
{
  /// <summary>
  /// Class NodesContainer.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.INodeContainer" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.INodeContainer" />
  internal abstract class NodesContainer : INodeContainer
  {
    /// <summary>
    /// Creates and adds a new node instance of the <see cref="T:UAOOI.SemanticData.InformationModelFactory.INodeFactory" />.
    /// </summary>
    /// <typeparam name="TNodeFactory">The type of the node factory must inherit from <see cref="T:UAOOI.SemanticData.InformationModelFactory.INodeFactory" />.</typeparam>
    /// <returns>Returns new object implementing <see cref="T:UAOOI.SemanticData.InformationModelFactory.INodeFactory" />.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public TNodeFactory AddNodeFactory<TNodeFactory>() where TNodeFactory : INodeFactory
    {
      NodeFactoryBase _df = null;
      if (typeof(TNodeFactory) == typeof(IReferenceTypeFactory))
        _df = new ReferenceTypeFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IObjectTypeFactory))
        _df = new ObjectTypeFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IVariableTypeFactory))
        _df = new VariableTypeFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IDataTypeFactory))
        _df = new DataTypeFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IObjectInstanceFactory))
        _df = new ObjectInstanceFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IPropertyInstanceFactory))
        _df = new PropertyInstanceFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IVariableInstanceFactory))
        _df = new VariableInstanceFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IMethodInstanceFactory))
        _df = new MethodInstanceFactoryBase();
      else if (typeof(TNodeFactory) == typeof(IViewInstanceFactory))
        _df = new ViewInstanceFactoryBase();
      else
        throw new NotImplementedException();
      m_Nodes.Add(_df);
      return (TNodeFactory)(INodeFactory)_df;
    }
    protected void Export (Action<NodeFactoryBase> callBack)
    {
      foreach (NodeFactoryBase item in m_Nodes)
      {
        callBack(item);
        item.Export(callBack);
      }
    }
    protected List<NodeFactoryBase> m_Nodes = new List<NodeFactoryBase>();
  }
}