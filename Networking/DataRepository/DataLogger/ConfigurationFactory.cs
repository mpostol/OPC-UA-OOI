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
using UAOOI.Networking.DataRepository.DataLogger.Diagnostic;

namespace UAOOI.Networking.DataRepository.DataLogger
{
  /// <summary>
  /// Class ConsumerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase{T}" /> for the UA Data consumer.
  /// Implements the <see cref="ConfigurationFactoryBase{T}" />
  /// </summary>
  /// <seealso cref="ConfigurationFactoryBase{T}" />
  /// <remarks>In production environment it shall be replaced by reading a configuration file.</remarks>
  internal class ConsumerConfigurationFactory : ConfigurationFactoryBase<ConfigurationData>
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerConfigurationFactory"/> class.
    /// </summary>
    public ConsumerConfigurationFactory(string configurationFileName) : base(configurationFileName)
    {
      _logger.EnteringMethodConfiguration();
      //Simulator.Boiler.ProducerConfigurationFactory - review the configuration loading sequence #461
      _logger.CreatingConfiguration(configurationFileName);
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

    protected override void RaiseEvents()
    {
      _logger.EnteringMethodConfiguration();
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }

    protected override void TraceData(TraceEventType eventType, int id, object data)
    {
      _logger.TraceData(eventType.ToString(), id, data.ToString());
    }

    #endregion ConfigurationFactoryBase

    #region private

    private readonly DataLoggerEventSource _logger = DataLoggerEventSource.Log();

    #endregion private
  }
}