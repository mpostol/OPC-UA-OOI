
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UAOOI.Configuration.Networking;

namespace UAOOI.Configuration.Networking.Upgrade.Re_l1_00_16
{

  /// <summary>
  /// Class ConfigurationData - contains configuration data of the UANetworking application.
  /// </summary>
  public partial class ConfigurationData : IConfigurationDataFactory
  {


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
      throw new NotImplementedException("The configuration is read only");
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

    Configuration.Networking.Serialization.ConfigurationData IConfigurationDataFactory.GetConfigurationData()
    {
      throw new NotImplementedException();
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
