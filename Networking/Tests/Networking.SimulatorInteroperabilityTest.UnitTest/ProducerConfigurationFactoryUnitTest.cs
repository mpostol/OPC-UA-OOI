//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
      FileInfo _configurationFile = new FileInfo(m_configurationFileName);
      Assert.IsTrue(_configurationFile.Exists, $"There is no file in path {_configurationFile.FullName}");
      ProducerConfigurationFactory _configuration = new ProducerConfigurationFactory("Configuration file path");
      Assert.IsNotNull(_configuration.Loader);
    }

    private const string m_configurationFileName = "ConfigurationDataProducer.xml";

  }
}
