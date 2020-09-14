//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory;
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
      using (DataManagementSetupTest _ndm = new DataManagementSetupTest())
      {
        Assert.IsNull(_ndm.BindingFactory);
        Assert.IsNull(_ndm.ConfigurationFactory);
        Assert.IsNull(_ndm.EncodingFactory);
        Assert.IsNull(_ndm.MessageHandlerFactory);
      }
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    public void InitializeTestMethod()
    {
      using (DataManagementSetupTest _ndm = new DataManagementSetupTest
      {
        BindingFactory = new BindingFactory(),
        ConfigurationFactory = new ConfigurationFactory(),
        EncodingFactory = new EncodingFactory(),
        MessageHandlerFactory = new MessageHandlerFactoryTest()
      }
              )
      {
        Assert.IsNull(_ndm.MessageHandlersCollection);
        _ndm.TestStart();
        Assert.AreEqual<int>(3, _ndm.MessageHandlersCollection.Count());
        _ndm.Dispose();
        ((MessageHandlerFactoryTest)_ndm.MessageHandlerFactory).AssertConsistency();
      }
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RunTestMethod()
    {
      using (DataManagementSetupTest _ndm = new DataManagementSetupTest())
        _ndm.TestStart();
    }

    #region instrumentation
    private class DataManagementSetupTest : DataManagementSetup
    {
      internal void TestStart()
      {
        base.Start();
      }
    }
    private class MessageHandlerFactoryTest : MessageHandlerFactoryFixture
    {
      protected override BinaryDataTransferGraphReceiverFixture NewBinaryDataTransferGraphReceiverFixture()
      {
        return new DTGReceiverTest(); ;
      }
      protected override BinaryDataTransferGraphSenderFixture NewBinaryDataTransferGraphSenderFixture()
      {
        throw new NotImplementedException();
      }
      internal override void AssertConsistency()
      {
        Assert.AreEqual<int>(3, MessageHandlerFactoryFixture.BinaryDataTransferGraphReceiverFixtureList.Count);
        Assert.AreEqual<int>(3, MessageHandlerFactoryFixture.BinaryDataTransferGraphReceiverFixtureList.
          Cast<BinaryDataTransferGraphReceiverFixture>().
          Where<BinaryDataTransferGraphReceiverFixture>((x) => { x.AssertConsistency(); return true; }).Count());
      }
    }
    private class DTGReceiverTest : BinaryDataTransferGraphReceiverFixture
    {
      internal override void AssertConsistency()
      {
        Assert.AreEqual<int>(1, base.NumberOfAttachToNetwork);
        Assert.AreEqual<int>(1, base.DisposeCount);
        Assert.AreEqual<HandlerState>(HandlerState.Operational, base.State.State);
      }
    }
    private class EncodingFactory : IEncodingFactory
    {
      public IUADecoder UADecoder { get; } = new Helpers.UABinaryDecoderImplementation();
      public IUAEncoder UAEncoder => throw new NotImplementedException();
      public void UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
      {
        binding.Converter = null;
        binding.Culture = null;
        binding.Parameter = null;
        Assert.IsNotNull(binding);
      }
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
