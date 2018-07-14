using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.AddressSpace
{
  [TestClass]
  public class BaseInstanceStateUnitTest
  {

    [TestMethod]
    public void AddChildTest()
    {
      BaseInstanceState _parentState = new TestBaseInstanceState(null, nameof(_parentState));
      BaseInstanceState _childState = new TestBaseInstanceState(_parentState, nameof(_childState));
      _parentState.AddChild(_childState);
      Assert.IsNotNull(_parentState.FindChild(null, new List<QualifiedName>() { _childState.BrowseName }, 0));

    }
    [TestMethod]
    public void GetChildrenTest()
    {

      BaseInstanceState _parentState = new TestBaseInstanceState(null, nameof(_parentState));
      BaseInstanceState _childState = new TestBaseInstanceState(null, nameof(_childState));
      _parentState.AddChild(_childState);
      List<BaseInstanceState> _children = new List<BaseInstanceState>();
      _parentState.GetChildren(_children);
      Assert.AreEqual<int>(1, _children.Count);

    }
    [TestMethod]
    public void FindChildTest()
    {
      TestBaseInstanceState _nodeState = new TestBaseInstanceState(null, nameof(_nodeState));
      BaseInstanceState _child = _nodeState.FindChildTest(new TestSystemContext(), null, true, null);
      Assert.IsNull(_child);
    }

    #region test instrumentation
    private class TestSystemContext : ISystemContext { }
    private class TestBaseInstanceState : BaseInstanceState
    {
      public TestBaseInstanceState(NodeState parent, string browseName) : base(parent, NodeClass.Unspecified_0, browseName) { }
      public BaseInstanceState FindChildTest(ISystemContext context, QualifiedName browseName, bool createOrReplace, BaseInstanceState replacement)
      {
        return base.FindChild(context, browseName, createOrReplace, replacement);
      }
    }
    #endregion

  }
}
