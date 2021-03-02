//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IUAModelContext - represents an OPC UA Information Model
  /// </summary>
  public interface IUAModelContext
  {
    /// <summary>
    /// Gets the model URI.
    /// </summary>
    /// <value>The model URI.</value>
    Uri ModelUri { get; }

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