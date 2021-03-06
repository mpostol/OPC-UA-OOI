﻿//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Diagnostics;

namespace UAOOI.Common.Infrastructure.Diagnostic
{
  /// <summary>
  /// Class TraceSourceBase - default implementation of the <see cref="ITraceSource"/>
  /// </summary>
  public class TraceSourceBase : ITraceSource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TraceSourceBase"/> class provider of a named <see cref="ITraceSource"/>.
    /// </summary>
    /// <param name="TraceSourceName">Name of the <see cref="TraceSource"/>.</param>
    public TraceSourceBase(string TraceSourceName)
    {
      m_TraceEventInternal = new Lazy<TraceSource>(() => new TraceSource(TraceSourceName));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TraceSourceBase"/> class provider of a default <see cref="ITraceSource"/>.
    /// </summary>
    public TraceSourceBase() : this("UAOOI.Common")
    {
    }

    /// <summary>
    /// Writes trace data to the trace listeners in the <see cref="TraceSource.Listeners" /> collection using the specified <paramref name="eventType" />,
    /// event identifier <paramref name="id" />, and trace <paramref name="data" />.
    /// </summary>
    /// <param name="eventType">One of the enumeration values <see cref="TraceEventType"/> that specifies the event type of the trace data.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data.</param>
    public virtual void TraceData(TraceEventType eventType, int id, object data)
    {
      m_TraceEventInternal.Value.TraceData(eventType, id, data);
    }

    /// <summary>
    /// Gets the trace source instance.
    /// </summary>
    /// <value>The trace source instance of type <see cref="TraceSource"/>.</value>
    public TraceSource TraceSource => m_TraceEventInternal.Value;

    private Lazy<TraceSource> m_TraceEventInternal = null;
  }
}