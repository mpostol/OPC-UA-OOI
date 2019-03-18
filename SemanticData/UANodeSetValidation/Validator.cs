//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
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

    #region internal API
    /// <summary>
    /// Validates the selected nodes <paramref name="nodesCollection"/> and export it using <paramref name="exportModelFactory"/>.
    /// </summary>
    /// <param name="nodesCollection">The items <see cref="UANodeContext" /> imported to the Address Space <see cref="IAddressSpaceContext" />.</param>
    /// <param name="exportModelFactory">The model export factory.</param>
    /// <param name="addressSpaceContext">The Address Space context.</param>
    /// <param name="traceEvent">The trace event method encapsulation.</param>
    internal static void ValidateExportModel
      (IEnumerable<UANodeContext> nodesCollection, IModelFactory exportModelFactory, AddressSpaceContext addressSpaceContext, Action<TraceMessage> traceEvent)
    {
      traceEvent(TraceMessage.DiagnosticTraceMessage(string.Format("Entering Validator.ValidateExportModel - starting creation of the ModelDesign for {0} nodes.", nodesCollection.Count<UANodeContext>())));
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
          _msg = string.Format("Error caught while processing the node {0}. The message: {1} at {2}.", _item.UANode.NodeId, _ex.Message, _ex.StackTrace);
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, _msg));
        }
      }
      if (_errors.Count == 0)
        _msg = string.Format("Finishing Validator.ValidateExportModel - the model contains {0} nodes.", _nc);
      else
        _msg = string.Format("Finishing Validator.ValidateExportModel - the model contains {0} nodes and {1} errors.", _nc, _errors.Count);
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
      Debug.Assert(nodeContext != null, "Validator.ValidateExportNode the argument nodeContext is null.");
      //TODO Handle HasComponent ReferenceType errors. #42
      if (nodeContext.UANode == null)
      {
        string _msg = string.Format("The node {0} is undefined", nodeContext.NodeIdContext);
        BuildError _be = null;
        if (parentReference == null || parentReference.ReferenceKind == ReferenceKindEnum.HasProperty)
          _be = BuildError.UndefinedHasPropertyTarget;
        else
          _be = BuildError.UndefinedHasComponentTarget;
        TraceMessage _traceMessage = TraceMessage.BuildErrorTraceMessage(_be, _msg);
        traceEvent(_traceMessage);
        CreateModelDesignStub(exportFactory);
      }
      else
      {
        string nodeType = nodeContext.UANode.GetType().Name;
        switch (nodeType)
        {
          case "UAReferenceType":
            CreateNode<IReferenceTypeFactory, UAReferenceType>(exportFactory.AddNodeFactory<IReferenceTypeFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateType, traceEvent);
            break;
          case "UADataType":
            CreateNode<IDataTypeFactory, UADataType>(exportFactory.AddNodeFactory<IDataTypeFactory>, nodeContext, (x, y) => Update(x, y, nodeContext.UAModelContext, traceEvent), UpdateType, traceEvent);
            break;
          case "UAVariableType":
            CreateNode<IVariableTypeFactory, UAVariableType>(exportFactory.AddNodeFactory<IVariableTypeFactory>, nodeContext, (x, y) => Update(x, y, nodeContext, traceEvent), UpdateType, traceEvent);
            break;
          case "UAObjectType":
            CreateNode<IObjectTypeFactory, UAObjectType>(exportFactory.AddNodeFactory<IObjectTypeFactory>, nodeContext, Update, UpdateType, traceEvent);
            break;
          case "UAView":
            CreateNode<IViewInstanceFactory, UAView>(exportFactory.AddNodeFactory<IViewInstanceFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAMethod":
            CreateNode<IMethodInstanceFactory, UAMethod>(exportFactory.AddNodeFactory<IMethodInstanceFactory>, nodeContext, (x, y) => Update(x, y, nodeContext, parentReference, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAVariable":
            if (parentReference == null || parentReference.ReferenceKind == ReferenceKindEnum.HasProperty)
              CreateNode<IPropertyInstanceFactory, UAVariable>(exportFactory.AddNodeFactory<IPropertyInstanceFactory>, nodeContext, (x, y) => Update(x, y, nodeContext, parentReference, traceEvent), UpdateInstance, traceEvent);
            else
              CreateNode<IVariableInstanceFactory, UAVariable>(exportFactory.AddNodeFactory<IVariableInstanceFactory>, nodeContext, (x, y) => Update(x, y, nodeContext, parentReference, traceEvent), UpdateInstance, traceEvent);
            break;
          case "UAObject":
            CreateNode<IObjectInstanceFactory, UAObject>(exportFactory.AddNodeFactory<IObjectInstanceFactory>, nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            break;
          default:
            Debug.Assert(false, "Wrong node type");
            break;
        }
      }
    }
    #endregion

    #region private
    private static void Update(IObjectInstanceFactory nodeDesign, UAObject nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(traceEvent);
    }
    private static void Update(IPropertyInstanceFactory propertyInstance, UAVariable nodeSet, UANodeContext nodeContext, UAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      try
      {
        Update(propertyInstance, nodeSet, nodeContext, traceEvent);
        propertyInstance.ReferenceType = parentReference == null ? null : parentReference.GetReferenceTypeName(traceEvent);
        if (!nodeContext.IsProperty)
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, string.Format("Creating Property - wrong reference type {0}", parentReference.ReferenceKind.ToString())));
      }
      catch (Exception _ex)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, string.Format("Cannot resolve the reference for Property because of error {0} at: {1}.", _ex, _ex.StackTrace)));
      }
    }
    private static void Update(IVariableInstanceFactory variableInstance, UAVariable nodeSet, UANodeContext nodeContext, UAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      try
      {
        Update(variableInstance, nodeSet, nodeContext, traceEvent);
        variableInstance.ReferenceType = parentReference == null ? null : parentReference.GetReferenceTypeName(traceEvent);
        if (nodeContext.IsProperty)
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Variable, string.Format("Creating Variable - wrong reference type {0}", parentReference.ReferenceKind.ToString())));
      }
      catch (Exception _ex)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Property, string.Format("Cannot resolve the reference for Variable because of error {0} at: {1}.", _ex, _ex.StackTrace)));
      }
    }
    private static void Update(IVariableInstanceFactory nodeDesign, UAVariable nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.AccessLevel = nodeSet.AccessLevel.GetAccessLevel(traceEvent);
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(string.Empty);
      nodeDesign.DataType = nodeContext.ExportBrowseName(nodeSet.DataType, DataTypes.Number, traceEvent);//TODO add test case must be DataType, must not be abstract
      nodeDesign.DefaultValue = nodeSet.Value; //TODO add test case must be of type defined by DataType
      nodeDesign.Historizing = nodeSet.Historizing.Export(false);
      nodeDesign.MinimumSamplingInterval = nodeSet.MinimumSamplingInterval.Export(0D);
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(traceEvent);
      if (nodeSet.Translation != null)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "- the Translation element for the UAVariable"));
    }
    private static void Update(IVariableTypeFactory nodeDesign, UAVariableType nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(string.Empty);
      nodeDesign.DataType = nodeContext.ExportBrowseName(nodeSet.DataType, DataTypes.Number, traceEvent);
      nodeDesign.DefaultValue = nodeSet.Value;
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(traceEvent);
    }
    private static void Update(IMethodInstanceFactory nodeDesign, UAMethod nodeSet, UANodeContext nodeContext, UAReferenceContext parentReference, Action<TraceMessage> traceEvent)
    {
      //TODO add test case validate parentReference
      if (nodeSet.ArgumentDescription != null)
        foreach (UAMethodArgument _argument in nodeSet.ArgumentDescription)
        {
          if (_argument.Description == null)
            continue;
          foreach (XML.LocalizedText _description in _argument.Description)
            nodeDesign.AddArgumentDescription(_argument.Name, _description.Locale, _description.Value );
        }
      nodeDesign.Executable = !nodeSet.Executable ? nodeSet.Executable : new Nullable<bool>();
      nodeDesign.UserExecutable = !nodeSet.UserExecutable ? nodeSet.UserExecutable : new Nullable<bool>();
      nodeDesign.AddInputArguments(x => nodeContext.GetParameters(x, traceEvent));
      nodeDesign.AddOutputArguments(x => nodeContext.GetParameters(x, traceEvent));
      //MethodDeclarationId is ignored
    }
    private static void Update(IViewInstanceFactory nodeDesign, UAView nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ContainsNoLoops = nodeSet.ContainsNoLoops;//TODO add test case against the loops in the model.
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(traceEvent);
    }
    private static void Update(IDataTypeFactory nodeDesign, UADataType nodeSet, UAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      nodeSet.Definition.GetParameters(nodeDesign.NewDefinition(), modelContext, traceEvent);
      if (nodeSet.Purpose != DataTypePurpose.Normal)
        traceEvent(TraceMessage.DiagnosticTraceMessage($"DataTypePurpose value {nodeSet.Purpose } is not supported by the tool"));
    }
    private static void Update(IReferenceTypeFactory nodeDesign, UAReferenceType nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeSet.InverseName.ExportLocalizedTextArray(nodeDesign.AddInverseName);
      nodeDesign.Symmetric = nodeSet.Symmetric;
      if (nodeSet.Symmetric && (nodeSet.InverseName != null && nodeSet.InverseName.Where(x => !string.IsNullOrEmpty(x.Value)).Any()))
      {
        XML.LocalizedText _notEmpty = nodeSet.InverseName.Where(x => !string.IsNullOrEmpty(x.Value)).First();
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, string.Format("If ReferenceType {0} is symmetric the InverseName {1}:{2} shall be omitted.", nodeSet.NodeIdentifier(), _notEmpty.Locale, _notEmpty.Value)));
      }
      else if (!nodeSet.Symmetric && !nodeSet.IsAbstract && (nodeSet.InverseName == null || !nodeSet.InverseName.Where(x => !string.IsNullOrEmpty(x.Value)).Any()))
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, string.Format("If ReferenceType {0} is not symmetric and not abstract the InverseName shall be specified.", nodeSet.NodeIdentifier())));
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
      XmlQualifiedName _browseName = nodeContext.ExportNodeBrowseName();
      string _symbolicName;
      if (string.IsNullOrEmpty(_nodeSet.SymbolicName))
        _symbolicName = _browseName.Name.ValidateIdentifier(traceEvent);
      else
        _symbolicName = _nodeSet.SymbolicName.ValidateIdentifier(traceEvent);
      _nodeFactory.BrowseName = _browseName.Name.ExportString(_symbolicName);
      _nodeSet.Description.ExportLocalizedTextArray(_nodeFactory.AddDescription);
      _nodeSet.DisplayName.Truncate(512, traceEvent).ExportLocalizedTextArray(_nodeFactory.AddDisplayName);
      _nodeFactory.SymbolicName = new XmlQualifiedName(_symbolicName, _browseName.Namespace);
      Action<uint, string> _doReport = (x, y) =>
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongWriteMaskValue, string.Format("The current value is {0:x} of the node type {1}.", x, y)));
      };
      _nodeFactory.WriteAccess = _nodeSet is UAVariable ? _nodeSet.WriteMask.Validate(0x200000, x => _doReport(x, _nodeSet.GetType().Name)) : _nodeSet.WriteMask.Validate(0x400000, x => _doReport(x, _nodeSet.GetType().Name));
      _nodeFactory.AccessRestrictions = ConvertToAccessRestrictions(_nodeSet.AccessRestrictions, _nodeSet.GetType().Name, traceEvent);
      if (_nodeSet.RolePermissions != null)
        traceEvent(TraceMessage.DiagnosticTraceMessage("RolePermissions is not supported. You must fix it manually."));
      if (!string.IsNullOrEmpty(_nodeSet.Documentation))
        traceEvent(TraceMessage.DiagnosticTraceMessage("Documentation is not supported. You must fix it manually."));
      updateBase(_nodeFactory, _nodeSet, nodeContext, traceEvent);
      updateNode(_nodeFactory, _nodeSet);
      return _nodeFactory;
    }
    private static AccessRestrictions ConvertToAccessRestrictions(byte accessRestrictions, string typeName, Action<TraceMessage> traceEvent)
    {
      if (accessRestrictions > 7)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongAccessLevel, $"The current value is {accessRestrictions} of the node type {typeName}. Assigned max value"));
        return AccessRestrictions.EncryptionRequired & AccessRestrictions.SessionRequired & AccessRestrictions.SigningRequired;
      }
      return (AccessRestrictions)accessRestrictions;
    }
    private static void UpdateType(ITypeFactory nodeDesign, UAType nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.BaseType = nodeContext.ExportBaseTypeBrowseName(true, traceEvent);
      nodeDesign.IsAbstract = nodeSet.IsAbstract;
    }
    private static void UpdateInstance(IInstanceFactory nodeDesign, UAInstance nodeSet, UANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ModelingRule = nodeContext.ModelingRule;
      nodeDesign.TypeDefinition = nodeContext.ExportBaseTypeBrowseName(false, traceEvent);
      //nodeSet.ParentNodeId - The NodeId of the Node that is the parent of the Node within the information model. This field is used to indicate 
      //that a tight coupling exists between the Node and its parent (e.g. when the parent is deleted the child is deleted 
      //as well). This information does not appear in the AddressSpace and is intended for use by design tools.
    }
    private static void CreateModelDesignStub(INodeContainer factory)
    {
      BuildError _err = BuildError.DanglingReferenceTarget;
      IPropertyInstanceFactory _pr = factory.AddNodeFactory<IPropertyInstanceFactory>();
      _pr.SymbolicName = new XmlQualifiedName(string.Format("{0}{1}", _err.Focus.ToString(), m_ErrorNumber++), "http://commsvr.com/OOIUA/SemanticData/UANodeSetValidation");
      _pr.AddDescription("en-en", _err.Descriptor);
      _pr.AddDisplayName("en-en", string.Format("ERROR{0}", m_ErrorNumber));
    }
    private static int m_ErrorNumber = 0;
    #endregion

  }

}
