using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Configuration.Networking.Upgrade.Re_l1_00_16;

namespace UAOOI.Configuration.Networking.Upgrade.UnitTest
{
  [TestClass]
  public class Re_l1_00_16UnitTest
  {
    [TestMethod]
    public void AfterCreationStateTest()
    {
      ConfigurationData _newInstance = new ConfigurationData();
      Assert.IsNull(_newInstance.DataSets);
      Assert.IsNull(_newInstance.MessageHandlers);
    }
  }
}
