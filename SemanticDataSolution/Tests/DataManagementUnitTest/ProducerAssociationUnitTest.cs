
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerAssociationUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociation")]
    public void ProducerAssociationCreatorTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SemanticData(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetDataSet(), new BindingFactory(Repository), new IEF());
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
    [TestCategory("DataManagement_ConsumerAssociation")]
    public void ConsumerAssociationCreatorTestMethod()
    {
      ConsumerAssociation _ca = new ConsumerAssociation(new SemanticData(), "ConsumerAssociationCreatorTestMethod", PersistentConfiguration.GetDataSet(), new BindingFactory(Repository), new IEF());
      Assert.IsNotNull(_ca);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociation")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddMessageWriterTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SemanticData(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetDataSet(), new BindingFactory(Repository), new IEF());
      Assert.IsNotNull(_npa);
      Assert.IsTrue(Repository.Count > 0);
      _npa.AddMessageWriter(null);
    }


    #region private
    /// <summary>
    /// Class SemanticData.
    /// </summary>
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
    private class BindingFactory : IBindingFactory
    {
      public BindingFactory(Dictionary<string, IBinding> repository)
      {
        m_Repository = repository;
      }
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        IConsumerBinding _ncb = new ConsumerBindingMonitoredValue<object>();
        string _key = String.Format("{0}.{1}", repositoryGroup, variableName);
        m_Repository.Add(_key, _ncb);
        return _ncb;
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        string _key = String.Format("{0}.{1}", repositoryGroup, variableName);
        IProducerBinding _npb = new ProducerBindingMonitoredValue<object>(_key);
        m_Repository.Add(_key, _npb);
        return _npb;
      }
      private Dictionary<string, IBinding> m_Repository = new Dictionary<string, IBinding>();
    }
    private Dictionary<string, IBinding> Repository = new Dictionary<string, IBinding>();
    private class IEF : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        converter.Culture = null;
        converter.Converter = null;
        converter.Parameter = null;
      }
    }
    private class MessageWriter : IMessageWriter
    {
      internal bool IsOk = false;
      public void Send(Func<int, IProducerBinding> producerBinding, int length)
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
    }
    private static void PropertyChangedTestMethod(ProducerBindingMonitoredValue<object> _values)
    {
      bool _isOk = false;
      Assert.IsFalse(_isOk);
      Assert.IsFalse(((IProducerBinding)_values).NewValue);
      _values.PropertyChanged += (x, y) => _isOk = true;
      _values.MonitoredValue = "new value";
      Assert.IsTrue(_isOk);
      ((IProducerBinding)_values).GetFromRepository();
      _isOk = false;
      _values.MonitoredValue = "new value";
      Assert.IsFalse(_isOk);
      _values.MonitoredValue = "";
      Assert.IsTrue(_isOk);
    }
    #endregion

  }
}


