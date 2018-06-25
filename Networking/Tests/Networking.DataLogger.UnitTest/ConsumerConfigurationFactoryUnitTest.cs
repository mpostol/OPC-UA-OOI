
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.Configuration.Networking;

namespace UAOOI.Networking.DataLogger.UnitTest
{
  [TestClass]
  [DeploymentItem(@".\ConfigurationDataConsumer.xml", @".\")]
  public class ConsumerConfigurationFactoryUnitTest
  {

    [TestMethod]
    public void ConstructorTestMethod()
    {
      IConfigurationFactory _configuration = new ConsumerConfigurationFactory("Configuration file name");
    }
    [TestMethod]
    public void ConfigurationFileExistsTest()
    {
      FileInfo _configurationFile = new FileInfo("ConfigurationDataConsumer.xml");
      Assert.IsTrue(_configurationFile.Exists, $"There is no file in path {Environment.CurrentDirectory}");

    }

  }
}
