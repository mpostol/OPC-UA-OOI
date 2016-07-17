
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.DataDiscovery.DiscoveryServices.Models;

namespace UAOOI.DataDiscovery.DiscoveryServices.UnitTest
{
  [TestClass]
  public class FieldMetaDataUnitTest
  {
    [TestMethod]
    public void CreatorTestMethod()
    {
      FieldMetaData _newItem = new FieldMetaData();
      Assert.IsTrue(String.IsNullOrEmpty(_newItem.ProcessValueName));
      Assert.IsTrue(String.IsNullOrEmpty(_newItem.SymbolicName));
      Assert.IsNull(_newItem.SymbolicName);
    }
    [TestMethod]
    public void ConverterTestMethod()
    {
      UAOOI.Configuration.Networking.Serialization.FieldMetaData _source = new Configuration.Networking.Serialization.FieldMetaData()
      {
        ProcessValueName = "ProcessValueName",
        SymbolicName = "SymbolicName",
        TypeInformation = new Configuration.Networking.Serialization.UATypeInfo()
      };
      FieldMetaData _destination = _source;
      Assert.AreEqual<string>("ProcessValueName", _destination.ProcessValueName);
      Assert.AreEqual<string>("SymbolicName", _destination.SymbolicName);
      Assert.IsNotNull(_destination.TypeInformation);
      Assert.IsInstanceOfType(_destination.TypeInformation, typeof(UATypeInfo));
    }
  }
}
