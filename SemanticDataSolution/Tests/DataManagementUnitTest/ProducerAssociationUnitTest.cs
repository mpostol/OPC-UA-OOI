
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerAssociationUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociation")]
    public void ProducerAssociationCreatorTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SD(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetDataSet(), new IBF(Repository), new IEF());
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
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociation")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddMessageWriterTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SD(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetDataSet(), new IBF(Repository), new IEF());
      Assert.IsNotNull(_npa);
      Assert.IsTrue(Repository.Count > 0);
      _npa.AddMessageWriter(null);
    }

    #region private
    private class SD : ISemanticData
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
    private class IBF : IBindingFactory
    {
      public IBF(Dictionary<string, IProducerBinding> repository)
      {
        m_Repository = repository;
      }
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        return new MyBinding();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        string _key = String.Format("{0}.{1}", repositoryGroup, variableName);
        IProducerBinding _npb = new ProducerBindingMonitoredValue<object>(_key);
        m_Repository.Add(_key, _npb);
        return _npb;
      }

      private class MyBinding : IConsumerBinding
      {
        public System.Windows.Data.IValueConverter Converter
        {
          set { }
        }
        public Type TargetType
        {
          get { throw new NotImplementedException(); }
        }
        public object Parameter
        {
          set { }
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
      private Dictionary<string, IProducerBinding> m_Repository = new Dictionary<string, IProducerBinding>();
      public event PropertyChangedEventHandler PropertyChanged;
    }
    private Dictionary<string, IProducerBinding> Repository = new Dictionary<string, IProducerBinding>();
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
      public void Send(Func<int, IProducerBinding> producerBinding)
      {
        IsOk = true;
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
    #endregion

  }
}


