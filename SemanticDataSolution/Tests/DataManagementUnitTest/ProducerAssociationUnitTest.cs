
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerAssociationUnitTest
  {
    #region ProducerAssociation
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociationUnitTest")]
    public void ProducerAssociationCreatorTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SemanticData(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetAssociationConfiguration(), new BindingFactory(Repository), new EncodingFactory());
      Assert.IsNotNull(_npa);
      Assert.IsTrue(Repository.Count > 0);
      ProducerBindingMonitoredValue<object>[] _values = Repository.Values.Cast<ProducerBindingMonitoredValue<object>>().ToArray<ProducerBindingMonitoredValue<object>>();
      Assert.IsTrue(_values.Length > 0);
      PropertyChangedTestMethod(_values[0]);
      MessageWriter _mw = new MessageWriter();
      _npa.AddMessageWriter(_mw);
      Assert.IsFalse(_mw.IsOk);
      _values[0].MonitoredValue = "new value";
      Assert.IsFalse(_mw.IsOk);
      ((IProducerBinding)_values[0]).GetFromRepository();
      _values[0].MonitoredValue = "";
      Assert.IsTrue(((IProducerBinding)_values[0]).NewValue);
      Assert.IsTrue(_mw.IsOk);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociationUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddMessageWriterTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SemanticData(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetAssociationConfiguration(), new BindingFactory(Repository), new EncodingFactory());
      Assert.IsNotNull(_npa);
      Assert.IsTrue(Repository.Count > 0);
      _npa.AddMessageWriter(null);
    }
    #endregion
    private class BindingFactory : IBindingFactory
    {
      public BindingFactory(Dictionary<string, IBinding> repository)
      {
        m_Repository = repository;
      }
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        IConsumerBinding _ncb = new ConsumerBindingMonitoredValue<object>(encoding);
        string _key = String.Format("{0}.{1}", repositoryGroup, variableName);
        m_Repository.Add(_key, _ncb);
        return _ncb;
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        string _key = String.Format("{0}.{1}", repositoryGroup, variableName);
        IProducerBinding _npb = new ProducerBindingMonitoredValue<object>(_key, encoding);
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
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, BuiltInType sourceEncoding)
      {
        converter.Culture = null;
        converter.Converter = null;
        converter.Parameter = null;
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
    private Dictionary<string, IBinding> Repository = new Dictionary<string, IBinding>();
    private static void PropertyChangedTestMethod(ProducerBindingMonitoredValue<object> values)
    {
      bool _isOk = false;
      Assert.IsFalse(_isOk);
      Assert.IsFalse(((IProducerBinding)values).NewValue);
      values.PropertyChanged += (x, y) => _isOk = true;
      values.MonitoredValue = "new value";
      Assert.IsTrue(_isOk);
      Assert.IsTrue(((IProducerBinding)values).NewValue);
      ((IProducerBinding)values).GetFromRepository();
      Assert.IsFalse(((IProducerBinding)values).NewValue);
      _isOk = false;
      values.MonitoredValue = "new value";
      Assert.IsFalse(_isOk);
      values.MonitoredValue = "";
      Assert.IsTrue(_isOk);
    }
    private class MessageWriter : IMessageWriter
    {
      internal bool IsOk = false;
      public void Send(Func<int, IProducerBinding> producerBinding, ushort length, ulong contentMask, ISemanticData semanticData, ushort messageSequenceNumber, DateTime timeStamp, MessageHeader.ConfigurationVersionDataType configurationVersion)
      {
        IsOk = true;
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

  }
}
