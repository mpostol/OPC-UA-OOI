//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  internal interface IAddressSpaceValidationContext
  {

    /// <summary>
    /// Exports the current namespace table containing all namespaces that have been registered.
    /// </summary>
    /// <value>An instance of <see cref="IEnumerable{IModelTableEntry}"/> containing.</value>
    IEnumerable<IModelTableEntry> ExportNamespaceTable { get; }

  }

}
