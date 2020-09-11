//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics.Tracing;

namespace UAOOI.Networking.DataRepository.AzureGateway.Diagnostic
{
  [EventSource(Name = "UAOOI-Networking-DataRepository-AzureGateway-Diagnostic", Guid = "BC7E8C08-C708-4E3C-A27E-237F093F175C")]
  public class AzureGatewaySemanticEventSource : EventSource
  {
    /// <summary>
    /// Class Tasks.
    /// </summary>
    public class Tasks
    {
      public const EventTask Consumer = (EventTask)1;
      public const EventTask Producer = (EventTask)2;
      public const EventTask Stack = (EventTask)3;
      public const EventTask CodeBehavior = (EventTask)4;
    }

    /// <summary>
    /// Class Keywords - defines the local keywords (flags) that apply to events.
    /// </summary>
    public class Keywords
    {
      public const EventKeywords PackageContent = (EventKeywords)(1 << 1);
      public const EventKeywords Diagnostic = (EventKeywords)(1 << 2);
      public const EventKeywords Performance = (EventKeywords)(1 << 3);
      public const EventKeywords Settings = (EventKeywords)(1 << 4);
    }

    /// <summary>
    /// Gets the log - implements singleton of the <see cref="AzureGatewaySemanticEventSource"/>.
    /// </summary>
    /// <value>The log.</value>
    internal static AzureGatewaySemanticEventSource Log { get; } = _singleto.Value;

    [Event(1, Message = "At {0}.{1} encountered application failure: {2}", Opcode = EventOpcode.Info, Task = Tasks.CodeBehavior, Level = EventLevel.Error, Keywords = Keywords.Diagnostic)]
    public void Failure(string className, string methodName, string problem)
    {
      WriteEvent(1, className, methodName, problem);
    }

    [Event(2, Message = "The IMessageHandlerFactory.{0} method has been called.", Opcode = EventOpcode.Start, Task = Tasks.CodeBehavior, Level = EventLevel.Informational)]
    internal void GetIMessageHandler(string iMessageHandlerName)
    {
      WriteEvent(2, iMessageHandlerName);
    }

    [Event(3, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Info, Task = EventTask.None, Level = EventLevel.Verbose)]
    internal void EnteringMethod(string className, string methodName)
    {
      if (IsEnabled())
        WriteEvent(3, className, methodName);
    }

    [Event(5, Message = "Received message: {0}", Opcode = EventOpcode.Info, Task = Tasks.Consumer, Level = EventLevel.Verbose)]
    internal void ReceivedMessageContent(string payload0)
    {
      WriteEvent(5, payload0);
    }

    [Event(6, Message = "Sent message: {0}", Opcode = EventOpcode.Info, Task = Tasks.Producer, Level = EventLevel.Verbose)]
    internal void SentMessageContent(string payload0)
    {
      WriteEvent(6, payload0);
    }

    [Event(7, Message = "Joining the multicast group: {0}", Opcode = EventOpcode.Start, Task = Tasks.Stack, Level = EventLevel.Informational)]
    internal void JoiningMulticastGroup(string multicastGroup)
    {
      WriteEvent(7, multicastGroup);
    }

    #region private

    private static readonly Lazy<AzureGatewaySemanticEventSource> _singleto = new Lazy<AzureGatewaySemanticEventSource>(() => new AzureGatewaySemanticEventSource());

    private AzureGatewaySemanticEventSource()
    {
    }

    #endregion private
  }
}