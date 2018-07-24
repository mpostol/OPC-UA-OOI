
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
      Assert.IsNotNull(_nodeState.BrowseName);
      Assert.AreEqual<string>("browseName", _nodeState.BrowseName.Name);
      Assert.IsFalse(_nodeState.BrowseName.NamespaceIndexSpecified);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _nodeState.ChangeMasks);
    }

    #region test instrumentation
    private class TestNodeState : NodeState
    {
      public TestNodeState() : base(NodeClass.Unspecified_0, "browseName") { }
    }
    #endregion

  }
}
