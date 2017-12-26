
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;
using UAOOI.Networking.SemanticData.Diagnostics;

namespace UAOOI.Networking.ReferenceApplication.Diagnostic
{

  [EventSource(Name = "UAOOI-Networking-ReferenceApplication-Diagnostic", Guid = "D8637D00-5EAD-4538-9286-8C6DE346D8C8")]
  public class ReferenceApplicationEventSource : EventSource
  {
    /// <summary>
    /// Class Keywords - defines the local keywords (flags) that apply to events.
    /// </summary>
    internal class Keywords
    {
      public const EventKeywords Setup = (EventKeywords)1;
      public const EventKeywords Configuration = (EventKeywords)2;
      public const EventKeywords Diagnostic = (EventKeywords)4;
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
    internal static ReferenceApplicationEventSource Log { get; } = new ReferenceApplicationEventSource();

    [Event(1, Message = "Application Failure: {0}", Opcode = EventOpcode.Info, Task = EventTask.None, Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
    internal void Failure(string message)
    {
      this.WriteEvent(1, message);
    }
    [Event(2, Message = "The application has been started", Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Keywords = Keywords.Setup, Level = EventLevel.Informational)]
    internal void StartingApplication()
    {
      this.WriteEvent(2);
    }
    [Event(3, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Keywords = Keywords.Diagnostic, Level = EventLevel.Verbose)]
    internal void EnteringMethod(string className, string methodName)
    {
      if (this.IsEnabled())
        this.WriteEvent(3, className, methodName);
    }

    private ReferenceApplicationEventSource() { }
  }
  [Export(typeof(INetworkingEventSourceProvider))]
  public class NetworkingEventSourceProvider : INetworkingEventSourceProvider
  {
    #region INetworkingEventSourceProvider
    public EventSource GetPartEventSource()
    {
      return ReferenceApplicationEventSource.Log;
    }
    #endregion

  }

}

