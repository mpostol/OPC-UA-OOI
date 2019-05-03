//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
    /// Exports the browse name.
    /// </summary>
    /// <param name="id">The identifier.</param>
    XmlQualifiedName ExportBrowseName(NodeId id);
    /// <summary>
    /// Exports the argument for a method.
    /// </summary>
    /// <param name="argument">The argument - it defines a Method input or output argument specification. It is for example used in the input and output argument Properties for Methods.</param>
    /// <param name="dataType">Type of the data.</param>
    Parameter ExportArgument(DataSerialization.Argument argument, XmlQualifiedName dataType);
    /// <summary>
    /// Gets the or create node context.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="uAModelContext">The u a model context.</param>
    /// <returns>IUANodeContext.</returns>
    IUANodeContext GetOrCreateNodeContext(NodeId id, IUAModelContext uAModelContext);
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
    /// Gets the references to me.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>All references targeting the selected by the <paramref name="node"/> node</returns>
    IEnumerable<UAReferenceContext> GetReferences2Me(IUANodeContext node);
    /// <summary>
    /// Gets the derived instances.
    /// </summary>
    /// <param name="rootNode">The root node.</param>
    /// <param name="list">The list o d nodes.</param>
    void GetDerivedInstances(IUANodeContext rootNode, List<IUANodeBase> list);
    /// <summary>
    /// Gets the base type node if exist.
    /// </summary>
    /// <param name="nodeClass">The node class.</param>
    /// <returns>An instance of the <see cref="IUANodeBase"/> if exist in the AddressSpace, null otherwise.</returns>
    IUANodeBase GetBaseTypeNode(XML.NodeClassEnum nodeClass);

  }

}
