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

  internal interface IUAModelContext
  {

    QualifiedName ImportQualifiedName(QualifiedName broseName);
    /// <summary>
    /// Exports the browse name of the node.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>An instance of the <see cref="XmlQualifiedName"/> or null if the <paramref name="nodeId"/> represents <see cref="NodeId.Null"/> or default identifier.</returns>
    XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue);
    Parameter ExportArgument(DataSerialization.Argument item);
    IUANodeContext GetOrCreateNodeContext(string value, bool v);
    /// <summary>
    /// Imports the node identifier if <paramref name="nodeId"/> is not empty.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <param name="lookupAlias">if set to <c>true</c> lookup the aliases table .</param>
    /// <returns>An instance of the <see cref="NodeId"/> or null is the <paramref name="nodeId"/> is null or empty.</returns>
    NodeId ImportNodeId(string nodeId, bool lookupAlias);
    /// <summary>
    /// Exports the name of the qualified.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns>XmlQualifiedName.</returns>
    XmlQualifiedName ExportQualifiedName(QualifiedName source);

  }

}