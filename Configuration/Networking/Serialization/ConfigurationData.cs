//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace UAOOI.Configuration.Networking.Serialization
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
    /// Gets the instance configuration - collection of data sets represented as the <see cref="DataSetConfiguration"/>.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    /// <returns>IEnumerable&lt;IInstanceConfiguration&gt;.</returns>
    public DataSetConfiguration GetInstanceConfiguration(NodeDescriptor descriptor)
    {
      DataSetConfiguration _node = DataSetsList.Where<DataSetConfiguration>(x => x.Root.CreateWrapper().CompareTo(descriptor) == 0).FirstOrDefault<DataSetConfiguration>();
      if (_node == null)
        _node = DataSetConfiguration.Create(descriptor);
      return _node;
    }

    #endregion API

    #region IConfigurationDataFactory

    /// <summary>
    /// Gets and instance of <see cref="ConfigurationData" />.
    /// </summary>
    /// <returns>Returns an instance of <see cref="ConfigurationData" />.</returns>
    public ConfigurationData GetConfigurationData()
    {
      return this;
    }

    /// <summary>
    /// Gets or sets the delegate capturing functionality that is executed when the configuration is changing.
    /// </summary>
    /// <value>The m_ on changed.</value>
    [XmlIgnore]
    public Action OnChanged
    {
      get; set;
    }

    /// <summary>
    /// Called when the configuration is loaded.
    /// </summary>
    public virtual void OnLoaded() { }

    /// <summary>
    /// Called before saving the configuration.
    /// </summary>
    public virtual void OnSaving()
    {
      if (b_DataSetConfigurationList == null)
        return;
      if (m_PendingChanges)
        DataSets = b_DataSetConfigurationList.ToArray<DataSetConfiguration>();
      if (m_MessageHandlersCollectionChanged)
        MessageHandlers = m_ObservableMessageHandlers.Select<ICloneable, MessageHandlerConfiguration>(x => (MessageHandlerConfiguration)x.Clone()).ToArray<MessageHandlerConfiguration>();
    }

    #endregion IConfigurationDataFactory

    #region private

    private bool m_PendingChanges = false;
    private bool m_MessageHandlersCollectionChanged = false;
    private List<DataSetConfiguration> b_DataSetConfigurationList;
    private ObservableCollection<MessageHandlerConfiguration> m_ObservableMessageHandlers;

    private void PendingChanges()
    {
      m_PendingChanges = true;
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

    #endregion private
  }
}