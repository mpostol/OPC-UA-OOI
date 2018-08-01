//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Simulator.Boiler.UnitTest.CommonServiceLocatorInstrumentation;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest
{

  [DeploymentItem(@"ConfigurationDataProducer.BoilersSet.xml")]
  [TestClass]
  public class ProducerConfigurationFactoryUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod1()
    {

      int LogStartPosition = Logger.Singleton.TraceLogList.Count;
      FileInfo _configurationFile = new FileInfo(m_configurationFileName);
      Assert.IsTrue(_configurationFile.Exists, $"There is no file in path {_configurationFile.FullName}");
      TestProducerConfigurationFactory _configurationFactory = new TestProducerConfigurationFactory(_configurationFile.FullName);
      Assert.IsNotNull(_configurationFactory.Loader);
      ConfigurationData _configuration = _configurationFactory.Loader();
      Assert.AreEqual<int>(4, _configuration.DataSets.Length);
      Assert.AreEqual<int>(1, _configuration.MessageHandlers.Length);
      Assert.AreEqual<AssociationRole>(AssociationRole.Producer, _configuration.MessageHandlers[0].TransportRole);
      Assert.IsTrue(_configuration.MessageHandlers[0] is MessageWriterConfiguration);
      Assert.IsNull(_configuration.TypeDictionaries);
      int LogEndPosition = Logger.Singleton.TraceLogList.Count;
      Assert.AreEqual<int>(2, LogEndPosition - LogStartPosition, $"Current number of log entries is {LogEndPosition - LogStartPosition}");

    }

    #region instrumentation
    private const string m_configurationFileName = "ConfigurationDataProducer.BoilersSet.xml";
    private class TestProducerConfigurationFactory : ProducerConfigurationFactory
    {
      public TestProducerConfigurationFactory(string configurationFileName) : base(configurationFileName) { }
      internal new Func<ConfigurationData> Loader { get { return base.Loader; } set { base.Loader = value; } }

    }
    #endregion

  }
}
