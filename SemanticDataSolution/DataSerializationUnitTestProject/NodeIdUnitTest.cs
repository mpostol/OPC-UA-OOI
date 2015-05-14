using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using System.Collections.Generic;
using System.Linq;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization.UnitTest
{
  [TestClass]
  public class NodeIdUnitTest
  {

    [TestMethod]
    public void NodeIdValidTestMethod1()
    {
      //Numeric
      NodeId _ni = NodeId.Parse("i=13");
      Assert.IsNotNull(_ni);
      _ni = NodeId.Parse("i=0");
      Assert.IsNotNull(_ni);
      //STRING
      _ni = NodeId.Parse("ns=10;s=Hello:World");
      Assert.IsNotNull(_ni);
      //GUID
      _ni = NodeId.Parse("g=09087e75-8e5e-499b-954f-f2a9603db28a");
      Assert.IsNotNull(_ni);
      //OPAQUE
      _ni = NodeId.Parse("ns=1;b=M/RbKBsRVkePCePcx24oRA==");
      Assert.IsNotNull(_ni);
      //?? Default string - should be not valid ?
      _ni = NodeId.Parse("M/RbKBsRVkePCePcx24oRA==");
      Assert.IsNotNull(_ni);
    }
    [TestMethod]
    [ExpectedException(typeof(ServiceResultException))]
    public void NodeIdNonValidNumericTestMethod1()
    {
      NodeId _ni;
      _ni = NodeId.Parse("ns=10;i=-1"); //this example is in the specification as valid 
    }
    [TestMethod]
    [ExpectedException(typeof(ServiceResultException))]
    public void NodeIdNonValidNumericTestMethod2()
    {
      NodeId _ni;
      _ni = NodeId.Parse("ns=-10;i=01");
    }
    [TestMethod]
    public void NodeIdOperatorTestMethod1()
    {
      NodeId _property = new NodeId(VariableTypes.PropertyType);
      Assert.AreNotSame(_property, VariableTypeIds.PropertyType);
      Assert.IsFalse(_property == VariableTypeIds.PropertyType); //This class do not support == operator;  consider implementation.
      Assert.IsTrue(_property.Equals(VariableTypeIds.PropertyType));
    }
    [TestMethod]
    public void NodeIdCompareToTestMethod()
    {
      NodeId _property = new NodeId(VariableTypes.PropertyType);
      Assert.AreNotSame(_property, VariableTypeIds.PropertyType);
      int _res = _property.CompareTo(VariableTypeIds.PropertyType);
      Assert.AreEqual<int>(0, _res);
    }
    [TestMethod]
    public void NodeIdToStringTestMethod()
    {
      NodeId _property = new NodeId(VariableTypes.PropertyType);
      Assert.AreNotSame(_property, VariableTypeIds.PropertyType);
      string _res = _property.ToString();
      Assert.AreEqual<string>("i=68", _res);
    }
    [TestMethod]
    public void NodeIdToStringTestMethod2()
    {
      NodeId _property = new NodeId(68, 1);
      Assert.AreNotSame(_property, VariableTypeIds.PropertyType);
      string _res = _property.ToString();
      Assert.AreEqual<string>("ns=1;i=68", _res);
    }
    [TestMethod]
    public void NodeIdToStringTestMethod3()
    {
      System.Guid _gd = new System.Guid("e08edc80-e771-43ff-b8f6-1fbb62ae5cda");
      NodeId _property = new NodeId(_gd, 1);
      string _res = _property.ToString();
      Assert.AreEqual<string>("ns=1;g=e08edc80-e771-43ff-b8f6-1fbb62ae5cda", _res);
    }
    [TestMethod]
    public void NodeIdDictionaryTestMethod()
    {
      Dictionary<NodeId, string> _dc = new Dictionary<NodeId, string>();
      _dc.Add(VariableTypeIds.PropertyType, VariableTypeIds.PropertyType.ToString());
      Assert.IsTrue(_dc.ContainsKey(VariableTypeIds.PropertyType));
    }
    [TestMethod]
    public void NodeIdDictionaryTestMethod2()
    {
      Dictionary<string, NodeId> _dc = new Dictionary<string, NodeId>();
      _dc.Add(VariableTypeIds.PropertyType.ToString(), VariableTypeIds.PropertyType);
      _dc.Add(VariableTypeIds.AnalogItemType.ToString(), VariableTypeIds.AnalogItemType);
      _dc.Add(VariableTypeIds.ArrayItemType.ToString(), VariableTypeIds.ArrayItemType);
      _dc.Add(VariableTypeIds.BaseDataVariableType.ToString(), VariableTypeIds.BaseDataVariableType);
      _dc.Add(VariableTypeIds.BaseVariableType.ToString(), VariableTypeIds.BaseVariableType);
      NodeId _id = NodeId.Null;
      string _ni = "g=09087e75-8e5e-499b-954f-f2a9603db28a";
      Assert.IsTrue(_dc.TryGetValue(VariableTypeIds.PropertyType.ToString(), out _id));
      Assert.IsFalse(_dc.TryGetValue(NodeId.Parse(_ni).ToString(), out _id));
      _id = NodeId.Parse(_ni);
      Assert.IsNotNull(_id);
      _dc.Add(_id.ToString(), _id);
      Assert.IsTrue(_dc.TryGetValue(VariableTypeIds.PropertyType.ToString(), out _id));
      NodeId _nid = NodeId.Parse(_ni);
      Assert.IsNotNull(_nid);
      Assert.AreEqual<int>(1, _dc.Where<KeyValuePair<string, NodeId>>(x => x.Key == _nid.ToString()).Count<KeyValuePair<string, NodeId>>());
      Assert.IsTrue(_dc.TryGetValue(_nid.ToString(), out _id));
      Assert.IsTrue(_dc.ContainsKey(_nid.ToString()));
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NodeIdDictionaryTestMethod3()
    {
      Dictionary<NodeId, string> _dc = new Dictionary<NodeId, string>();
      _dc.Add(VariableTypeIds.PropertyType, VariableTypeIds.PropertyType.ToString());
      _dc.Add(VariableTypeIds.AnalogItemType, VariableTypeIds.AnalogItemType.ToString());
      _dc.Add(VariableTypeIds.ArrayItemType, VariableTypeIds.ArrayItemType.ToString());
      _dc.Add(VariableTypeIds.BaseDataVariableType, VariableTypeIds.BaseDataVariableType.ToString());
      _dc.Add(VariableTypeIds.BaseVariableType, VariableTypeIds.BaseVariableType.ToString());
      _dc.Add(VariableTypeIds.PropertyType, VariableTypeIds.PropertyType.ToString());
    }

  }

}
