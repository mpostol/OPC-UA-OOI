
using System;
using System.IO;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Networking.ReferenceApplication.Producer
{
  /// <summary>
  /// Class ProducerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase"/> for the producer.
  /// </summary>
  /// <remarks>In production environment it shall be replaced by reading a configuration file.</remarks>
  internal class ProducerConfigurationFactory : ConfigurationFactoryBase
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerConfigurationFactory"/> class.
    /// </summary>
    public ProducerConfigurationFactory()
    {
      Loader = LoadConfig;
    }

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

    private ConfigurationData LoadConfig()
    {
      FileInfo _configurationFile = new FileInfo(Properties.Settings.Default.ProducerConfigurationFileName);
      return ConfigurationDataFactoryIO.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configurationFile, (x, y, z) => { }), () => RaiseEvents());
    }
    protected override void RaiseEvents()
    {
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }

  }
}
