//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.SemanticData.UnitTest.Simulator
{
  /// <summary>
  /// Class OPCUAServerSimulator - the class is to be used as a simulator of Address Space component.
  /// It is used to simulate producer role on the server side.
  /// </summary>
  internal class OPCUAServerProducerSimulator : DataManagementSetup
  {

    #region creator
    internal static OPCUAServerProducerSimulator CreateDevice(IMessageHandlerFactory messageHandlerFactory, Guid dataSetGuid)
    {
      AssociationConfigurationId = dataSetGuid;
      OPCUAServerProducerSimulator _ret = new OPCUAServerProducerSimulator();
      _ret.ConfigurationFactory = new MyConfigurationFactory();
      _ret.BindingFactory = new MyBindingFFactory();
      _ret.EncodingFactory = new MyEncodingFactory();
      _ret.MessageHandlerFactory = messageHandlerFactory;
      return _ret;
    }
    #endregion

    #region testing environment
    internal void TestStart()
    {
      base.Start();
    }
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
                                                                                     Configuration = new MessageChannelConfiguration() { ChannelConfiguration = "4840,localhost" },
                                                                                     Name = "UDP",
                                                                                     TransportRole = AssociationRole.Producer } };
      }
      private ProducerAssociationConfiguration[] GetTransportAssociations()
      {
        return new ProducerAssociationConfiguration[] { new ProducerAssociationConfiguration() { AssociationName = AssociationConfigurationAlias, DataSetWriterId = UInt16.MaxValue } };
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
                                                                       }
        };
      }
      private FieldMetaData[] GetMembers()
      {
        return new FieldMetaData[]
        {
          new FieldMetaData() { ProcessValueName = "Value1", TypeInformation = new UATypeInfo( BuiltInType.String), SymbolicName = "Value1" },
          new FieldMetaData() { ProcessValueName = "Value2", TypeInformation = new UATypeInfo( BuiltInType.Double), SymbolicName = "Value2" },
        };
      }
      #endregion
    }
    private class MyBindingFFactory : IBindingFactory
    {

      #region IBindingFactory
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentNullException("repositoryGroup");
        return m_CustomNodesManager.GetProducerBinding(processValueName, fieldTypeInfo.BuiltInType);
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

      public void UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
      {
        if (repositoryGroup != m_RepositoryGroup)
          throw new ArgumentOutOfRangeException("repositoryGroup");
        Assert.AreEqual<BuiltInType>(sourceEncoding.BuiltInType, binding.Encoding.BuiltInType);
      }

      public IUADecoder UADecoder { get; } = new Helpers.UABinaryDecoderImplementation();

      public IUAEncoder UAEncoder { get; } = new Helpers.UABinaryEncoderImplementation();
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
    /// <summary>
    /// Gets the content mask. The content mast read from the message or provided by the writer.
    /// The order of the bits starting from the least significant bit matches the order of the data items
    /// within the data set.
    /// </summary>
    /// <value>The content mask represented as unsigned number <see cref="T:System.UInt64" />. The order of the bits starting from the least significant
    /// bit matches the order of the data items within the data set.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public ulong ContentMask
    {
      get { throw new NotImplementedException(); }
    }
    /// <summary>
    /// Sends the data described by a data set collection to remote destination.
    /// </summary>
    /// <param name="producerBinding">Encapsulates functionality used by the <see cref="T:UAOOI.Networking.SemanticData.MessageHandling.IMessageWriter" /> to collect all the data (data set items) required to prepare new message and send it over the network.</param>
    /// <param name="length">Number of items to be send used to calculate the length of the message.</param>
    /// <param name="contentMask">The content mask represented as unsigned number <see cref="T:System.UInt64" />. The order of the bits starting from the least significant
    /// bit matches the order of the data items within the data set.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="dataSelector">The data selector.</param>
    /// <param name="messageSequenceNumber">The message sequence number. A monotonically increasing sequence number assigned by the publisher to each message sent.</param>
    /// <param name="timeStamp">The time stamp - the time the Data was collected.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    /// <exception cref="ArgumentOutOfRangeException">length</exception>
    public void Send
      (Func<int, IProducerBinding> producerBinding, ushort length, ulong contentMask, FieldEncodingEnum encoding, DataSelector
       dataSelector, ushort messageSequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion)
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

    public void Dispose()
    {
      throw new NotImplementedException();
    }

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
