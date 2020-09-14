//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace UAOOI.Networking.DataRepository.AzureGateway.Diagnostic
{
  /// <summary>
  /// Class AzureGatewaySemanticEventSource capturing event source functionality supporting semantic par logging
  /// Implements the <see cref="System.Diagnostics.Tracing.EventSource" />
  /// </summary>
  /// <seealso cref="EventSource" />
  [EventSource(Name = "UAOOI.Networking.DataRepository.AzureGateway.Diagnostic", Guid = "BC7E8C08-C708-4E3C-A27E-237F093F175C")]
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

    [Event(1, Message = "At {0}.{1} encountered application failure: {2}",
      Channel = EventChannel.Admin, Opcode = EventOpcode.Info, Task = Tasks.Code, Level = EventLevel.Error, Keywords = Keywords.Diagnostic, Version = 0x01)]
    internal void ProgramFailure(string className, string problem, [CallerMemberName] string methodName = nameof(ProgramFailure))
    {
      WriteEvent(1, className, methodName, problem);
    }

    [Event(2, Message = "Disposing an object: {0}.{1}.",
       Channel = EventChannel.Debug, Opcode = EventOpcode.Stop, Task = Tasks.Code, Level = EventLevel.Verbose)]
    internal void DisposingObject(string className, string methodName)
    {
      WriteEvent(2, className, methodName);
    }

    [Event(3, Message = "At {0}.{1} encountered Azure transient communication problem: {2}",
      Channel = EventChannel.Admin, Opcode = EventOpcode.Suspend, Task = Tasks.Azure, Level = EventLevel.Warning, Keywords = EventKeywords.AuditFailure)]
    internal void AzureCommunicationFailure(string className, string methodName, string problem)
    {
      WriteEvent(3, className, methodName, problem);
    }

    [Event(4, Message = "Entering method {0}.{1}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Start, Task = Tasks.Azure, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodAzure(string className, string methodName)
    {
      WriteEvent(4, className, methodName);
    }

    [Event(5, Message = "Start creating client using {0}.{1} for {2} with authenticationMethod {3}, and using the transport {3}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Start, Task = Tasks.Azure, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void StartCreatingClient(string className, string methodName, string assignedHub, string authenticationMethod, string transportType)
    {
      WriteEvent(5, className, methodName, assignedHub, authenticationMethod, transportType);
    }

    [Event(6, Message = "Azure communication machine entering state {0}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Start, Task = Tasks.Azure, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringState(string machineState)
    {
      WriteEvent(6, machineState);
    }

    [Event(7, Message = "Unexpected provisioning resultStatus {0} reporting error {1}",
     Channel = EventChannel.Admin, Opcode = EventOpcode.Info, Task = Tasks.Azure, Level = EventLevel.Warning, Keywords = EventKeywords.AuditFailure)]
    internal void UnexpectedProvisioningResultStatus(string provisioningRegistrationStatusType, string errorMessage)
    {
      WriteEvent(7, provisioningRegistrationStatusType, errorMessage);
    }

    [Event(8, Message = "The message {0} has been successfully send to Azure service",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Azure, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void SendEvenSuccided(string payload)
    {
      WriteEvent(8, payload.Substring(0, 80));
    }

    [Event(9, Message = "Starting time delay {0} for a transient failure",
      Channel = EventChannel.Admin, Opcode = EventOpcode.Info, Task = Tasks.Azure, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void StartingTimeDelay(string timeSpan)
    {
      WriteEvent(9, timeSpan);
    }

    [Event(10, Message = "Entering method {0}.{1}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Binding, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodBinding(string className, string methodName)
    {
      WriteEvent(10, className, methodName);
    }

    [Event(11, Message = "Entering method {0}.{1}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Configuration, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodConfiguration(string className, string methodName)
    {
      WriteEvent(11, className, methodName);
    }

    [Event(12, Message = "Entering method {0}.{1}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Part, Level = EventLevel.Verbose, Keywords = EventKeywords.AuditSuccess)]
    internal void EnteringMethodPart(string className, string methodName)
    {
      WriteEvent(12, className, methodName);
    }

    [Event(13, Message = "Successfully composed {0} using instance of type {1}",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Info, Task = Tasks.Part, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void Composed(string variable, string typeName)
    {
      WriteEvent(13, variable, typeName);
    }

    [Event(14, Message = "Setup of the producer engine has been accomplished and it starts sending data.",
      Channel = EventChannel.Debug, Opcode = EventOpcode.Start, Task = Tasks.Part, Level = EventLevel.Informational, Keywords = EventKeywords.AuditSuccess)]
    internal void PartInitializationCompleted()
    {
      WriteEvent(14);
    }

    #region private

    private static Lazy<AzureGatewaySemanticEventSource> _singleto = new Lazy<AzureGatewaySemanticEventSource>(() => new AzureGatewaySemanticEventSource());

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.Tracing.EventSource"></see> class and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
        _singleto = new Lazy<AzureGatewaySemanticEventSource>(() => new AzureGatewaySemanticEventSource());
    }

    private AzureGatewaySemanticEventSource()
    {
      //this.Chann
    }

    #endregion private
  }
}