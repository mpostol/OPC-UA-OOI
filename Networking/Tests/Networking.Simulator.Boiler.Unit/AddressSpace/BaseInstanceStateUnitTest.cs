using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.AddressSpace
{
  [TestClass]
  public class BaseInstanceStateUnitTest
  {

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void AddChildTest()
    {
      BaseInstanceState _nodeState = new TestBaseInstanceState(null);
      _nodeState.AddChild(new TestBaseInstanceState(null));
    }
    [TestMethod]
    public void FindChildTest()
    {
      TestBaseInstanceState _nodeState = new TestBaseInstanceState(null);
      BaseInstanceState _child = _nodeState.FindChildTest(new TestSystemContext(), null, true, null);
      Assert.IsNull(_child);
    }

    #region test instrumentation
    private class TestSystemContext : ISystemContext { }
    private class TestBaseInstanceState : BaseInstanceState
    {
      public TestBaseInstanceState(NodeState nodeState) : base(nodeState, NodeClass.Unspecified_0, "browseName") { }
      public BaseInstanceState FindChildTest(ISystemContext context, QualifiedName browseName, bool createOrReplace, BaseInstanceState replacement)
      {
        return base.FindChild(context, browseName, createOrReplace, replacement);
      }
    }
    #endregion

  }
}
