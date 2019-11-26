//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.UnitTest.Instrumentation;

namespace UAOOI.Configuration.Networking.UnitTest
{

  [TestClass]
  [DeploymentItem(@"TestData\", @"TestData\")]
  public class UANetworkingConfigurationUnitTest
  {

    #region TestClass
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void CreatorTest()
    {
      CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => null);
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      Assert.IsNull(_newConfiguration.ConfigurationData);
      Assert.IsNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNotNull(_newConfiguration.TraceSource);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void CustomLoggerTraceSourceTest()
    {
      Logger _Logger = new Logger();
      Container _container = new Container(new Object[] { _Logger });
      ServiceLocator.SetLocatorProvider(() => _container);
      Assert.IsTrue(ServiceLocator.IsLocationProviderSet);
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.AreSame(_Logger, _newConfiguration.TraceSource);
      Assert.AreEqual<int>(0, _Logger.TraceLogList.Count);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ReadConfigurationTest()
    {
      Logger _Logger = new Logger();
      CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new Container(new Object[] { _Logger }));
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataConsumer.xml");
      Assert.IsTrue(_configFile.Exists);
      bool _ConfigurationFileChanged = false;
      Assert.IsNull(_newConfiguration.ConfigurationData);
      _newConfiguration.OnModified += (x, y) => { _ConfigurationFileChanged = true; };
      _newConfiguration.ReadConfiguration(_configFile);
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNotNull(_newConfiguration.ConfigurationData);
      Assert.AreEqual<int>(1, _Logger.TraceLogList.Count);
      Logger.TraceLogEntity _logEntry = _Logger.TraceLogList[0];
      Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, _logEntry.EventType);
      Assert.AreEqual<int>(52, _logEntry.Id);
      string _logMessage = $"Data = {_logEntry.Data}, EventType = {_logEntry.EventType}  Id = {_logEntry.Id}";
      Debug.WriteLine(_logMessage);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void OnChangedConfigurationTest()
    {
      Logger _Logger = new Logger();
      CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new Container(new Object[] { _Logger }));
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataConsumer.xml");
      Assert.IsTrue(_configFile.Exists);
      bool _ConfigurationFileChanged = false;
      Assert.IsNull(_newConfiguration.ConfigurationData);
      _newConfiguration.ReadConfiguration(_configFile);
      Assert.IsNotNull(_newConfiguration.ConfigurationData);
      _newConfiguration.OnModified += (x, y) => { _ConfigurationFileChanged = true; };
      Assert.IsNotNull(_newConfiguration.ConfigurationData.OnChanged);
      _newConfiguration.ConfigurationData.OnChanged();
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.AreEqual<int>(1, _Logger.TraceLogList.Count);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ReadSaveConfigurationTest()
    {
      Logger _Logger = new Logger();
      CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new Container(new Object[] { _Logger }));
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataConsumer.xml");
      Assert.IsNull(_newConfiguration.ConfigurationData);
      _newConfiguration.ReadConfiguration(_configFile);
      Assert.IsNotNull(_newConfiguration.ConfigurationData);

      //SaveConfiguration
      bool _ConfigurationFileChanged = false;
      _newConfiguration.OnModified += (x, y) => { _ConfigurationFileChanged = true; };
      FileInfo _fi = new FileInfo(@"BleBle.txt");
      Assert.IsFalse(_fi.Exists);
      _newConfiguration.SaveConfiguration(_fi);
      Assert.IsFalse(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      _fi.Refresh();
      Assert.IsTrue(_fi.Exists);
      Assert.AreEqual<int>(2, _Logger.TraceLogList.Count);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CurrentConfigurationNullTest()
    {
      Logger _Logger = new Logger();
      CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new Container(new Object[] { _Logger }));
      UANetworkingConfigurationConfigurationDataWrapper _newConfiguration = new UANetworkingConfigurationConfigurationDataWrapper();
      _newConfiguration.CurrentConfiguration = null;
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataWrapperNull.xml");
      Assert.IsFalse(_configFile.Exists);
      _newConfiguration.SaveConfiguration(_configFile);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(SerializationException))]
    public void ConfigurationDataNullTest()
    {
      Logger _Logger = new Logger();
      CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new Container(new Object[] { _Logger }));
      UANetworkingConfigurationConfigurationDataWrapper _newConfiguration = new UANetworkingConfigurationConfigurationDataWrapper();
      _newConfiguration.CurrentConfiguration.ConfigurationData = null;
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataWrapper.ConfigurationDataNull.xml");
      Assert.IsFalse(_configFile.Exists);
      _newConfiguration.SaveConfiguration(_configFile);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ReadSaveConfigurationDataWrapperTest()
    {
      Logger _Logger = new Logger();
      CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new Container(new Object[] { _Logger }));
      UANetworkingConfigurationConfigurationDataWrapper _newConfiguration = new UANetworkingConfigurationConfigurationDataWrapper();
      Assert.AreEqual<int>(0, _newConfiguration.CurrentConfiguration.OnLoadedCount);
      bool _ConfigurationFileChanged = false;
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataWrapper.xml");
      Assert.IsFalse(_configFile.Exists);
      Assert.AreEqual<int>(0, _newConfiguration.CurrentConfiguration.OnSavingCount);
      _newConfiguration.SaveConfiguration(_configFile);

      //on SaveConfiguration tests
      Assert.AreEqual<int>(1, _newConfiguration.CurrentConfiguration.OnSavingCount);
      Assert.IsFalse(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      _configFile.Refresh();
      Assert.IsTrue(_configFile.Exists);
      Assert.IsNotNull(_newConfiguration.ConfigurationData);
      Assert.AreEqual<int>(0, _newConfiguration.CurrentConfiguration.OnLoadedCount);

      //prepare ReadConfiguration
      _newConfiguration.OnModified += (x, y) => { _ConfigurationFileChanged = true; };
      _newConfiguration.ReadConfiguration(_configFile);

      //on ReadConfiguration test
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNotNull(_newConfiguration.ConfigurationData);
      Assert.AreEqual<int>(1, _newConfiguration.CurrentConfiguration.OnLoadedCount);
      Assert.AreEqual<int>(0, _newConfiguration.CurrentConfiguration.OnSavingCount);
      Assert.AreEqual<int>(2, _Logger.TraceLogList.Count);
      //Assert.Fail(); //To get created file the test must fail.
    }
    #endregion

    #region private

    private class UANetworkingConfigurationConfigurationDataWrapper : UANetworkingConfiguration<ConfigurationDataWrapper>
    {
      public UANetworkingConfigurationConfigurationDataWrapper()
      {
        this.CurrentConfiguration = new ConfigurationDataWrapper();
      }
    }
    private class DerivedUANetworkingConfiguration : UANetworkingConfiguration<ConfigurationData>
    {
      public DerivedUANetworkingConfiguration()
      {
        CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => null);
      }
    }
    #endregion

  }
}
