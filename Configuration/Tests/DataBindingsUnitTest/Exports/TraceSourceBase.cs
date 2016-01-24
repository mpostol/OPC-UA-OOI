
using System.ComponentModel.Composition;
using System.Diagnostics;
using UAOOI.SemanticData.UANetworking.Configuration;

namespace UAOOI.DataBindings.UnitTest.Exports
{
  [Export(typeof(ITraceSource))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class TraceSourceBase : ITraceSource
  {
    public virtual void TraceData(TraceEventType eventType, int id, object data) { }

  }
}
