using System.Diagnostics.Tracing;

namespace UAOOI.Networking.ReferenceApplication.Core.Diagnostic
{

  [EventSource(Name = "UAOOI-Networking-ReferenceApplication-Diagnostic", Guid = "D8637D00-5EAD-4538-9286-8C6DE346D8C8")]
  public class ReferenceApplicationEventSource : EventSource
  {
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
    public static ReferenceApplicationEventSource Log { get; } = new ReferenceApplicationEventSource();

    [Event(1, Message = "Application Failure: {0}", Opcode = EventOpcode.Info, Task = Tasks.Infrastructure, Level = EventLevel.Error/*, Keywords = Keywords.Diagnostic*/)]
    internal void Failure(string message)
    {
      this.WriteEvent(1, message);
    }
    [Event(2, Message = "The application has been started using the message handling provider {0}.", Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, /*Keywords = Keywords.Setup,*/ Level = EventLevel.Informational)]
    public void StartingApplication(string transportName)
    {
      this.WriteEvent(2, transportName);
    }
    [Event(3, Message = "The part {0} has been just created and configured.", Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Level = EventLevel.Informational/*, Keywords = Keywords.Setup*/ )]
    public void PartCreated(string partName)
    {
      this.WriteEvent(3, partName);
    }
    [Event(4, Message = "Initialization of {0}", Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, Level = EventLevel.Informational /*, Keywords = Keywords.Setup,*/ )]
    public void Initialization(string message)
    {
      this.WriteEvent(4, message);
    }
    [Event(5, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, /*Keywords = Keywords.Diagnostic,*/ Level = EventLevel.Verbose)]
    public void EnteringMethod(string className, string methodName)
    {
      this.WriteEvent(5, className, methodName);
    }
    [Event(6, Message = "Entering Dispose method in {0} class disposing = {1}", Opcode = EventOpcode.Start, Task = Tasks.Infrastructure, /*Keywords = Keywords.Diagnostic,*/ Level = EventLevel.Informational)]
    public void EnteringDispose(string className, bool disposing)
    {
      this.WriteEvent(6, className, disposing);
    }

    private ReferenceApplicationEventSource() { }
  }

}

