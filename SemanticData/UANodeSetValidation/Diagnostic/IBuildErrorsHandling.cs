//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  internal interface IBuildErrorsHandling
  {
    //TODO Enhance/Improve the Program logging and tracing infrastructure. #590
    //      BuildErrorsHandling.Log.TraceEventAction += y => traceSource.TraceSource.TraceEvent(y.TraceLevel, 566981851, y.ToString());
    event Action<TraceMessage> TraceEventAction;

    void TraceEvent(TraceMessage traceMessage);
  }
}