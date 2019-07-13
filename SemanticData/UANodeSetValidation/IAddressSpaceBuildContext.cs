//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IAddressSpaceBuildContext Address Space Context used during build operation
  /// </summary>
  internal interface IAddressSpaceBuildContext
  {

    /// <summary>
    /// Exports the browse name if it is not default value, otherwise null.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>XmlQualifiedName.</returns>
    XmlQualifiedName ExportBrowseName(NodeId id, NodeId defaultValue);
    /// <summary>
    /// Exports the argument for a method.
    /// </summary>
    /// <param name="argument">The argument - it defines a Method input or output argument specification. It is for example used in the input and output argument Properties for Methods.</param>
    /// <param name="dataType">Type of the data.</param>
    Parameter ExportArgument(DataSerialization.Argument argument, XmlQualifiedName dataType);
    /// <summary>
    /// Gets the or create node context.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="createUAModelContext">Delegated capturing functionality to create ua model context.</param>
    /// <returns>Returns an instance of <see cref="IUANodeContext"/>.</returns>
    IUANodeContext GetOrCreateNodeContext(NodeId nodeId, Func<NodeId, IUANodeContext> createUAModelContext);
    /// <summary>
    /// Gets the index or append the URI.
    /// </summary>
    /// <param name="identifier">The identifier.</param>
    ushort GetIndexOrAppend(string identifier);
    /// <summary>
    /// Gets the namespace value as the <see cref="string"/>.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    /// <returns>The namespace of the index pointed out by the <paramref name="namespaceIndex"/></returns>
    string GetNamespace(ushort namespaceIndex);
    /// <summary>
    /// Gets my references.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>IEnumerable&lt;UAReferenceContext&gt;.</returns>
    IEnumerable<UAReferenceContext> GetMyReferences(IUANodeBase index);
    /// <summary>
    /// Gets the references to me.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>All references targeting the selected by the <paramref name="node"/> node</returns>
    IEnumerable<UAReferenceContext> GetReferences2Me(IUANodeContext node);
    /// <summary>
    /// Gets the children nodes for the <paramref name="rootNode" />.
    /// </summary>
    /// <param name="rootNode">The root node of the requested children.</param>
    /// <returns>Return an instance of <see cref="IEnumerable"/> capturing all children of the selected node.</returns>
    IEnumerable<IUANodeBase> GetChildren(IUANodeContext rootNode);

    Parameter ExportArgument(DataSerialization.Argument argument);
  }

}
