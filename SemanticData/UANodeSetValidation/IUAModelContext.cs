//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Interface IUAModelContext - represents an OPC UA Information Model
  /// </summary>
  internal interface IUAModelContext
  {

    /// <summary>
    /// Imports the qualified name. It recalculates the <see cref="QualifiedName.NamespaceIndex"/> against local namespace index table. 
    /// </summary>
    /// <param name="browseName">The browse name to be imported.</param>
    /// <returns> An instance of the <see cref="QualifiedName"/> with recalculated <see cref="QualifiedName.NamespaceIndex"/>.</returns>
    QualifiedName ImportQualifiedName(QualifiedName browseName);
    /// <summary>
    /// Exports the browse name of the node.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>An instance of the <see cref="XmlQualifiedName"/> or null if the <paramref name="nodeId"/> is equal <see cref="NodeId.Null"/> or <paramref name="defaultValue"/>.</returns>
    XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue);
    /// <summary>
    /// Exports the argument.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Parameter.</returns>
    Parameter ExportArgument(DataSerialization.Argument item);
    /// <summary>
    /// Gets the or create node context.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="lookupAlias">if set to <c>true</c> lookup alias table.</param>
    /// <returns>An instance of <see cref="IUANodeContext"/>.</returns>
    IUANodeContext GetOrCreateNodeContext(string nodeId, bool lookupAlias);
    /// <summary>
    /// Imports the node identifier if <paramref name="nodeId"/> is not empty.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="lookupAlias">if set to <c>true</c> lookup the aliases table .</param>
    /// <returns>An instance of the <see cref="NodeId"/> or null is the <paramref name="nodeId"/> is null or empty.</returns>
    NodeId ImportNodeId(string nodeId, bool lookupAlias);
    /// <summary>
    /// Exports the qualified name to <see cref="XmlQualifiedName"/>. 
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns>XmlQualifiedName.</returns>
    XmlQualifiedName ExportQualifiedName(QualifiedName source);

  }

}