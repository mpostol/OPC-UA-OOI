//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
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

namespace UAOOI.Networking.SemanticData.UnitTest.Simulator
{

  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// It could be for example HMI or PLC.
  /// </summary>
  internal class ConsumerDeviceSimulator : DataManagementSetup
  {

    #region creator of the ConsumerDeviceSimulator
    internal static ConsumerDeviceSimulator CreateDevice(IMessageHandlerFactory messageHandlerFactory, UInt32 dataSetGuid)
    {
      AssociationConfigurationId = dataSetGuid;
      ConsumerDeviceSimulator _ret = new ConsumerDeviceSimulator();
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
    }
    private void CheckConsistency(ConsumerAssociation item)
    {

      Assert.AreEqual(HandlerState.Operational, item.State.State);
      //Assert.AreEqual<UInt32>(AssociationConfigurationId, _item.DataDescriptor.Guid);
      Assert.AreEqual<string>(AssociationConfigurationInformationModelURI, item.DataDescriptor.Identifier.ToString());
      Assert.AreEqual<string>(AssociationConfigurationDataSymbolicName, item.DataDescriptor.SymbolicName);

    }
    internal void InitializeAndRun()
    {
      Start();
    }
    #endregion

    #region Factories set
    /// <summary>
    /// Class ConfigurationFactory.
    /// </summary>
    private class ConsumerConfigurationFactory : ConfigurationFactoryBase<ConfigurationData>
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
                                                                                       Configuration = new MessageChannelConfiguration(),
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
      public IUADecoder UADecoder { get; } = new Helpers.UABinaryDecoderImplementation();
      public IUAEncoder UAEncoder
      {
        get
        {
          throw new NotImplementedException();
        }
      }
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


}
