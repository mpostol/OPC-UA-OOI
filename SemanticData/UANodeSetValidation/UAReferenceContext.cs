//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
  public class UAReferenceContext
  {

    #region constructor
    internal UAReferenceContext(Reference reference, IAddressSpaceBuildContext addressSpaceContext, IUAModelContext modelContext, IUANodeContext parentNode)
    {
      IUANodeContext targetNode = modelContext.GetOrCreateNodeContext(reference.Value);
      this.m_AddressSpace = addressSpaceContext;
      this.ParentNode = parentNode;
      this.SourceNode = reference.IsForward ? parentNode : targetNode;
      this.Reference = reference;
      this.TargetNode = reference.IsForward ? targetNode : parentNode;
      this.TypeNode = modelContext.GetOrCreateNodeContext(reference.ReferenceType);
    }
    #endregion

    #region API
    /// <summary>
    /// Gets the kind of the reference.
    /// </summary>
    /// <value>The kind of the reference.</value>
    internal ReferenceKindEnum ReferenceKind
    {
      get
      {
        if ((TypeNode == null) || TypeNode.NodeIdContext.NamespaceIndex != 0)
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
    }
    /// <summary>
    /// Gets the name of the reference type.
    /// </summary>
    /// <returns>XmlQualifiedName.</returns>
    internal XmlQualifiedName GetReferenceTypeName()
    {
      if (IsDefault(this.TypeNode.NodeIdContext))
        return null;
      return m_AddressSpace.ExportBrowseName(this.TypeNode.NodeIdContext);
    }
    /// <summary>
    /// Calculates the browse path starting from the node pointed out by this reference. If <see cref="XML.Reference.IsForward"/> is <c>true</c> <see cref="UAReferenceContext.TargetNode"/> is use,  <see cref="UAReferenceContext.SourceNode"/> otherwise. 
    /// </summary>
    /// <returns>An instance of <see cref="System.Xml.XmlQualifiedName" /> representing the browse path.</returns>
    internal XmlQualifiedName BrowsePath()
    {
      List<string> _path = new List<string>();
      IUANodeContext _startingNode = this.Reference.IsForward ? TargetNode : SourceNode;
      _startingNode.BuildSymbolicId(_path);
      string _symbolicId = _path.SymbolicName();
      //return new XmlQualifiedName(_symbolicId, m_Context.m_NamespaceTable.GetString(this.TargetNode.NodeIdContext.NamespaceIndex));
      return new XmlQualifiedName(_symbolicId, m_AddressSpace.GetNamespace(this.TargetNode.NodeIdContext.NamespaceIndex));
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
    internal IUANodeContext ParentNode { get; private set; }
    /// <summary>
    /// Gets the target node context.
    /// </summary>
    /// <value>The target node context.</value>
    internal IUANodeContext TypeNode { get; private set; }
    internal IUANodeContext TargetNode { get; private set; }
    /// <summary>
    /// Gets the source node context.
    /// </summary>
    /// <value>The source node context.</value>
    internal IUANodeContext SourceNode { get; private set; }
    internal bool ChildConnector => (ReferenceKind == ReferenceKindEnum.HasProperty) || (ReferenceKind == ReferenceKindEnum.HasComponent);
    /// <summary>
    /// Ir recursively builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    internal void BuildSymbolicId(List<string> path)
    {
      this.SourceNode.BuildSymbolicId(path);
    }
    internal string Key => string.Format("{0}:{1}:{2}", SourceNode.NodeIdContext.Format(), TypeNode.NodeIdContext.Format(), TargetNode.NodeIdContext.Format());
    /// <summary>
    /// Gets the reference.
    /// </summary>
    /// <value>The reference.</value>
    internal Reference Reference { get; private set; }
    #endregion

    #region private
    //fields
    private IAddressSpaceBuildContext m_AddressSpace;
    //methods
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
    #endregion

  };

}
