//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.UnitTest.Helpers;
using UAOOI.Networking.SemanticData.UnitTest.Simulator;

namespace UAOOI.Networking.SemanticData.UnitTest
{

  [TestClass]
  public class DataManagementSetupUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    public void DataManagementSetupCreatorTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      Assert.IsNull(_ndm.BindingFactory);
      Assert.IsNull(_ndm.ConfigurationFactory);
      Assert.IsNull(_ndm.EncodingFactory);
      Assert.IsNull(_ndm.MessageHandlerFactory);
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    public void InitializeTestMethod()
    {
      TestDataManagementSetup _ndm = new TestDataManagementSetup
      {
        BindingFactory = new BindingFactory(),
        ConfigurationFactory = new ConfigurationFactory(),
        EncodingFactory = new EncodingFactory(),
        MessageHandlerFactory = new MessageHandlerFactory()
      };
      Assert.IsNull(_ndm.MessageHandlersCollection);
      _ndm.TestStart();
      Assert.AreEqual<int>(3, _ndm.MessageHandlersCollection.Count());
      _ndm.Dispose();
      Assert.AreEqual<int>(3, MessageHandlerFactory.MessageHandlersCollection.Count);
      Assert.AreEqual<int>(3, MessageHandlerFactory.MessageHandlersCollection.Where<BinaryDataTransferGraphReceiverFixture>(x => x.CheckConsistency()).Count());
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RunTestMethod()
    {
      TestDataManagementSetup _ndm = new TestDataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.TestStart();
    }

    #region instrumentation
    private class TestDataManagementSetup : DataManagementSetup
    {
      internal void TestStart()
      {
        base.Start();
      }
    }
    private class MessageHandlerFactory : IMessageHandlerFactory
    {

      internal static List<BinaryDataTransferGraphReceiverFixture> MessageHandlersCollection = new List<BinaryDataTransferGraphReceiverFixture>();
      #region IMessageHandlerFactory
      public IBinaryDataTransferGraphReceiver GetBinaryDTGReceiver(string name, string configuration)
      {
        BinaryDataTransferGraphReceiverFixture _newFixture = new BinaryDataTransferGraphReceiverFixture();
        MessageHandlersCollection.Add(_newFixture);
        return _newFixture;
      }
      public IBinaryDataTransferGraphSender GetBinaryDTGSender(string name, string configuration)
      {
        throw new NotImplementedException();
      }
      #endregion
    }
    private class BinaryDataTransferGraphReceiverFixture : IBinaryDataTransferGraphReceiver
    {
      public IAssociationState State { get; set; } = new MyState();
      public event EventHandler<byte[]> OnNewFrameArrived;
      public void AttachToNetwork()
      {
        AttachToNetworkCount++;
      }
      public void Dispose()
      {
        DisposeCount++;
      }

      internal bool CheckConsistency()
      {
        Assert.AreEqual<int>(1, AttachToNetworkCount);
        Assert.AreEqual<int>(1, DisposeCount);
        Assert.AreEqual<HandlerState>(HandlerState.Operational, State.State);
        return true;
      }

      internal int AttachToNetworkCount = 0;
      internal int DisposeCount = 0;
      internal int StateCalled = -1;
    }
    private class EncodingFactory : IEncodingFactory
    {
      public IUADecoder UADecoder => m_IUADecoder;
      public IUAEncoder UAEncoder => throw new NotImplementedException();
      public void UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
      {
        binding.Converter = null;
        binding.Culture = null;
        binding.Parameter = null;
        Assert.IsNotNull(binding);
      }
      private readonly IUADecoder m_IUADecoder = new Helpers.UABinaryDecoderImplementation();

    }
    private class ConfigurationFactory : IConfigurationFactory
    {
      public ConfigurationData GetConfiguration()
      {
        return PersistentConfiguration.GetLocalConfiguration();
      }
      public event EventHandler<EventArgs> OnAssociationConfigurationChange;
      public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
    }
    private class BindingFactory : IBindingFactory
    {
      #region IBindingFactory
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo field)
      {
        return new Binding();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo encoding)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        throw new NotImplementedException();
      }
      #endregion

      private class Binding : IConsumerBinding
      {
        public IValueConverter Converter
        {
          set { }
        }
        public UATypeInfo Encoding => null;
        public object Parameter
        {
          set { }
          get => null;
        }
        public System.Globalization.CultureInfo Culture
        {
          set { }
        }
        public object FallbackValue { set => throw new NotImplementedException(); }
        public void Assign2Repository(object value)
        {
          throw new NotImplementedException();
        }
        public void OnEnabling() { }
        public void OnDisabling()
        {
          throw new NotImplementedException();
        }
      }

    }
    #endregion

  }

}
