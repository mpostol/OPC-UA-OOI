
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
    /// <param name="nodesCollection">The items <see cref="IUANodeContext" /> imported to the Address Space <see cref="IAddressSpaceContext" />.</param>
    /// <param name="exportModelFactory">The model export factory.</param>
    /// <param name="addressSpaceContext">The Address Space context.</param>
    /// <param name="traceEvent">The trace event method encapsulation.</param>
    internal static void ValidateExportModel(IEnumerable<UANodeContext> nodesCollection, IExportModelFactory exportModelFactory, AddressSpaceContext addressSpaceContext, Action<TraceMessage> traceEvent)
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
    /// Validates <paramref name="nodeContext"/> and exports it using an object od <see cref="IExportModelFactory"/>.
    /// </summary>
    /// <param name="nodeContext">The node context to be validated and exported.</param>
    /// <param name="exportFactory">A model export factory.</param>
    /// <param name="parentReference">The reference to parent node.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>An object of <see cref="IExportNodeFactory"/>.</returns>
    internal static void ValidateExportNode(UANodeContext nodeContext, IExportNodeContainer exportFactory, UAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      Debug.Assert(nodeContext != null, "UANodeSetFactory.CreateNodeDesign the argument nodeContext is null.");
      if (nodeContext.UANode == null)
      {
        TraceMessage _traceMessage = TraceMessage.BuildErrorTraceMessage(BuildError.DanglingReferenceTarget, "Compilation Error at CreateNodeDesign");
        traceEvent(_traceMessage);
        CreateModelDesignStub(); //TODO must be implemented
      }
      else
      {
        string nodeType = nodeContext.UANode.GetType().Name;
        switch (nodeType)
        {
          case "UAReferenceType":
            CreateNode<IExportReferenceTypeFactory, UAReferenceType>(exportFactory.NewExportNodeFFactory<IExportReferenceTypeFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateType, traceEvent);
            break;
          case "UADataType":
            CreateNode<IExportDataTypeFactory, UADataType>(exportFactory.NewExportNodeFFactory<IExportDataTypeFactory>, nodeContext, (x, y) => Update(x, y, nodeContext.UAModelContext, traceEvent), UpdateType, traceEvent);
            break;
          case "UAVariableType":
            CreateNode<IExportVariableTypeFactory, UAVariableType>(exportFactory.NewExportNodeFFactory<IExportVariableTypeFactory>, nodeContext, (x, y) => Update(x, y, nodeContext, traceEvent), UpdateType, traceEvent);
            break;
          case "UAObjectType":
            CreateNode<IExportObjectTypeFactory, UAObjectType>(exportFactory.NewExportNodeFFactory<IExportObjectTypeFactory>, nodeContext, Update, UpdateType, traceEvent);
            break;
          case "UAView":
            CreateNode<IExportViewInstanceFactory, UAView>(exportFactory.NewExportNodeFFactory<IExportViewInstanceFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAMethod":
            CreateNode<IExportMethodInstanceFactory, UAMethod>(exportFactory.NewExportNodeFFactory<IExportMethodInstanceFactory>, nodeContext, (x, y) => Update(x, y, parentReference, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAVariable":
            if (nodeContext.IsProperty)
              CreateNode<IExportPropertyInstanceFactory, UAVariable>(exportFactory.NewExportNodeFFactory<IExportPropertyInstanceFactory>, nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            else
              CreateNode<IExportVariableInstanceFactory, UAVariable>(exportFactory.NewExportNodeFFactory<IExportVariableInstanceFactory>, nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAObject":
            CreateNode<IExportObjectInstanceFactory, UAObject>(exportFactory.NewExportNodeFFactory<IExportObjectInstanceFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            break;
          default:
            Debug.Assert(false, "Wrong node type");
            break;
        }
      }
    }

    //private
    private static void Update(IExportObjectInstanceFactory nodeDesign, UAObject nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => nodeDesign.SupportsEventsSpecified = x, traceEvent);
    }
    private static void Update(IExportPropertyInstanceFactory nodeDesign, UAVariable nodeSet, UAReferenceContext parentReference, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
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
    private static void Update(IExportVariableInstanceFactory nodeDesign, UAVariable nodeSet, UAReferenceContext parentReference, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
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
    private static void Update(IExportVariableInstanceFactory nodeDesign, UAVariable nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.AccessLevel = nodeSet.AccessLevel.GetAccessLevel(x => nodeDesign.AccessLevelSpecified = x, traceEvent);
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(String.Empty);
      nodeDesign.DataType = nodeContext.ExportNodeId(nodeSet.DataType, DataTypes.Number, traceEvent);//TODO add testcase must be DataType, must not be abstract
      nodeDesign.DefaultValue = nodeSet.Value; //TODO add testcase must be of type defined by DataType
      nodeDesign.Historizing = nodeSet.Historizing.Export<bool>(false, x => nodeDesign.HistorizingSpecified = x);
      nodeDesign.MinimumSamplingInterval = Convert.ToInt32(nodeSet.MinimumSamplingInterval.Export<double>(0D, x => nodeDesign.MinimumSamplingIntervalSpecified = x));
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(x => nodeDesign.ValueRankSpecified = x, traceEvent);
      if (nodeSet.Translation != null)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "- the Translation element for the UAVariable"));
      nodeSet.UserAccessLevel = nodeSet.AccessLevel.GetAccessLevel(x => nodeDesign.AccessLevelSpecified = x, traceEvent);
    }
    private static void Update(IExportVariableTypeFactory nodeDesign, UAVariableType nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(String.Empty);
      nodeDesign.DataType = nodeContext.ExportNodeId(nodeSet.DataType, DataTypes.Number, traceEvent);
      nodeDesign.DefaultValue = nodeSet.Value;
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(x => nodeDesign.ValueRankSpecified = x, traceEvent);
    }
    private static void Update(IExportMethodInstanceFactory nodeDesign, UAMethod nodeContext, UAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      //TODO add test case validate parentReference
      nodeDesign.NonExecutable = !nodeContext.Executable;
      nodeDesign.NonExecutableSpecified = !nodeContext.Executable;
    }
    private static void Update(IExportViewInstanceFactory nodeDesign, UAView nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ContainsNoLoops = nodeSet.ContainsNoLoops;//TODO add test case against the loops in the model.
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => { }, traceEvent);
    }
    private static void Update(IExportDataTypeFactory nodeDesign, UADataType nodeSet, IUAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      nodeSet.Definition.GetParameters(nodeDesign.NewDefinition(), modelContext, traceEvent);
    }
    private static void Update(IExportReferenceTypeFactory nodeDesign, UAReferenceType nodeSet, Action<TraceMessage> traceEvent)
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
    private static void Update(IExportObjectTypeFactory nodeDesign, UAObjectType nodeSet) { }
    //Validation
    private static FactoryType CreateNode<FactoryType, NodeSetType>
      (
        Func<FactoryType> createNode,
        UANodeContext nodeContext,
        Action<FactoryType, NodeSetType> updateNode,
        Action<FactoryType, NodeSetType, UANodeContext, Action<TraceMessage>> updateBase,
        Action<TraceMessage> traceEvent
      )
      where FactoryType : IExportNodeFactory
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
    private static void UpdateType(IExportTypeFactory nodeDesign, UAType nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.BaseType = nodeContext.BaseType(traceEvent);
      nodeDesign.IsAbstract = nodeSet.IsAbstract;
    }
    private static void UpdateInstance(IExportInstanceFactory nodeDesign, UAInstance nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
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

  }
}
