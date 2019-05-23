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
    /// <param name="instanceDeclaration">The instance declaration.</param>
    /// <param name="exportFactory">A model export factory.</param>
    /// <param name="parentReference">The reference to parent node.</param>
    /// <param name="traceEvent">The trace event.</param>
    void ValidateExportNode(IUANodeBase nodeContext, IUANodeBase instanceDeclaration, INodeContainer exportFactory, UAReferenceContext parentReference, Action<TraceMessage> traceEvent);

  }

}