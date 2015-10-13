
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.SemanticData.DataManagement.Configuration;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{
  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// It could be for example HMI or PLC.
  /// </summary>
  internal class ConsumerDeviceSimulator : DataManagementSetup
  {

    #region creator of the ConsumerDeviceSimulator
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
    #endregion

    #region tests instrumentation
    /// <summary>
    /// Checks the consistency of the all items in the <see cref="AssociationsCollection"/> colection.
    /// </summary>
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
    #endregion

    #region Factories set
    /// <summary>
    /// Class ConfigurationFactory.
    /// </summary>
    private class MyConfigurationFactory : IConfigurationFactory
    {

      #region IConfigurationFactory
      /// <summary>
      /// Gets the configuration.
      /// </summary>
      /// <returns>Am object of <see cref="ConfigurationData" /> type capturing the communication configuration.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public ConfigurationData GetConfiguration()
      {
        return new ConfigurationData() { Associations = GetAssociations(), MessageTransport = GetMessageTransport() };
      }
      /// <summary>
      /// Occurs after the association configuration has been changed.
      /// </summary>
      public event EventHandler<EventArgs> OnAssociationConfigurationChange;
      /// <summary>
      /// Occurs after the communication configuration has been changed.
      /// </summary>
      public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
      #endregion

      #region configuration
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
          new DataMemberConfiguration() { ProcessValueName = "Value1", SourceEncoding = "System.String", SymbolicName = "Value1" },
          new DataMemberConfiguration() { ProcessValueName = "Value2", SourceEncoding = "System.Double", SymbolicName = "Value2" },
        };
      }
      #endregion

    }
    /// <summary>
    /// Class MVVMSimulator it is simulator of a component providing user interface constructed according to the Model View ViewModel pattern
    /// </summary>
    private class MVVMSimulator : IBindingFactory
    {

      #region IBindingFactory
      /// <summary>
      /// Gets the binding captured by an instance of the <see cref="IConsumerBinding" /> type used by the consumer to save the data in the data repository.
      /// </summary>
      /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
      /// The configuration of the repositories belong to the same group are handled according to the same profile.</param>
      /// <param name="variableName">The name of a variable that is the ultimate destination of the values recovered from messages. Must be unique in the context of the repositories group.
      /// is updated periodically by a data produced - user of the <see cref="IBinding" /> object.</param>
      /// <returns>Returns an object implementing the <see cref="IBinding" /> interface that can be used to update selected variable on the factory side.</returns>
      /// <exception cref="System.ArgumentNullException">repositoryGroup</exception>
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentNullException("repositoryGroup");
        return _viewModel.GetConsumerBinding(variableName);
      }
      /// <summary>
      /// Gets the producer binding.
      /// </summary>
      /// <param name="repositoryGroup">The repository group.</param>
      /// <param name="variableName">Name of the variable.</param>
      /// <returns>IProducerBinding.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
      #endregion

      private ScreeViewModel _viewModel = new ScreeViewModel();

    }
    private class MyEncodingFactory : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentOutOfRangeException("repositoryGroup");
        Assert.AreEqual<string>(sourceEncoding, converter.TargetType.ToString());
      }
    }
    #endregion

    #region preconfigured settings
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private static Guid AssociationConfigurationId = Guid.NewGuid();
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = "https://github.com/mpostol/OPC-UA-OOI";
    #endregion

  }
}
