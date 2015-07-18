using System;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
{
  /// <summary>
  /// Class Extensions - contains helper functions to parse values of built-in data types.
  /// </summary>
  internal static class Extensions
  {
    internal static string NamePartOfBrowseName(this UANode value)
    {
      return QualifiedName.Parse(value.BrowseName).Name;
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
    /// <param name="eventNotifier">The event notifier.</param>
    /// <param name="isSpecified">The <see cref="Action{T}"/> is called if this value is specified.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    internal static bool? GetSupportsEvents(this byte eventNotifier, Action<TraceMessage> traceEvent)
    {
      if (eventNotifier > EventNotifiers.SubscribeToEvents + EventNotifiers.HistoryRead + EventNotifiers.HistoryWrite)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongEventNotifier, String.Format("EventNotifier value: {0}", eventNotifier)));
      else if (eventNotifier > EventNotifiers.SubscribeToEvents)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.EventNotifierValueNotSupported, String.Format("EventNotifier value: {0}", eventNotifier)));
      return eventNotifier != 0 ?(eventNotifier & EventNotifiers.SubscribeToEvents) != 0 : new Nullable<bool>();
    }
    internal static byte? GetAccessLevel(this byte accessLevel, Action<TraceMessage> traceEvent)
    {
      byte? _ret = new Nullable<byte>();
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
