
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.DataRepository;
using System;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.UnitTest
{
  [TestClass]
  public class ConsumerBindingMonitoredValueUnitTest
  {
    [TestMethod]
    [TestCategory("ConsumerBindingMonitoredValueUnitTest_ConstructorTest")]
    public void ConstructorTest()
    {
      ConsumerBindingMonitoredValue<Int32> _binding = new ConsumerBindingMonitoredValue<int>(new UATypeInfo(BuiltInType.Int32));
      Assert.IsNotNull(_binding);
    }
    [TestMethod]
    [TestCategory("ConsumerBindingMonitoredValueUnitTest_ConstructorTest")]
    public void ToStringScalarTestMethod()
    {
      ConsumerBindingMonitoredValue<Int32> _binding = new ConsumerBindingMonitoredValue<int>(new UATypeInfo(BuiltInType.Int32));
      string _name = String.Empty;
      _binding.PropertyChanged += (sender, e) => _name = e.PropertyName;
      Assert.IsNotNull(_binding);
      IConsumerBinding _bindingInterface = _binding;
      const int _newValue = 1234567;
      _bindingInterface.Assign2Repository(_newValue);
      Assert.AreEqual<int>(_newValue, _binding.Value);
      Assert.AreEqual<string>(_newValue.ToString(), _binding.ToString());
      Assert.AreEqual<string>("Value", _name);
    }
    [TestMethod]
    [TestCategory("ConsumerBindingMonitoredValueUnitTest_ConstructorTest")]
    public void ToStringArrayTestMethod()
    {
      ConsumerBindingMonitoredValue<Int32[]> _binding = new ConsumerBindingMonitoredValue<int[]>(new UATypeInfo(BuiltInType.Int32, 1));
      string _name = String.Empty;
      _binding.PropertyChanged += (sender, e) => _name = e.PropertyName;
      Assert.IsNotNull(_binding);
      IConsumerBinding _bindingInterface = _binding;
      int[] _newValue = new int[] { 1234567, 7654321 };
      _bindingInterface.Assign2Repository(_newValue);
      Assert.AreEqual<int[]>(_newValue, _binding.Value);
      Assert.AreEqual<string>(@"Array Rank=1 Values [1234567, 7654321]", _binding.ToString());
      Assert.AreEqual<string>("Value", _name);
    }
    [TestMethod]
    [TestCategory("ConsumerBindingMonitoredValueUnitTest_ConstructorTest")]
    public void ToStringCLRArrayTestMethod()
    {
      ConsumerBindingMonitoredValue<Int32[]> _binding = new ConsumerBindingMonitoredValue<int[]>(new UATypeInfo(BuiltInType.Int32, 1));
      string _name = String.Empty;
      _binding.PropertyChanged += (sender, e) => _name = e.PropertyName;
      Assert.IsNotNull(_binding);
      IConsumerBinding _bindingInterface = _binding;
      Array _newValue = new int[] { 1234567, 7654321 };
      _bindingInterface.Assign2Repository(_newValue);
      CollectionAssert.AreEqual(_newValue, _binding.Value);
      Assert.AreEqual<string>(@"Array Rank=1 Values [1234567, 7654321]", _binding.ToString());
      Assert.AreEqual<string>("Value", _name);
    }
    [TestMethod]
    [TestCategory("ConsumerBindingMonitoredValueUnitTest_ConstructorTest")]
    public void ToStringArrayRank3TestMethod()
    {
      ConsumerBindingMonitoredValue<Int32[,,]> _binding = new ConsumerBindingMonitoredValue<int[,,]>(new UATypeInfo(BuiltInType.Int32, 0, new int[] { int.MaxValue, int.MaxValue, int.MaxValue }));
      string _name = String.Empty;
      _binding.PropertyChanged += (sender, e) => _name = e.PropertyName;
      Assert.IsNotNull(_binding);
      IConsumerBinding _bindingInterface = _binding;
      int[,,] _newValue = new int[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };
      _bindingInterface.Assign2Repository(_newValue);
      Assert.AreEqual<int[,,]>(_newValue, _binding.Value);
      Assert.AreEqual<string>(@"Array Rank=3 Values [1, 2, 3, 4, 5, 6, 7, 8]", _binding.ToString());
      Assert.AreEqual<string>("Value", _name);
    }
    [TestMethod]
    [TestCategory("ConsumerBindingMonitoredValueUnitTest_ConstructorTest")]
    public void ToStringCLRArrayRank3TestMethod()
    {
      ConsumerBindingMonitoredValue<Int32[,,]> _binding = new ConsumerBindingMonitoredValue<int[,,]>(new UATypeInfo(BuiltInType.Int32, 0, new int[] { int.MaxValue, int.MaxValue, int.MaxValue }));
      string _name = String.Empty;
      _binding.PropertyChanged += (sender, e) => _name = e.PropertyName;
      Assert.IsNotNull(_binding);
      IConsumerBinding _bindingInterface = _binding;
      Array _newValue = new int[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };
      _bindingInterface.Assign2Repository(_newValue);
      CollectionAssert.AreEqual(_newValue, _binding.Value);
      Assert.AreEqual<string>(@"Array Rank=3 Values [1, 2, 3, 4, 5, 6, 7, 8]", _binding.ToString());
      Assert.AreEqual<string>("Value", _name);
    }
    [TestMethod]
    [TestCategory("ConsumerBindingMonitoredValueUnitTest_ConstructorTest")]
    [ExpectedException(typeof(InvalidCastException))]
    public void ToStringCLRArrayRankErrorTestMethod()
    {
      ConsumerBindingMonitoredValue<Int32[,,]> _binding = new ConsumerBindingMonitoredValue<int[,,]>(new UATypeInfo(BuiltInType.Int32, 0, new int[] { int.MaxValue, int.MaxValue, int.MaxValue }));
      string _name = String.Empty;
      _binding.PropertyChanged += (sender, e) => _name = e.PropertyName;
      Assert.IsNotNull(_binding);
      IConsumerBinding _bindingInterface = _binding;
      Array _newValue = new int[,] { { 1, 2 }, { 3, 4 } };
      _bindingInterface.Assign2Repository(_newValue);
    }

  }
}
