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
    /// <param name="isSpecified">The <see cref="Action[bool]"/> is called if this value is specified.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    internal static bool GetSupportsEvents(this byte eventNotifier, Action<bool> isSpecified, Action<TraceMessage> traceEvent)
    {
      isSpecified(eventNotifier != 0);
      if (eventNotifier > EventNotifiers.SubscribeToEvents + EventNotifiers.HistoryRead + EventNotifiers.HistoryWrite)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongEventNotifier, String.Format("EventNotifier value: {0}", eventNotifier)));
      else if (eventNotifier > EventNotifiers.SubscribeToEvents)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.EventNotifierValueNotSupported, String.Format("EventNotifier value: {0}", eventNotifier)));
      return (eventNotifier & EventNotifiers.SubscribeToEvents) != 0;
    }
    internal static byte GetAccessLevel(this byte accessLevel, Action<bool> accessLevelSpecified, Action<TraceMessage> traceEvent)
    {
      byte _ret = AccessLevels.None;
      if (accessLevel <= AccessLevels.CurrentReadOrWrite)
        _ret = accessLevel;
      else
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongAccessLevel, String.Format("The AccessLevel value {0:X} is not supported", accessLevel)));
      accessLevelSpecified((int)_ret != 1);
      return _ret;
    }
    /// <summary>
    /// Gets the value rank.
    /// </summary>
    /// <param name="valueRank">The value rank.</param>
    /// <param name="specified">if set to <c>true</c> the parameter is specified.</param>
    /// <param name="traceEvent">An <see cref="Action"/> delegate is used to trace event as the <see cref="TraceMessage"/>.</param>
    /// <returns>Returns value of <see cref="ModelDesign.ValueRank" />.</returns>
    internal static int GetValueRank(this int valueRank, Action<bool> specified, Action<TraceMessage> traceEvent)
    {
      int _vr = -1;
      bool _specified = true;
      if (valueRank < -2)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongValueRank, String.Format("The value {0} is not supported", valueRank)));
      else if (valueRank == -3)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongValueRank, String.Format("The value {0} is not supported", valueRank)));
      else
        _vr = valueRank;
      if (valueRank == -1)
        _specified = false;
      specified(_specified);
      return _vr;
    }
  }
}
