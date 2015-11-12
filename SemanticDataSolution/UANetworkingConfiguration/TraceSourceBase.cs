
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  [Export(typeof(ITraceSource))]
  public class TraceSourceBase : ITraceSource
  {
    public virtual void TraceData(TraceEventType eventType, int id, object data) { }

  }
}
