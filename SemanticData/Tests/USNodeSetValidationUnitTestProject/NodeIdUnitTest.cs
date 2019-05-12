//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
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
