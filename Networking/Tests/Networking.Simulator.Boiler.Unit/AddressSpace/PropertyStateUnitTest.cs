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
  public class PropertyStateUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {

      PropertyState<double> _property = new PropertyState<double>(null, "PropertyState");
      Assert.IsNotNull(_property.BrowseName);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Value, _property.ChangeMasks);
      Assert.AreEqual<NodeClass>(NodeClass.Variable_2, _property.NodeClass);
      Assert.IsNull(_property.Parent);
      Assert.AreEqual<double>(default(double), _property.Value);
      _property.ClearChangeMasks(new SystemContextFixture(), true);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _property.ChangeMasks);
    }
    [TestMethod]
    public void ValueTest()
    {

      PropertyState<double> _property = new PropertyState<double>(null, "PropertyState");
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Value, _property.ChangeMasks);
      Assert.AreEqual<double>(default(double), _property.Value);
      _property.ClearChangeMasks(new SystemContextFixture(), true);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _property.ChangeMasks);
      ISystemContext _returnedContext = null;
      NodeStateChangeMasks _returnedMask = NodeStateChangeMasks.None;
      NodeState _returnedNodeState = null;
      int _handlerCalled = 0;
      _property.OnStateChanged += (x, y, z) => { _returnedContext = x; _returnedNodeState = y; _returnedMask = z; _handlerCalled++; };
      _property.Value = 999.99;
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Value, _property.ChangeMasks);
      Assert.AreEqual<double>(999.99, _property.Value);
      _property.ClearChangeMasks(new SystemContextFixture(), true);
      Assert.AreEqual<int>(1, _handlerCalled);
      Assert.IsNotNull(_returnedContext);
      Assert.AreSame(_property, _returnedNodeState);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Value, _returnedMask);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _property.ChangeMasks);

    }
    private class SystemContextFixture : ISystemContext { }
  }
}
