//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.IO;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Interface IAddressSpaceContext - represents a service used to buildup OPc UA Address Space
  /// </summary>
  public interface IAddressSpaceContext
  {

    /// <summary>
    /// Imports a part of the OPC UA Address Space contained in the <see cref="UANodeSet" /> object model.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    void ImportUANodeSet(UANodeSet model);
    /// <summary>
    /// Imports a part of the OPC UA Address Space contained in the file <see cref="FileInfo" />.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    void ImportUANodeSet(FileInfo model);
    /// <summary>
    /// Sets the information model factory, which can be used to export a part of the OPC UA Address Space. If not set or set null an internal stub implementation will be used.
    /// </summary>
    /// <remarks>It is defined to handle dependency injection.</remarks>
    /// <value>The information model factory.</value>
    IModelFactory InformationModelFactory { set; }
    /// <summary>
    /// Validates and exports the selected model for the default namespace at index 1 if defined or standard OPC UA.
    /// </summary>
    void ValidateAndExportModel();
    /// <summary>
    /// Validates and exports the selected model.
    /// </summary>
    /// <param name="targetNamespace">The target namespace of the validated model.</param>
    void ValidateAndExportModel(string targetNamespace);

  }


}
