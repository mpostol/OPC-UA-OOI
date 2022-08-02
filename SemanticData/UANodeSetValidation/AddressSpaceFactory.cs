//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class AddressSpaceFactory - provides entry point for UA Address Space Management
  /// </summary>
  public abstract class AddressSpaceFactory
  {
    /// <summary>
    /// Creates Address Space infrastructure exposed to the API clients as the <see cref="IAddressSpaceContext"/> interface using default tracing infrastructure.
    /// </summary>
    // TODO Define independent Address Space API #645
    public static IAddressSpaceContext AddressSpace()
    {
      return new AddressSpaceContext(new AssemblyTraceSource());
    }

    /// <summary>
    /// Creates Address Space infrastructure exposed to the API clients as the <see cref="IAddressSpaceContext"/> interface using default tracing infrastructure.
    /// </summary>
    internal static IAddressSpaceContext AddressSpace(IBuildErrorsHandling traceEvent)
    {
      return new AddressSpaceContext(traceEvent);
    }
  }
}