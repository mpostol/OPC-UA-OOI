
using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

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
    internal static OldModel.ModelDesign CreateModelDesign(IEnumerable<IUANodeContext> items, IExportModelFactory exportFactory, IAddressSpaceContext context, Action<TraceMessage> traceEvent)
    {
      traceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Entering ModelDesignFactory.CreateModelDesign - starting creation of the ModelDesign for {0} nodes.", items.Count<IUANodeContext>())));
      List<BuildError> _errors = new List<BuildError>(); //TODO should be added to the model;
      foreach (srting  _ns in collection)
        exportFactory.ExportNamespace(_ns);
      //List<OldModel.NodeDesign> _mdNodes = new List<OldModel.NodeDesign>();
      string _msg = null;
      foreach (IUANodeContext _item in items)
      {
        try
        {
          OldModel.NodeDesign _newNode = CreateNodeDesign((x, y) => { return CreateInstanceType(x, y, _mdNodes, _item.UAModelContext, traceEvent); }, null, _item, y =>
              {
                if (y.TraceLevel != TraceEventType.Verbose)
                  _errors.Add(y.BuildError);
                traceEvent(y);
              });
          _mdNodes.Add(_newNode);
        }
        catch (Exception _ex)
        {
          _msg = String.Format("Error caught while processing the node {0}. The message: {1} at {2}.", _item.UANode.NodeId, _ex.Message, _ex.StackTrace);
          traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, _msg));
        }
      }
      OldModel.ModelDesign _ret = new OldModel.ModelDesign()
        {
          Items = _mdNodes.ToArray<OldModel.NodeDesign>(),
          Namespaces = _namespaces,
          TargetNamespace = _namespaces[1].Value
        };
      if (_errors.Count == 0)
        _msg = String.Format("Finishing ModelDesignFactory.CreateModelDesign - created the ModelDesign containing {0} nodes.", _mdNodes.Count<OldModel.NodeDesign>());
      else
        _msg = String.Format("Finishing ModelDesignFactory.CreateModelDesign - created the ModelDesign containing {0} nodes and {1} errors.", _mdNodes.Count<OldModel.NodeDesign>(), _errors.Count);
      traceEvent(TraceMessage.DiagnosticTraceMessage(_msg));
      return _ret;
    }
    internal static OldModel.NodeDesign CreateNodeDesign
      (Func<OldModel.InstanceDesign, List<string>, OldModel.InstanceDesign> createType, IUAReferenceContext parentReference, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      Debug.Assert(nodeContext != null, "UANodeSetFactory.CreateNodeDesign the argument nodeContext is null.");
      OldModel.NodeDesign _ret = null;
      if (nodeContext.UANode == null)
      {
        _ret = CreateModelDesignStub();
        BuildError _err = BuildError.DanglingReferenceTarget;
        traceEvent(TraceMessage.BuildErrorTraceMessage(_err, "Compilation Error at CreateNodeDesign"));
      }
      else
      {
        nodeContext.CalculateNodeReferences(createType, traceEvent);
        string nodeType = nodeContext.UANode.GetType().Name;
        switch (nodeType)
        {
          case "UAReferenceType":
            OldModel.ReferenceTypeDesign _referenceTypeDesign = CreateNode<OldModel.ReferenceTypeDesign, UAReferenceType>(nodeContext, (x, y) => Update(x, y, traceEvent), UpdateType, traceEvent);
            _ret = _referenceTypeDesign;
            break;
          case "UADataType":
            OldModel.DataTypeDesign _DataTypeDesign = CreateNode<OldModel.DataTypeDesign, UADataType>(nodeContext, (x, y) => Update(x, y, nodeContext.UAModelContext, traceEvent), UpdateType, traceEvent);
            _ret = _DataTypeDesign;
            break;
          case "UAVariableType":
            OldModel.VariableTypeDesign _VariableTypeDesign = CreateNode<OldModel.VariableTypeDesign, UAVariableType>(nodeContext, (x, y) => Update(x, y, nodeContext, traceEvent), UpdateType, traceEvent);
            _ret = _VariableTypeDesign;
            break;
          case "UAObjectType":
            OldModel.ObjectTypeDesign _objectTypeDesign = CreateNode<OldModel.ObjectTypeDesign, UAObjectType>(nodeContext, Update, UpdateType, traceEvent);
            _ret = _objectTypeDesign;
            break;
          case "UAView":
            OldModel.ViewDesign _ViewDesign = CreateNode<OldModel.ViewDesign, UAView>(nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
            _ret = _ViewDesign;
            break;
          case "UAMethod":
            OldModel.MethodDesign _MethodDesign = CreateNode<OldModel.MethodDesign, UAMethod>(nodeContext, Update, UpdateInstance, traceEvent);
            if (parentReference == null)
              _ret = _MethodDesign;
            else
            {
              if (parentReference.ReferenceKind == ReferenceKindEnum.HasProperty)
                traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongReference2Method, "Creating method component - HasProperty cannot be used."));
              List<string> _symbolicName = new List<string>();
              _symbolicName.Add(nodeContext.BranchName);
              _ret = createType(_MethodDesign, _symbolicName);
            }
            break;
          case "UAVariable":
            OldModel.VariableDesign _VariableDesign;
            if (nodeContext.IsProperty)
              _VariableDesign = CreateNode<OldModel.PropertyDesign, UAVariable>(nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            else
              _VariableDesign = CreateNode<OldModel.VariableDesign, UAVariable>(nodeContext, (x, y) => Update(x, y, parentReference, nodeContext, traceEvent), UpdateInstance, traceEvent);
            _ret = _VariableDesign;
            break;
          case "UAObject":
            OldModel.ObjectDesign _ObjectDesign = CreateNode<OldModel.ObjectDesign, UAObject>(nodeContext, (x, y) => Update(x, y, traceEvent), UpdateInstance, traceEvent);
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
    private static void Update(OldModel.ObjectDesign nodeDesign, UAObject nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => nodeDesign.SupportsEventsSpecified = x, traceEvent);
    }
    private static void Update(OldModel.PropertyDesign nodeDesign, UAVariable nodeSet, IUAReferenceContext parentReference, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
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
    private static void Update(OldModel.VariableDesign nodeDesign, UAVariable nodeSet, IUAReferenceContext parentReference, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
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
    private static void Update(OldModel.VariableDesign nodeDesign, UAVariable nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
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
      // nodeSet.UserAccessLevel;
    }
    private static void Update(OldModel.VariableTypeDesign nodeDesign, UAVariableType nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ArrayDimensions = nodeSet.ArrayDimensions.ExportString(String.Empty);
      nodeDesign.DataType = nodeContext.ExportNodeId(nodeSet.DataType, DataTypes.Number, traceEvent);
      nodeDesign.DefaultValue = nodeSet.Value;
      nodeDesign.ValueRank = nodeSet.ValueRank.GetValueRank(x => nodeDesign.ValueRankSpecified = x, traceEvent);
      //Not supported by the VariableType NodeClass 
      nodeDesign.ExposesItsChildren = false; // It is not attribute 
      nodeDesign.AccessLevel = OldModel.AccessLevel.ReadWrite;
      nodeDesign.AccessLevelSpecified = false;
      nodeDesign.Historizing = false;
      nodeDesign.HistorizingSpecified = false;
      nodeDesign.MinimumSamplingInterval = 0;
      nodeDesign.MinimumSamplingIntervalSpecified = false;
    }
    private static void Update(OldModel.MethodDesign nodeDesign, UAMethod nodeContext)
    {
      nodeDesign.InputArguments = null;
      nodeDesign.OutputArguments = null;
      nodeDesign.NonExecutable = !nodeContext.Executable;
      nodeDesign.NonExecutableSpecified = !nodeContext.Executable;
    }
    private static void Update(OldModel.ViewDesign nodeDesign, UAView nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.ContainsNoLoops = nodeSet.ContainsNoLoops;//TODO test against the loops.
      nodeDesign.SupportsEvents = nodeSet.EventNotifier.GetSupportsEvents(x => { }, traceEvent);
    }
    private static void Update(OldModel.DataTypeDesign nodeDesign, UADataType nodeSet, IUAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.Fields = nodeSet.Definition.GetParameters(modelContext, traceEvent);
      nodeDesign.Encodings = null; //Not supported
      nodeDesign.NoArraysAllowed = false; //Not supported
      nodeDesign.NotInAddressSpace = false; //Not supported
    }
    private static void Update(OldModel.ReferenceTypeDesign nodeDesign, UAReferenceType nodeSet, Action<TraceMessage> traceEvent)
    {
      nodeDesign.InverseName = nodeSet.InverseName.GetModelCompilerLocalizedText();
      nodeDesign.Symmetric = nodeSet.Symmetric;
      nodeDesign.SymmetricSpecified = nodeSet.Symmetric;
      if (nodeDesign.Symmetric && (nodeDesign.InverseName != null && !String.IsNullOrEmpty(nodeDesign.InverseName.Value)))
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, String.Format("If ReferenceType {0} is symmetric the InverseName {1} shall be omitted.", nodeSet.NodeIdentifier(), nodeSet.InverseName.ConvertToString())));
      else if (!nodeDesign.Symmetric && !nodeSet.IsAbstract && (nodeDesign.InverseName == null || String.IsNullOrEmpty(nodeDesign.InverseName.Value)))
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongInverseName, String.Format("If ReferenceType {0} is not symmetric and not abstract the InverseName shall be specified.", nodeSet.NodeIdentifier())));
    }
    private static void Update(OldModel.ObjectTypeDesign nodeDesign, UAObjectType nodeSet)
    {
      nodeDesign.SupportsEvents = false;
      nodeDesign.SupportsEventsSpecified = false;
    }
    //Validation
    private static ModelDesignType CreateNode<ModelDesignType, NodeSetType>
      (
        IUANodeContext nodeContext,
        Action<ModelDesignType, NodeSetType> updateNode,
        Action<ModelDesignType, NodeSetType, IUANodeContext, Action<TraceMessage>> updateBase,
        Action<TraceMessage> traceEvent)
      where ModelDesignType : OldModel.NodeDesign, new()
      where NodeSetType : UANode
    {
      ModelDesignType _nodeDesign = new ModelDesignType();
      NodeSetType _nodeSet = (NodeSetType)nodeContext.UANode;
      XmlQualifiedName _browseName = nodeContext.ExportNodeBrowseName(traceEvent);
      string _symbolicName;
      if (String.IsNullOrEmpty(_nodeSet.SymbolicName))
        _symbolicName = _browseName.Name.ValidateIdentifier(traceEvent);
      else
        _symbolicName = _nodeSet.SymbolicName.ValidateIdentifier(traceEvent);
      _nodeDesign.BrowseName = _browseName.Name.ExportString(_symbolicName);
      _nodeDesign.Children = nodeContext.Children;
      _nodeDesign.Description = _nodeSet.Description.GetModelCompilerLocalizedText();
      _nodeDesign.DisplayName = _nodeSet.DisplayName.GetModelCompilerLocalizedText(_browseName.Name).Truncate(512, traceEvent);
      _nodeDesign.IsDeclaration = false;
      _nodeDesign.NumericId = 0;
      _nodeDesign.NumericIdSpecified = false;
      _nodeDesign.References = nodeContext.References;
      _nodeDesign.StringId = null;
      _nodeDesign.SymbolicId = null;
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
    private static void UpdateType(OldModel.TypeDesign nodeDesign, UAType nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.BaseType = nodeContext.BaseType(traceEvent);
      nodeDesign.ClassName = String.Empty;
      nodeDesign.IsAbstract = nodeSet.IsAbstract;
      nodeDesign.NoClassGeneration = false;
    }
    private static void UpdateInstance(OldModel.InstanceDesign nodeDesign, UAInstance nodeSet, IUANodeContext nodeContext, Action<TraceMessage> traceEvent)
    {
      nodeDesign.Declaration = null;
      nodeDesign.MaxCardinality = 0;
      nodeDesign.MinCardinality = 0;
      nodeDesign.ModellingRule = nodeContext.ModellingRule.GetValueOrDefault(OldModel.ModellingRule.None);
      nodeDesign.ModellingRuleSpecified = nodeContext.ModellingRule.HasValue;
      nodeDesign.PreserveDefaultAttributes = false;
      //mn.ReferenceType = references.ReferenceType;
      nodeDesign.TypeDefinition = nodeContext.BaseType(traceEvent);
      //nodeSet.ParentNodeId - The NodeId of the Node that is the parent of the Node within the information model. This field is used to indicate 
      //that a tight coupling exists between the Node and its parent (e.g. when the parent is deleted the child is deleted 
      //as well). This information does not appear in the AddressSpace and is intended for use by design tools.
    }
    //private static OldModel.Namespace CreateNamespace(string uri)
    //{
    //  return new OldModel.Namespace()
    //  {
    //    FilePath = string.Empty,
    //    InternalPrefix = uri,
    //    Name = String.Format("Name{0}", m_Count),
    //    Prefix = String.Format("Prefix{0}", m_Count++),
    //    Value = uri,
    //    XmlNamespace = uri,
    //    XmlPrefix = String.Format("Prefix{0}", m_Count++)
    //  };
    //}
    private static OldModel.InstanceDesign CreateInstanceType(OldModel.InstanceDesign instance, List<string> browsePath, List<OldModel.NodeDesign> mdNodes, IUAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      Debug.Assert(instance != null, "CreateInstanceType.instance cannot be null");
      mdNodes.Add(instance);
      OldModel.InstanceDesign _ret = null;
      if (instance is OldModel.MethodDesign)
      {
        OldModel.MethodDesign _src = (OldModel.MethodDesign)instance;
        XmlQualifiedName _newSymbolicName = new XmlQualifiedName(browsePath.SymbolicName(), instance.SymbolicName.Namespace);
        OldModel.MethodDesign _method = new OldModel.MethodDesign
        {
          BrowseName = _src.BrowseName,
          Children = null,
          Declaration = _src.Declaration,
          Description = _src.Description,
          DisplayName = _src.DisplayName,
          InputArguments = null,
          IsDeclaration = _src.IsDeclaration,
          MaxCardinality = _src.MaxCardinality,
          MinCardinality = _src.MinCardinality,
          ModellingRule = _src.ModellingRule,
          ModellingRuleSpecified = _src.ModellingRuleSpecified,
          NonExecutable = _src.NonExecutable,
          NonExecutableSpecified = _src.NonExecutableSpecified,
          NumericId = _src.NumericId,
          NumericIdSpecified = _src.NumericIdSpecified,
          OutputArguments = null,
          PartNo = _src.PartNo,
          PreserveDefaultAttributes = _src.PreserveDefaultAttributes,
          References = _src.References,
          ReferenceType = _src.ReferenceType,
          StringId = _src.StringId,
          SymbolicId = _src.SymbolicId,
          SymbolicName = _src.SymbolicName,
          TypeDefinition = _newSymbolicName,
          WriteAccess = _src.WriteAccess,
        };
        _src.InputArguments = GetParameters(instance.Children, Opc.Ua.BrowseNames.InputArguments, modelContext, traceEvent);
        _src.OutputArguments = GetParameters(instance.Children, Opc.Ua.BrowseNames.OutputArguments, modelContext, traceEvent);
        if (instance.Children == null || instance.Children.Items == null || instance.Children.Items.Length == 0)
          instance.Children = null;
        _src.ModellingRule = OldModel.ModellingRule.None;
        _src.ModellingRuleSpecified = false;
        _src.SymbolicName = _newSymbolicName;
        _ret = _method;
      }
      else
        Debug.Fail("In this release expected Method");
      return _ret;
    }
    private static OldModel.Parameter[] GetParameters(OldModel.ListOfChildren listOfChildren, string parameterKind, IUAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      if (listOfChildren == null || listOfChildren.Items == null)
        return null;
      List<OldModel.InstanceDesign> _newChildrenCollection = new List<OldModel.InstanceDesign>();
      List<OldModel.Parameter> _parameters = new List<OldModel.Parameter>();
      foreach (OldModel.InstanceDesign _item in listOfChildren.Items)
      {
        if (_item.SymbolicName.Equals(new XmlQualifiedName(parameterKind, Opc.Ua.Namespaces.OpcUa)))
        {
          OldModel.VariableDesign _arg = (OldModel.VariableDesign)_item;
          foreach (CAS.UA.Common.Types.Argument _argument in _arg.DefaultValue.GetParameters())
            _parameters.Add(modelContext.ExportArgument(_argument, traceEvent));
        }
        else
          _newChildrenCollection.Add(_item);
      }
      listOfChildren.Items = _newChildrenCollection.Count == 0 ? null : _newChildrenCollection.ToArray();
      return _parameters.ToArray<OldModel.Parameter>();
    }
    //TODO creation of stub should depend on the reference type.
    private static OldModel.NodeDesign CreateModelDesignStub()
    {
      BuildError _err = BuildError.DanglingReferenceTarget;
      return new OldModel.PropertyDesign()
        {
          BrowseName = _err.Focus.ToString(),
          Description = new OldModel.LocalizedText() { Value = _err.Descriptor, Key = "en-en" },
          DisplayName = new OldModel.LocalizedText() { Value = "ERROR", Key = "en-en" },
          PartNo = 3,
        };
    }
    private static int m_Count = 0;

  }
}
