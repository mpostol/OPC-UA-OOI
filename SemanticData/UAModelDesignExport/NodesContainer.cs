//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Class NodesContainer.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.INodeContainer" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.INodeContainer" />
  internal abstract class NodesContainer : INodeContainer
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="NodesContainer"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    internal NodesContainer(Action<TraceMessage> traceEvent)
    {
      Debug.Assert(traceEvent != null);
      this.TraceEvent = traceEvent;
    }

    //INodeContainer
    /// <summary>
    /// Creates and adds a new node instance of the <see cref="T:UAOOI.SemanticData.InformationModelFactory.INodeFactory" />.
    /// </summary>
    /// <typeparam name="NodeFactory">The type of the node factory must inherit from <see cref="T:UAOOI.SemanticData.InformationModelFactory.INodeFactory" />.</typeparam>
    /// <returns>Returns new object implementing <see cref="T:UAOOI.SemanticData.InformationModelFactory.INodeFactory" />.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public NodeFactory AddNodeFactory<NodeFactory>() where NodeFactory : INodeFactory
    {
      NodeFactoryBase _df = default(NodeFactoryBase);
      if (typeof(NodeFactory) == typeof(IReferenceTypeFactory))
        _df = new ReferenceTypeFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IObjectTypeFactory))
        _df = new ObjectTypeFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IVariableTypeFactory))
        _df = new VariableTypeFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IDataTypeFactory))
        _df = new DataTypeFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IObjectInstanceFactory))
        _df = new ObjectInstanceFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IPropertyInstanceFactory))
        _df = new PropertyInstanceFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IVariableInstanceFactory))
        _df = new VariableInstanceFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IMethodInstanceFactory))
        _df = new MethodInstanceFactoryBase(TraceEvent);
      else if (typeof(NodeFactory) == typeof(IViewInstanceFactory))
        _df = new ViewInstanceFactoryBase(TraceEvent);
      else
        throw new NotImplementedException();
      m_Nodes.Add(_df);
      return (NodeFactory)(INodeFactory)_df;
    }

    //private
    protected void ExportNodes(List<NodeDesign> members, List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      NodeFactoryBase _currItem = null;
      foreach (NodeFactoryBase _item in m_Nodes)
      {
        try
        {
          _currItem = _item;
          NodeDesign _newNode = _item.Export(new List<string>(path), createInstanceType);
          members.Add(_newNode);
        }
        catch (Exception _ex)
        {
          string _msg = String.Format("Error caught while processing the node {0}. The message: {1} at {2}.", _currItem.SymbolicName, _ex.Message, _ex.StackTrace);
          TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, _msg));
        }
      }
    }
    protected Action<TraceMessage> TraceEvent { get; private set; }
    protected List<NodeFactoryBase> m_Nodes = new List<NodeFactoryBase>();

  }
}
