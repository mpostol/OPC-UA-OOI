//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics;
using System.IO;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Configuration.Networking
{

  /// <summary>
  /// Class ConfigurationFactory - provides basic implementation of the <see cref="IConfigurationFactory"/>.
  /// </summary>
  /// <remarks>
  /// It read configuration using custom or default function.
  /// </remarks>
  public abstract class ConfigurationFactoryBase<ConfigurationType> : IConfigurationFactory
    where ConfigurationType : class, IConfigurationDataFactory, new()
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationFactoryBase{ConfigurationType}"/> class. 
    /// It loads the custom configuration using a default loader function. Using this overload <see cref="ConfigurationFactoryBase{ConfigurationType}.TraceData"/> should be overrioden.
    /// </summary>
    /// <param name="filePath">The file path of the configuration to read.</param>
    /// <exception cref="ArgumentException">filePath - Configuration file does not exist</exception>
    public ConfigurationFactoryBase(string filePath)
    {
      m_ConfigurationFileInformation = new FileInfo(filePath);
      if (!m_ConfigurationFileInformation.Exists)
        throw new ArgumentException(nameof(filePath), "Configuration file does not exist");
      Loader = LoadConfig;
    }
    public ConfigurationFactoryBase() { }
    public ConfigurationType Configuration { get; private set; }
    #region IConfigurationFactory
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <returns>Am object of <see cref="ConfigurationData" /> type capturing the communication configuration.</returns>
    public ConfigurationData GetConfiguration()
    {
      Configuration = ConfigurationDataFactoryIO.Load<ConfigurationType>(Loader, RaiseEvents);
      return Configuration.GetConfigurationData();
    }
    /// <summary>
    /// Occurs after the association configuration has been changed.
    /// </summary>
    public abstract event EventHandler<EventArgs> OnAssociationConfigurationChange;
    /// <summary>
    /// Occurs after the communication configuration has been changed.
    /// </summary>
    public abstract event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
    #endregion

    /// <summary>
    /// Gets or sets the loader of the configuration.
    /// </summary>
    /// <remarks>Allows late binding - injection point of the configuration loader.</remarks>
    /// <value>The loader that is to be used to create or load new instance of <see cref="ConfigurationData"/>.</value>
    protected Func<ConfigurationType> Loader { get; set; } = () => throw new NotImplementedException("Configuration loader must be assigned by the derived class");
    /// <summary>
    /// Raises the events.
    /// </summary>
    protected abstract void RaiseEvents();
    /// <summary>
    /// Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified <paramref name="eventType" />,
    /// event identifier <paramref name="id" />, and trace <paramref name="data" />.
    /// </summary>
    /// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data.</param>
    protected virtual void TraceData(TraceEventType eventType, int id, object data) { }
    private FileInfo m_ConfigurationFileInformation = null;
    private ConfigurationType LoadConfig()
    {
      return ConfigurationDataFactoryIO.Load<ConfigurationType>(() => XmlDataContractSerializers.Load<ConfigurationType>(m_ConfigurationFileInformation, TraceData), () => RaiseEvents());
    }

  }
}
