
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Configuration.Networking
{
  /// <summary>
  /// Class UANetworkingConfiguration - Provides implementation of the <see cref="IConfigurationDataFactory"/> for the UANetworking application.
  /// </summary>
  /// <typeparam name="ConfigurationDataType">The type of the configuration data type.</typeparam>
  public class UANetworkingConfiguration<ConfigurationDataType>
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="UANetworkingConfiguration{ConfigurationDataType}"/> class.
    /// </summary>
    public UANetworkingConfiguration()
    {
      TraceSource = new TraceSourceDefault();
    }
    #endregion

    #region ConfigurationBase
    /// <summary>
    /// Reads the configuration from the <see cref="FileInfo"/>.
    /// </summary>
    /// <param name="configurationFile">The file <see cref="FileInfo"/> containing the configuration data of the UANetworking application.</param>
    public void ReadConfiguration(FileInfo configurationFile)
    {
      CurrentConfiguration = ConfigurationDataFactoryIO.Load<ConfigurationDataType>
        (Properties.Settings.Default.Serializer.ToUpper() == "XML" ? SerializerType.Xml : SerializerType.Json, configurationFile, (x, y, z) => TraceSource.TraceData(x, y, z), () => RaiseOnChangeEvent());
    }
    /// <summary>
    /// Saves the configuration.
    /// </summary>
    /// <param name="configurationFile">The configuration file.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SaveConfiguration(FileInfo configurationFile)
    {
      if (CurrentConfiguration == null)
        throw new ArgumentNullException(nameof(CurrentConfiguration));
      ConfigurationDataFactoryIO.Save<ConfigurationDataType>(CurrentConfiguration, Properties.Settings.Default.Serializer.ToUpper() == "XML" ? SerializerType.Xml : SerializerType.Json, configurationFile, (x, y, z) => TraceSource.TraceData(x, y, z));
    }
    /// <summary>
    /// Gets or sets the current configuration <typeparamref name="ConfigurationDataType"/>.
    /// </summary>
    /// <value>The current configuration as instance of <typeparamref name="ConfigurationDataType"/>.</value>
    public ConfigurationDataType CurrentConfiguration
    {
      get { return m_CurrentConfiguration; }
      set
      {
        if (Object.Equals(CurrentConfiguration, value))
          return;
        m_CurrentConfiguration = value;
        RaiseOnChangeEvent();
      }
    }
    /// <summary>
    /// Occurs any time the configuration is modified.
    /// </summary>
    public event EventHandler<EventArgs> OnModified;
    /// <summary>
    /// Gets the configuration data.
    /// </summary>
    /// <value>The configuration data.</value>
    public ConfigurationData ConfigurationData
    {
      get { return CurrentConfiguration?.GetConfigurationData(); }
    }
    #endregion

    #region MEF composition
    /// <summary>
    /// Gets or sets the trace source - an access point to the external component.
    /// By default an empty private implementation (do nothing) of the <see cref="ITraceSource"/> is provided.
    /// </summary>
    /// <value>The trace source - an implementation of the <see cref="ITraceSource"/>.</value>
    //[Import(typeof(ITraceSource))] //TODO
    public ITraceSource TraceSource
    {
      get { return b_TraceSource; }
      set { b_TraceSource = value; }
    }
    #endregion

    #region privat
    private class TraceSourceDefault : ITraceSource
    {
      /// <summary>
      /// Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified <paramref name="eventType" />,
      /// event identifier <paramref name="id" />, and trace <paramref name="data" />.
      /// </summary>
      /// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
      /// <param name="id">A numeric identifier for the event.</param>
      /// <param name="data">The trace data.</param>
      public void TraceData(TraceEventType eventType, int id, object data) { }
    }
    private ConfigurationDataType m_CurrentConfiguration;
    private ITraceSource b_TraceSource ;
    /// <summary>
    /// Raises the on change event.
    /// </summary>
    protected void RaiseOnChangeEvent()
    {
      OnModified?.Invoke(this, EventArgs.Empty);
    }
    #endregion

  }

}

