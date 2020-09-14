//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.Configuration.Networking;

namespace UAOOI.Networking.DataRepository.DataLogger
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