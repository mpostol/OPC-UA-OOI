//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest
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
      ProducerConfigurationFactory _configurationFactory = new ProducerConfigurationFactory(_configurationFile.FullName);
      Assert.IsNotNull(_configurationFactory.Loader);
      ConfigurationData _configuration = _configurationFactory.Loader();
      Assert.AreEqual<int>(3, _configuration.DataSets.Length);
      Assert.AreEqual<int>(1, _configuration.MessageHandlers.Length);
      Assert.AreEqual<int>(1, _configuration.MessageHandlers.Length);
      Assert.AreEqual<AssociationRole>(AssociationRole.Producer, _configuration.MessageHandlers[0].TransportRole);
      Assert.IsTrue(_configuration.MessageHandlers[0] is MessageWriterConfiguration);
      Assert.IsNull(_configuration.TypeDictionaries);
    }

    private const string m_configurationFileName = "ConfigurationDataProducer.xml";
  }
}
