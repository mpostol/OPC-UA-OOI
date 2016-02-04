
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.SemanticData.UnitTest.Simulator;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.UnitTest
{
  [TestClass]
  public class ProducerAssociationUnitTest
  {
    #region ProducerAssociation
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociationUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddMessageWriterTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation
        (new SemanticData(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetAssociationConfiguration(), new BindingFactory(Repository), new EncodingFactory());
      Assert.IsNotNull(_npa);
      Assert.IsTrue(Repository.Count > 0);
      _npa.AddMessageWriter(null);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociationUnitTest")]
    public void ProducerAssociationCreatorTestMethod()
    {
      using (ProducerAssociation _npa = new ProducerAssociation(
                                                                new SemanticData(),
                                                                "DataManagement_ProducerAssociation",
                                                                PersistentConfiguration.GetAssociationConfiguration(),
                                                                new BindingFactory(Repository),
                                                                new EncodingFactory())
                                                                )
      {
        Assert.IsNotNull(_npa);
        Assert.IsTrue(Repository.Count > 0);
        ProducerBindingMonitoredValue<object>[] _values = Repository.Values.Cast<ProducerBindingMonitoredValue<object>>().ToArray<ProducerBindingMonitoredValue<object>>();
        Assert.IsTrue(_values.Length > 0);
        _values[0].MonitoredValue = "1234567";
        MessageWriter _mw = new MessageWriter();
        _npa.AddMessageWriter(_mw);
        System.Threading.Thread.Sleep(1200);
        Assert.AreEqual<int>(1, _mw.IsOk);
        System.Threading.Thread.Sleep(1200);
        Assert.AreEqual<int>(1, _mw.IsOk);
      }
    }
    #endregion
    private class BindingFactory : IBindingFactory
    {
      public BindingFactory(Dictionary<string, IBinding> repository)
      {
        m_Repository = repository;
      }
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo field)
      {
        IConsumerBinding _ncb = new ConsumerBindingMonitoredValue<object>(field);
        string _key = String.Format("{0}.{1}", repositoryGroup, processValueName);
        m_Repository.Add(_key, _ncb);
        return _ncb;
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo encoding)
      {
        string _key = String.Format("{0}.{1}", repositoryGroup, processValueName);
        ProducerBindingMonitoredValue<object> _npb = new ProducerBindingMonitoredValue<object>(_key, new UATypeInfo(encoding.BuiltInType));
        _npb.MonitoredValue = Guid.NewGuid();
        m_Repository.Add(_key, _npb);
        return _npb;
      }

      private Dictionary<string, IBinding> m_Repository = new Dictionary<string, IBinding>();
    }
    private class EncodingFactory : IEncodingFactory
    {
      public IUADecoder UADecoder
      {
        get { return m_UADecoder; }
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
        binding.Culture = null;
        binding.Converter = null;
        binding.Parameter = null;
      }
      private readonly IUADecoder m_UADecoder = new Helpers.UABinaryDecoderImplementation();

    }
    private class SemanticData : ISemanticData
    {
      public Uri Identifier
      {
        get { throw new NotImplementedException(); }
      }
      public string SymbolicName
      {
        get { throw new NotImplementedException(); }
      }
      public IComparable NodeId
      {
        get { throw new NotImplementedException(); }
      }
      public Guid Guid
      {
        get { return Guid.NewGuid(); }
      }
    }
    private class MessageWriter : IMessageWriter
    {
      internal int IsOk = 0;
      public void Send
        (Func<int, IProducerBinding> producerBinding, ushort length, ulong contentMask, FieldEncodingEnum encoding, ushort dataSetWriterId, ushort messageSequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion)
      {
        IsOk++;
        Assert.AreEqual<int>(3, length);
      }
      public IAssociationState State
      {
        get { throw new NotImplementedException(); }
      }
      public void AttachToNetwork()
      {
        throw new NotImplementedException();
      }

      public ulong ContentMask
      {
        get { throw new NotImplementedException(); }
      }
    }
    private Dictionary<string, IBinding> Repository = new Dictionary<string, IBinding>();

  }
}
