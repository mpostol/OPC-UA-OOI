
using System.Diagnostics.Tracing;

namespace UAOOI.Networking.UDPMessageHandler.Diagnostic
{
  [EventSource(Name = "UAOOI-Networking-UDPMessageHandler-Diagnostic")]
  public class UDPMessageHandlerSemanticEventSource : EventSource
  {
    /// <summary>
    /// Class Keywords - defines the local keywords (flags) that apply to events.
    /// </summary>
    public class Keywords
    {
      public const EventKeywords PackageContent = (EventKeywords)1;
      public const EventKeywords Diagnostic = (EventKeywords)2;
      public const EventKeywords Performance = (EventKeywords)4;
    }

    /// <summary>
    /// Class Tasks.
    /// </summary>
    public class Tasks
    {
      public const EventTask Consumer = (EventTask)1;
      public const EventTask Producer = (EventTask)2;
    }

    /// <summary>
    /// Gets the log - implements singleton of the <see cref="UDPMessageHandlerSemanticEventSource"/>.
    /// </summary>
    /// <value>The log.</value>
    internal static UDPMessageHandlerSemanticEventSource Log { get; } = new UDPMessageHandlerSemanticEventSource();

    [Event(1, Message = "Application Failure: {0}", Opcode = EventOpcode.Start, Task = Tasks.Consumer, Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
    public void Failure(string message)
    {
      this.WriteEvent(1, message);
    }
    [Event(2, Message = "Starting up.", Keywords = Keywords.Performance, Level = EventLevel.Informational)]
    public void Startup()
    {
      this.WriteEvent(2);
    }
    [Event(3, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Start, Task = EventTask.None, Keywords = Keywords.Performance, Level = EventLevel.Informational)]
    internal void EnteringMethod(string className, string methodName)
    {
      if (this.IsEnabled()) this.WriteEvent(3, className, methodName);
    }
    [Event(4, Message = "Unexpected end of message while reading #{0} element.", Opcode = EventOpcode.Receive, Task = Tasks.Consumer, Keywords = Keywords.PackageContent, Level = EventLevel.Warning)]
    internal void MessageInconsistency(int i)
    {
      this.WriteEvent(4, i);
    }
    [Event(5, Message = "Message: {0}", Opcode = EventOpcode.Start, Task = Tasks.Consumer, Keywords = Keywords.PackageContent, Level = EventLevel.Warning)]
    internal void MessageContent(string payload0)
    {
      WriteEvent(5, payload0);
    }
    [Event(6, Message = "Joining the multicast group: {0}", Opcode = EventOpcode.Receive, Task = Tasks.Consumer, Keywords = Keywords.PackageContent, Level = EventLevel.Informational)]
    internal void JoiningMulticastGroup(string payload0)
    {
      WriteEvent(6, payload0);
    }
    private UDPMessageHandlerSemanticEventSource() { }
  }

}

