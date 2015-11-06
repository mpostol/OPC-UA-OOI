using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UAOOI.DataBindings.UnitTest
{
  [TestClass]
  public class NodeDescriptorBaseUnitTest
  {
    [TestMethod]
    [TestCategory("DataBindings_NodeDescriptorBaseUnitTest")]
    public void CreatorTestMethod()
    {
      NodeDescriptorBase _new = NodeDescriptor.GetTestInstance();
      Assert.IsNotNull(_new);
      Assert.IsNotNull(_new.NodeIdentifier);
    }
    [TestMethod]
    [TestCategory("DataBindings_NodeDescriptorBaseUnitTest")]
    public void IComparerTestMethod()
    {
      NodeDescriptorBase _new = NodeDescriptor.GetTestInstance();
      Assert.IsNotNull(_new);
      Assert.IsNotNull(_new.NodeIdentifier);
      NodeDescriptorBase _Other = NodeDescriptor.GetTestInstance();
      Assert.IsNotNull(_new);
      Assert.IsNotNull(_new.NodeIdentifier);
      Assert.AreEqual<int>(0, _new.CompareTo(_Other));
      Assert.AreNotEqual<NodeDescriptorBase>(_new, _Other);
    }
    [TestMethod]
    [TestCategory("DataBindings_NodeDescriptorBaseUnitTest")]
    public void IEqualityComparerTestMethod()
    {
      NodeDescriptorBase _new = NodeDescriptor.GetTestInstance();
      Assert.IsNotNull(_new);
      Assert.IsNotNull(_new.NodeIdentifier);
      NodeDescriptorBase _other = NodeDescriptor.GetTestInstance();
      Assert.IsNotNull(_new);
      Assert.IsNotNull(_new.NodeIdentifier);
      Assert.AreEqual<int>(_new.GetHashCode(_new), _other.GetHashCode(_other));
      Assert.AreNotEqual<NodeDescriptorBase>(_new, _other);
    }
    [TestMethod]
    [TestCategory("DataBindings_NodeDescriptorBaseUnitTest")]
    [ExpectedException(typeof(ArgumentException))]
    public void DictionaryTestMethod()
    {
      NodeDescriptorBase _new = NodeDescriptor.GetTestInstance();
      Assert.IsNotNull(_new);
      Assert.IsNotNull(_new.NodeIdentifier);
      NodeDescriptorBase _Other = NodeDescriptor.GetTestInstance();
      Assert.IsNotNull(_new);
      Assert.IsNotNull(_new.NodeIdentifier);
      Assert.AreEqual<int>(0, _new.CompareTo(_Other));
      Assert.AreNotEqual<NodeDescriptorBase>(_new, _Other);
      Dictionary<NodeDescriptorBase, NodeDescriptorBase> _newDic = new Dictionary<NodeDescriptorBase, NodeDescriptorBase>();
      _newDic.Add(_new, _new);
      Assert.IsTrue(_newDic.ContainsKey(_new));
      Assert.IsFalse(_newDic.ContainsKey(_Other));  //Looks like framework error
      _newDic.Add(_new, _Other);
    }
  }
}
