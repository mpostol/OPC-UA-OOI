
using CAS.UA.IServerConfiguration;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest.Exports
{
  [Export(typeof(IInstanceConfigurationFactory))]
  public class InstanceConfigurationFactory : IInstanceConfigurationFactory
  {
    public IInstanceConfiguration GetIInstanceConfiguration(DataSetConfiguration dataSet, ObservableCollection<MessageHandlerConfiguration> availableHandlers, TraceEvent trace, Action onModification)
    {
      return new InstanceConfiguration(dataSet, availableHandlers, trace, onModification);
    }
    private class InstanceConfiguration : IInstanceConfiguration, INotifyPropertyChanged
    {

      public InstanceConfiguration(DataSetConfiguration dataSet, ObservableCollection<MessageHandlerConfiguration> availableHandlers, TraceEvent trace, Action onModification)
      {
        AvailableMessageHandlers = availableHandlers;
        AssociatedMessageHandlers = new ObservableCollection<MessageHandlerConfiguration>(availableHandlers.Where<MessageHandlerConfiguration>(x => x.Associated(dataSet.AssociationName)).ToArray<MessageHandlerConfiguration>());
        DataSetConfiguration = dataSet;
        PropertyChanged += (x, y) => onModification();
      }
      public event PropertyChangedEventHandler PropertyChanged;
      #region IInstanceConfiguration
      public void ClearConfiguration()
      {
        MessageBox.Show("ClearConfiguration for IInstanceConfigurations is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
      }
      public void Edit()
      {
        MessageBox.Show("Edit for IInstanceConfigurations is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
      }
      #endregion

      #region properties for view interface
      [DisplayName("Available Handlers")]
      [Description("Available massage handlers collection - use the provided row editor to add, remove or modify available data sources.")]
      [Category("Message Handlers")]
      [ReadOnly(false)]
      [TypeConverterAttribute(typeof(CollectionConverter))]
      public ObservableCollection<MessageHandlerConfiguration> AvailableMessageHandlers { get; set; }
      [DisplayName("Associated Handlers")]
      [Description("Associated massage handlers collection - use the provided row editor to add, remove or modify available data sources.")]
      [Category("Message Handlers")]
      [ReadOnly(true)]
      [TypeConverterAttribute(typeof(CollectionConverter))]
      public ObservableCollection<MessageHandlerConfiguration> AssociatedMessageHandlers { get; set; }
      [DisplayName("DataSet")]
      [Description("Selected node DataSet that is to be used by a process data binding manager at run time to couple the instantiated object with the message centric communication.")]
      [Category("Data Set")]
      [ReadOnly(false)]
      [NotifyParentProperty(true)]
      [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
      public DataSetConfiguration DataSetConfiguration { get; set; }
      public override string ToString()
      {
        return $"Configuration of: {DataSetConfiguration} associated with {String.Join(", ", AssociatedMessageHandlers.Select<MessageHandlerConfiguration, string>(x => x.Name))}";
      }

      #endregion

    }

  }
}
