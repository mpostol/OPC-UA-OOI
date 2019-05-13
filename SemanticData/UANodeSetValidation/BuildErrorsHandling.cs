using System;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal class BuildErrorsHandling
  {

    internal event Action<TraceMessage> TraceEventAction;
    internal static BuildErrorsHandling Log => m_Instance.Value;
    internal void TraceEvent(TraceMessage traceMessage)
    {
      TraceEventAction?.Invoke(traceMessage);
    }

    private BuildErrorsHandling() { }
    private static Lazy<BuildErrorsHandling> m_Instance = new Lazy<BuildErrorsHandling>(() => new BuildErrorsHandling());

  }
}
