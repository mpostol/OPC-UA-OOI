//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IAddressSpaceURIRecalculate is used to recalculate indexes in the imported model
  /// </summary>
  public interface INamespaceTable
  {
    /// <summary>
    /// Searches for an index that matches the <paramref name="URI"/>, and returns the zero-based index of the first occurrence within the namespace table.
    /// </summary>
    /// <param name="URI">The URI to be glistered in the namespace table.</param>
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="URI"/>
    /// </returns>
    /// <exception cref="System.ArgumentNullException">URI is null.</exception>
    ushort GetURIIndexOrAppend(Uri URI);

    /// <summary>
    /// Updates the model or append it the existing collection.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="defaultModel">if set to <c>true</c> it the default model, otherwise <c>false</c>.</param>
    void UpadateModelOrAppend(IModelTableEntry model, bool defaultModel);

    IModelTableEntry GetModelTableEntry(ushort namespaceIndex);

    int GetURIIndex(Uri URI);
    int DefaultModelURI { get; }
  }
}