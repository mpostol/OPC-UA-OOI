
using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal static class ModelDesignFactory
  {

    /// <summary>
    /// Creates the model design.
    /// </summary>
    /// <param name="items">The items <see cref="IUANodeContext"/> imported to the Address Space <see cref="IAddressSpaceContext"/>.</param>
    /// <param name="context">The context.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>OldModel.ModelDesign.</returns>
    internal static void CreateModelDesign(IEnumerable<IUANodeContext> items, IExportModelFactory exportFactory, IAddressSpaceContext context, Action<TraceMessage> traceEvent)
    {
      traceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Entering ModelDesignFactory.CreateModelDesign - starting creation of the ModelDesign for {0} nodes.", items.Count<IUANodeContext>())));
      List<BuildError> _errors = new List<BuildError>(); //TODO should be added to the model;
      foreach (string _ns in context.ExportNamespaceTable())
        exportFactory.CreateNamespace(_ns);
      //List<INodeFactory> _mdNodes = new List<INodeFactory>();
      string _msg = null;
      int _nc = 0;
      foreach (IUANodeContext _item in items)
      {
        try
        {
          CreateNodeDesign(exportFactory/*, (x, y) => { return CreateInstanceType(x, y, exportFactory_mdNodes, _item.UAModelContext, traceEvent); }*/, null, _item, y =>
              {
                if (y.TraceLevel != TraceEventType.Verbose)
                  _errors.Add(y.BuildError);
                traceEvent(y);
              });
          _nc++;
          //_mdNodes.Add(_newNode);
        }
        catch (Exception _ex)
        {
          _msg = String.Format("Error caught while processing the node {0}. The message: {1} at {2}.", _item.UANode.NodeId, _ex.Message, _ex.StackTrace);
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, _msg));
        }
      }
      //OldModel.ModelDesign _ret = new OldModel.ModelDesign()
      //  {
      //    Items = _mdNodes.ToArray<INodeFactory>(),
      //    Namespaces = _namespaces,
      //    TargetNamespace = _namespaces[1].Value
      //  };
      if (_errors.Count == 0)
        _msg = String.Format("Finishing ModelDesignFactory.CreateModelDesign - created the ModelDesign containing {0} nodes.", _nc);
      else
        _msg = String.Format("Finishing ModelDesignFactory.CreateModelDesign - created the ModelDesign containing {0} nodes and {1} errors.", _nc, _errors.Count);
      traceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
    }
    internal static IExportNodeFactory CreateNodeDesign(IExportModelFactory exportFactory, IUAReferenceContext parentReference, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      Debug.Assert(nodeContext != null, "UANodeSetFactory.CreateNodeDesign the argument nodeContext is null.");
      IExportNodeFactory _ret = null;
      if (nodeContext.UANode == null)
      {
        TraceMessage _traceMessage = TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, "Compilation Error at CreateNodeDesign");
        traceEvent(_traceMessage);
        _ret = CreateModelDesignStub();
      }
      else
      {
        nodeContext.CalculateNodeReferences(exportFactory, traceEvent);
        string nodeType = nodeContext.UANode.GetType().Name;
        switch (nodeType)
        {
          case "UAReferenceType":
            IExportReferenceTypeFactory _referenceTypeDesign = CreateNode<IExportReferenceTypeFactory, UAReferenceType>(exportFactory, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateType, traceEvent);
            _ret = _referenceTypeDesign;
            break;
          case "UADataType":
            IExportDataTypeFactory _DataTypeDesign = CreateNode<IExportDataTypeFactory, UADataType>(exportFactory, nodeContext, (x, y) => Update(x, y, nodeContext.UAModelContext, traceEvent), UpdateType, traceEvent);
            _ret = _DataTypeDesign;
            break;
          case "UAVariableType":
            IExportVariableTypeFactory _VariableTypeDesign = CreateNode<IExportVariableTypeFactory, UAVariableType>(exportFactory, nodeContext, (x, y) => Update(x, y, nodeContext, traceEvent), UpdateType, traceEvent);
            _ret = _VariableTypeDesign;
            break;
          case "UAObjectType":
            IExportObjectTypeFactory _objectTypeDesign = CreateNode<IExportObjectTypeFactory, UAObjectType>(exportFactory, nodeContext, Update, UpdateType, traceEvent);
            _ret = _objectTypeDesign;
            break;
          case "UAView":
            IExportViewInstanceFactory _ViewDesign = CreateNode<IExportViewInstanceFactory, UAView>(exportFactory, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            _ret = _ViewDesign;
            break;
          case "UAMethod":
            IExportMethodnstanceFactory _MethodDesign = CreateNode<IExportMethodnstanceFactory, UAMethod>(exportFactory, nodeContext, (x, y) => Update(x, y, parentReference, traceEvent), UpdateInstance, traceEvent);
            _ret = _MethodDesign;
            break;
          case "UAVariable":
            IExportVariableInstanceFactory _VariableDesign;
            if (nodeContext.IsProperty)
              _VariableDesign = CreateNode<IExportPropertyInstanceFactory, UAVariable>(exportFactory, nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            else
              _VariableDesign = CreateNode<IExportVariableInstanceFactory, UAVariable>(exportFactory, nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            _ret = _VariableDesign;
            break;
          case "UAObject":
            IExportObjectnstanceFactory _ObjectDesign = CreateNode<IExportObjectnstanceFactory, UAObject>(exportFactory, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            _ret = _ObjectDesign;
            break;
          default:
            Debug.Assert(false, "Wrong node type");
            break;
        }
      }
      return _ret;
    }

    //private
    private static void Update(IExportObjectnstanceFactory nodeDesign, UAObject nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => nodeDesign.SupportsEventsSpecified = x, traceEvent);
    }
    private static void Update(IExportPropertyInstanceFactory nodeDesign, UAVariable nodeSet, IUAReferenceContext parentReference, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      try
      {
        Update(nodeDesign, nodeSet, nodeContext, traceEvent);
        Debug.Assert(parentReference != null);
        if (parentReference == null)
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, "Creating property - wrong reference type"));
        else
        {
          nodeDesign.ReferenceType = parentReference.GetReferenceTypeName(nodeContext.UAModelContext, traceEvent);
          if (parentReference.ReferenceKind != ReferenceKindEnum.HasProperty)
            traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, String.Format("Creating Property - wrong reference type {0}", parentReference.ReferenceKind.ToString())));
        }
      }
      catch (Exception _ex)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, String.Format("Cannot resolve the reference for Property because of error {0} at: {1}.", _ex, _ex.StackTrace)));
      }
    }
    private static void Update(IExportVariableInstanceFactory nodeDesign, UAVariable nodeSet, IUAReferenceContext parentReference, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      try
      {
        Update(nodeDesign, nodeSet, nodeContext, traceEvent);
        Debug.Assert(parentReference != null);
        if (parentReference == null)
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, "Creating Variable - wrong reference type"));
        else
        {
          nodeDesign.ReferenceType = parentReference == null ? null : parentReference.GetReferenceTypeName(nodeContext.UAModelContext, traceEvent);
          if (parentReference.ReferenceKind != ReferenceKindEnum.HasComponent)
            traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Variable, String.Format("Creating Variable - wrong reference type {0}", parentReference.ReferenceKind.ToString())));
        }
      }
      catch (Exception _ex)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, String.Format("Cannot resolve the reference for Variable because of error {0} at: {1}.", _ex, _ex.StackTrace)));
      }
    }
    private static void Update(IExportVariableInstanceFactory nodeDesign, UAVariable nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.AccessLevel = nodeSet.AccessLevel.GetAccessLevel(x => nodeDesign.AccessLevelSpecified = x, traceEvent);
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(String.Empty);
      nodeDesign.DataType = nodeContext.ExportNodeId(nodeSet.DataType, Opc.Ua.DataTypes.Number, traceEvent);//TODO must be DataType, must not be abstract
      nodeDesign.DefaultValue = nodeSet.Value; //TODO must be of type defined by DataType
      nodeDesign.Historizing = nodeSet.Historizing.Export<bool>(false, x => nodeDesign.HistorizingSpecified = x);
      nodeDesign.MinimumSamplingInterval = Convert.ToInt32(nodeSet.MinimumSamplingInterval.Export<double>(0D, x => nodeDesign.MinimumSamplingIntervalSpecified = x));
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(x => nodeDesign.ValueRankSpecified = x, traceEvent);
      if (nodeSet.Translation != null)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "- the Translation element for the UAVariable"));
      nodeSet.UserAccessLevel = nodeSet.AccessLevel.GetAccessLevel(x => nodeDesign.AccessLevelSpecified = x, traceEvent);
    }
    private static void Update(IExportVariableTypeFactory nodeDesign, UAVariableType nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(String.Empty);
      nodeDesign.DataType = nodeContext.ExportNodeId(nodeSet.DataType, DataTypes.Number, traceEvent);
      nodeDesign.DefaultValue = nodeSet.Value;
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(x => nodeDesign.ValueRankSpecified = x, traceEvent);
    }
    private static void Update(IExportMethodnstanceFactory nodeDesign, UAMethod nodeContext, IUAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      //TODO validate parentReference
      nodeDesign.NonExecutable = !nodeContext.Executable;
      nodeDesign.NonExecutableSpecified = !nodeContext.Executable;
    }
    private static void Update(IExportViewInstanceFactory nodeDesign, UAView nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ContainsNoLoops = nodeSet.ContainsNoLoops;//TODO test against the loops.
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => { }, traceEvent);
    }
    private static void Update(IExportDataTypeFactory nodeDesign, UADataType nodeSet, IUAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.Fields = nodeSet.Definition.GetParameters(nodeDesign, modelContext, traceEvent);
    }
    private static void Update(IExportReferenceTypeFactory nodeDesign, UAReferenceType nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.InverseName = nodeSet.InverseName;
      nodeDesign.Symmetric = nodeSet.Symmetric;
      if (nodeSet.Symmetric && (nodeSet.InverseName != null && nodeSet.InverseName.Where(x => !String.IsNullOrEmpty(x.Value)).Any()))
      {
        XML.LocalizedText _notEmpty = nodeSet.InverseName.Where(x => !String.IsNullOrEmpty(x.Value)).First();
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, String.Format("If ReferenceType {0} is symmetric the InverseName {1}:{2} shall be omitted.", nodeSet.NodeIdentifier(), _notEmpty.Locale, _notEmpty.Value)));
      }
      else if (!nodeSet.Symmetric && !nodeSet.IsAbstract && (nodeSet.InverseName == null || !nodeSet.InverseName.Where(x => !String.IsNullOrEmpty(x.Value)).Any()))
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, String.Format("If ReferenceType {0} is not symmetric and not abstract the InverseName shall be specified.", nodeSet.NodeIdentifier())));
    }
    private static void Update(IExportObjectTypeFactory nodeDesign, UAObjectType nodeSet)
    {
      //nodeDesign.SupportsEvents = false;
      //nodeDesign.SupportsEventsSpecified = false;
    }
    //Validation
    private static ModelDesignType CreateNode<ModelDesignType, NodeSetType>
      (
      IExportModelFactory exportFactory,
      IUANodeContext nodeContext,
        Action<ModelDesignType, NodeSetType> updateNode,
        Action<ModelDesignType, NodeSetType, IUANodeContext, Action<TraceMessage>> updateBase,
        Action<TraceMessage> traceEvent)
      where ModelDesignType : IExportNodeFactory
      where NodeSetType : UANode
    {
      ModelDesignType _nodeDesign = exportFactory.NewExportNodeFFactory<ModelDesignType>();
      NodeSetType _nodeSet = (NodeSetType)nodeContext.UANode;
      XmlQualifiedName _browseName = nodeContext.ExportNodeBrowseName(traceEvent);
      string _symbolicName;
      if (String.IsNullOrEmpty(_nodeSet.SymbolicName))
        _symbolicName = _browseName.Name.ValidateIdentifier(traceEvent);
      else
        _symbolicName = _nodeSet.SymbolicName.ValidateIdentifier(traceEvent);
      _nodeDesign.BrowseName = _browseName.Name.ExportString(_symbolicName);
      _nodeDesign.Children = nodeContext.Children;
      _nodeDesign.Description = _nodeSet.Description;
      _nodeDesign.DisplayName = _nodeSet.DisplayName.Truncate(512, traceEvent);
      _nodeDesign.References = nodeContext.References;
      _nodeDesign.SymbolicName = new XmlQualifiedName(_symbolicName, _browseName.Namespace);
      Action<UInt32, string> _doReport = (x, y) =>
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongWriteMaskValue, String.Format("The current value is {0:x} of the node type {1}.", x, y)));
      };
      _nodeDesign.WriteAccess = _nodeSet is UAVariable ? _nodeSet.WriteMask.Validate(0x200000, x => _doReport(x, _nodeSet.GetType().Name)) : _nodeSet.WriteMask.Validate(0x400000, x => _doReport(x, _nodeSet.GetType().Name));
      updateBase(_nodeDesign, _nodeSet, nodeContext, traceEvent);
      updateNode(_nodeDesign, _nodeSet);
      return _nodeDesign;
    }
    private static void UpdateType(IExportTypeFactory nodeDesign, UAType nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.BaseType = nodeContext.BaseType(traceEvent);
      nodeDesign.IsAbstract = nodeSet.IsAbstract;
    }
    private static void UpdateInstance(IExportInstanceFactory nodeDesign, UAInstance nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ModelingRule = nodeContext.ModelingRule;
      nodeDesign.TypeDefinition = nodeContext.BaseType(traceEvent);
      //nodeSet.ParentNodeId - The NodeId of the Node that is the parent of the Node within the information model. This field is used to indicate 
      //that a tight coupling exists between the Node and its parent (e.g. when the parent is deleted the child is deleted 
      //as well). This information does not appear in the AddressSpace and is intended for use by design tools.
    }
    //TODO creation of stub should depend on the reference type.
    private static IExportNodeFactory CreateModelDesignStub()
    {
      //BuildError _err = BuildError.DanglingReferenceTarget;
      //return new IExportPropertyInstanceFactory()
      //  {
      //    BrowseName = _err.Focus.ToString(),
      //    Description = new OldModel.LocalizedText() { Value = _err.Descriptor, Key = "en-en" },
      //    DisplayName = new OldModel.LocalizedText() { Value = "ERROR", Key = "en-en" },
      //    PartNo = 3,
      //  };
      throw new NotImplementedException("CreateModelDesignStub");
    }
    private static int m_Count = 0;

  }
}
