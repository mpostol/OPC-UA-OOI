//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;

namespace UAOOI.SemanticData.UANodeSetValidation.Helpers
{
  internal class TracedAddressSpaceContext : IBuildErrorsHandling, IDisposable
  {
    internal IAddressSpaceContext CreateAddressSpaceContext()
    {
      return new AddressSpaceContext(this);
    }

    internal readonly List<TraceMessage> TraceList = new List<TraceMessage>();

    public void Dispose()
    {
    }

    internal void Clear()
    {
      Errors = 0;
      TraceList.Clear();
    }

    #region IBuildErrorsHandling

    public int Errors { get; set; }

    public void TraceData(TraceEventType eventType, int id, object data)
    {
      string message = $"TraceData eventType = {eventType}, id = {id}, {data}";
      Console.WriteLine(message);
      if (eventType == TraceEventType.Critical || eventType == TraceEventType.Error)
        throw new ApplicationException(message);
    }

    public void WriteTraceMessage(TraceMessage traceMessage)
    {
      Console.WriteLine(traceMessage.ToString());
      if (traceMessage.BuildError.Focus != Focus.Diagnostic)
        TraceList.Add(traceMessage);
    }

    #endregion IBuildErrorsHandling
  }
}