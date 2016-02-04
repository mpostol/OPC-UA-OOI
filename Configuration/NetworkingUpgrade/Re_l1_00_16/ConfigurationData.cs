
using System;
using System.Collections.Generic;

namespace UAOOI.Configuration.Networking.Upgrade.Re_l1_00_16
{

  /// <summary>
  /// Class ConfigurationData - contains configuration data of the UANetworking application.
  /// </summary>
  public partial class ConfigurationData : IConfigurationDataFactory
  {


    #region IConfigurationDataFactory
    /// <summary>
    /// Gets or sets the the delegate capturing functionality tha is executed when the configuration is changing.
    /// </summary>
    /// <value>The m_ on changed.</value>
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
    /// <summary>
    /// Gets and instance of <see cref="T:UAOOI.Configuration.Networking.Serialization.ConfigurationData" />.
    /// </summary>
    /// <returns>Returns an instance of <see cref="T:UAOOI.Configuration.Networking.Serialization.ConfigurationData" />.</returns>
    /// <exception cref="NotImplementedException"></exception>
    Configuration.Networking.Serialization.ConfigurationData IConfigurationDataFactory.GetConfigurationData()
    {
      throw new NotImplementedException();
    }
    #endregion

    #region private
    private List<DataSetConfiguration> b_DataSetConfigurationList;
    private void PendingChanges()
    {
      OnChanged();
    }
    private void M_MessageHandlers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
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
