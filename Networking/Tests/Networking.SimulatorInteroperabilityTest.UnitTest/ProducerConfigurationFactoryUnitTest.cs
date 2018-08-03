//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Configuration.Networking.Serialization;

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
      TestProducerConfigurationFactory _configuration = new TestProducerConfigurationFactory("Configuration file path");
      Assert.IsNotNull(_configuration.Loader);
    }

    private const string m_configurationFileName = "ConfigurationDataProducer.xml";
    private class TestProducerConfigurationFactory : ProducerConfigurationFactory
    {
      public TestProducerConfigurationFactory(string configurationFileName) : base(configurationFileName) { }
      internal new Func<ConfigurationData> Loader { get { return base.Loader; } set { base.Loader = value; } }
    }

  }
}
