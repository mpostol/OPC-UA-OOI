//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;

//TODO BuildErrorsHandling - review the code #578
//TODO Enhance/Improve the Program logging and tracing infrastructure. #590
namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal class BuildErrorsHandling : IBuildErrorsHandling
  {
    internal static IBuildErrorsHandling Log => m_Instance.Value;

    #region IBuildErrorsHandling

    public event Action<TraceMessage> TraceEventAction;

    public void TraceEvent(TraceMessage traceMessage)
    {
      TraceEventAction?.Invoke(traceMessage);
    }

    #endregion IBuildErrorsHandling

    private BuildErrorsHandling()
    {
    }

    private static Lazy<BuildErrorsHandling> m_Instance = new Lazy<BuildErrorsHandling>(() => new BuildErrorsHandling());
  }
}