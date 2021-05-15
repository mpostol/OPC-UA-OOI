//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.Diagnostic
{
  internal interface IBuildErrorsHandling : ITraceSource
  {
    /// <summary>
    /// Traces the event using <see cref="TraceMessage"/>.
    /// </summary>
    /// <param name="traceMessage">The message to be send to trace.</param>
    void WriteTraceMessage(TraceMessage traceMessage);

    /// <summary>
    /// Gets the number of traced errors.
    /// </summary>
    /// <value>The errors.</value>
    int Errors { get; }
  }
}