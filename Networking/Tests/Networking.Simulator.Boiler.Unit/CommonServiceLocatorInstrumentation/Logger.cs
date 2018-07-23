//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.Common.Infrastructure.Diagnostic;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.CommonServiceLocatorInstrumentation
{

  public class Logger : ITraceSource
  {

    internal static Logger Singleton { get { return m_Singleton.Value; } }

    public class TraceLogEntity
    {
      public TraceEventType EventType { get; private set; }
      public int Id { get; private set; }
      public object Data { get; private set; }
      public TraceLogEntity(TraceEventType eventType, int id, object data)
      {
        this.EventType = eventType;
        this.Id = id;
        this.Data = data;
      }
    }
    public List<TraceLogEntity> TraceLogList { get; } = new List<TraceLogEntity>();

    #region ITraceSource
    void ITraceSource.TraceData(TraceEventType eventType, int id, object data)
    {
      TraceLogList.Add(new TraceLogEntity(eventType, id, data));
    }
    #endregion

    private Logger() { }
    private static Lazy<Logger> m_Singleton = new Lazy<Logger>(() => new Logger());

  }
}
