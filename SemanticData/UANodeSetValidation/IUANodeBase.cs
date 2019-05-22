//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
  internal interface IUANodeBase: IEquatable<IUANodeBase>
  {

    /// <summary>
    /// Calculates the node references.
    /// </summary>
    /// <param name="nodeFactory">The node factory.</param>
    void CalculateNodeReferences(INodeFactory nodeFactory);
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
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the <see cref="BrowseName"/> of the node.</returns>
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
    /// Exports the browse name of the base type.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    XmlQualifiedName ExportBrowseNameBaseType(Action<NodeId> traceEvent);
    /// <summary>
    /// Gets the derived instances.
    /// </summary>
    Dictionary<string, IUANodeBase> GetDerivedInstances();
    /// <summary>
    /// Gets or sets the browse name of this node.
    /// </summary>
    QualifiedName BrowseName { get; set; }
    /// <summary>
    /// Gets the instance of <see cref="IUAModelContext" />containing definition of this node.
    /// </summary>
    /// <value>The model context for this node.</value>
    IUAModelContext UAModelContext { get; }
    /// <summary>
    /// Converts the <paramref name="nodeId" /> representing instance of <see cref="NodeId" /> and returns <see cref="XmlQualifiedName" />
    /// representing the BrowseName name of the <see cref="UANode" /> pointed out by it.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the BrowseName of <see cref="UANode" /> of the node indexed by <paramref name="nodeId" /></returns>
    XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue);
    /// <summary>
    /// Exports the BrowseName of the BaseType.
    /// </summary>
    /// <param name="type">if set to <c>true</c> the source node represents type. <c>false</c> if it is an instance.</param>
    /// <value>An instance of <see cref="XmlQualifiedName" /> representing the base type.</value>
    XmlQualifiedName ExportBaseTypeBrowseName(bool type);
    /// <summary>
    /// Gets the modeling rule associated with this node.
    /// </summary>
    /// <value>The <see cref="ModelingRules"/> associated with the node. Null if valid modeling rule cannot be recognized.</value>
    ModelingRules? ModelingRule { get; }
    /// <summary>
    /// Gets the parameters.
    /// </summary>
    /// <param name="arguments">The <see cref="XmlElement"/> encapsulates the arguments.</param>
    Parameter[] GetParameters(XmlElement arguments);
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
