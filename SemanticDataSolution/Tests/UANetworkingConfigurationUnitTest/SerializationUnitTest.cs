using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{
  [TestClass]
  public class SerializationUnitTest
  {
    [TestMethod]
    [TestCategory("Configuration_DataSetConfigurationUnitTest")]
    public void GuidTestMethod1()
    {
      Guid _newGuid = Guid.NewGuid();
      DataSetConfiguration _newConfiguration = new DataSetConfiguration()
      {
        Id = _newGuid,
      };
      Assert.IsNotNull(_newConfiguration);
      Assert.AreEqual<Guid>(_newGuid, _newConfiguration.Id);
    }
    [TestMethod]
    [TestCategory("Configuration_MessageTransportConfiguration")]
    public void AssociationNamesTestMethod()
    {
      MessageTransportConfiguration _transport = new MessageTransportConfiguration();
      string[] _refArray = new string[] { "First", "Second", "Third" };
      _transport.AssociationNames = _refArray;
      CollectionAssert.AreEqual(_transport.AssociationNames, _refArray);
      Assert.AreEqual<int>(_refArray.Length, _transport.AssociationNamesArrayOfString.Count);
    }
  }
}
