//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class ExtensionsUnitTest
  {
    [TestMethod]
    public void ArrayDimensionsToStringTestMethod()
    {
      uint[] _testingData = new uint[] { 1, 2, 3, 4, 5 };
      string _result = _testingData.ArrayDimensionsToString();
      Assert.AreEqual<string>("1, 2, 3, 4, 5", _result);
    }
    [TestMethod]
    public void LocalizedTextArraysEqualTest()
    {
      XML.LocalizedText[] _first = null;
      Assert.IsTrue(_first.LocalizedTextArraysEqual(_first));
      Assert.IsFalse(_first.LocalizedTextArraysEqual(new XML.LocalizedText[] { }));
      Assert.IsFalse((new XML.LocalizedText[] { }).LocalizedTextArraysEqual(null));
      _first = new XML.LocalizedText[] { };
      Assert.IsTrue(_first.LocalizedTextArraysEqual(new XML.LocalizedText[] { }));
      _first = new XML.LocalizedText[] { new XML.LocalizedText() { Locale = "Locale1", Value = "Value1" }, new XML.LocalizedText() { Locale = "Locale2", Value = "Value2" } };
      Assert.IsTrue(_first.LocalizedTextArraysEqual(new XML.LocalizedText[] { new XML.LocalizedText() { Locale = "Locale2", Value = "Value2" }, new XML.LocalizedText() { Locale = "Locale1", Value = "Value1" } }));
    }
    [TestMethod]
    public void RolePermissionsEqualsTest()
    {
      XML.RolePermission[] _first = null;
      Assert.IsTrue(_first.RolePermissionsEquals(_first));
      Assert.IsFalse(_first.RolePermissionsEquals(new XML.RolePermission[] { }));
      _first = new XML.RolePermission[] { };
      Assert.IsTrue(_first.RolePermissionsEquals(new XML.RolePermission[] { }));
      _first = new XML.RolePermission[] { new XML.RolePermission() { Permissions = 1234, Value = "Value1" }, new XML.RolePermission() { Permissions = 4321, Value = "Value2" } };
      Assert.IsTrue(_first.RolePermissionsEquals(new XML.RolePermission[] { new XML.RolePermission() { Permissions = 4321, Value = "Value2" }, new XML.RolePermission() { Permissions = 1234, Value = "Value1" } }));
    }
  }
}
