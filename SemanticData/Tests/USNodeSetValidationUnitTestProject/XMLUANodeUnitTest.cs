//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class XMLUANodeUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void UANodeEqualsTest()
    {
      UANode _first = TestData.CreateUAReferenceType();
      UANode _second = TestData.CreateUAReferenceType();
      Assert.IsTrue(_first == _second);
    }

    [TestMethod]
    public void UANodeEquals()
    {
      UANode _first = TestData.CreateUAObject();
      UANode _second = TestData.CreateUAObject();
      Assert.AreNotSame(_first, _second); //to make sure we have two objects
      Assert.IsTrue(_first != null);
      Assert.IsTrue(null != _first);
      Assert.IsTrue(_first == _second);
      Assert.IsTrue(_second == _first);
    }

    [TestMethod]
    public void UAInstanceEqualsTest()
    {
      UAInstance _first = TestData.CreateUAObject();
      UAInstance _second = TestData.CreateUAObject();
      _second.ParentNodeId = Guid.NewGuid().ToString();
      _second.NodeId = Guid.NewGuid().ToString();
      _second.BrowseName = Guid.NewGuid().ToString();
      Assert.IsTrue(_first == _second);
    }

    [TestMethod]
    public void UAObjectEqualsTest()
    {
      UAObject _first = TestData.CreateUAObject();
      UAObject _second = TestData.CreateUAObject();
      _second.EventNotifier ^= _second.EventNotifier;
      Assert.IsTrue(_first != _second);
    }
  }
}