
using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using UAOOI.Configuration.Core;
using UAOOI.Configuration.DataBindings.UnitTest.Exports;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.DataBindings.UnitTest
{

  [TestClass]
  public class UANetworkingConfigurationEditorUnitTest
  {

    #region Test Methods
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void GetIServerConfigurationTestMethod()
    {
      FileInfo _fileInfo = new FileInfo("UAOOI.Configuration.DataBindings.dll");
      Assert.IsTrue(_fileInfo.Exists);
      Assembly _pluginAssembly = null;
      IConfiguration _serverConfiguration = null;
      Container _container = new Container();
      ServiceLocator.SetLocatorProvider(() => _container);
      GetIServerConfiguration(_fileInfo, out _pluginAssembly, out _serverConfiguration);
      Assert.IsNotNull(_pluginAssembly);
      Assert.IsNotNull(_serverConfiguration);
      Assert.IsInstanceOfType(_serverConfiguration, typeof(UANetworkingConfigurationEditor));
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void AfterCreationStateTest()
    {
      UANetworkingConfigurationEditor _mc = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_mc.ConfigurationEditor);
      Assert.IsNotNull(_mc.CurrentConfiguration);
      Assert.IsFalse(String.IsNullOrEmpty(_mc.DefaultFileName));
      Assert.IsNotNull(_mc.InstanceConfigurationFactory);
      Assert.IsNotNull(_mc.TraceSource);
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    [ExpectedException(typeof(NotImplementedException))]
    public void EditConfigurationTest()
    {
      Container _container = new Container();
      ServiceLocator.SetLocatorProvider(() => _container);
      UANetworkingConfigurationEditor _mc = new UANetworkingConfigurationEditor();
      _mc.EditConfiguration();
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    [ExpectedException(typeof(NotImplementedException))]
    public void GetInstanceConfigurationTest()
    {
      Container _container = new Container();
      ServiceLocator.SetLocatorProvider(() => _container);
      UANetworkingConfigurationEditor _mc = new UANetworkingConfigurationEditor();
      IInstanceConfiguration _ic = _mc.GetInstanceConfiguration(new NodeDescriptorBase() { NodeIdentifier = new System.Xml.XmlQualifiedName("TestNodeIdentifier", "UAOOI.Configuration.DataBindings.UnitTest") });
      Assert.IsNotNull(_ic);
      _ic.Edit();
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void DefaultFileNameTestMethod()
    {
      Container _container = new Container();
      ServiceLocator.SetLocatorProvider(() => _container);
      UANetworkingConfigurationEditor _mc = new UANetworkingConfigurationEditor();
      string _fileName = _mc.DefaultFileName;
      FileInfo _fi = new FileInfo(_fileName);
      Assert.AreEqual<string>(".uasconfig", _fi.Extension);
      Assert.AreEqual<string>("UANetworkingConfiguration.uasconfig", _fi.Name);
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void CreateDefaultConfigurationTestMethod()
    {
      Container _container = new Container();
      ServiceLocator.SetLocatorProvider(() => _container);
      UANetworkingConfigurationEditor _newConfiguration = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_newConfiguration);
      _newConfiguration.CreateDefaultConfiguration();
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      ConfigurationData _CurrentConfiguration = _newConfiguration.CurrentConfiguration;
      Assert.IsNotNull(_CurrentConfiguration.DataSets);
      Assert.AreEqual<int>(0, _CurrentConfiguration.DataSets.Length);
      Assert.IsNotNull(_CurrentConfiguration.MessageHandlers);
      Assert.AreEqual<int>(0, _CurrentConfiguration.MessageHandlers.Length);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetInstanceConfigurationNullTestMethod()
    {
      Container _container = new Container();
      ServiceLocator.SetLocatorProvider(() => _container);
      UANetworkingConfigurationEditor _newConfiguration = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_newConfiguration);
      IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(null);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void GetInstanceConfigurationNoConfigurationTestMethod()
    {
      Container _container = new Container();
      ServiceLocator.SetLocatorProvider(() => _container);
      UANetworkingConfigurationEditor _newConfiguration = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_newConfiguration);
      NodeDescriptor _nd = NodeDescriptor.GetTestInstance();
      IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
      Assert.IsNotNull(_newInstanceConfiguration);
      IInstanceConfiguration _nxtInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
      Assert.AreNotSame(_newInstanceConfiguration, _nxtInstanceConfiguration);
      Assert.AreEqual<string>(_newInstanceConfiguration.ToString(), _nxtInstanceConfiguration.ToString());
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void OnModifiedTestMethod()
    {
      UANetworkingConfigurationEditor _newConfiguration = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_newConfiguration);
      bool _ConfigurationFileChanged = false;
      _newConfiguration.OnModified += (x, y) => { _ConfigurationFileChanged = true; };
      _newConfiguration.CreateDefaultConfiguration();
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
    }
    #endregion

    #region test instrumentation
    private static void GetIServerConfiguration(FileInfo info, out Assembly pluginAssembly, out IConfiguration serverConfiguration)
    {
      string iName = typeof(IConfiguration).ToString();
      pluginAssembly = Assembly.LoadFrom(info.FullName);
      serverConfiguration = null;
      foreach (Type pluginType in pluginAssembly.GetExportedTypes())
        //Only look at public types
        if (pluginType.IsPublic && !pluginType.IsAbstract && pluginType.GetInterface(iName) != null)
          try
          {
            serverConfiguration = (IConfiguration)Activator.CreateInstance(pluginType);
          }
          catch (TargetInvocationException _ex)
          {
            throw new ApplicationException(String.Format("The server configuration plug-in {0}/{1} cannot be loaded. Contact the vendor to get current version of this component", pluginType.FullName, info.Name), _ex);
          }
    }
    #endregion

  }

}

