
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerBindingUnitTest
  {
    #region tests
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void CreatorTestMethod1()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName");
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void GetNewValueTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName");
      Assert.IsNotNull(_bn);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void NewValueTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName");
      Assert.IsNotNull(_bn);
      Assert.IsFalse(_bn.NewValue);
      _pr.Modify("654321");
      Assert.IsTrue(_bn.NewValue);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void CreatorTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName");
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void GetNewValueTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName");
      Assert.IsNotNull(_bn);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void NewValueTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName");
      Assert.IsNotNull(_bn);
      Assert.IsFalse(_bn.NewValue);
      _pr.Modify("654321");
      Assert.IsTrue(_bn.NewValue);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }

    #endregion
    private class ProducerBindingFactory : IBindingFactory
    {
      private ValueClass<string> _value = new ValueClass<string>();
      private ProducerBindingMonitoredValue<string> _monitoredValue = new ProducerBindingMonitoredValue<string>();

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        if (repositoryGroup == "ProducerBinding")
        {
          ProducerBinding<string> _ret = new ProducerBinding<string>(() => _value.Value);
          _value.PropertyChanged += (x, y) => _ret.OnNewValue();
          return _ret;
        }
        else if (repositoryGroup == "ProducerBindingMonitoredValue")
          return _monitoredValue;
        throw new ArgumentOutOfRangeException("repositoryGroup");
      }

      public class ProducerBindingMonitoredValue<type> : ProducerBinding<type>
      {
        public ProducerBindingMonitoredValue()
          : base()
        { }
        public type MonitoredValue
        {
          get
          {
            return b_MyProperty;
          }
          set
          {
            if (Object.Equals(b_MyProperty, value))
              return;
            b_MyProperty = value;
            OnNewValue();
          }
        }
        protected override Func<type> GetReadValueDelegate
        {
          get
          {
            return () => MonitoredValue;
          }
        }
        private type b_MyProperty;

      }

      internal class ValueClass<type> : INotifyPropertyChanged
      {
        public type Value
        {
          get
          {
            return b_Value;
          }
          set
          {
            PropertyChanged.RaiseHandler<type>(value, ref b_Value, "Value", this);
          }
        }
        private type b_Value;
        public event PropertyChangedEventHandler PropertyChanged;
      }
      internal void Modify(string value)
      {
        _value.Value = value;
        _monitoredValue.MonitoredValue = value;
      }

    }

  }

}
