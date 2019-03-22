using System;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal class BuildErrorsHandling
  {

    internal static BuildErrorsHandling Log => m_Instance.Value;
    private BuildErrorsHandling() { }
    private static Lazy<BuildErrorsHandling> m_Instance = new Lazy<BuildErrorsHandling>(() => new BuildErrorsHandling());

    internal void TraceEvent(TraceMessage traceMessage)
    {
      throw new NotImplementedException();
    }
  }
}
