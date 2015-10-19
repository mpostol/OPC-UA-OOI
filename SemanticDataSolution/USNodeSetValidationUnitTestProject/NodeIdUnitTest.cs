using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class NodeIdUnitTest
  {
    [TestMethod]
    [TestCategory("Code")]
    public void NodeIdGetHashCodeTestMethod()
    {
      Random _random = new Random();
      Dictionary<NodeId, NodeId> _dic = new Dictionary<NodeId, NodeId>();
      for (int i = 0; i < 5000; i++)
      {
        NodeId _nid = (uint)_random.Next(0, int.MaxValue);
        if (_dic.ContainsKey(_nid))
          continue;
        _dic.Add(_nid, _nid);
      }
      Assert.IsTrue(true);
    }
  }
}
