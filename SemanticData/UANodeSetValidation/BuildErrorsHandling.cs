using System;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal class BuildErrorsHandling : IBuildErrorsHandling
  {

    internal event Action<TraceMessage> TraceEventAction;
    internal static BuildErrorsHandling Log => m_Instance.Value;
    public void TraceEvent(TraceMessage traceMessage)
    {
      TraceEventAction?.Invoke(traceMessage);
    }

    private BuildErrorsHandling() { }
    private static Lazy<BuildErrorsHandling> m_Instance = new Lazy<BuildErrorsHandling>(() => new BuildErrorsHandling());

  }
}
