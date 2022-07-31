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
      LocalizedText[] _first = null;
      Assert.IsTrue(_first.LocalizedTextArraysEqual(_first));
      Assert.IsFalse(_first.LocalizedTextArraysEqual(new LocalizedText[] { }));
      Assert.IsFalse((new LocalizedText[] { }).LocalizedTextArraysEqual(null));
      _first = new LocalizedText[] { };
      Assert.IsTrue(_first.LocalizedTextArraysEqual(new LocalizedText[] { }));
      _first = new LocalizedText[] { new LocalizedText() { Locale = "Locale1", Text = "Value1" }, new LocalizedText() { Locale = "Locale2", Text = "Value2" } };
      Assert.IsTrue(_first.LocalizedTextArraysEqual(new LocalizedText[] { new LocalizedText() { Locale = "Locale2", Text = "Value2" }, new LocalizedText() { Locale = "Locale1", Text = "Value1" } }));
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
