
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class Validator - contains static methods used to validate and export a collection of nodes - part of the Address Space.
  /// </summary>
  internal static class Validator
  {

    /// <summary>
    /// Validates the selected nodes <paramref name="nodesCollection"/> and export it using <paramref name="exportModelFactory"/>.
    /// </summary>
    /// <param name="nodesCollection">The items <see cref="UANodeContext" /> imported to the Address Space <see cref="IAddressSpaceContext" />.</param>
    /// <param name="exportModelFactory">The model export factory.</param>
    /// <param name="addressSpaceContext">The Address Space context.</param>
    /// <param name="traceEvent">The trace event method encapsulation.</param>
    internal static void ValidateExportModel(IEnumerable<UANodeContext> nodesCollection, IModelFactory exportModelFactory, AddressSpaceContext addressSpaceContext, Action<TraceMessage> traceEvent)
    {
      traceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Entering ModelDesignFactory.CreateModelDesign - starting creation of the ModelDesign for {0} nodes.", nodesCollection.Count<UANodeContext>())));
      List<BuildError> _errors = new List<BuildError>(); //TODO should be added to the model;
      foreach (string _ns in addressSpaceContext.ExportNamespaceTable())
        exportModelFactory.CreateNamespace(_ns);
      string _msg = null;
      int _nc = 0;
      foreach (UANodeContext _item in nodesCollection)
      {
        try
        {
          ValidateExportNode(_item, exportModelFactory, null, y =>
              {
                if (y.TraceLevel != TraceEventType.Verbose)
                  _errors.Add(y.BuildError);
                traceEvent(y);
              });
          _nc++;
        }
        catch (Exception _ex)
        {
          _msg = String.Format("Error caught while processing the node {0}. The message: {1} at {2}.", _item.UANode.NodeId, _ex.Message, _ex.StackTrace);
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, _msg));
        }
      }
      if (_errors.Count == 0)
        _msg = String.Format("Finishing ModelDesignFactory.CreateModelDesign - created the ModelDesign containing {0} nodes.", _nc);
      else
        _msg = String.Format("Finishing ModelDesignFactory.CreateModelDesign - created the ModelDesign containing {0} nodes and {1} errors.", _nc, _errors.Count);
      traceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
    }
    /// <summary>
    /// Validates <paramref name="nodeContext"/> and exports it using an object od <see cref="IModelFactory"/>.
    /// </summary>
    /// <param name="nodeContext">The node context to be validated and exported.</param>
    /// <param name="exportFactory">A model export factory.</param>
    /// <param name="parentReference">The reference to parent node.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>An object of <see cref="INodeFactory"/>.</returns>
    internal static void ValidateExportNode(UANodeContext nodeContext, INodeContainer exportFactory, UAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      Debug.Assert(nodeContext != null, "UANodeSetFactory.CreateNodeDesign the argument nodeContext is null.");
      if (nodeContext.UANode == null)
      {
        TraceMessage _traceMessage = TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, "Compilation Error at CreateNodeDesign");
        traceEvent(_traceMessage);
        CreateModelDesignStub(exportFactory);
      }
      else
      {
        string nodeType = nodeContext.UANode.GetType().Name;
        switch (nodeType)
        {
          case "UAReferenceType":
            CreateNode<IReferenceTypeFactory, UAReferenceType>(exportFactory.NewExportNodeFFactory<IReferenceTypeFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateType, traceEvent);
            break;
          case "UADataType":
            CreateNode<IDataTypeFactory, UADataType>(exportFactory.NewExportNodeFFactory<IDataTypeFactory>, nodeContext, (x, y) => Update(x, y, nodeContext.UAModelContext, traceEvent), UpdateType, traceEvent);
            break;
          case "UAVariableType":
            CreateNode<IVariableTypeFactory, UAVariableType>(exportFactory.NewExportNodeFFactory<IVariableTypeFactory>, nodeContext, (x, y) => Update(x, y, nodeContext, traceEvent), UpdateType, traceEvent);
            break;
          case "UAObjectType":
            CreateNode<IObjectTypeFactory, UAObjectType>(exportFactory.NewExportNodeFFactory<IObjectTypeFactory>, nodeContext, Update, UpdateType, traceEvent);
            break;
          case "UAView":
            CreateNode<IViewInstanceFactory, UAView>(exportFactory.NewExportNodeFFactory<IViewInstanceFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAMethod":
            CreateNode<IMethodInstanceFactory, UAMethod>(exportFactory.NewExportNodeFFactory<IMethodInstanceFactory>, nodeContext, (x, y) => Update(x, y, parentReference, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAVariable":
            if (parentReference == null || parentReference.ReferenceKind == ReferenceKindEnum.HasProperty)
              CreateNode<IPropertyInstanceFactory, UAVariable>(exportFactory.NewExportNodeFFactory<IPropertyInstanceFactory>, nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            else
              CreateNode<IVariableInstanceFactory, UAVariable>(exportFactory.NewExportNodeFFactory<IVariableInstanceFactory>, nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAObject":
            CreateNode<IObjectInstanceFactory, UAObject>(exportFactory.NewExportNodeFFactory<IObjectInstanceFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            break;
          default:
            Debug.Assert(false, "Wrong node type");
            break;
        }
      }
    }

    #region private
    private static void Update(IObjectInstanceFactory nodeDesign, UAObject nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => nodeDesign.SupportsEventsSpecified = x, traceEvent);
    }
    private static void Update(IPropertyInstanceFactory nodeDesign, UAVariable nodeSet, UAReferenceContext parentReference, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      try
      {
        Update(nodeDesign, nodeSet, nodeContext, traceEvent);
        nodeDesign.ReferenceType = parentReference == null ? null : parentReference.GetReferenceTypeName(nodeContext.UAModelContext, traceEvent);
        if (!nodeContext.IsProperty)
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, String.Format("Creating Property - wrong reference type {0}", parentReference.ReferenceKind.ToString())));
      }
      catch (Exception _ex)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, String.Format("Cannot resolve the reference for Property because of error {0} at: {1}.", _ex, _ex.StackTrace)));
      }
    }
    private static void Update(IVariableInstanceFactory nodeDesign, UAVariable nodeSet, UAReferenceContext parentReference, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      try
      {
        Update(nodeDesign, nodeSet, nodeContext, traceEvent);
        nodeDesign.ReferenceType = parentReference == null ? null : parentReference.GetReferenceTypeName(nodeContext.UAModelContext, traceEvent);
        if (nodeContext.IsProperty)
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Variable, String.Format("Creating Variable - wrong reference type {0}", parentReference.ReferenceKind.ToString())));
      }
      catch (Exception _ex)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, String.Format("Cannot resolve the reference for Variable because of error {0} at: {1}.", _ex, _ex.StackTrace)));
      }
    }
    private static void Update(IVariableInstanceFactory nodeDesign, UAVariable nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.AccessLevel = nodeSet.AccessLevel.GetAccessLevel(x => nodeDesign.AccessLevelSpecified = x, traceEvent);
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(String.Empty);
      nodeDesign.DataType = nodeContext.ExportNodeId(nodeSet.DataType, DataTypes.Number, traceEvent);//TODO add test case must be DataType, must not be abstract
      nodeDesign.DefaultValue = nodeSet.Value; //TODO add test case must be of type defined by DataType
      nodeDesign.Historizing = nodeSet.Historizing.Export<bool>(false, x => nodeDesign.HistorizingSpecified = x);
      nodeDesign.MinimumSamplingInterval = Convert.ToInt32(nodeSet.MinimumSamplingInterval.Export<double>(0D, x => nodeDesign.MinimumSamplingIntervalSpecified = x));
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(x => nodeDesign.ValueRankSpecified = x, traceEvent);
      if (nodeSet.Translation != null)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "- the Translation element for the UAVariable"));
    }
    private static void Update(IVariableTypeFactory nodeDesign, UAVariableType nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(String.Empty);
      nodeDesign.DataType = nodeContext.ExportNodeId(nodeSet.DataType, DataTypes.Number, traceEvent);
      nodeDesign.DefaultValue = nodeSet.Value;
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(x => nodeDesign.ValueRankSpecified = x, traceEvent);
    }
    private static void Update(IMethodInstanceFactory nodeDesign, UAMethod nodeContext, UAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      //TODO add test case validate parentReference
      nodeDesign.NonExecutable = !nodeContext.Executable;
      nodeDesign.NonExecutableSpecified = !nodeContext.Executable;
    }
    private static void Update(IViewInstanceFactory nodeDesign, UAView nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ContainsNoLoops = nodeSet.ContainsNoLoops;//TODO add test case against the loops in the model.
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => { }, traceEvent);
    }
    private static void Update(IDataTypeFactory nodeDesign, UADataType nodeSet, UAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      nodeSet.Definition.GetParameters(nodeDesign.NewDefinition(), modelContext, traceEvent);
    }
    private static void Update(IReferenceTypeFactory nodeDesign, UAReferenceType nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeSet.InverseName.ExportLocalizedTextArray(nodeDesign.AddInverseName);
      nodeDesign.Symmetric = nodeSet.Symmetric;
      if (nodeSet.Symmetric && (nodeSet.InverseName != null && nodeSet.InverseName.Where(x => !String.IsNullOrEmpty(x.Value)).Any()))
      {
        XML.LocalizedText _notEmpty = nodeSet.InverseName.Where(x => !String.IsNullOrEmpty(x.Value)).First();
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, String.Format("If ReferenceType {0} is symmetric the InverseName {1}:{2} shall be omitted.", nodeSet.NodeIdentifier(), _notEmpty.Locale, _notEmpty.Value)));
      }
      else if (!nodeSet.Symmetric && !nodeSet.IsAbstract && (nodeSet.InverseName == null || !nodeSet.InverseName.Where(x => !String.IsNullOrEmpty(x.Value)).Any()))
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, String.Format("If ReferenceType {0} is not symmetric and not abstract the InverseName shall be specified.", nodeSet.NodeIdentifier())));
    }
    private static void Update(IObjectTypeFactory nodeDesign, UAObjectType nodeSet) { }
    private static FactoryType CreateNode<FactoryType, NodeSetType>
      (
        Func<FactoryType> createNode,
        UANodeContext nodeContext,
        Action<FactoryType, NodeSetType> updateNode,
        Action<FactoryType, NodeSetType, UANodeContext, Action<TraceMessage>> updateBase,
        Action<TraceMessage> traceEvent
      )
      where FactoryType : INodeFactory
      where NodeSetType : UANode
    {
      FactoryType _nodeFactory = createNode();
      nodeContext.CalculateNodeReferences(_nodeFactory, traceEvent);
      NodeSetType _nodeSet = (NodeSetType)nodeContext.UANode;
      XmlQualifiedName _browseName = nodeContext.ExportNodeBrowseName(traceEvent);
      string _symbolicName;
      if (String.IsNullOrEmpty(_nodeSet.SymbolicName))
        _symbolicName = _browseName.Name.ValidateIdentifier(traceEvent);
      else
        _symbolicName = _nodeSet.SymbolicName.ValidateIdentifier(traceEvent);
      _nodeFactory.BrowseName = _browseName.Name.ExportString(_symbolicName);
      _nodeSet.Description.ExportLocalizedTextArray(_nodeFactory.AddDescription);
      _nodeSet.DisplayName.Truncate(512, traceEvent).ExportLocalizedTextArray(_nodeFactory.AddDisplayName);
      _nodeFactory.SymbolicName = new XmlQualifiedName(_symbolicName, _browseName.Namespace);
      Action<UInt32, string> _doReport = (x, y) =>
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongWriteMaskValue, String.Format("The current value is {0:x} of the node type {1}.", x, y)));
      };
      _nodeFactory.WriteAccess = _nodeSet is UAVariable ? _nodeSet.WriteMask.Validate(0x200000, x => _doReport(x, _nodeSet.GetType().Name)) : _nodeSet.WriteMask.Validate(0x400000, x => _doReport(x, _nodeSet.GetType().Name));
      updateBase(_nodeFactory, _nodeSet, nodeContext, traceEvent);
      updateNode(_nodeFactory, _nodeSet);
      return _nodeFactory;
    }
    private static void UpdateType(ITypeFactory nodeDesign, UAType nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.BaseType = nodeContext.BaseType(traceEvent);
      nodeDesign.IsAbstract = nodeSet.IsAbstract;
    }
    private static void UpdateInstance(IInstanceFactory nodeDesign, UAInstance nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ModelingRule = nodeContext.ModelingRule;
      nodeDesign.TypeDefinition = nodeContext.BaseType(traceEvent);
      //nodeSet.ParentNodeId - The NodeId of the Node that is the parent of the Node within the information model. This field is used to indicate 
      //that a tight coupling exists between the Node and its parent (e.g. when the parent is deleted the child is deleted 
      //as well). This information does not appear in the AddressSpace and is intended for use by design tools.
    }
    private static void CreateModelDesignStub(INodeContainer factory)
    {
      BuildError _err = BuildError.DanglingReferenceTarget;
      IPropertyInstanceFactory _pr = factory.NewExportNodeFFactory<IPropertyInstanceFactory>();
      _pr.BrowseName = string.Format("{0}: #{1}", _err.Focus.ToString(), m_ErrorNumber++);
      _pr.AddDescription("en-en", _err.Descriptor);
      _pr.AddDisplayName("en-en", "ERROR");
    }
    private static int m_ErrorNumber = 0;
    #endregion

  }
}
