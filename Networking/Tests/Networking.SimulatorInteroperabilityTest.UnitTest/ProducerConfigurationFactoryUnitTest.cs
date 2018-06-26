
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.Networking.SimulatorInteroperabilityTest.UnitTest
{

  [DeploymentItem(@".\ConfigurationDataProducer.xml", @".\")]
  [TestClass]
  public class ProducerConfigurationFactoryUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod1()
    {
      ProducerConfigurationFactory _configuration = new ProducerConfigurationFactory("Configuration file path");
    }
    [TestMethod]
    public void ConfigurationFileExistsTest()
    {
      FileInfo _configurationFile = new FileInfo("ConfigurationDataProducer.xml");
      Assert.IsTrue(_configurationFile.Exists, $"There is no file in path {Environment.CurrentDirectory}");

    }

  }
}
