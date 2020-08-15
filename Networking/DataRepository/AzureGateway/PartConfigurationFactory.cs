//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.Diagnostics;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  /// <summary>
  /// Class ProducerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase{T}"/> for the producer.
  /// </summary>
  internal class PartConfigurationFactory : ConfigurationFactoryBase<ConfigurationData>
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="PartConfigurationFactory" /> class.
    /// </summary>
    /// <param name="configurationFileName">Name of the producer configuration file.</param>
    public PartConfigurationFactory(string configurationFileName) : base(configurationFileName)
    {
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      //TODO Create and Register the EventSource #455
      _TraceSource = _serviceLocator.GetInstance<ITraceSource>();
      _TraceSource.TraceData(TraceEventType.Information, 36, $"Starting {nameof(PartConfigurationFactory)} with the configuration file name {configurationFileName}");
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

    protected override void TraceData(TraceEventType eventType, int id, object data)
    {
      _TraceSource.TraceData(eventType, id, data);
    }

    protected override void RaiseEvents()
    {
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }

    #endregion ConfigurationFactoryBase

    #region private

    private ITraceSource _TraceSource = null;

    #endregion private
  }
}