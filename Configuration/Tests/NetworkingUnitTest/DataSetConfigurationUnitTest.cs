using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
  }
}
