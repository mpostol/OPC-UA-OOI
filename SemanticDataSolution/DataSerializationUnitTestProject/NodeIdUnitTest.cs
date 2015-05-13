using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

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

  }
}
