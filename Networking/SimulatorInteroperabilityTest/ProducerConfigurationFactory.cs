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

namespace UAOOI.Networking.SimulatorInteroperabilityTest
{
  /// <summary>
  /// Class ProducerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase{ConfigurationData}" /> for the producer.
  /// </summary>
  /// <seealso cref="ConfigurationFactoryBase{ConfigurationData}" />
  internal class ProducerConfigurationFactory : ConfigurationFactoryBase<ConfigurationData>
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerConfigurationFactory" /> class.
    /// </summary>
    /// <param name="configurationFileName">Name of the producer configuration file.</param>
    public ProducerConfigurationFactory(string configurationFileName)
    {
      //Simulator.Boiler.ProducerConfigurationFactory - review the configuration loading sequence #461
      //here a default loader instead of a local one shall be used - don't use the parameterless constructor of the base class
      m_ProducerConfigurationFileName = configurationFileName;
      Loader = LoadConfig;
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

    #endregion ConfigurationFactoryBase

    #region private

    private readonly string m_ProducerConfigurationFileName;

    private ConfigurationData LoadConfig()
    {
      FileInfo _configurationFile = new FileInfo(m_ProducerConfigurationFileName);
      return ConfigurationDataFactoryIO.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configurationFile, (x, y, z) => { }), () => RaiseEvents());
    }

    protected override void RaiseEvents()
    {
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }

    #endregion private
  }
}