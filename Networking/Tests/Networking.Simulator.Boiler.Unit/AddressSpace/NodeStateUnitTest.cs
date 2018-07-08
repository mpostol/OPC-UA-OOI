using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.AddressSpace
{
  [TestClass]
  public class NodeStateUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      NodeState _nodeState = new TestNodeState();
      Assert.IsNull(_nodeState.BrowseName);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _nodeState.ChangeMasks);
    }
    #region test instrumentation
    private class TestNodeState : NodeState
    {
      public TestNodeState() : base(NodeClass.Unspecified_0) { }
    }

    #endregion

  }
}
