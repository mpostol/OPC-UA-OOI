//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.AddressSpace
{
  [TestClass]
  public class SemanticDataSetSourceUnitTest
  {
    [TestMethod]
    public void TestMethod1()
    {
      using (StateFixture _object = new StateFixture())
      {
        SemanticDataSetSource _register1 = new SemanticDataSetSource(_object, nameof(SemanticDataSetSource));
        Assert.AreEqual<int>(3, _register1.Count);
        Assert.IsTrue(_register1.ContainsKey("Property0"));
        Assert.IsTrue(_register1.ContainsKey("Property1"));
        Assert.IsTrue(_register1.ContainsKey("Property2"));
        Assert.AreEqual<string>(nameof(SemanticDataSetSource), _register1.SemanticDataSetName);
      }
    }
    class StateFixture : BaseInstanceState
    {
      public StateFixture() : base(null, NodeClass.Object_1, "BaseObjectStateFixture")
      {
        new PropertyState<int>(this, "Property0");
        new PropertyState<int>(this, "Property1");
        new PropertyState<int>(this, "Property2");
      }
    }
  }
}
