
//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.Diagnostics;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.Simulator.Boiler
{

  /// <summary>
  /// Class ProducerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase"/> for the producer.
  /// </summary>
  internal class ProducerConfigurationFactory : ConfigurationFactoryBase<ConfigurationData>
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerConfigurationFactory" /> class.
    /// </summary>
    /// <param name="configurationFileName">Name of the producer configuration file.</param>
    public ProducerConfigurationFactory(string configurationFileName) : base(configurationFileName)
    {
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      m_TraceSource = _serviceLocator.GetInstance<ITraceSource>();
      m_TraceSource.TraceData(TraceEventType.Information, 36, $"Starting {nameof(ProducerConfigurationFactory)} with the configuration file name {configurationFileName}");
      m_ProducerConfigurationFileName = configurationFileName;
    }
    #endregion

    #region ConfigurationFactoryBase
    /// <summary>
    /// Occurs after the association configuration has been changed.
    /// </summary>
    public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
    /// <summary>
    /// Occurs after the communication configuration has been changed.
    /// </summary>
    public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
    #endregion

    #region private
    protected override void TraceData(TraceEventType eventType, int id, object data)
    {
      m_TraceSource.TraceData(eventType, id, data);
    }
    private string m_ProducerConfigurationFileName;
    private ITraceSource m_TraceSource = null;
    protected override void RaiseEvents()
    {
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }
    #endregion

  }
}
