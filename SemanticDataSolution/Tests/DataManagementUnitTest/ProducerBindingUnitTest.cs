
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerBindingUnitTest
  {

    #region tests
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingUnitTest")]
    public void CreatorTestMethod1()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName", BuiltInType.String);
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingUnitTest")]
    public void GetNewValueTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName", BuiltInType.String);
      Assert.IsNotNull(_bn);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingUnitTest")]
    public void NewValueTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName", BuiltInType.String);
      Assert.IsNotNull(_bn);
      int _changeCounter = 0;
      _bn.PropertyChanged += (x, y) => _changeCounter++;
      Assert.IsFalse(_bn.NewValue);
      Assert.AreEqual<int>(0, _changeCounter);
      _pr.Modify("654321");
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<int>(1, _changeCounter);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<int>(1, _changeCounter);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
      Assert.AreEqual<int>(1, _changeCounter);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
      _pr.Modify(_testValue);
      Assert.IsFalse(_bn.NewValue);
      Assert.AreEqual<int>(1, _changeCounter);
      _testValue = "987654321";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<int>(2, _changeCounter);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
    }
    #endregion

    #region private
    private class ProducerBindingFactory : IBindingFactory
    {
      #region IBindingFactory
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
        throw new ArgumentOutOfRangeException("repositoryGroup");
      }
      private ValueClass<string> _value = new ValueClass<string>();
      #endregion

      #region test instrumentation
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
      }
      #endregion
    }
    #endregion

  }

}
