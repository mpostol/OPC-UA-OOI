//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Interface IAddressSpaceContext - represents a service used to buildup OPC UA Address Space and export information model
  /// </summary>
  public interface IAddressSpaceContext
  {
    /// <summary>
    /// Imports all OPC UA Address Space models contained in the <see cref="IUANodeSet" /> XML document, and populates internal OPC UA Address Space.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <returns>Return a default <see cref="Uri"/> for the model defined in <see cref="IUANodeSet"/>.</returns>
    Uri ImportUANodeSet(IUANodeSet model);

    /// <summary>
    /// Validates and exports the selected model using <see cref="IModelFactory"/>.
    /// </summary>
    /// <param name="informationModelFactory">
    /// Information model factory, which can be used to export a part of the OPC UA Address Space using a selected language. If not set or set to null an internal stub implementation will be used.
    /// </param>
    /// <param name="targetNamespace">The target namespace of the validated model.</param>
    void ValidateAndExportModel(Uri targetNamespace, IModelFactory informationModelFactory);
  }
}