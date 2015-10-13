
using System;
using System.ComponentModel;
using UAOOI.SemanticData.DataManagement.Configuration;
using UAOOI.SemanticData.DataManagement.DataRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{
  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// It could be for example HMI or PLC.
  /// </summary>
  internal class ConsumerDeviceSimulator : DataManagementSetup
  {

    internal static DataManagementSetup CreateDevice(IMessageHandlerFactory communicationFactory, Guid dataSetGuid)
    {
      AssociationConfigurationId = dataSetGuid;
      DataManagementSetup _ret = new ConsumerDeviceSimulator();
      _ret.ConfigurationFactory = new MyConfigurationFactory();
      _ret.BindingFactory = new MVVMSimulator();
      _ret.EncodingFactory = new MyEncodingFactory();
      _ret.MessageHandlerFactory = communicationFactory;
      return _ret;
    }
    /// <summary>
    /// Class ConfigurationFactory.
    /// </summary>
    private class MyConfigurationFactory : IConfigurationFactory
    {
      /// <summary>
      /// Gets the configuration.
      /// </summary>
      /// <returns>Am object of <see cref="ConfigurationData" /> type capturing the communication configuration.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public ConfigurationData GetConfiguration()
      {
        return new ConfigurationData() { Associations = GetAssociations(), MessageTransport = GetMessageTransport() };
      }
      private MessageTransportConfiguration[] GetMessageTransport()
      {
        return new MessageTransportConfiguration[] { new MessageTransportConfiguration() { Associations = GetTransportAssociations(), 
                                                                                           Configuration = null, 
                                                                                           Name = "MessageTransportConfiguration", 
                                                                                           TransportRole = AssociationRole.Consumer } };
      }
      private string[] GetTransportAssociations()
      {
        return new string[1] { AssociationConfigurationAlias };
      }
      private AssociationConfiguration[] GetAssociations()
      {
        return new AssociationConfiguration[] { new AssociationConfiguration() { Alias = AssociationConfigurationAlias, 
                                                                                 AssociationRole = AssociationRole.Consumer, 
                                                                                 DataSet = GetDataSet(), 
                                                                                 DataSymbolicName = "DataSymbolicName",
                                                                                 Id = AssociationConfigurationId,
                                                                                 InformationModelURI= AssociationConfigurationInformationModelURI  
        } };
      }
      private DataSetConfiguration GetDataSet()
      {
        return new DataSetConfiguration() { Members = GetMembers(), RepositoryGroup = m_RepositoryGroup };
      }
      private DataMemberConfiguration[] GetMembers()
      {
        return new DataMemberConfiguration[]
        {
          new DataMemberConfiguration() { ProcessValueName = "Value1", SourceEncoding = "string", SymbolicName = "Value1" },
          new DataMemberConfiguration() { ProcessValueName = "Value2", SourceEncoding = "double", SymbolicName = "Value2" },
        };
      }
      public event EventHandler<EventArgs> OnAssociationConfigurationChange;
      public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

    }
    private class MVVMSimulator : IBindingFactory
    {
      ScreeViewModel _viewModel = new ScreeViewModel();
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentNullException("repositoryGroup");
        return _viewModel.GetConsumerBinding(variableName);
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
    }
    private class ScreeViewModel : INotifyPropertyChanged
    {

      #region API
      /// <summary>
      /// Helper method that gets the consumer binding.
      /// </summary>
      /// <param name="variableName">Name of the variable.</param>
      /// <returns>IConsumerBinding.</returns>
      /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
      internal IConsumerBinding GetConsumerBinding(string variableName)
      {
        if (variableName == "Value1")
        {
          Value1 = new ConsumerBindingMonitoredValue<string>();
          return Value1;
        }
        else if (variableName == "Value2")
        {
          Value2 = new ConsumerBindingMonitoredValue<double>();
          return Value2;
        }
        throw new ArgumentOutOfRangeException("variableName");
      }
      #endregion

      #region ViewModel implementation
      public ConsumerBindingMonitoredValue<string> Value1
      {
        get
        {
          return b_Value1;
        }
        set
        {
          PropertyChanged.RaiseHandler<ConsumerBindingMonitoredValue<string>>(value, ref b_Value1, "Value1", this);
        }
      }
      public ConsumerBindingMonitoredValue<double> Value2
      {
        get
        {
          return b_Value2;
        }
        set
        {
          PropertyChanged.RaiseHandler<ConsumerBindingMonitoredValue<double>>(value, ref b_Value2, "Value2", this);
        }
      }
      private ConsumerBindingMonitoredValue<string> b_Value1;
      private ConsumerBindingMonitoredValue<double> b_Value2;
      public event PropertyChangedEventHandler PropertyChanged;

      #endregion
    }
    private class MyEncodingFactory : Encoding.IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentOutOfRangeException("repositoryGroup");
      }
    }
    internal void ThreadSimulator(Action<byte[]> predicate)
    {
      byte[] _buffer = m_UDPSimulator.Receive();
      predicate(_buffer);
    }
    private UDPSimulator m_UDPSimulator = null;
    internal void CheckConsistency()
    {
      foreach (ConsumerAssociation _item in AssociationsCollection.Values)
        CheckConsistency(_item);
    }
    private void CheckConsistency(ConsumerAssociation _item)
    {

      Assert.AreEqual(HandlerState.Operational, _item.State.State);
      Assert.AreEqual<Guid>(AssociationConfigurationId, _item.DataDescriptor.Guid);
      Assert.AreEqual<string>(AssociationConfigurationInformationModelURI, _item.DataDescriptor.Identifier.ToString());
      Assert.AreEqual<string>(AssociationConfigurationDataSymbolicName, _item.DataDescriptor.SymbolicName);
    }

    #region preconfigured settings
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private static Guid AssociationConfigurationId = Guid.NewGuid();
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = "https://github.com/mpostol/OPC-UA-OOI";
    #endregion

  }
}
