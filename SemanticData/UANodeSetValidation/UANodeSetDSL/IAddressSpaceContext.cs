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
  /// Interface IAddressSpaceContext - represents a service used to buildup OPc UA Address Space
  /// </summary>
  // TODO Define independent Address Space API #645
  public interface IAddressSpaceContext
  {
    /// <summary>
    /// Imports all OPC UA Address Space models contained in the <see cref="IUANodeSet" /> XML document, and populates internal OPC UA Address Space.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <returns>Return a default <see cref="Uri"/> for the model defined in <see cref="IUANodeSet"/>.</returns>
    Uri ImportUANodeSet(IUANodeSet model);

    /// <summary>
    /// Imports all OPC UA Address Space models contained in the file <paramref name="document"/> described by the <see cref="FileInfo"/>, and populates internal OPC UA Address Space.
    /// </summary>
    /// <remarks>
    /// The input document must be compliant with the `UANodeSet` schema.
    /// </remarks>
    /// <param name="document">The UANodeSet document to be imported, and described by the <see cref="FileInfo"/>.</param>
    /// <returns>Return a default <see cref="Uri"/> for the model defined in a file represented by <see cref="FileInfo"/></returns>
    //Uri ImportUANodeSet(FileInfo document, Func<FileInfo, IUANodeSet> readModelFile);

    /// <summary>
    /// Sets the information model factory, which can be used to export a part of the OPC UA Address Space using a selected language. If not set or set to null an internal stub implementation will be used.
    /// </summary>
    /// <remarks>It is defined to handle dependency injection.</remarks>
    /// <value>The information model factory captured by an instance of the <see cref="IModelFactory"/>.</value>
    IModelFactory InformationModelFactory { set; }

    /// <summary>
    /// Validates and exports the selected model using <see cref="IAddressSpaceContext.InformationModelFactory"/>, or alternatively a stub embedded implementation.
    /// </summary>
    /// <param name="targetNamespace">The target namespace of the validated model.</param>
    void ValidateAndExportModel(Uri targetNamespace);
  }
}