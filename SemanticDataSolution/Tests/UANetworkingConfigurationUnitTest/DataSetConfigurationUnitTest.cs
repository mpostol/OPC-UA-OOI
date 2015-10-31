using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{
  [TestClass]
  public class DataSetConfigurationUnitTest
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
  }
}
