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
  /// <summary>
  /// Class AzureGatewaySemanticEventSource capturing event source functionality supporting semantic par logging
  /// Implements the <see cref="System.Diagnostics.Tracing.EventSource" />
  /// </summary>
  /// <seealso cref="EventSource" />
  [EventSource(Name = "UAOOI-Networking-DataRepository-AzureGateway-Diagnostic", Guid = "BC7E8C08-C708-4E3C-A27E-237F093F175C")]
  public class AzureGatewaySemanticEventSource : EventSource
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
      public const EventTask Azure = (EventTask)5;
    }

    /// <summary>
    /// Class Keywords - defines the local keywords (flags) that apply to events.
    /// </summary>
    public class Keywords
    {
      /// <summary>
      /// The PackageContent <see cref="EventKeywords"/>
      /// </summary>
      public const EventKeywords PackageContent = (EventKeywords)(1 << 1);

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
    /// Gets the log - implements singleton of the <see cref="AzureGatewaySemanticEventSource"/>.
    /// </summary>
    /// <value>The log.</value>
    internal static AzureGatewaySemanticEventSource Log() { return _singleto.Value; }

    [Event(1, Message = "At {0}.{1} encountered application failure: {2}", Opcode = EventOpcode.Info, Task = Tasks.Code, Level = EventLevel.Error, Keywords = Keywords.Diagnostic | EventKeywords.AuditFailure)]
    internal void Failure(string className, string methodName, string problem)
    {
      WriteEvent(1, className, methodName, problem);
    }

    [Event(2, Message = "Disposing an object: {0}.{1}.", Opcode = EventOpcode.Stop, Task = Tasks.Code, Level = EventLevel.Verbose)]
    internal void DisposingObject(string className, string methodName)
    {
      WriteEvent(2, className, methodName);
    }

    [Event(3, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Info, Task = Tasks.Azure, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodAzure(string className, string methodName)
    {
      if (IsEnabled())
        WriteEvent(3, className, methodName);
    }

    [Event(111, Message = "Setup of the producer engine has been accomplished and it starts sending data.", Opcode = EventOpcode.Start, Task = Tasks.Part, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void PartInitializationCompleted()
    {
      if (IsEnabled())
        WriteEvent(111);
    }

    [Event(4, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Info, Task = Tasks.Binding, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodBinding(string className, string methodName)
    {
      if (IsEnabled())
        WriteEvent(4, className, methodName);
    }

    [Event(5, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Info, Task = Tasks.Configuration, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodConfiguration(string className, string methodName)
    {
      if (IsEnabled())
        WriteEvent(5, className, methodName);
    }

    [Event(6, Message = "Entering method {0}.{1}", Opcode = EventOpcode.Info, Task = Tasks.Part, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodPart(string className, string methodName)
    {
      if (IsEnabled())
        WriteEvent(6, className, methodName);
    }
    [Event(7, Message = "Successfully composed {0} using instance of type {1}", Opcode = EventOpcode.Info, Task = Tasks.Part, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void Composed(string variable, string typeName)
    {
      if (IsEnabled())
        WriteEvent(6, variable, typeName);
    }



    //[Event(7, Message = "Emitted the message: {0}", Opcode = EventOpcode.Info, Task = Tasks.Azure, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    //internal void MessagePayload(string payload0)
    //{
    //  WriteEvent(7, payload0);
    //}

    //[Event(8, Message = "Sent message: {0}", Opcode = EventOpcode.Info, Task = Tasks.Producer, Level = EventLevel.Verbose)]
    //internal void SentMessageContent(string payload0)
    //{
    //  WriteEvent(8, payload0);
    //}

    //[Event(9, Message = "Joining the multicast group: {0}", Opcode = EventOpcode.Start, Task = Tasks.Stack, Level = EventLevel.Informational)]
    //internal void JoiningMulticastGroup(string multicastGroup)
    //{
    //  WriteEvent(9, multicastGroup);
    //}

    #region private

    private static readonly Lazy<AzureGatewaySemanticEventSource> _singleto = new Lazy<AzureGatewaySemanticEventSource>(() => new AzureGatewaySemanticEventSource());

    private AzureGatewaySemanticEventSource()
    {
    }

    internal void EnteringMethodAzure(string v1, string v2, string v3)
    {
      throw new NotImplementedException();
    }

    internal void EnteringState(string v)
    {
      throw new NotImplementedException();
    }

    internal void UnexpectedProvisioningResultStatus(string v, string v1)
    {
      throw new NotImplementedException();
    }

    internal void SendEvenSuccided(string payload)
    {
      throw new NotImplementedException();
    }

    internal void StartingTimeDelay(TimeSpan timeSpan)
    {
      throw new NotImplementedException();
    }

    #endregion private
  }
}