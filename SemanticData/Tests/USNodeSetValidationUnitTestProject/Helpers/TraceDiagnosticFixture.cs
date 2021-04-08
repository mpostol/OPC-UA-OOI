//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.Helpers
{
  internal class TraceDiagnosticFixture
  {
    internal readonly List<TraceMessage> TraceList = new List<TraceMessage>();
    internal int DiagnosticCounter = 0;

    internal void Clear()
    {
      DiagnosticCounter = 0;
      TraceList.Clear();
    }

    internal void TraceDiagnostic(TraceMessage msg)
    {
      Debug.WriteLine(msg.ToString());
      if (msg.BuildError.Focus == Focus.Diagnostic)
        DiagnosticCounter++;
      else
        TraceList.Add(msg);
    }
  }
}