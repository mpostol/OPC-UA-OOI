
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
    internal static DataManagementSetup CreateDevice(IMessageHandlerFactory messageHandlerFactory, Guid dataSetGuid)
    {
      AssociationConfigurationId = dataSetGuid;
      DataManagementSetup _ret = new ConsumerDeviceSimulator();
      _ret.ConfigurationFactory = new ConsumerConfigurationFactory();
      _ret.BindingFactory = new MVVMSimulatorFactory();
      _ret.EncodingFactory = new MyEncodingFactory();
      _ret.MessageHandlerFactory = messageHandlerFactory;
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
      foreach (MessageReader _item in MessageHandlersCollection.Values)
        _item.CheckConsistency();
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
    private class ConsumerConfigurationFactory : ConfigurationFactoryBase
    {

      public ConsumerConfigurationFactory()
      {
        this.Loader = m_GetConfiguration;
      }

      #region ConfigurationFactoryBase
      ///// <summary>
      ///// Occurs after the association configuration has been changed.
      ///// </summary>
      public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
      ///// <summary>
      ///// Occurs after the communication configuration has been changed.
      ///// </summary>
      public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
      #endregion

      #region configuration
      /// <summary>
      /// Gets the configuration.
      /// </summary>
      /// <returns>Am object of <see cref="ConfigurationData" /> type capturing the communication configuration.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      private ConfigurationData m_GetConfiguration()
      {
        return new ConfigurationData() { Associations = GetAssociations(), MessageTransport = GetMessageTransport() };
      }
      private MessageTransportConfiguration[] GetMessageTransport()
      {
        return new MessageTransportConfiguration[] { new MessageTransportConfiguration() { Associations = GetTransportAssociations(),
                                                                                           Configuration = null,
                                                                                           Name = "UDP",
                                                                                           TransportRole = AssociationRole.Consumer } };
      }
      private string[] GetTransportAssociations()
      {
        return new string[] { AssociationConfigurationAlias };
      }
      private AssociationConfiguration[] GetAssociations()
      {
        return new AssociationConfiguration[] {
          new AssociationConfiguration()
          { Alias = AssociationConfigurationAlias,
            AssociationRole = AssociationRole.Consumer,
            DataSet = GetMembers(),
            DataSymbolicName = "DataSymbolicName",
            Id = AssociationConfigurationId,
            InformationModelURI= AssociationConfigurationInformationModelURI,
            RepositoryGroup = m_RepositoryGroup
        } };
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
    private class MVVMSimulatorFactory : IBindingFactory
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
        return m_ViewModel.GetConsumerBinding(variableName);
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

      private ScreeViewModel m_ViewModel = new ScreeViewModel();

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
    private static Guid AssociationConfigurationId;
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = "https://github.com/mpostol/OPC-UA-OOI";
    #endregion

  }

  internal class MessageReader : IMessageReader
  {
    public MessageReader(Guid dataSetGuid)
    {
      State = new MyState();
      m_DataSetGuid = dataSetGuid;
    }

    #region IMessageReader
    public event EventHandler<MessageEventArg> ReadMessageCompleted;
    public IAssociationState State
    {
      get;
      private set;
    }
    public void AttachToNetwork()
    {
      m_HaveBeenActivated = true;
    }
    public void UpdateMyValues(Func<int, IConsumerBinding> update, int length)
    {
      for (int i = 0; i < length; i++)
      {
        IConsumerBinding _bind = update(i);
        _bind.Assign2Repository(m_Message[i]);
      }
    }
    public bool IAmDestination(ISemanticData dataId)
    {
      return dataId.Guid == m_DataSetGuid;
    }
    public ulong ContentMask
    {
      get { throw new NotImplementedException(); }
    }
    #endregion

    #region testing environment
    internal void SendData()
    {
      ReadMessageCompleted(this, new MessageEventArg(this));
    }
    internal void CheckConsistency()
    {
      Assert.IsNotNull(State);
      Assert.AreEqual<HandlerState>(HandlerState.Operational, State.State);
      Assert.IsNotNull(ReadMessageCompleted);
      Assert.IsTrue(m_HaveBeenActivated);
    }
    #endregion

    #region private
    private class MyState : IAssociationState
    {

      /// <summary>
      /// Initializes a new instance of the <see cref="MyState"/> class.
      /// </summary>
      public MyState()
      {
        State = HandlerState.Disabled;
      }
      /// <summary>
      /// Gets the current state <see cref="HandlerState" /> of the <see cref="Association" /> instance.
      /// </summary>
      /// <value>The state of <see cref="HandlerState" /> type.</value>
      public HandlerState State
      {
        get;
        private set;
      }
      /// <summary>
      /// This method is used to enable a configured <see cref="Association" /> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational" /> state.
      /// In the case of an error situation, the state changes into <see cref="HandlerState.Error" />. The operation is rejected if the current <see cref="State" />  is not <see cref="HandlerState.Disabled" />.
      /// </summary>
      /// <exception cref="System.ArgumentException">Wrong state</exception>
      public void Enable()
      {
        if (State != HandlerState.Disabled)
          throw new ArgumentException("Wrong state");
        State = HandlerState.Operational;
      }
      /// <summary>
      /// This method is used to disable an already enabled <see cref="Association" /> object.
      /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled" /> or <see cref="HandlerState.NoConfiguration" />.
      /// </summary>
      /// <exception cref="System.ArgumentException">Wrong state</exception>
      public void Disable()
      {
        if (State != HandlerState.Operational)
          throw new ArgumentException("Wrong state");
        State = HandlerState.Disabled;
      }

    }
    private object[] m_Message = new object[] { "123", 1.23 };
    private Guid m_DataSetGuid { get; set; }
    private bool m_HaveBeenActivated;
    #endregion

  }

}
