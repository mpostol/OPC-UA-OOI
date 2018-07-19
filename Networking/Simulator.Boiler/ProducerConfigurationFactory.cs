
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

namespace UAOOI.Networking.Simulator.Boiler
{

  /// <summary>
  /// Class ProducerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase"/> for the producer.
  /// </summary>
  internal class ProducerConfigurationFactory : ConfigurationFactoryBase
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerConfigurationFactory" /> class.
    /// </summary>
    /// <param name="configurationFileName">Name of the producer configuration file.</param>
    public ProducerConfigurationFactory(string configurationFileName)
    {
      m_ProducerConfigurationFileName = configurationFileName;
      Loader = LoadConfig;
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
    private string m_ProducerConfigurationFileName;
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
    #endregion

  }
}
