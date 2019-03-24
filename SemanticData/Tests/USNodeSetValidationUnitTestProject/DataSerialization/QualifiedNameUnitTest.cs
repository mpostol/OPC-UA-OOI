//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class QualifiedNameUnitTest
  {
    [TestMethod]
    public void NotEqualOperatorTest()
    {
      QualifiedName _qualifiedName = null;
      Assert.IsFalse(_qualifiedName != null);
      _qualifiedName = new QualifiedName("name", 1);
      Assert.IsTrue(_qualifiedName != null);
    }
    [TestMethod]
    public void EqualOperatorTest()
    {
      QualifiedName _qualifiedName = null;
      Assert.IsTrue(_qualifiedName == null);
      _qualifiedName = new QualifiedName("name", 1);
      Assert.IsFalse(_qualifiedName == null);
    }
    [TestMethod]
    public void EqualsTest()
    {
      QualifiedName _qualifiedName = new QualifiedName("name", 1);
      Assert.IsTrue(_qualifiedName.NamespaceIndexSpecified);
      Assert.IsFalse(_qualifiedName.Equals(null));
      Assert.IsTrue(_qualifiedName.Equals(_qualifiedName));
      QualifiedName _qualifiedNameSecond = new QualifiedName("name", 1);
      Assert.IsTrue(_qualifiedName.Equals(_qualifiedNameSecond));
      _qualifiedNameSecond = new QualifiedName("NAME", 1);
      Assert.IsFalse(_qualifiedName.Equals(_qualifiedNameSecond));
      _qualifiedNameSecond = new QualifiedName("name", 0);
      Assert.IsFalse(_qualifiedName.Equals(_qualifiedNameSecond));
    }
    [TestMethod]
    public void ToStringTest()
    {
      QualifiedName _qualifiedName = new QualifiedName("name", 1);
      Assert.AreEqual<string>($"1:name", _qualifiedName.ToString());
      _qualifiedName = new QualifiedName("name");
      Assert.AreEqual<string>("name", _qualifiedName.ToString());
      _qualifiedName = new QualifiedName();
      Assert.AreEqual<string>("", _qualifiedName.ToString());
    }
  }
}
