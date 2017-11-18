
using System;
using System.Diagnostics.Tracing;

namespace UAOOI.Networking.SemanticData.Diagnostics
{
  [EventSource(Name = "UAOOI-Networking-SemanticData-Diagnostics")]
  public class SemanticEventSource : EventSource
  {
    /// <summary>
    /// Class Keywords - defines the local keywords (flags) that apply to events.
    /// </summary>
    internal class Keywords
    {
      public const EventKeywords PackageContent = (EventKeywords)1;
      public const EventKeywords Diagnostic = (EventKeywords)2;
      public const EventKeywords Performance = (EventKeywords)4;
    }

    internal class Tasks
    {
      public const EventTask Consumer = (EventTask)1;
      public const EventTask Producer = (EventTask)2;
    }

    /// <summary>
    /// Gets the log - implements singleton of the <see cref="SemanticEventSource"/>.
    /// </summary>
    /// <value>The log.</value>
    internal static SemanticEventSource Log { get; } = new SemanticEventSource();

    [Event(1, Message = "Application Failure: {0}", Opcode = EventOpcode.Info, Task = EventTask.None, Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
    internal void Failure(string message)
    {
      this.WriteEvent(1, message);
    }
    [Event(2, Message = "Starting up.", Keywords = Keywords.Performance, Level = EventLevel.Informational)]
    internal void Startup()
    {
      this.WriteEvent(2);
    }
    [Event(3, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Start, Task = EventTask.None, Keywords = Keywords.Performance, Level = EventLevel.Informational)]
    internal void EnteringMethod(string className, string methodName)
    {
      if (this.IsEnabled()) this.WriteEvent(3, className, methodName);
    }
    private SemanticEventSource() { }

    [Event(4, Message = "Unexpected end of message while reading #{0} element.", Opcode = EventOpcode.Receive, Task = Tasks.Consumer, Keywords = Keywords.PackageContent, Level = EventLevel.Warning)]

    internal void MessageInconsistency(int i)
    {
      this.WriteEvent(3);
    }
  }
}
