using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class DataManagementSetupUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    public void DataManagementSetupCreatorTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      Assert.IsNull(_ndm.BindingFactory);
      Assert.IsNull(_ndm.ConfigurationFactory);
      Assert.IsNull(_ndm.EncodingFactory);
      Assert.IsNull(_ndm.MessageHandlerFactory);
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    [ExpectedException(typeof(NotImplementedException))]
    public void InitializeTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.Initialize();
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    [ExpectedException(typeof(NotImplementedException))]
    public void RunTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.Run();
    }
  }
}
