
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
    public ObservableCollection<MessageHandlerConfiguration> GetMessageHandlers()
    {
      if (m_ObservableMessageHandlers == null)
      {
        m_ObservableMessageHandlers = new ObservableCollection<MessageHandlerConfiguration>(MessageHandlers);
        m_ObservableMessageHandlers.CollectionChanged += M_MessageHandlers_CollectionChanged;
      }
      return m_ObservableMessageHandlers;
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
