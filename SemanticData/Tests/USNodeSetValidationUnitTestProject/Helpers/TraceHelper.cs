//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers
{
  internal static class TraceHelper
  {
    internal static void  TraceDiagnostic(TraceMessage msg, List<TraceMessage> errors, ref int diagnosticCounter)
    {
      if (errors == null)
        throw new ArgumentNullException("errors");
      Console.WriteLine(msg.ToString());
      if (msg.BuildError.Focus == Focus.Diagnostic)
      {
        diagnosticCounter++;
      }
      else
        errors.Add(msg);
    }
  }
}
