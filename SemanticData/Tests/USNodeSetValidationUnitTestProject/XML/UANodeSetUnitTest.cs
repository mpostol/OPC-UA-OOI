//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  [TestClass]
  public class UANodeSetUnitTest
  {
    [TestMethod]
    public void RemoveInheritedKeepDifferentValuesTest()
    {
      UAObjectType _derived = GetDerivedFromComplexObjectType();
      UAObjectType _base = GetComplexObjectType();
      _derived.RemoveInheritedValues(_base);
      Assert.AreEqual<int>(1, _derived.DisplayName.Length);
      Assert.AreEqual<string>("DerivedFromComplexObjectType", _derived.DisplayName[0].Value);
    }
    [TestMethod]
    public void RemoveInheritedRemoveSameValuesTest()
    {
      UAObjectType _derived = GetDerivedFromComplexObjectType();
      UAObjectType _base = GetDerivedFromComplexObjectType();
      _derived.RemoveInheritedValues(_base);
      Assert.IsNull(_derived.DisplayName);
    }
    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void EqualsTypesTest()
    {
      UANode _derived = GetDerivedFromComplexObjectType();
      UANode _base = GetDerivedFromComplexObjectType();
      Assert.IsTrue(_derived.Equals(_base));
    }
    [TestMethod]
    public void EqualsInstancesTest()
    {
      UAObject _derived = GetInstanceOfDerivedFromComplexObjectType();
      UAObject _base = GetInstanceOfDerivedFromComplexObjectType();
      Assert.IsTrue(_derived.Equals(_base));
    }
    [TestMethod]
    public void NotEqualsInstancesTest()
    {
      UAObject _derived = GetInstanceOfDerivedFromComplexObjectType();
      UAObject _base = GetInstanceOfDerivedFromComplexObjectType2();
      Assert.IsFalse(_derived.Equals(_base));
    }
    private static UAObject GetInstanceOfDerivedFromComplexObjectType()
    {
      return new UAObject()
      {
        BrowseName = "1:InstanceOfDerivedFromComplexObjectType",
        NodeId = "ns=1;i=30",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "InstanceOfDerivedFromComplexObjectType" } },
        References = new XML.Reference[]
        {
            new XML.Reference(){ IsForward = true, ReferenceType = "HasProperty", Value = "ns=1;i=32" }
        }
      };
    }
    private static UAObject GetInstanceOfDerivedFromComplexObjectType2()
    {
      return new UAObject()
      {
        BrowseName = "1:InstanceOfDerivedFromComplexObjectType",
        NodeId = "ns=1;i=30",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "NewDisplayName" } },
        References = new XML.Reference[]
        {
            new XML.Reference(){ IsForward = true, ReferenceType = "HasProperty", Value = "ns=1;i=32" }
        }
      };
    }
    private static UAObjectType GetDerivedFromComplexObjectType()
    {
      return new UAObjectType()
      {
        NodeId = "ns=1;i=16",
        BrowseName = "1:DerivedFromComplexObjectType",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "DerivedFromComplexObjectType" } },
        References = new XML.Reference[]
        {
            new XML.Reference(){ IsForward = true, ReferenceType = "HasSubtype", Value = "ns=1;i=25" }
        }
      };
    }
    private static UAObjectType GetComplexObjectType()
    {
      return new UAObjectType()
      {
        NodeId = "ns=1;i=1",
        BrowseName = "1:ComplexObjectType",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ComplexObjectType" } },
      };
    }

  }
}
