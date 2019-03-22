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

  internal interface IUANodeContext : IUANodeBase
  {


    /// <summary>
    /// Builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    void BuildSymbolicId(List<string> path);
    Dictionary<string, IUANodeContext> GetDerivedChildren();
    /// <summary>
    /// Gets or sets a value indicating whether the node is in recursion chain - selected for analysis second time.
    /// </summary>
    /// <value><c>true</c> if the node is in recursion chain; otherwise, <c>false</c>.</value>
    bool InRecursionChain { get; set; }
    bool IsPropertyVariableType { get; }
    QualifiedName m_BrowseName { get; set; }
    XmlQualifiedName ExportBrowseNameBaseType(Action<NodeId> traceEvent);
    /// <summary>
    /// Updates this instance in case the wrapped <see cref="UANode"/> is recognized in the model.
    /// </summary>
    /// <param name="node">The node <see cref="UANode"/> containing definition to be added to the model.</param>
    void Update(UANode node);

  }
  internal interface IUANodeValidator : IUANodeBase
  {
    /// <summary>
    /// Gets the instance of <see cref="IUAModelContext" />containing definition of this node.
    /// </summary>
    /// <value>The model context for this node.</value>
    IUAModelContext UAModelContext { get; }
    /// <summary>
    /// Gets a value indicating whether this instance is a property.
    /// </summary>
    /// <value><c>true</c> if this instance is property; otherwise, <c>false</c>.</value>
    bool IsProperty { get; }
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
    /// <param name="traceEvent">A delegate encapsulates the action to report any error and trace processing progress.</param>
    /// <returns>XmlQualifiedName.</returns>
    /// <value>An instance of <see cref="XmlQualifiedName" /> representing the base type.</value>
    XmlQualifiedName ExportBaseTypeBrowseName(bool type);
    /// <summary>
    /// Gets the modeling rule associated with this node.
    /// </summary>
    /// <value>The modeling rule. Null if valid modeling rule cannot be recognized.</value>
    ModelingRules? ModelingRule { get; }
    /// <summary>
    /// Gets the parameters.
    /// </summary>
    /// <param name="arguments">The <see cref="XmlElement"/> encapsulates the arguments.</param>
    /// <returns>Parameter[].</returns>
    Parameter[] GetParameters(XmlElement arguments);
  }

  internal interface IUANodeBase
  {
    void CalculateNodeReferences(INodeFactory nodeFactory);
    /// <summary>
    /// Gets the node identifier.
    /// </summary>
    /// <value>The imported node identifier.</value>
    NodeId NodeIdContext { get; }
    UANode UANode { get; }
    /// <summary>
    /// Exports the browse name of the wrapped node by this instance.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the BrowseName of the node.</returns>
    XmlQualifiedName ExportNodeBrowseName();

  }
}