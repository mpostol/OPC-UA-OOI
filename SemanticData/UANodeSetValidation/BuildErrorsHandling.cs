//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
//TODO BuildErrorsHandling - review the code #578
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public class BuildErrorsHandling : IBuildErrorsHandling
  {

    public static IBuildErrorsHandling Log => m_Instance.Value;

    #region IBuildErrorsHandling
    public event Action<TraceMessage> TraceEventAction;
    public void TraceEvent(TraceMessage traceMessage)
    {
      TraceEventAction?.Invoke(traceMessage);
    }
    #endregion

    private BuildErrorsHandling() { }
    private static Lazy<BuildErrorsHandling> m_Instance = new Lazy<BuildErrorsHandling>(() => new BuildErrorsHandling());

  }
}
