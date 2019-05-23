//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class AddressSpaceFactory - provides entry point for UA Address Space Management
  /// </summary>
  public static class AddressSpaceFactory
  {

    /// <summary>
    /// Creates Address Space infrastructure exposed to the API clients as the <see cref="IAddressSpaceContext"/> interface
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns>An instance of the <see cref="IAddressSpaceContext"/> populated with the standard OPC UA nodes.</returns>
    public static IAddressSpaceContext AddressSpace(Action<TraceMessage> traceEvent)
    {
      return new AddressSpaceContext(traceEvent);
    }

  }
}
