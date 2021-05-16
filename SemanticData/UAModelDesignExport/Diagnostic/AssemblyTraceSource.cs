//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Diagnostics;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UAModelDesignExport.Diagnostic
{
  /// <summary>
  /// Class AssemblyTraceSource. Implements the <see cref="ITraceSource" />
  /// </summary>
  /// <seealso cref="ITraceSource" />
  internal class AssemblyTraceSource : ITraceSource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyTraceSource"/> class using the default of the <see cref="ITraceSource"/>.
    /// </summary>
    internal AssemblyTraceSource()
    {
      traceSource = new TraceSourceBase("UAModelDesignExport");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyTraceSource"/> class using a provided implementation of the <see cref="ITraceSource"/>.
    /// </summary>
    /// <param name="traceEvent">The provided implementation of the <see cref="ITraceSource"/>.</param>
    internal AssemblyTraceSource(ITraceSource traceEvent)
    {
      traceSource = traceEvent;
    }

    /// <summary>
    /// Writes the trace message.
    /// </summary>
    /// <param name="traceMessage">The trace message.</param>
    internal void WriteTraceMessage(TraceMessage traceMessage)
    {
      traceSource.TraceData(traceMessage.TraceLevel, 39445735, traceMessage.ToString());
    }

    #region ITraceSource

    /// <summary>
    /// Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified <paramref name="eventType" />,
    /// event identifier <paramref name="id" />, and trace <paramref name="data" />.
    /// </summary>
    /// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data.</param>
    public void TraceData(TraceEventType eventType, int id, object data)
    {
      traceSource.TraceData(eventType, id, data);
    }

    #endregion ITraceSource

    private readonly ITraceSource traceSource;
  }
}