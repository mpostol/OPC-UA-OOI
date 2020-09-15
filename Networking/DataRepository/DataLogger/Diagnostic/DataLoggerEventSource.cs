//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics.Tracing;

namespace UAOOI.Networking.DataRepository.DataLogger.Diagnostic
{
  /// <summary>
  /// Class DataLoggerEventSource.
  /// Implements the <see cref="EventSource" />
  /// </summary>
  /// <seealso cref="EventSource" />
  [EventSource(Name = "UAOOI.Networking.DataRepository.DataLogger.Diagnostic.DataLoggerEventSource", Guid = "B28CBA3C-E2B7-4C5B-A045-E21FD3158D9B")]
  public class DataLoggerEventSource : EventSource
  {
    /// <summary>
    /// Class Tasks.
    /// </summary>
    public class Tasks
    {
      public const EventTask Consumer = (EventTask)1;
      public const EventTask Producer = (EventTask)2;
      public const EventTask Stack = (EventTask)3;
      public const EventTask Infrastructure = (EventTask)4;
    }

    ///// <summary>
    ///// Class Keywords - defines the local keywords (flags) that apply to events.
    ///// </summary>
    //internal class Keywords
    //{
    //  public const EventKeywords Setup = (EventKeywords)1;
    //  public const EventKeywords Configuration = (EventKeywords)2;
    //  public const EventKeywords Diagnostic = (EventKeywords)4;
    //}
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

    [Event(1, Message = "Application Failure: {0}",
      Opcode = EventOpcode.Info, Task = Tasks.Infrastructure, Level = EventLevel.Error, Keywords = Keywords.Diagnostic)]
    internal void Failure(string message)
    {
      this.WriteEvent(1, message);
    }

    [Event(2, Message = "The application has been started using the message handling provider {0}.",
      Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Level = EventLevel.Informational, Keywords = Keywords.Diagnostic)]
    public void StartingApplication(string transportName)
    {
      this.WriteEvent(2, transportName);
    }

    [Event(3, Message = "The part {0} has been just created and configured.",
      Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Level = EventLevel.Informational, Keywords = Keywords.Setup)]
    public void PartCreated(string partName)
    {
      this.WriteEvent(3, partName);
    }

    [Event(4, Message = "Initialization of {0}",
      Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Level = EventLevel.Informational, Keywords = Keywords.Setup)]
    public void Initialization(string message)
    {
      this.WriteEvent(4, message);
    }

    [Event(5, Message = "Entering method {0}.{1}",
      Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Level = EventLevel.Verbose, Keywords = Keywords.Diagnostic)]
    public void EnteringMethod(string className, string methodName)
    {
      this.WriteEvent(5, className, methodName);
    }

    [Event(6, Message = "Entering Dispose method in {0} class disposing = {1}",
      Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Level = EventLevel.Informational, Keywords = Keywords.Diagnostic)]
    public void EnteringDispose(string className, bool disposing)
    {
      this.WriteEvent(6, className, disposing);
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