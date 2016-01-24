
using System.ComponentModel.Composition;
using System.Diagnostics;
using UAOOI.SemanticData.UANetworking.Configuration;

namespace UAOOI.Common.Infrastructure.Diagnostic
{
  /// <summary>
  /// Class TraceSourceBase - default implementation of the <see cref="ITraceSource"/>
  /// </summary>
  [Export(typeof(ITraceSource))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class TraceSourceBase : ITraceSource
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="TraceSourceBase"/> class.
    /// </summary>
    public TraceSourceBase()
    {
      m_Listener = new ConsoleTraceListener();
    }
    /// <summary>
    /// Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified <paramref name="eventType" />,
    /// event identifier <paramref name="id" />, and trace <paramref name="data" />.
    /// </summary>
    /// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data.</param>
    public virtual void TraceData(TraceEventType eventType, int id, object data)
    {
      m_Listener.WriteLine($"Event: {eventType}, Identifier: {id}, Message: {data}");
    }

    private ConsoleTraceListener m_Listener;

  }
}
