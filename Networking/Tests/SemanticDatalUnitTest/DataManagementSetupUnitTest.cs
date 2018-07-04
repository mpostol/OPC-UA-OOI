
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData.Common;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
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
      TestDataManagementSetup _ndm = new TestDataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.BindingFactory = new BindingFactory();
      _ndm.ConfigurationFactory = new ConfigurationFactory();
      _ndm.EncodingFactory = new EncodingFactory();
      _ndm.MessageHandlerFactory = new MessageHandlerFactory();
      _ndm.TestStart();
      Assert.AreEqual<int>(3, _ndm.MessageHandlersCollection.Count);
      Assert.AreEqual<int>(0, _ndm.MessageHandlersCollection.Values.Cast<MessageHandlerFactory.MessageReader>().First().AttachToNetworkCalled);
      Assert.AreEqual<int>(1, _ndm.MessageHandlersCollection.Values.Cast<MessageHandlerFactory.MessageReader>().First().StateCalled);
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
    private class MessageHandlerFactory : IMessageHandlerFactory
    {
      public IMessageReader GetIMessageReader(string name, string configuration, IUADecoder uaDecoder)
      {
        return new MessageReader();
      }
      public IMessageWriter GetIMessageWriter(string name, string configuration, IUAEncoder uaEncoder)
      {
        throw new NotImplementedException();
      }
      internal class MessageReader : IMessageReader
      {
        public IAssociationState State
        {
          get
          {
            StateCalled = Progress++;
            return new AssociationState();
          }
        }
        public void AttachToNetwork()
        {
          AttachToNetworkCalled = Progress++;
        }
        public event EventHandler<MessageEventArg> ReadMessageCompleted;
        public void UpdateMyValues(Func<int, IConsumerBinding> update, int length)
        {
          throw new NotImplementedException();
        }
        public bool CheckDestination(uint dataId)
        {
          throw new NotImplementedException();
        }
        public void Dispose()
        {
          throw new NotImplementedException();
        }
        public ulong ContentMask
        {
          get { throw new NotImplementedException(); }
        }

        #region testing instrumentation
        private class AssociationState : IAssociationState
        {
          public HandlerState State => HandlerState.Operational;
          public void Disable()
          {
            throw new NotImplementedException();
          }
          public void Enable()
          {
            ;
          }
        }
        internal int Progress = 0;
        internal int AttachToNetworkCalled = -1;
        internal int StateCalled = -1;
        #endregion
      }
    }
    private class EncodingFactory : IEncodingFactory
    {
      public IUADecoder UADecoder
      {
        get { return m_IUADecoder; }
      }
      public IUAEncoder UAEncoder
      {
        get
        {
          throw new NotImplementedException();
        }
      }
      public void UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
      {
        binding.Converter = null;
        binding.Culture = null;
        binding.Parameter = null;
        Assert.IsNotNull(binding);
      }
      private IUADecoder m_IUADecoder = new Helpers.UABinaryDecoderImplementation();

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
        public UATypeInfo Encoding
        {
          get { return null; }
        }
        public object Parameter
        {
          set { }
          get { return null; }
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
    private class TestDataManagementSetup : DataManagementSetup
    {
      internal void TestStart()
      {
        base.Start();
      }
    }
  }

}
