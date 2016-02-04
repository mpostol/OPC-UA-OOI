
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.SemanticData.UnitTest.Simulator;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;

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
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.BindingFactory = new BF();
      _ndm.ConfigurationFactory = new CF();
      _ndm.EncodingFactory = new EF();
      _ndm.MessageHandlerFactory = new MF();
      _ndm.Initialize();
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RunTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.Run();
    }
    private class MF : IMessageHandlerFactory
    {
      public IMessageReader GetIMessageReader(string name, MessageChannelConfiguration configuration, IUADecoder uaDecoder)
      {
        return new MR();
      }
      public IMessageWriter GetIMessageWriter(string name, MessageChannelConfiguration configuration, IUAEncoder uaEncoder)
      {
        throw new NotImplementedException();
      }
      private class MR : IMessageReader
      {
        public IAssociationState State
        {
          get { throw new NotImplementedException(); }
        }
        public void AttachToNetwork()
        {
          throw new NotImplementedException();
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
        public ulong ContentMask
        {
          get { throw new NotImplementedException(); }
        }
      }
    }
    private class EF : IEncodingFactory
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
    private class CF : IConfigurationFactory
    {
      public ConfigurationData GetConfiguration()
      {
        return PersistentConfiguration.GetLocalConfiguration();
      }
      public event EventHandler<EventArgs> OnAssociationConfigurationChange;
      public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
    }
    private class BF : IBindingFactory
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
        public System.Windows.Data.IValueConverter Converter
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
        public void Assign2Repository(object value)
        {
          throw new NotImplementedException();
        }
        public void OnEnabling()
        {
          throw new NotImplementedException();
        }
        public void OnDisabling()
        {
          throw new NotImplementedException();
        }
      }

    }

  }

}
