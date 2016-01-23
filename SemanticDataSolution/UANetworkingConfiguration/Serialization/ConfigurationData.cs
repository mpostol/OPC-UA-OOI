
using CAS.UA.IServerConfiguration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.UANetworking.Configuration.Serializers;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class ConfigurationData - contains configuration data of the UANetworking application.
  /// </summary>
  public partial class ConfigurationData : IConfigurationDataFactory
  {

    #region API
    /// <summary>
    /// Creates the <typeparam name="ConfigurationDataType" /> instance using specified loader.
    /// </summary>
    /// <typeparam name="ConfigurationDataType">The type of the configuration data type.</typeparam>
    /// <param name="loader">The delegate <see cref="Func{ConfigurationDataType}" /> capturing the loader of functionality
    /// of the class derived from <see cref="ConfigurationData" />.</param>
    /// <param name="onChanged">A delegate <see cref="Action"/> encapsulating operation called when this instance is changed.</param>
    /// <returns>An instance of <typeparam name="ConfigurationDataType" /> derived from <see cref="ConfigurationData" />.</returns>
    internal static ConfigurationDataType Load<ConfigurationDataType>(Func<ConfigurationDataType> loader, Action onChanged)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      ConfigurationDataType _configuration = loader();
      _configuration.OnChanged = onChanged;
      _configuration.OnLoaded();
      return _configuration;
    }
    public static ConfigurationDataType Load<ConfigurationDataType>(SerializerType serializer, FileInfo configurationFile, Action<TraceEventType, int, string> trace, Action onChanged)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      Func<FileInfo, Action<TraceEventType, int, string>, ConfigurationDataType> _loader = null;
      if (serializer == SerializerType.Xml)
        _loader = (file, tracer) => XmlDataContractSerializers.Load<ConfigurationDataType>(file, tracer);
      else
        _loader = (conf, tracer) => JSONDataContractSerializers.Load<ConfigurationDataType>(conf, tracer);
      ConfigurationDataType _configuration = _loader(configurationFile, (x, y, z) => trace?.Invoke(x, y, z));
      _configuration.OnChanged = onChanged;
      _configuration.OnLoaded();
      return _configuration;
    }
    public ObservableCollection<MessageHandlerConfiguration> GetMessageHandlers()
    {
      if (m_ObservableMessageHandlers == null)
      {
        m_ObservableMessageHandlers = new ObservableCollection<MessageHandlerConfiguration>(MessageHandlers);
        m_ObservableMessageHandlers.CollectionChanged += M_MessageHandlers_CollectionChanged;
      }
      return m_ObservableMessageHandlers;
    }
    internal static void Save<ConfigurationDataType>(ConfigurationDataType configuration, Action<ConfigurationDataType> saver)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      configuration.OnSaving();
      saver(configuration);
    }
    internal static void Save<ConfigurationDataType>(ConfigurationDataType configuration, SerializerType serializer, FileInfo configurationFile, Action<TraceEventType, int, string> trace)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      configuration?.OnSaving();
      Action<FileInfo, ConfigurationDataType, Action<TraceEventType, int, string>> _saver = null;
      if (serializer == SerializerType.Xml)
        _saver = (conf, file, tracer) => XmlDataContractSerializers.Save<ConfigurationDataType>(conf, file, tracer);
      else
        _saver = (conf, file, tracer) => JSONDataContractSerializers.Save<ConfigurationDataType>(conf, file, tracer);
      _saver(configurationFile, configuration, (x, y, z) => trace?.Invoke(x, y, z));
    }
    /// <summary>
    /// Gets the instance configuration - collection of data sets represented as the <see cref="IInstanceConfiguration"/>.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    /// <returns>IEnumerable&lt;IInstanceConfiguration&gt;.</returns>
    public DataSetConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      DataSetConfiguration _node = DataSetsList.Where<DataSetConfiguration>(x => x.Root.CreateWrapper().CompareTo(descriptor) == 0).FirstOrDefault<DataSetConfiguration>();
      if (_node == null)
        _node = DataSetConfiguration.Create(descriptor);
      return _node;
    }
    #endregion

    #region IConfigurationDataFactory
    public ConfigurationData GetConfigurationData()
    {
      return this;
    }
    public Action OnChanged
    {
      get; set;
    }
    /// <summary>
    /// Called when the configuration is loaded.
    /// </summary>
    public virtual void OnLoaded() { }
    /// <summary>
    /// Called before the saving the configuration.
    /// </summary>
    public virtual void OnSaving()
    {
      if (b_DataSetConfigurationList == null)
        return;
      if (m_PendingChages)
        DataSets = b_DataSetConfigurationList.ToArray<DataSetConfiguration>();
      if (m_MessageHandlersCollectionChanged)
        MessageHandlers = m_ObservableMessageHandlers.Select<ICloneable, MessageHandlerConfiguration>(x => (MessageHandlerConfiguration)x.Clone()).ToArray<MessageHandlerConfiguration>();
    }
    #endregion

    #region private
    private bool m_PendingChages = false;
    private bool m_MessageHandlersCollectionChanged = false;
    private List<DataSetConfiguration> b_DataSetConfigurationList;
    private ObservableCollection<MessageHandlerConfiguration> m_ObservableMessageHandlers;
    private void PendingChanges()
    {
      m_PendingChages = true;
      OnChanged();
    }
    private void M_MessageHandlers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      m_MessageHandlersCollectionChanged = true;
      OnChanged();
    }
    private List<DataSetConfiguration> DataSetsList
    {
      get
      {
        if (b_DataSetConfigurationList == null)
          b_DataSetConfigurationList = new List<DataSetConfiguration>(DataSets);
        return b_DataSetConfigurationList;
      }
    }
    #endregion

  }

}
