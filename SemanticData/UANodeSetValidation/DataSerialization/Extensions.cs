//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
{
  /// <summary>
  /// Class Extensions - contains helper functions to parse values of built-in data types.
  /// </summary>
  internal static class Extensions
  {
    internal static QualifiedName Parse(this string qualifiedName, Action<TraceMessage> traceEvent)
    {
      try
      {
        return QualifiedName.Parse(qualifiedName);
      }
      catch (ServiceResultException _sre)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.QualifiedNameInvalidSyntax, String.Format("Error message: {0}", _sre.Message)));
        return QualifiedName.Null;
      }
    }
    /// <summary>
    /// Gets the <see cref="NodeId.IdentifierPart" /> as uint number.
    /// </summary>
    /// <param name="nodeId">The node identifier.</param>
    /// <returns>Returns <see cref="NodeId.IdentifierPart" /> as the System.UInt32.</returns>
    /// <exception cref="System.ArgumentNullException">NodeId must not be null</exception>
    /// <exception cref="System.ApplicationException">To get the identifier as uint the NodeId must be Numeric</exception>
    internal static uint? UintIdentifier(this NodeId nodeId)
    {
      if (nodeId == null || nodeId.IdType != IdType.Numeric_0)
        return new Nullable<uint>();
      return (uint)nodeId.IdentifierPart;
    }
    /// <summary>
    /// Gets the supports events.
    /// </summary>
    /// <param name="eventNotifier">The event notifier. The EventNotifier represents the mandatory EventNotifier attribute of the Object NodeClass and identifies whether 
    /// the object can be used to subscribe to events or to read and write the history of the events.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns><c>true</c> if supports events, <c>false</c> otherwise.</returns>
    internal static bool? GetSupportsEvents(this byte eventNotifier, Action<TraceMessage> traceEvent)
    {
      if (eventNotifier > EventNotifiers.SubscribeToEvents + EventNotifiers.HistoryRead + EventNotifiers.HistoryWrite)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongEventNotifier, String.Format("EventNotifier value: {0}", eventNotifier)));
      else if (eventNotifier > EventNotifiers.SubscribeToEvents)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.EventNotifierValueNotSupported, String.Format("EventNotifier value: {0}", eventNotifier)));
      return eventNotifier != 0 ? (eventNotifier & EventNotifiers.SubscribeToEvents) != 0 : new Nullable<bool>();
    }
    internal static uint? GetAccessLevel(this uint accessLevel, Action<TraceMessage> traceEvent)
    {
      uint? _ret = new Nullable<byte>();
      if (accessLevel <= 0x7F)
        _ret = accessLevel;
      else
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongAccessLevel, String.Format("The AccessLevel value {0:X} is not supported", accessLevel)));
      return _ret;
    }
    /// <summary>
    /// Gets the value rank.
    /// </summary>
    /// <param name="valueRank">The value rank.</param>
    /// <param name="traceEvent">An <see cref="Action" /> delegate is used to trace event as the <see cref="TraceMessage" />.</param>
    /// <returns>Returns validated value.</returns>
    internal static int? GetValueRank(this int valueRank, Action<TraceMessage> traceEvent)
    {
      int? _vr = new Nullable<int>();
      if (valueRank < -2)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongValueRank, String.Format("The value {0} is not supported", valueRank)));
      else if (valueRank == -3)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongValueRank, String.Format("The value {0} is not supported", valueRank)));
      else if (valueRank != -1)
        _vr = valueRank;
      return _vr;
    }
  }
}
