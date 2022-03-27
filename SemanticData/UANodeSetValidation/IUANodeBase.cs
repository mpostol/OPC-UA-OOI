//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IUANodeBase - if implemented captures all basic information represented by the UA Node
  /// </summary>
  internal interface IUANodeBase : IEquatable<IUANodeBase>
  {
    /// <summary>
    /// Calculates the node references.
    /// </summary>
    /// <param name="nodeFactory">The node factory.</param>
    /// <param name="allNodesInConcern">list of selected members to export.</param>
    /// <param name="validator">The validator.</param>
    /// <param name="validateExportNode2Model">It creates the node at the top level of the model. Called if the node has reference to another node that cannot be defined as a child.</param>
    void CalculateNodeReferences(INodeFactory nodeFactory, List<IUANodeBase> allNodesInConcern, IValidator validator, Action<IUANodeContext> validateExportNode2Model);

    /// <summary>
    /// Gets the node identifier.
    /// </summary>
    /// <value>The imported node identifier.</value>
    NodeId NodeIdContext { get; }

    /// <summary>
    /// Gets the wrapped node described by the <see cref="UANode"/> type.
    /// </summary>
    UANode UANode { get; }

    /// <summary>
    /// Exports the browse name of the wrapped node by this instance.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the browse name of the node.</returns>
    XmlQualifiedName ExportNodeBrowseName();

    /// <summary>
    /// Gets a value indicating whether this instance is a property.
    /// </summary>
    /// <value><c>true</c> if this instance is property; otherwise, <c>false</c>.</value>
    bool IsProperty { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is property variable type.
    /// </summary>
    /// <value><c>true</c> if this instance is property variable type; otherwise, <c>false</c>.</value>
    bool IsPropertyVariableType { get; }

    /// <summary>
    /// Exports the browse name of this node recognized as <see cref="ReferenceKindEnum.HasSubtype"/> or <see cref="ReferenceKindEnum.HasTypeDefinition"/> target.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>An instance of <see cref="XmlQualifiedName"/> representing subtype or type of an instance.</returns>
    XmlQualifiedName ExportBrowseNameBaseType(Action<NodeId> traceEvent);

    /// <summary>
    /// Gets the derived instances.
    /// </summary>
    NodesCollection GetDerivedInstances();

    /// <summary>
    ///  Exports the BrowseName of the BaseType.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the base type..</returns>
    XmlQualifiedName ExportBaseTypeBrowseName();

    /// <summary>
    /// Gets the modeling rule associated with this node.
    /// </summary>
    /// <value>The <see cref="ModelingRules"/> associated with the node. Null if valid modeling rule cannot be recognized.</value>
    ModelingRules? ModelingRule { get; }

    /// <summary>
    /// Removes the inherited values.
    /// </summary>
    /// <param name="instanceDeclaration">The instance declaration.</param>
    /// <remarks>
    /// If a member is overridden all inherited values of the node attributes must be removed.
    /// </remarks>
    void RemoveInheritedValues(IUANodeBase instanceDeclaration);
  }
}