
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.DataSerialization.UnitTest
{
  [TestClass]
  public class QualifiedNameUnitTest
  {
    [TestMethod]
    [TestCategory("Serialization")]
    public void QualifiedNameTestMethod1()
    {
      string name = "Default Binary";
      QualifiedName _qn = new QualifiedName("Default Binary");
      Assert.IsNotNull(_qn);
      //Assert.AreEqual<int>(_qn.NamespaceIndex, 0);
      Assert.IsFalse(_qn.NamespaceIndexSpecified);
      Assert.AreEqual<string>(_qn.Name, name);
    }
    [TestMethod]
    [TestCategory("Serialization")]
    public void QualifiedNameParseTestMethod2()
    {
      QualifiedName _qn = QualifiedName.Parse("0:Name");
      Assert.IsNotNull(_qn);
    }
    [TestMethod]
    [TestCategory("Serialization")]
    public void QualifiedNameParseTestMethod3()
    {
      QualifiedName _qn = QualifiedName.Parse("Name"); //Cannot find information that the NamespaceIndex is optional
      Assert.IsNotNull(_qn);
      Assert.AreEqual<int>(_qn.NamespaceIndex, 0);
      Assert.IsFalse(_qn.NamespaceIndexSpecified);
      Assert.AreEqual<string>(_qn.Name, "Name");
    }
  }
}
