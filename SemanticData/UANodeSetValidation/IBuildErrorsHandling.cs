using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal interface IBuildErrorsHandling
  {
    void TraceEvent(TraceMessage traceMessage);
  }
}