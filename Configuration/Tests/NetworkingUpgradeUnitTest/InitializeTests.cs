using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UAOOI.Configuration.Networking.Upgrade.UnitTest
{
  [TestClass]
  [DeploymentItem(@"TestingData\", @"TestingData\")]
  public class AssemblyInitialize
  {
    [AssemblyInitialize]
    public static void Initialize(TestContext context) { }
    [TestMethod]
    public void TestingDataTestMethod()
    {
      FileInfo _testFile = new FileInfo(@"TestingData\ConfigurationDataConsumer.xml");
      Assert.IsTrue(_testFile.Exists);
    }

  }
}
