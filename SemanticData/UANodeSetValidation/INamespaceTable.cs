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
    /// <param name="URI">The URI to search for in the namespace table.</param>
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="URI"/> that matches the conditions defined by <paramref name="URI"/>, if found; otherwise, –1.
    /// </returns>
    ushort GetURIIndexOrAppend(Uri URI);

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
    /// Gets the model table entry.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    /// <returns>IModelTableEntry.</returns>
    IModelTableEntry GetModelTableEntry(ushort namespaceIndex);

    /// <summary>
    /// Gets the index of the URI.
    /// </summary>
    /// <param name="URI">The URI.</param>
    /// <returns>System.Int32.</returns>
    int GetURIIndex(Uri URI);

    //TOD AddressSpacePrototyping - IMNamespace must be required in case of export #584
    ///// <summary>
    ///// Gets the index of the default model.
    ///// </summary>
    ///// <value>The index of the default model.</value>
    //int DefaultModelIndex { get; }
  }
}