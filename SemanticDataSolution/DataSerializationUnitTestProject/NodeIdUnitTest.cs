using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
      _ni = NodeId.Parse("ns=-10;i=01"); //this example is in the specification.
    }
    [TestMethod]
    public void NodeIdOperatorTestMethod1()
    {
      NodeId _property = new NodeId(Opc.Ua.VariableTypes.PropertyType);
      Assert.AreNotSame(_property, Opc.Ua.VariableTypeIds.PropertyType);
      Assert.Inconclusive();
      Assert.IsTrue(_property == Opc.Ua.VariableTypeIds.PropertyType);
    }

  }
}
