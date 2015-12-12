
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{
  /// <summary>
  /// Class OPCUAServerSimulator - the class is to be used as a simulator of Address Space component.
  /// It is used to simulate producer role on the server side.
  /// </summary>
  internal class OPCUAServerProducerSimulator : DataManagementSetup
  {

    #region creator
    internal static DataManagementSetup CreateDevice(IMessageHandlerFactory messageHandlerFactory, Guid dataSetGuid)
    {
      AssociationConfigurationId = dataSetGuid;
      DataManagementSetup _ret = new OPCUAServerProducerSimulator();
      _ret.ConfigurationFactory = new MyConfigurationFactory();
      _ret.BindingFactory = new MyBindingFFactory();
      _ret.EncodingFactory = new MyEncodingFactory();
      _ret.MessageHandlerFactory = messageHandlerFactory;
      return _ret;
    }
    #endregion

    #region testing environment
    /// <summary>
    /// Checks the consistency of the all items in the <see cref="AssociationsCollection"/> collection.
    /// </summary>
    internal void CheckConsistency()
    {
      foreach (ProducerAssociation _item in AssociationsCollection.Values)
        CheckConsistency(_item);
      foreach (MyMessageWriter _item in MessageHandlersCollection.Values)
        _item.CheckConsistency();
    }
    private void CheckConsistency(ProducerAssociation _item)
    {

      Assert.AreEqual(HandlerState.Operational, _item.State.State);
      Assert.AreEqual<Guid>(AssociationConfigurationId, _item.DataDescriptor.Guid);
      Assert.AreEqual<string>(AssociationConfigurationInformationModelURI, _item.DataDescriptor.Identifier.ToString());
      Assert.AreEqual<string>(AssociationConfigurationDataSymbolicName, _item.DataDescriptor.SymbolicName);

    }
    internal void Update(object value, string name)
    {
      ((MyBindingFFactory)this.BindingFactory).Update(value, name);
    }
    #endregion

    #region Factories set
    /// <summary>
    /// Class MyConfigurationFactory.
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
        return new ConfigurationData() { DataSets = GetAssociations(), MessageHandlers = GetMessageTransport() };
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
      private MessageHandlerConfiguration[] GetMessageTransport()
      {
        return new MessageWriterConfiguration[] { new MessageWriterConfiguration() { ProducerAssociationConfigurations = GetTransportAssociations(),
                                                                                     Configuration = null,
                                                                                     Name = "UDP",
                                                                                     TransportRole = AssociationRole.Producer } };
      }
      private ProducerAssociationConfiguration[] GetTransportAssociations()
      {
        return new ProducerAssociationConfiguration[] { new ProducerAssociationConfiguration() { AssociationName = AssociationConfigurationAlias, DataSetWriterId = UInt32.MaxValue } };
      }
      private DataSetConfiguration[] GetAssociations()
      {
        return new DataSetConfiguration[] { new DataSetConfiguration() { AssociationName = AssociationConfigurationAlias,
                                                                         AssociationRole = AssociationRole.Producer,
                                                                         DataSet = GetMembers(),
                                                                         DataSymbolicName = "DataSymbolicName",
                                                                         Id = AssociationConfigurationId,
                                                                         RepositoryGroup = m_RepositoryGroup,
                                                                         InformationModelURI= AssociationConfigurationInformationModelURI
        } };
      }
      private FieldMetaData[] GetMembers()
      {
        return new FieldMetaData[]
        {
          new FieldMetaData() { ProcessValueName = "Value1", Encoding = BuiltInType.String, SymbolicName = "Value1" },
          new FieldMetaData() { ProcessValueName = "Value2", Encoding = BuiltInType.Double, SymbolicName = "Value2" },
        };
      }
      #endregion
    }
    private class MyBindingFFactory : IBindingFactory
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
      /// <exception cref="System.NotImplementedException"></exception>
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        throw new NotImplementedException();
      }
      /// <summary>
      /// Gets the producer binding.
      /// </summary>
      /// <param name="repositoryGroup">The repository group.</param>
      /// <param name="variableName">Name of the variable.</param>
      /// <param name="encoding">The encoding.</param>
      /// <returns>IProducerBinding.</returns>
      /// <exception cref="System.ArgumentNullException">repositoryGroup</exception>
      /// <exception cref="System.NotImplementedException"></exception>
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentNullException("repositoryGroup");
        return m_CustomNodesManager.GetProducerBinding(variableName, encoding);
      }
      #endregion

      #region private
      private CustomNodeManager m_CustomNodesManager = new CustomNodeManager();
      #endregion

      #region testing environment
      internal void Update(object value, string name)
      {
        m_CustomNodesManager.Update(value, name);
      }
      #endregion

    }
    private class MyEncodingFactory : IEncodingFactory
    {

      public void UpdateValueConverter(IBinding converter, string repositoryGroup, BuiltInType sourceEncoding)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentOutOfRangeException("repositoryGroup");
        Assert.AreEqual<BuiltInType>(sourceEncoding, converter.Encoding);
      }
      public IUADecoder UADecoder
      {
        get { return m_UADecoder; }
      }
      public IUAEncoder UAEncoder
      {
        get { return m_UAEncoder; }
      }

      private readonly IUADecoder m_UADecoder = new Helpers.UABinaryDecoderImplementation();
      private readonly IUAEncoder m_UAEncoder = new Helpers.UABinaryEncoderImplementation();

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
  internal class MyMessageWriter : IMessageWriter
  {

    public MyMessageWriter(Guid dataSetGuid)
    {
      this.dataSetGuid = dataSetGuid;
      State = new MyState();
    }

    #region IMessageWriter
    public ulong ContentMask
    {
      get { throw new NotImplementedException(); }
    }
    public void Send(Func<int, IProducerBinding> producerBinding, ushort length, ulong contentMask, ISemanticData semanticData, ushort messageSequenceNumber, DateTime timeStamp, MessageHeader.ConfigurationVersionDataType configurationVersion)
    {
      if (length > 2)
        throw new ArgumentOutOfRangeException("length");
      m_Buffer = new Object[length];
      for (int i = 0; i < 2; i++)
        m_Buffer[i] = producerBinding(i);
      m_HaveSendData = true;
    }
    public IAssociationState State
    {
      get;
      private set;
    }
    public void AttachToNetwork()
    {
      m_HaveBeenActivated = true;
    }
    #endregion

    #region testing environment
    internal void ReadData()
    {
      throw new NotFiniteNumberException();
    }
    internal void CheckConsistency()
    {
      Assert.IsNotNull(State);
      Assert.AreEqual<HandlerState>(HandlerState.Operational, State.State);
      Assert.IsNotNull(m_HaveSendData);
      Assert.IsTrue(m_HaveBeenActivated);
    }

    private bool m_HaveBeenActivated = false;
    private bool m_HaveSendData;
    private Object[] m_Buffer = null;
    #endregion

    #region private
    /// <summary>
    /// Class MyState.
    /// </summary>
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
    private Guid dataSetGuid;
    #endregion

  }

}
