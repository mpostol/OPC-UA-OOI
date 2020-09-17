//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.DataRepository.AzureGateway.Diagnostic;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  /// <summary>
  /// Class ProducerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase{T}" /> for the producer.
  /// Implements the <see cref="ConfigurationFactoryBase{T}" />
  /// </summary>
  /// <seealso cref="ConfigurationFactoryBase{T}" />
  internal class PartConfigurationFactory : ConfigurationFactoryBase<ConfigurationData>
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="PartConfigurationFactory" /> class.
    /// </summary>
    /// <param name="configurationFileName">Name of the producer configuration file.</param>
    public PartConfigurationFactory(string configurationFileName) : base(configurationFileName)
    {
      _log.CreatingConfiguration(configurationFileName);
    }

    #endregion constructor

    #region ConfigurationFactoryBase

    /// <summary>
    /// Occurs after the association configuration has been changed.
    /// </summary>
    public override event EventHandler<EventArgs> OnAssociationConfigurationChange;

    /// <summary>
    /// Occurs after the communication configuration has been changed.
    /// </summary>
    public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

    /// <summary>
    /// Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified <paramref name="eventType" />,
    /// event identifier <paramref name="id" />, and trace <paramref name="data" />.
    /// </summary>
    /// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data.</param>
    protected override void TraceData(TraceEventType eventType, int id, object data)
    {
      _log.TraceData(eventType.ToString(), id, data.ToString());
    }

    protected override void RaiseEvents()
    {
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }

    #endregion ConfigurationFactoryBase

    #region private

    private readonly AzureGatewaySemanticEventSource _log = AzureGatewaySemanticEventSource.Log();

    #endregion private
  }
}