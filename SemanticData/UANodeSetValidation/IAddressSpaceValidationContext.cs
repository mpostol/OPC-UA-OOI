//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal interface IAddressSpaceValidationContext
  {
    /// <summary>
    /// Exports the current namespace table containing all namespaces that have been registered.
    /// </summary>
    /// <value>An instance of <see cref="IEnumerable{IModelTableEntry}"/> containing.</value>
    // TODO Import all dependencies for the model #575
    //IEnumerable<IModelTableEntry> ExportNamespaceTable { get; }
  }
}