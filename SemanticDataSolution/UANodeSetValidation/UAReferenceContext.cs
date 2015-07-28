
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class UAReferenceContext - encapsulates information about a reference
  /// </summary>
  internal class UAReferenceContext
  {

    #region creator
    internal static UAReferenceContext NewReferenceStub
      (Reference reference, AddressSpaceContext addressSpaceContext, UAModelContext modelContext, UANodeContext parentNode, Action<TraceMessage> traceEvent)
    {
      UANodeContext targetNode = addressSpaceContext.ImportNodeId(reference.Value, modelContext, true, traceEvent);
      UAReferenceContext _stb = new UAReferenceContext()
      {
        m_Context = addressSpaceContext,
        ParentNode = parentNode,
        SourceNode = reference.IsForward ? parentNode : targetNode,
        ModelNode = reference,
        TargetNode = reference.IsForward ? targetNode : parentNode,
        TypeNode = addressSpaceContext.ImportNodeId(reference.ReferenceType, modelContext, true, traceEvent),
      };
      if (_stb.TypeNode != null && _stb.TypeNode.NodeIdContext.NamespaceIndex == 0)
        _stb.ReferenceKind = _stb.GetReferenceKind(_stb.TypeNode);
      return _stb;
    }
    #endregion

    #region IReferenceContext
    /// <summary>
    /// Gets the kind of the reference.
    /// </summary>
    /// <value>The kind of the reference.</value>
    internal ReferenceKindEnum ReferenceKind
    {
      get { return b_ReferenceKindEnum; }
      private set { b_ReferenceKindEnum = value; }
    }
    /// <summary>
    /// Gets the name of the reference type.
    /// </summary>
    /// <param name="modelContext">The model context.</param>
    /// <param name="traceEvent">An <see cref="Action" /> delegate is used to trace event as the <see cref="TraceMessage" />.</param>
    /// <returns>XmlQualifiedName.</returns>
    internal XmlQualifiedName GetReferenceTypeName(Action<TraceMessage> traceEvent)
    {
      if (IsDefault(this.TypeNode.NodeIdContext))
        return null;
      return m_Context.ExportBrowseName(this.TypeNode.NodeIdContext, traceEvent);
    }
    /// <summary>
    /// Returns the name of the reference target and calculates the Target identifier by traversing the components hierarchical path.
    /// </summary>
    /// <param name="reportError">The report error.</param>
    /// <returns><see cref="System.Xml.XmlQualifiedName" />.</returns>
    internal XmlQualifiedName BrowsePath(Action<BuildError> reportError)
    {
      List<string> _path = new List<string>();
      var _startingNode = ModelNode.IsForward ? TargetNode : SourceNode;
      _startingNode.BuildSymbolicId(_path, reportError);
      string _symbolicId = _path.SymbolicName();
      //return new XmlQualifiedName(_symbolicId, m_Context.m_NamespaceTable.GetString(this.TargetNode.NodeIdContext.NamespaceIndex));
      return new XmlQualifiedName(_symbolicId, m_Context.GetNamespace(this.TargetNode.NodeIdContext.NamespaceIndex));
    }
    /// <summary>
    /// Gets or sets the parent node (the reference is originated from) of the reference.
    /// </summary>
    /// <value>The parent node.</value>
    internal ModelingRules? GetModelingRule()
    {
      Debug.Assert(TargetNode.NodeIdContext.IdType == IdType.Numeric_0);
      Debug.Assert(ReferenceKind == ReferenceKindEnum.HasModellingRule);
      int _targetId = TargetNode.NodeIdContext.NamespaceIndex != 0 ? -1 : Convert.ToInt32(TargetNode.NodeIdContext.IdentifierPart);
      ModelingRules? _ret = new Nullable<ModelingRules>();
      if (_targetId == Objects.ModellingRule_Mandatory)
        _ret = ModelingRules.Mandatory;
      else if (_targetId == Objects.ModellingRule_Optional)
        _ret = ModelingRules.Optional;
      else if (_targetId == Objects.ModellingRule_MandatoryPlaceholder)
        _ret = ModelingRules.MandatoryPlaceholder;
      else if (_targetId == Objects.ModellingRule_OptionalPlaceholder)
        _ret = ModelingRules.OptionalPlaceholder;
      else if (_targetId == Objects.ModellingRule_ExposesItsArray)
        _ret = ModelingRules.OptionalPlaceholder;
      return _ret;
    }
    internal UANodeContext ParentNode { get; private set; }
    internal UANodeContext TypeNode { get; private set; }
    internal UANodeContext TargetNode { get; private set; }
    internal UANodeContext SourceNode { get; private set; }
    internal bool ChildConnector { get { return (ReferenceKind == ReferenceKindEnum.HasProperty) || (ReferenceKind == ReferenceKindEnum.HasComponent); } }
    internal bool HierarchicalReference
    {
      get { return b_HierarchicalReference; }
      set { b_HierarchicalReference = value; }
    }
    internal void BuildSymbolicId(List<string> path, Action<BuildError> reportError)
    {
      this.SourceNode.BuildSymbolicId(path, reportError);
    }
    internal string Key
    {
      get
      {
        return String.Format("{0}:{1}:{2}", SourceNode.NodeIdContext.Format(), TypeNode.NodeIdContext.Format(), TargetNode.NodeIdContext.Format());
      }
    }
    /// <summary>
    /// Gets the target node context.
    /// </summary>
    /// <value>The target node context.</value>
    internal UANodeContext TargetNodeContext
    {
      get { return TargetNode; }
    }
    /// <summary>
    /// Gets the source node context.
    /// </summary>
    /// <value>The source node context.</value>
    internal UANodeContext SourceNodeContext
    {
      get { return SourceNode; }
    }
    /// <summary>
    /// Gets the reference.
    /// </summary>
    /// <value>The reference.</value>
    internal Reference Reference
    {
      get { return ModelNode; }
    }
    #endregion

    #region private
    //fields
    private bool b_HierarchicalReference;
    private Reference ModelNode;
    private AddressSpaceContext m_Context;
    private ReferenceKindEnum b_ReferenceKindEnum = ReferenceKindEnum.Custom;
    //methods
    private ReferenceKindEnum GetReferenceKind(UANodeContext TypeNode)
    {
      if (TypeNode.NodeIdContext.NamespaceIndex != 0)
        return ReferenceKindEnum.Custom;
      ReferenceKindEnum _ret = default(ReferenceKindEnum);
      switch (TypeNode.NodeIdContext.UintIdentifier())
      {
        case ReferenceTypes.HierarchicalReferences:
          _ret = ReferenceKindEnum.HierarchicalReferences;
          break;
        case ReferenceTypes.HasComponent:
          _ret = ReferenceKindEnum.HasComponent;
          break;
        case ReferenceTypes.HasProperty:
          _ret = ReferenceKindEnum.HasProperty;
          break;
        case ReferenceTypes.HasModellingRule:
          _ret = ReferenceKindEnum.HasModellingRule;
          break;
        case ReferenceTypes.HasTypeDefinition:
          _ret = ReferenceKindEnum.HasTypeDefinition;
          break;
        case ReferenceTypes.HasSubtype:
          _ret = ReferenceKindEnum.HasSubtype;
          break;
        default:
          _ret = ReferenceKindEnum.Custom;
          break;
      }
      return _ret;
    }
    private bool IsDefault(NodeId node)
    {
      NodeId _default = NodeId.Null;
      switch (ReferenceKind)
      {
        case ReferenceKindEnum.HasComponent:
          _default = ReferenceTypeIds.HasComponent;
          break;
        case ReferenceKindEnum.HasTypeDefinition:
          _default = ReferenceTypeIds.HasTypeDefinition;
          break;
        case ReferenceKindEnum.HasSubtype:
          _default = ReferenceTypeIds.HasSubtype;
          break;
        case ReferenceKindEnum.HasProperty:
          _default = ReferenceTypeIds.HasProperty;
          break;
        default:
          break;
      }
      return _default == node;
    }
    //creator
    private UAReferenceContext() { }
    #endregion

  };

}
