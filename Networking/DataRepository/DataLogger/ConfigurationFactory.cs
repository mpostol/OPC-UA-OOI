//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Networking.DataLogger
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
    public ConsumerConfigurationFactory(string configurationFileName)
    {
      //Simulator.Boiler.ProducerConfigurationFactory - review the configuration loading sequence #461
      //here a default loader instead of a local one shall be used - don't use the parameterless constructor of the base class
      Loader = LoadConfig;
      m_ConfigurationFileName = configurationFileName;
      //TODO Create and Register the EventSource #455
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
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }

    #endregion ConfigurationFactoryBase

    #region private

    private readonly string m_ConfigurationFileName;

    private ConfigurationData LoadConfig()
    {
      FileInfo _configurationFile = new FileInfo(m_ConfigurationFileName);
      return ConfigurationDataFactoryIO.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configurationFile, (x, y, z) => { }), () => RaiseEvents());
    }

    #endregion private
  }
}