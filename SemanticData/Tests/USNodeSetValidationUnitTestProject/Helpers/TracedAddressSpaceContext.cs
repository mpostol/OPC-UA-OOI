//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.Helpers
{
  internal class TracedAddressSpaceContext : IDisposable
  {
    internal IAddressSpaceContext CreateAddressSpaceContext()
    {
      return new AddressSpaceContext(z => TraceDiagnostic(z, TraceList, ref _diagnosticCounter));
    }

    internal readonly List<TraceMessage> TraceList = new List<TraceMessage>();
    internal int _diagnosticCounter = 0;

    public void Dispose()
    {
    }

    internal void Clear()
    {
      _diagnosticCounter = 0;
      TraceList.Clear();
    }

    private void TraceDiagnostic(TraceMessage msg, List<TraceMessage> errors, ref int diagnosticCounter)
    {
      Debug.WriteLine(msg.ToString());
      if (msg.BuildError.Focus == Focus.Diagnostic)
        diagnosticCounter++;
      else
        errors.Add(msg);
    }
  }
}
