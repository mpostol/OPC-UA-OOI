
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace UAOOI.SemanticData.UANetworking.Configuration.Exports
{
  [Export(typeof(ITraceSource))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class TraceSourceBase : ITraceSource
  {
    public virtual void TraceData(TraceEventType eventType, int id, object data) { }

  }
}
