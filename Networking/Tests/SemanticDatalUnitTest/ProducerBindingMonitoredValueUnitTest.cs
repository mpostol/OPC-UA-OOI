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
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingMonitoredValueUnitTest")]
    public void GetNewValueTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(_bn);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingMonitoredValueUnitTest")]
    public void NewValueTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", new UATypeInfo(BuiltInType.String));
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
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingMonitoredValueUnitTest")]
    public void WrongInitializationTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(_bn);
      Assert.IsFalse(_bn.NewValue);
      _pr.Modify("654321");
      Assert.IsTrue(_bn.NewValue);
      int _changeCounter = 0;
      _bn.PropertyChanged += (x, y) => _changeCounter++;
      Assert.AreEqual<int>(0, _changeCounter);
      _pr.Modify("1234567");
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<int>(0, _changeCounter);
      _pr.Modify("654321");
      _pr.Modify("1234567");
      Assert.AreEqual<int>(0, _changeCounter);
    }
    [TestMethod]
    [TestCategory("DataManagement_ProducerBindingMonitoredValueUnitTest")]
    public void CorrectInitializationTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName", new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(_bn);
      Assert.IsFalse(_bn.NewValue);
      _pr.Modify("654321");
      Assert.IsTrue(_bn.NewValue);
      _bn.GetFromRepository();
      Assert.IsFalse(_bn.NewValue);
      int _changeCounter = 0;
      _bn.PropertyChanged += (x, y) => _changeCounter++;
      Assert.AreEqual<int>(0, _changeCounter);
      _pr.Modify("1234567");
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<int>(1, _changeCounter);
      _bn.GetFromRepository();
      _pr.Modify("654321");
      _pr.Modify("1234567");
      Assert.AreEqual<int>(2, _changeCounter);
    }
    private class ProducerBindingFactory : IBindingFactory
    {
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        if (repositoryGroup == "ProducerBindingMonitoredValue")
          return _monitoredValue;
        throw new ArgumentOutOfRangeException("repositoryGroup");
      }
      private ProducerBindingMonitoredValue<string> _monitoredValue = new ProducerBindingMonitoredValue<string>("ProducerBindingMonitoredValue._monitoredValue", new UATypeInfo(BuiltInType.String));
      internal void Modify(string value)
      {
        _monitoredValue.MonitoredValue = value;
      }
    }
  }
}
