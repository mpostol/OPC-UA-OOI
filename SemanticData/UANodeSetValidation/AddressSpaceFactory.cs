//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class AddressSpaceFactory - provides entry point for UA Address Space Management
  /// </summary>
  public static class AddressSpaceFactory
  {
    /// <summary>
    /// Creates Address Space infrastructure exposed to the API clients as the <see cref="IAddressSpaceContext"/> interface using default tracing infrastructure.
    /// </summary>
    public static IAddressSpaceContext AddressSpace => new AddressSpaceContext(new AssemblyTraceSource());

    ///// <summary>
    ///// Gets the address space.
    ///// </summary>
    ///// <param name="traceCallback">The trace callback.</param>
    ///// <returns>An instance of the <see cref="IAddressSpaceContext"/> type.</returns>
    //public static IAddressSpaceContext GetAddressSpace(Action<TraceMessage> traceCallback)
    //{
    //  return new AddressSpaceContext(traceCallback);
    //}
  }
}