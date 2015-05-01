using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UnitTest.Helpers
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
