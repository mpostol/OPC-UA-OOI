using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerBindingMonitoredValueUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingMonitoredValueUnitTest")]
    public void CreatorTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", BuiltInType.String);
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingMonitoredValueUnitTest")]
    public void GetNewValueTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", BuiltInType.String);
      Assert.IsNotNull(_bn);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingMonitoredValueUnitTest")]
    public void NewValueTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", BuiltInType.String);
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

    private class ProducerBindingFactory : IBindingFactory
    {
      private ValueClass<string> _value = new ValueClass<string>();
      private ProducerBindingMonitoredValue<string> _monitoredValue = new ProducerBindingMonitoredValue<string>("ProducerBindingMonitoredValue._monitoredValue", BuiltInType.String);
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        if (repositoryGroup == "ProducerBinding")
        {
          Assert.AreEqual<BuiltInType>(BuiltInType.String, encoding);
          ProducerBinding<string> _ret = new ProducerBinding<string>("ProducerBinding._value", () => _value.Value, encoding);
          _value.PropertyChanged += (x, y) => _ret.OnNewValue();
          return _ret;
        }
        else if (repositoryGroup == "ProducerBindingMonitoredValue")
          return _monitoredValue;
        throw new ArgumentOutOfRangeException("repositoryGroup");
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
