//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IUANodeBase
  /// </summary>
  internal interface IUANodeBase
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
    /// Gets the ua node described by <see cref="UANode"/>.
    /// </summary>
    /// <value>The ua node.</value>
    UANode UANode { get; }
    /// <summary>
    /// Exports the browse name of the wrapped node by this instance.
    /// </summary>
    /// <returns>An instance of <see cref="XmlQualifiedName" /> representing the BrowseName of the node.</returns>
    XmlQualifiedName ExportNodeBrowseName();

  }
}
