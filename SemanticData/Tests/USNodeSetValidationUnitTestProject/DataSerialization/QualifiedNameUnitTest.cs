﻿//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
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
    [TestMethod]
    public void QualifiedNameConstructorTest()
    {
      string name = "Default Binary";
      QualifiedName _qn = new QualifiedName("Default Binary");
      Assert.IsNotNull(_qn);
      //Assert.AreEqual<int>(_qn.NamespaceIndex, 0);
      Assert.IsFalse(_qn.NamespaceIndexSpecified);
      Assert.AreEqual<string>(_qn.Name, name);
    }

    [TestMethod]
    public void QualifiedNameParseTest()
    {
      //TODO Enhance/Improve BrowseName parser #538
      QualifiedName _qn = QualifiedName.Parse("Name"); //Cannot find information that the NamespaceIndex is optional
      Assert.IsNotNull(_qn);
      Assert.AreEqual<int>(_qn.NamespaceIndex, 0);
      Assert.IsFalse(_qn.NamespaceIndexSpecified);
      Assert.AreEqual<string>(_qn.Name, "Name");
    }

    [TestMethod]
    public void QualifiedNameParse0NamespaceIndexTest()
    {
      //TODO Enhance/Improve BrowseName parser #538
      QualifiedName _qn = QualifiedName.Parse("0:Name"); //Cannot find information that the NamespaceIndex is optional
      Assert.IsNotNull(_qn);
      Assert.IsFalse(_qn.NamespaceIndexSpecified);
      //Assert.AreEqual<int>(_qn.NamespaceIndex, 0);
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
      _qn2 = new QualifiedName("1:Default Binary");
      Assert.IsTrue(_qn1 != _qn2);
    }
    [TestMethod]
    public void QualifiedNameParseDefaultNamespaceTestMethod()
    {
      AssertQualifiedNameParse("1:Default Binary", 1, "Default Binary", 1);
      AssertQualifiedNameParse(":Default Binary", 123, "Default Binary", 123);
      AssertQualifiedNameParse("1:Default Binary", 3, "Default Binary", 1);
      AssertQualifiedNameParse("Default Binary", 123, "Default Binary", 123);
      AssertQualifiedNameParse("   1:Default Binary   ", 1, "Default Binary", 1);
      AssertQualifiedNameParse("   Default Binary   ", 1, "Default Binary", 1);
      AssertQualifiedNameParse("   Default Binary   ", 0, "Default Binary", 0);
      AssertQualifiedNameParse("   1:<Default Binary>   ", 1, "<Default Binary>", 1);
      AssertQualifiedNameParse("   1:[Default Binary]   ", 1, "[Default Binary]", 1);
      AssertQualifiedNameParse("   1:{Default Binary}   ", 1, "{Default Binary}", 1);
      AssertQualifiedNameParse("   1:Default Binary {n}   ", 1, "Default Binary {n", 1);
    }
    private void AssertQualifiedNameParse(string text, ushort defIndex, string expectedName, ushort expectedNamespaceIndex)
    {
      QualifiedName newQualifiedName = QualifiedName.Parse(text, defIndex);
      Assert.IsNotNull(newQualifiedName);
      Assert.AreEqual<int>(expectedNamespaceIndex, newQualifiedName.NamespaceIndex);
      Assert.AreEqual<string>(expectedName, newQualifiedName.Name);
    }
  }
}
