//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest
{
  [TestClass]
  public class DataGeneratorUnitTest
  {
    [TestMethod]
    public void GetProducerBindingTest()
    {
      BoilersSetFixture _boiler = new BoilersSetFixture();
      using (DataGenerator _generator = new DataGenerator(_boiler))
      {
        IBindingFactory _bindingFactory = _generator;
        IProducerBinding _binding = _bindingFactory.GetProducerBinding("repositoryGroup", "processValueName", new UATypeInfo(BuiltInType.Boolean));
        Assert.IsNotNull(_binding);
        _binding.Encoding.IsEqual(new UATypeInfo(BuiltInType.Boolean));
        Assert.IsFalse(_binding.NewValue);
        Assert.IsNull(_binding.Parameter);
        int _newValueInvocationCount = 0;
        _binding.PropertyChanged += (x, evetArgs) => _newValueInvocationCount++;
        _boiler.Variable.Change();
        Assert.AreEqual<int>(1, _newValueInvocationCount);
        Assert.IsTrue(_binding.NewValue);
        Assert.IsTrue((bool)_binding.GetFromRepository());
        Assert.IsFalse(_binding.NewValue);
        Assert.AreEqual<int>(1, _newValueInvocationCount);
      }
      Assert.AreEqual<int>(1, _boiler.DisposeCount);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetProducerBindingTypesMismatchTest()
    {
      BoilersSetFixture _boiler = new BoilersSetFixture();
      using (DataGenerator _generator = new DataGenerator(_boiler))
      {
        IBindingFactory _bindingFactory = _generator;
        IProducerBinding _binding = _bindingFactory.GetProducerBinding("repositoryGroup", "processValueName", new UATypeInfo(BuiltInType.Int16));
      }
    }

    private class BoilersSetFixture : IBoilersSet
    {
      internal int DisposeCount = 0;
      internal VariableFixture Variable = new VariableFixture();
      public void Dispose()
      {
        DisposeCount++;
      }
      public void GetSemanticDataSetSources(Action<string, string, IVariable> registerSemanticData)
      {
        registerSemanticData("repositoryGroup", "processValueName", Variable);
      }
    }
    private class VariableFixture : IVariable
    {

      #region IVariable
      public object Value { get; private set; } = false;
      public UATypeInfo ValueType => new UATypeInfo(BuiltInType.Boolean);
      public event NodeStateChangedHandler OnStateChanged;
      #endregion

      internal void Change()
      {
        Value = !(bool)Value;
        OnStateChanged?.Invoke(null, null, NodeStateChangeMasks.Value);
      }
    }
  }
}
