
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
      private ProducerBindingMonitoredValue<string> _monitoredValue = new ProducerBindingMonitoredValue<string>("ProducerBindingMonitoredValue._monitoredValue", BuiltInType.String);
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        if (repositoryGroup == "ProducerBinding")
        {
          ProducerBinding<string> _ret = new ProducerBinding<string>("ProducerBinding._value", () => _value.Value, BuiltInType.String);
          _value.PropertyChanged += (x, y) => _ret.OnNewValue();
          return _ret;
        }
        else if (repositoryGroup == "ProducerBindingMonitoredValue")
          return _monitoredValue;
        throw new ArgumentOutOfRangeException("repositoryGroup");
      }

      ///// <summary>
      ///// Class ProducerBindingMonitoredValue - it implements the <see cref="ProducerBinding"/> as a placeholder of the value to send over the network by the producer.
      ///// </summary>
      ///// <typeparam name="type">The type of the object in the repository.</typeparam>
      //public sealed class ProducerBindingMonitoredValue<type> : ProducerBinding<type>
      //{
      //  /// <summary>
      //  /// Initializes a new instance of the <see cref="ProducerBinding{type}" /> class.
      //  /// </summary>
      //  /// <param name="valueName">Name of the "repository group" and "variable" separated by "."</param>
      //  public ProducerBindingMonitoredValue(string valueName)
      //    : base(valueName)
      //  { }
      //  /// <summary>
      //  /// Gets or sets the monitored value - it is placeholder of the variable in the repository.
      //  /// </summary>
      //  /// <value>The monitored value.</value>
      //  public type MonitoredValue
      //  {
      //    get
      //    {
      //      return b_MyProperty;
      //    }
      //    set
      //    {
      //      if (Object.Equals(b_MyProperty, value))
      //        return;
      //      b_MyProperty = value;
      //      OnNewValue();
      //    }
      //  }

      //  protected override Func<type> GetReadValueDelegate
      //  {
      //    get
      //    {
      //      return () => MonitoredValue;
      //    }
      //  }
      //  private type b_MyProperty;

      //}
      
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
