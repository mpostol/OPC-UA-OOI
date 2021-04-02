//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
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
using System.Linq;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class UAReferenceContext - encapsulates information about a reference
  /// </summary>
  public class UAReferenceContext
  {
    //TODO UAReferenceContext - causes circular references #558

    #region constructor

    internal UAReferenceContext(Reference reference, IAddressSpaceBuildContext addressSpaceContext, IUANodeContext parentNode)
    {
      if (reference == null)
        throw new ArgumentNullException(nameof(reference));
      this.m_AddressSpace = addressSpaceContext ?? throw new ArgumentNullException(nameof(addressSpaceContext));
      if (parentNode == null)
        throw new ArgumentNullException(nameof(parentNode));
      IUANodeContext targetNode = m_AddressSpace.GetOrCreateNodeContext(reference.ValueNodeId, parentNode.CreateUANodeContext);
      this.IsForward = reference.IsForward;
      this.SourceNode = reference.IsForward ? parentNode : targetNode;
      this.TargetNode = reference.IsForward ? targetNode : parentNode;
      this.TypeNode = addressSpaceContext.GetOrCreateNodeContext(reference.ReferenceTypeNodeid, parentNode.CreateUANodeContext);
    }

    #endregion constructor

    #region API

    #region semantics

    //TODO UAReferenceContext - causes circular references #558
    internal bool IsSubtypeOf(NodeId referenceType)
    {
      List<IUANodeContext> inheritanceChain = new List<IUANodeContext>();
      m_AddressSpace.GetBaseTypes(TypeNode, inheritanceChain);
      return inheritanceChain.Where<IUANodeContext>(x => x.NodeIdContext == referenceType).Any<IUANodeContext>();
    }

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
    /// Gets the modeling rule.
    /// </summary>
    /// <returns>System.Nullable&lt;ModelingRules&gt;.</returns>
    internal ModelingRules? GetModelingRule()
    {
      Debug.Assert(TargetNode.NodeIdContext.IdType == IdType.Numeric_0);
      //Debug.Assert(ReferenceKind == ReferenceKindEnum.HasModellingRule);
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

    /// <summary>
    /// Gets a value indicating whether the reference has been derived form <see cref="ReferenceKindEnum.HasProperty"/> or <see cref="ReferenceKindEnum.HasComponent"/>.
    /// </summary>
    /// <value><c>true</c> if is child reference; otherwise, <c>false</c>.</value>
    internal bool ChildConnector => IsSubtypeOf(ReferenceTypeIds.Aggregates);

    #endregion semantics

    #region naming

    /// <summary>
    /// Gets the name of the reference type.
    /// </summary>
    /// <returns>XmlQualifiedName.</returns>
    internal XmlQualifiedName GetReferenceTypeName()
    {
      return m_AddressSpace.ExportBrowseName(this.TypeNode.NodeIdContext, GetDefault());
    }

    /// <summary>
    /// Calculates the browse path starting from the node pointed out by this reference. If <see cref="XML.Reference.IsForward"/> is <c>true</c> <see cref="UAReferenceContext.TargetNode"/> is use,  <see cref="UAReferenceContext.SourceNode"/> otherwise.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the browse path.</returns>
    internal XmlQualifiedName BrowsePath()
    {
      List<string> _path = new List<string>();
      IUANodeContext _startingNode = this.IsForward ? TargetNode : SourceNode;
      _startingNode.BuildSymbolicId(_path);
      string _symbolicId = _path.SymbolicName();
      return new XmlQualifiedName(_symbolicId, m_AddressSpace.GetNamespace(_startingNode.NodeIdContext.NamespaceIndex));
    }

    /// <summary>
    /// Ir recursively builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    internal void BuildSymbolicId(List<string> path)
    {
      this.SourceNode.BuildSymbolicId(path);
    }

    #endregion naming

    #region navigation

    /// <summary>
    /// Gets the parent node that the reference is attached to.
    /// </summary>
    /// <value>An instance of the <see cref="IUANodeContext"/> of the parent node.</value>
    internal IUANodeContext ParentNode => IsForward ? SourceNode : TargetNode;

    /// <summary>
    /// Gets the type node.
    /// </summary>
    /// <value>An instance of <see cref="IUANodeContext "/> that captures information about a node representing type of the reference.</value>
    internal IUANodeContext TypeNode { get; private set; }

    /// <summary>
    /// Gets the target node.
    /// </summary>
    /// <value>An instance of <see cref="IUANodeContext "/> that captures information about a target node.</value>
    internal IUANodeContext TargetNode { get; private set; }

    /// <summary>
    /// Gets the source node context.
    /// </summary>
    /// <value>An instance of <see cref="IUANodeContext "/> that captures information about a source node.</value>
    internal IUANodeContext SourceNode { get; private set; }

    /// <summary>
    /// Gets the key.
    /// </summary>
    /// <value>The key.</value>
    internal string Key => string.Format("{0}:{1}:{2}", SourceNode.NodeIdContext.Format(), TypeNode.NodeIdContext.Format(), TargetNode.NodeIdContext.Format());

    /// <summary>
    /// Gets a value indicating whether this instance is forward.
    /// </summary>
    /// <value><c>true</c> if this instance is forward; otherwise, <c>false</c>.</value>
    internal bool IsForward { get; private set; }

    #endregion navigation

    #endregion API

    #region private

    //fields
    private IAddressSpaceBuildContext m_AddressSpace;

    //methods
    private NodeId GetDefault()
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
      return _default;
    }

    #endregion private
  };
}