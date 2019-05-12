//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization.UnitTest
{
  [TestClass]
  public class QualifiedNameUnitTest
  {
    [TestMethod]
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
    public void QualifiedNameParseTestMethod3()
    {
      QualifiedName _qn = QualifiedName.Parse("Name"); //Cannot find information that the NamespaceIndex is optional
      Assert.IsNotNull(_qn);
      Assert.AreEqual<int>(_qn.NamespaceIndex, 0);
      Assert.IsFalse(_qn.NamespaceIndexSpecified);
      Assert.AreEqual<string>(_qn.Name, "Name");
    }
    [TestMethod]
    public void OperatorsTest()
    {
      QualifiedName _qn1 = new QualifiedName("Default Binary");
      QualifiedName _qn2 = new QualifiedName("Default Binary");
      Assert.IsTrue(_qn1 != null);
      Assert.IsTrue(null != _qn1);
      Assert.IsTrue(_qn1 == _qn2);
      Assert.IsTrue(_qn2 == _qn1);
      _qn2 = new QualifiedName("Something else");
      Assert.IsTrue(_qn1 != _qn2);
    }
  }
}
