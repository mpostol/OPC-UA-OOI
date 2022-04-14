//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// The INamespaceTable interface to decouple the code from implementation of the <see cref="NamespaceTable"/>.
  /// </summary>
  public interface INamespaceTable
  {
    /// <summary>
    /// Updates the model or append it to the existing collection
    /// </summary>
    /// <param name="model">The model in concern.</param>
    void RegisterModel(IModelTableEntry model);

    /// <summary>
    /// Registers the dependency.
    /// </summary>
    /// <param name="model">The model that is required.</param>
    void RegisterDependency(IModelTableEntry model);

    /// <summary>
    /// Searches for an index that matches the <paramref name="URI"/>, and returns the zero-based index of the first occurrence within the namespace table.
    /// </summary>
    /// <param name="URI">The URI to search for in the namespace table.</param>
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="URI"/>, if found; otherwise, it is appended.
    /// </returns>
    ushort GetURIIndexOrAppend(Uri URI);

    /// <summary>
    /// Searches for an <paramref name="URI"/>, and returns the zero-based index of the first occurrence within the <see cref="INamespaceTable"/>.
    /// </summary>
    /// <param name="URI">The URI.</param>
    /// <returns>The zero-based index of the first occurrence of an <paramref name="URI"/>, if found; otherwise, –1.</returns>
    int GetURIIndex(Uri URI);

    /// <summary>
    /// Gets the model <see cref="Uri"/>.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    /// <returns>An instance that captures <see cref="Uri"/> of the requested model if already registered, otherwise, null.</returns>
    Uri GetModelTableEntry(ushort namespaceIndex);
  }
}