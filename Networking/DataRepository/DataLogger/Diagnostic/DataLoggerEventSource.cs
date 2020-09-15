//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace UAOOI.Networking.DataRepository.DataLogger.Diagnostic
{
  /// <summary>
  /// Class DataLoggerEventSource captures event source functionality supporting semantic par logging.
  /// Implements the <see cref="EventSource" />
  /// </summary>
  /// <seealso cref="EventSource" />
  [EventSource(Name = "UAOOI.Networking.DataRepository.DataLogger.Diagnostic.DataLoggerEventSource", Guid = "B28CBA3C-E2B7-4C5B-A045-E21FD3158D9B")]
  public class DataLoggerEventSource : EventSource
  {
    /// <summary>
    /// Class Tasks - capturing definitions of the tasks that apply to events.
    /// </summary>
    public class Tasks
    {
      /// <summary>
      /// The part behavior event task
      /// </summary>
      public const EventTask Part = (EventTask)1;

      /// <summary>
      /// The code behavior event task
      /// </summary>
      public const EventTask Code = (EventTask)2;

      /// <summary>
      /// The binding behavior event task
      /// </summary>
      public const EventTask Binding = (EventTask)3;

      /// <summary>
      /// The configuration behavior event task
      /// </summary>
      public const EventTask Configuration = (EventTask)4;

      /// <summary>
      /// The azure behavior event task
      /// </summary>
      public const EventTask UserInterface = (EventTask)5;
    }

    /// <summary>
    /// Class Keywords - defines the local keywords (flags) that apply to events.
    /// </summary>
    public class Keywords
    {
      /// <summary>
      /// The PackageContent <see cref="EventKeywords"/>
      /// </summary>
      public const EventKeywords Setup = (EventKeywords)(1 << 1);

      /// <summary>
      /// The Diagnostic <see cref="EventKeywords"/>
      /// </summary>
      public const EventKeywords Diagnostic = (EventKeywords)(1 << 2);

      /// <summary>
      /// The Performance <see cref="EventKeywords"/>
      /// </summary>
      public const EventKeywords Performance = (EventKeywords)(1 << 3);

      /// <summary>
      /// The Settings <see cref="EventKeywords"/>
      /// </summary>
      public const EventKeywords Settings = (EventKeywords)(1 << 4);
    }

    /// <summary>
    /// Gets the log - implements singleton of the <see cref="DataLoggerEventSource"/>.
    /// </summary>
    /// <value>The log.</value>
    internal static DataLoggerEventSource Log() { return _singleton.Value; }

    [Event(1, Message = "At {0}.{1} encountered application failure: {2}",
      Channel = EventChannel.Admin, Opcode = EventOpcode.Info, Task = Tasks.Code, Level = EventLevel.Error, Keywords = Keywords.Diagnostic, Version = 0x01)]
    internal void ProgramFailure(string className, string problem, [CallerMemberName] string methodName = nameof(ProgramFailure))
    {
      WriteEvent(1, className, methodName, problem);
    }

    [Event(2, Message = "Disposing an object: {0}.{1}.",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Stop, Task = Tasks.Code, Level = EventLevel.Verbose)]
    internal void DisposingObject(string className, [CallerMemberName] string methodName = nameof(DisposingObject))
    {
      WriteEvent(2, className, methodName);
    }

    [Event(3, Message = "Entering method ConsumerViewModell.{0}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Start, Task = Tasks.UserInterface, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodUserInterface( [CallerMemberName] string methodName = nameof(EnteringMethodUserInterface))
    {
      WriteEvent(3, methodName);
    }

    [Event(4, Message = "Entering method PartBindingFactory.{0}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Binding, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodBinding([CallerMemberName] string methodName = nameof(EnteringMethodBinding))
    {
      WriteEvent(4, methodName);
    }

    [Event(5, Message = "Entering method ConfigurationFactory.{0}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Configuration, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodConfiguration([CallerMemberName] string methodName = nameof(EnteringMethodBinding))
    {
      WriteEvent(5, methodName);
    }

    [Event(6, Message = "Opening the configuration file {0}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Configuration, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void CreatingConfiguration(string configurationFileName)
    {
      WriteEvent(6, configurationFileName);
    }

    [Event(7, Message = "Entering method LoggerManagementSetup.{0}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Part, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodPart([CallerMemberName] string methodName = nameof(EnteringMethodPart))
    {
      WriteEvent(7, methodName);
    }

    [Event(8, Message = "Successfully composed {0} using instance of type {1}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Part, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void Composed(string variable, string typeName)
    {
      WriteEvent(8, variable, typeName);
    }

    [Event(9, Message = "Setup of the consumer engine has been accomplished and it starts receiving data.",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Start, Task = Tasks.Part, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void PartInitializationCompleted()
    {
      WriteEvent(9);
    }

    [Event(10, Message = "TraceData of the EventType={0} with id={1} and description={2}",
      Channel = EventChannel.Analytic, Opcode = EventOpcode.Start, Task = Tasks.Code, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void TraceData(string eventType, int id, string data)
    {
      WriteEvent(10, eventType, id, data);
    }

    #region private

    private static Lazy<DataLoggerEventSource> _singleton = new Lazy<DataLoggerEventSource>(() => new DataLoggerEventSource());

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.Tracing.EventSource"></see> class and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
        _singleton = new Lazy<DataLoggerEventSource>(() => new DataLoggerEventSource());
    }

    private DataLoggerEventSource()
    {
    }

    #endregion private
  }
}