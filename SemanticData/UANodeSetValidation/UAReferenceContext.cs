//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    #region constructor

    internal UAReferenceContext(Reference reference, IAddressSpaceBuildContext addressSpaceContext, IUANodeContext parentNode)
    {
      if (reference == null)
        throw new ArgumentNullException(nameof(reference));
      this._AddressSpace = addressSpaceContext ?? throw new ArgumentNullException(nameof(addressSpaceContext));
      if (parentNode == null)
        throw new ArgumentNullException(nameof(parentNode));
      IUANodeContext targetNode = _AddressSpace.GetOrCreateNodeContext(reference.ValueNodeId, parentNode.CreateUANodeContext);
      this.IsForward = reference.IsForward;
      this.SourceNode = reference.IsForward ? parentNode : targetNode;
      this.TargetNode = reference.IsForward ? targetNode : parentNode;
      this.TypeNode = addressSpaceContext.GetOrCreateNodeContext(reference.ReferenceTypeNodeid, parentNode.CreateUANodeContext);
    }

    #endregion constructor

    #region API

    #region semantics

    /// <summary>
    /// Gets the kind of the reference.
    /// </summary>
    /// <value>The kind of the reference.</value>
    internal ReferenceKindEnum ReferenceKind
    {
      get
      {
        if (_ReferenceKindEnum == null)
          _ReferenceKindEnum = CalculateReferenceKind();
        return _ReferenceKindEnum.Value;
      }
    }

    /// <summary>
    /// Gets the modeling rule.
    /// </summary>
    /// <returns>System.Nullable{ModelingRules}.</returns>
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
    /// <value><c>true</c> if it is child reference; otherwise, <c>false</c>.</value>
    //TODO NetworkIdentifier is missing in generated Model Design for DI model #629
    //TODO The exported model doesn't contain all nodes #653
    internal bool ChildConnector => (ReferenceKind == ReferenceKindEnum.HasProperty) || (ReferenceKind == ReferenceKindEnum.HasComponent);

    #endregion semantics

    #region naming

    /// <summary>
    /// Gets the name of the reference type.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName"/> capturing name of the reference type.</returns>
    internal XmlQualifiedName GetReferenceTypeName()
    {
      return _AddressSpace.ExportBrowseName(this.TypeNode.NodeIdContext, GetDefault());
    }

    /// <summary>
    /// Calculates the browse path starting from the node pointed out by this reference. If <see cref="XML.Reference.IsForward"/> the <see cref="UAReferenceContext.TargetNode"/> is use,  <see cref="UAReferenceContext.SourceNode"/> otherwise.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the browse path.</returns>
    internal XmlQualifiedName BrowsePath()
    {
      List<string> _path = new List<string>();
      IUANodeContext _startingNode = this.IsForward ? TargetNode : SourceNode;
      _startingNode.BuildSymbolicId(_path);
      string _symbolicId = _path.SymbolicName();
      return new XmlQualifiedName(_symbolicId, _AddressSpace.GetNamespace(_startingNode.NodeIdContext.NamespaceIndex));
    }

    /// <summary>
    /// It recursively builds the symbolic identifier.
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
    private IAddressSpaceBuildContext _AddressSpace;

    private ReferenceKindEnum? _ReferenceKindEnum = new Nullable<ReferenceKindEnum>();

    //methods
    private ReferenceKindEnum CalculateReferenceKind()
    {
      if ((TypeNode == null) || TypeNode.NodeIdContext.NamespaceIndex != 0)
        return ReferenceKindEnum.Custom;
      ReferenceKindEnum _ret = default(ReferenceKindEnum);
      List<IUANodeContext> inheritanceChain = new List<IUANodeContext>();
      _AddressSpace.GetBaseTypes(TypeNode, inheritanceChain);
      if (inheritanceChain.Where<IUANodeContext>(x => x.NodeIdContext == ReferenceTypeIds.HasProperty).Any<IUANodeContext>())
        _ret = ReferenceKindEnum.HasProperty;
      else if (inheritanceChain.Where<IUANodeContext>(x => x.NodeIdContext == ReferenceTypeIds.HasComponent).Any<IUANodeContext>())
        _ret = ReferenceKindEnum.HasComponent;
      else if (inheritanceChain.Where<IUANodeContext>(x => x.NodeIdContext == ReferenceTypeIds.HasSubtype).Any<IUANodeContext>())
        _ret = ReferenceKindEnum.HasSubtype;
      else if (inheritanceChain.Where<IUANodeContext>(x => x.NodeIdContext == ReferenceTypeIds.HasTypeDefinition).Any<IUANodeContext>())
        _ret = ReferenceKindEnum.HasTypeDefinition;
      else if (inheritanceChain.Where<IUANodeContext>(x => x.NodeIdContext == ReferenceTypeIds.HasModellingRule).Any<IUANodeContext>())
        _ret = ReferenceKindEnum.HasModellingRule;
      else
        _ret = ReferenceKindEnum.Custom;
      return _ret;
    }

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