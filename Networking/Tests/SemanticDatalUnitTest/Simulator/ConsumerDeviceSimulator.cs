
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{

  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// It could be for example HMI or PLC.
  /// </summary>
  internal class ConsumerDeviceSimulator : DataManagementSetup
  {

    #region creator of the ConsumerDeviceSimulator
    internal static DataManagementSetup CreateDevice(IMessageHandlerFactory messageHandlerFactory, UInt32 dataSetGuid)
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
    /// Checks the consistency of the all items in the <see cref="AssociationsCollection"/> collection.
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
      //Assert.AreEqual<UInt32>(AssociationConfigurationId, _item.DataDescriptor.Guid);
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
        return new ConfigurationData() { DataSets = GetAssociations(), MessageHandlers = GetMessageTransport() };
      }
      private MessageHandlerConfiguration[] GetMessageTransport()
      {
        return new MessageHandlerConfiguration[] { new MessageReaderConfiguration() { ConsumerAssociationConfigurations = GetTransportAssociations(),
                                                                                       Configuration = null,
                                                                                       Name = "UDP",
                                                                                       TransportRole = AssociationRole.Consumer } };
      }
      private ConsumerAssociationConfiguration[] GetTransportAssociations()
      {
        return new ConsumerAssociationConfiguration[] { new ConsumerAssociationConfiguration() { AssociationName = AssociationConfigurationAlias, DataSetWriterId = UInt16.MaxValue, PublisherId = Guid.NewGuid() } };
      }
      private DataSetConfiguration[] GetAssociations()
      {
        return new DataSetConfiguration[] {
          new DataSetConfiguration()
          { AssociationName = AssociationConfigurationAlias,
            AssociationRole = AssociationRole.Consumer,
            DataSet = GetMembers(),
            DataSymbolicName = "DataSymbolicName",
            Id = Guid.NewGuid(),
            InformationModelURI= AssociationConfigurationInformationModelURI,
            RepositoryGroup = m_RepositoryGroup
        } };
      }
      private FieldMetaData[] GetMembers()
      {
        return new FieldMetaData[]
        {
          new FieldMetaData() { ProcessValueName = "Value1",   TypeInformation = new UATypeInfo(BuiltInType.String), SymbolicName = "Value1" },
          new FieldMetaData() { ProcessValueName = "Value2", TypeInformation = new UATypeInfo(BuiltInType.Double), SymbolicName = "Value2" },
        };
      }
      protected override void RaiseEvents()
      {
        OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
        OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
      }
      #endregion

    }
    /// <summary>
    /// Class MVVMSimulator it is simulator of a component providing user interface constructed according to the Model View ViewModel pattern
    /// </summary>
    private class MVVMSimulatorFactory : IBindingFactory
    {

      #region IBindingFactory
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentNullException("repositoryGroup");
        return m_ViewModel.GetConsumerBinding(processValueName, fieldTypeInfo.BuiltInType);
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        throw new NotImplementedException();
      }
      #endregion

      private ScreeViewModel m_ViewModel = new ScreeViewModel();

    }
    private class MyEncodingFactory : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentOutOfRangeException("repositoryGroup");
        Assert.AreEqual<BuiltInType>(sourceEncoding.BuiltInType, binding.Encoding.BuiltInType);
      }
      public IUADecoder UADecoder
      {
        get
        {
          return m_UADecoder;
        }
      }
      public IUAEncoder UAEncoder
      {
        get
        {
          throw new NotImplementedException();
        }
      }
      private readonly IUADecoder m_UADecoder = new Helpers.UABinaryDecoderImplementation();
    }
    #endregion

    #region preconfigured settings
    private static UInt32 AssociationConfigurationId = UInt32.MaxValue;
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = "https://github.com/mpostol/OPC-UA-OOI";
    #endregion

  }

  internal class MessageReader : IMessageReader
  {
    public MessageReader(UInt32 dataSetGuid)
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
    public bool CheckDestination(UInt32 dataId)
    {
      return dataId == m_DataSetGuid;
    }
    public ulong ContentMask
    {
      get { throw new NotImplementedException(); }
    }
    #endregion

    #region testing environment
    internal void SendData()
    {
      ReadMessageCompleted(this, new MessageEventArg(this, UInt16.MaxValue, Guid.NewGuid()));
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
    private UInt32 m_DataSetGuid { get; set; }
    private bool m_HaveBeenActivated;
    #endregion

  }

}
