//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Interface IValidator - interface capturing functionality supporting the Ind=formation Model export and validation from the Address Space
  /// </summary>
  internal interface IValidator
  {
    /// <summary>
    /// Validates <paramref name="nodeContext" /> and exports it using an object of <see cref="IModelFactory" />  type.
    /// </summary>
    /// <param name="nodeContext">The node context to be validated and exported.</param>
    /// <param name="exportFactory">A model export factory.</param>
    /// <param name="parentReference">The reference to parent node.</param>
    void ValidateExportNode(IUANodeBase nodeContext, INodeContainer exportFactory, UAReferenceContext parentReference);

  }

}