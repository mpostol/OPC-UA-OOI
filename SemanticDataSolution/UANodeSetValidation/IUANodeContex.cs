
using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal interface IUANodeContext : IEquatable<IUANodeContext>
  {

    /// <summary>
    /// Processes the node references to calculate all relevant properties. Mut be called after finishing import of all the parent model <see cref="IAddressSpaceContext.ImportNodeSet"/>
    /// </summary>
    /// <param name="createType">A delegate function to create top level definition for children like methods.</param>
    /// <param name="traceEvent">A delegate action to report and error and trace processing progress.</param>
    void CalculateNodeReferences(Func<IInstanceDesignFactory, List<string>, IInstanceDesignFactory> createType, Action<TraceMessage> traceEvent); 
    /// <summary>
    /// Gets the instance of <see cref="UANode"/> of this context source
    /// </summary>
    /// <value>The source UA node from the model.</value>
    UANode UANode { get; }
    /// <summary>
    /// Gets the instance of <see cref="IUAModelContext"/>, which the node is defined in.
    /// </summary>
    /// <value>The model context.</value>
    IUAModelContext UAModelContext { get; }
    /// <summary>
    /// Gets the branch name to calculate the node path - it is name part of the browse name or symbolic name depending which one is not empty.
    /// </summary>
    /// <value>The branch name to create the path.</value>
    string BranchName { get; }
    /// <summary>
    /// Gets a value indicating whether this instance is property.
    /// </summary>
    /// <value><c>true</c> if this instance is property; otherwise, <c>false</c>.</value>
    bool IsProperty { get; }
    /// <summary>
    /// Gets the base type of the node.
    /// </summary>
    /// <param name="traceEvent">A delegate action to report an error and trace processing progress.</param>
    /// <returns>XmlQualifiedName.</returns>
    /// <value>Returns an object of <see cref="XmlQualifiedName" /> representing the base type of the node.</value>
    XmlQualifiedName BaseType(Action<TraceMessage> traceEvent);
    /// <summary>
    /// Exports the browse name of the node.
    /// </summary>
    /// <param name="traceEvent">A delegate action to report an error and trace processing progress.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the browse name of the node.</returns>
    XmlQualifiedName ExportNodeBrowseName(Action<TraceMessage> traceEvent);
    /// <summary>
    /// Converts the <paramref name="nodeId" /> representing instance of <see cref="Opc.Ua.NodeId" /> and returns <see cref="XmlQualifiedName" />
    /// representing the BrowseName of the <see cref="UANode" /> of the node pointed out by it.
    /// </summary>
    /// <param name="nodeId">The node identifier to be parsed.</param>
    /// <param name="defaultValue">The default value of the browse name. If <paramref name="nodeId"/> is equal to default null is returned.</param>
    /// <param name="traceEvent">A delegate action to report an error and trace processing progress.</param>
    /// <returns>An object of <see cref="XmlQualifiedName" /> representing the BrowseName of the <see cref="UANode" /> of the node indexed by <paramref name="nodeId" /></returns>
    XmlQualifiedName ExportNodeId(string nodeId, DataSerialization.NodeId defaultValue, Action<TraceMessage> traceEvent);
    /// <summary>
    /// Gets the children of the node.
    /// </summary>
    /// <value>The <see cref="MD.ListOfChildren"/> containing collection of children.</value>
    IListOfChildrenFactory Children { get; }
    /// <summary>
    /// Gets the modeling rule.
    /// </summary>
    /// <value>The modeling rule.</value>
    IModelingRuleFactory ModelingRule { get; }
    /// <summary>
    /// Gets the references.
    /// </summary>
    /// <value>The references of the node.</value>
    IReferenceFactory[] References { get; }

  }
}
