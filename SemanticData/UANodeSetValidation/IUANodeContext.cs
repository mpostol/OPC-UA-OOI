//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IUANodeContext - captures all information about the <see cref="IUANode"/> required by the Address Space build process
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.IUANodeBase" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.IUANodeBase" />
  internal interface IUANodeContext : IUANodeBase
  {
    /// <summary>
    /// Builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    void BuildSymbolicId(List<string> path);

    /// <summary>
    /// Gets or sets a value indicating whether the node is in recursion chain - selected for analysis second time.
    /// </summary>
    /// <value><c>true</c> if the node is in recursion chain; otherwise, <c>false</c>.</value>
    bool InRecursionChain { get; set; }

    /// <summary>
    /// Updates this instance in case the wrapped <see cref="IUANode" /> is recognized in the model.
    /// </summary>
    /// <param name="node">The node <see cref="IUANode" /> containing definition to be added to the model.</param>
    /// <param name="addReference">Used to add new reference to the common collection of references.</param>
    void Update(IUANode node, Action<UAReferenceContext> addReference);

    /// <summary>
    /// Creates new embedded node exposed as the <see cref="IUANodeContext"/>.
    /// </summary>
    /// <param name="id">The identifier of the new node.</param>
    /// <returns>An instance of the <see cref="IUANodeContext"/>. representing the new embedded node.</returns>
    IUANodeContext CreateUANodeContext(NodeId id);
  }
}