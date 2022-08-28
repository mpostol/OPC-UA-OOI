//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Interface IUAModelContext - represents an OPC UA Information Model
  /// </summary>
  internal interface IUAModelContext
  {
    /// <summary>
    /// Registers the <see cref="QualifiedName"/> of ReferenceType Node.
    /// </summary>
    /// <param name="browseName">An instance of <see cref="QualifiedName"/> used as a name of the ReferenceType node.</param>
    void RegisterUAReferenceType(QualifiedName browseName);

    /// <summary>
    /// Imports the browse name <see cref="QualifiedName"/> and Node identifier as <see cref="NodeId"/>. It recalculates the <see cref="QualifiedName.NamespaceIndex"/> and <see cref="NodeId.NamespaceIndex"/> against local namespace index table.
    /// </summary>
    /// <param name="browseNameText">The <see cref="QualifiedName" /> serialized as text to be imported.</param>
    /// <param name="nodeIdText">The <see cref="NodeId"/> serialized as text to be imported.</param>
    /// <param name="trace">Captures the functionality of trace.</param>
    /// <returns>A <see cref="ValueTuple{T1, T2}"/> instance containing <see cref="QualifiedName" /> and <see cref="NodeId"/> with recalculated NamespaceIndex.</returns>
    (QualifiedName browseName, NodeId nodeId) ImportBrowseName(string browseNameText, string nodeIdText, Action<TraceMessage> trace);

    /// <summary>
    /// Imports the node identifier if <paramref name="nodeId" /> is not empty.
    /// </summary>
    /// <param name="nodeId">The <see cref="NodeId"/> serialized as string to be imported.</param>
    /// <param name="trace">Captures the functionality of trace.</param>
    /// <returns>An instance of the <see cref="NodeId" /> or random if the <paramref name="nodeId" /> is null or empty.</returns>
    NodeId ImportNodeId(string nodeId, Action<TraceMessage> trace);
  }
}