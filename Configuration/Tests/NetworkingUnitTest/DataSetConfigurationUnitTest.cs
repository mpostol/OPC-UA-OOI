//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest
{
  [TestClass]
  public class DataSetConfigurationUnitTest
  {
    [TestMethod]
    public void AfterCreationStateTest()
    {
      DataSetConfiguration _dataSet = new DataSetConfiguration();
      Assert.IsTrue(String.IsNullOrEmpty(_dataSet.Guid));
      Assert.AreEqual<Guid>(Guid.Empty, _dataSet.Id);
    }
    [TestMethod]
    public void IdTest()
    {
      DataSetConfiguration _dataSet = new DataSetConfiguration();
      Assert.IsTrue(String.IsNullOrEmpty(_dataSet.Guid));
      Assert.AreEqual<Guid>(Guid.Empty, _dataSet.Id);
      Guid _newId = System.Guid.NewGuid();
      _dataSet.Id = _newId;
      Assert.IsFalse(String.IsNullOrEmpty(_dataSet.Guid));
      Assert.AreEqual<string>(XmlConvert.ToString(_newId), _dataSet.Guid);
    }
  }
}
