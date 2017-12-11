
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
      public const EventTask Stack = (EventTask)3;
      public const EventTask Infrastructure = (EventTask)4;
    }


    /// <summary>
    /// Gets the log - implements singleton of the <see cref="UDPMessageHandlerSemanticEventSource"/>.
    /// </summary>
    /// <value>The log.</value>
    internal static UDPMessageHandlerSemanticEventSource Log { get; } = new UDPMessageHandlerSemanticEventSource();

    [Event(1, Message = "Application Failure: {0}", Opcode = EventOpcode.Start, Task = Tasks.Consumer, Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
    public void Failure(string message)
    {
      WriteEvent(1, message);
    }
    [Event(2, Message = "Starting up {0}.", Task = Tasks.Infrastructure, Keywords = Keywords.Performance, Level = EventLevel.Informational)]
    internal void Startup(string stackName)
    {
      WriteEvent(2, stackName);
    }
    [Event(3, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Start, Task = EventTask.None, Keywords = Keywords.Performance, Level = EventLevel.Verbose)]
    internal void EnteringMethod(string className, string methodName)
    {
      if (IsEnabled())
        WriteEvent(3, className, methodName);
    }
    [Event(4, Message = "Unexpected end of message while reading #{0} element.", Opcode = EventOpcode.Receive, Task = Tasks.Consumer, Keywords = Keywords.PackageContent, Level = EventLevel.Warning)]
    internal void MessageInconsistency(int i)
    {
      WriteEvent(4, i);
    }
    [Event(5, Message = "Received message: {0}", Opcode = EventOpcode.Start, Task = Tasks.Consumer, Keywords = Keywords.PackageContent, Level = EventLevel.Verbose)]
    internal void ReceivedMessageContent(string payload0)
    {
      WriteEvent(5, payload0);
    }
    [Event(6, Message = "Sent message: {0}", Opcode = EventOpcode.Start, Task = Tasks.Producer, Keywords = Keywords.PackageContent, Level = EventLevel.Verbose)]
    internal void SentMessageContent(string payload0)
    {
      WriteEvent(6, payload0);
    }
    [Event(7, Message = "Joining the multicast group: {0}", Opcode = EventOpcode.Receive, Task = Tasks.Consumer, Keywords = Keywords.PackageContent, Level = EventLevel.Informational)]
    internal void JoiningMulticastGroup(string payload0)
    {
      WriteEvent(7, payload0);
    }
    [Event(8, Message = "Udp statistics: datagrams received = {0} sent = {1}", Opcode = EventOpcode.Info, Task = Tasks.Stack, Keywords = Keywords.Performance, Level = EventLevel.Informational)]

    internal void ReaderUdpStatistics(long datagramsReceived, long datagramsSent)
    {
      WriteEvent(8, datagramsReceived, datagramsSent);
    }
    private UDPMessageHandlerSemanticEventSource() { }

  }

}

