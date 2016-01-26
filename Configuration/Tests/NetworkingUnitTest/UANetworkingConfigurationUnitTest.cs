
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UAOOI.Configuration.Networking.Serialization;

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
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      Assert.IsNull(_newConfiguration.ConfigurationData);
      Assert.IsNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNull(_newConfiguration.TraceSource);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ComposePartsTest()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      Assert.IsNull(_newConfiguration.ConfigurationData);
      Assert.IsNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNull(_newConfiguration.TraceSource);
      _newConfiguration.ComposeParts();
      Assert.IsNotNull(_newConfiguration.TraceSource);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ReadSaveConfigurationTest()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      _newConfiguration.ComposeParts();
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataConsumer.xml");
      Assert.IsTrue(_configFile.Exists);
      bool _ConfigurationFileChanged = false;
      Assert.IsNull(_newConfiguration.ConfigurationData);
      _newConfiguration.OnModified += (x, y) => { _ConfigurationFileChanged = true; };
      _newConfiguration.ReadConfiguration(_configFile);
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNotNull(_newConfiguration.ConfigurationData);

      //SaveConfiguration
      _ConfigurationFileChanged = false;
      FileInfo _fi = new FileInfo(@"BleBle.txt");
      Assert.IsFalse(_fi.Exists);
      _newConfiguration.SaveConfiguration(_fi);
      Assert.IsFalse(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      _fi.Refresh();
      Assert.IsTrue(_fi.Exists);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CurrentConfigurationNullTest()
    {
      UANetworkingConfigurationConfigurationDataWrapper _newConfiguration = new UANetworkingConfigurationConfigurationDataWrapper();
      _newConfiguration.CurrentConfiguration = null;
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataWrapperNull.xml");
      Assert.IsFalse(_configFile.Exists);
      _newConfiguration.SaveConfiguration(_configFile);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(System.Runtime.Serialization.SerializationException))]
    public void ConfigurationDataNullTest()
    {
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
    }
    #endregion

    #region private
    private const string m_Namespace = "http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/UnitTest/Serialization.xsd";

    [DataContractAttribute(Name = "ConfigurationDataWrapper", Namespace = m_Namespace)]
    [System.SerializableAttribute()]
    [XmlRoot(Namespace = CommonDefinitions.Namespace)]
    private class ConfigurationDataWrapper : IConfigurationDataFactory
    {
      public ConfigurationDataWrapper()
      {
        ConfigurationData = ReferenceConfiguration.LoadConsumer();
      }

      private ConfigurationData m_ConfigurationDataWrapperField;

      [DataMember(EmitDefaultValue = false, IsRequired = true)]
      public ConfigurationData ConfigurationData
      {
        get { return m_ConfigurationDataWrapperField; }
        set { m_ConfigurationDataWrapperField = value; }
      }

      #region IConfigurationDataFactory
      public ConfigurationData GetConfigurationData()
      {
        return ConfigurationData;
      }
      public void OnLoaded()
      {
        OnLoadedCount++;
      }
      public void OnSaving()
      {
        OnSavingCount++;
      }
      public Action OnChanged
      {
        get; set;
      }
      #endregion

      internal int OnSavingCount = 0;
      internal int OnLoadedCount = 0;
      private CompositionContainer m_Container = null;

    }
    private class UANetworkingConfigurationConfigurationDataWrapper : UANetworkingConfiguration<ConfigurationDataWrapper>
    {
      private CompositionContainer m_Container;

      public UANetworkingConfigurationConfigurationDataWrapper()
      {
        ComposeParts();
        this.CurrentConfiguration = new ConfigurationDataWrapper();
      }
      internal void ComposeParts()
      {
        //An aggregate catalog that combines multiple catalogs
        AggregateCatalog _catalog = new AggregateCatalog();
        //Create the CompositionContainer with the parts in the catalog
        _catalog.Catalogs.Add(new DirectoryCatalog(System.IO.Path.GetDirectoryName(typeof(UANetworkingConfigurationUnitTest).Assembly.Location)));
        m_Container = new CompositionContainer(_catalog);
        //Fill the imports of this object
        m_Container.ComposeParts(this);
      }

    }
    private class DerivedUANetworkingConfiguration : UANetworkingConfiguration<ConfigurationData>
    {
      public DerivedUANetworkingConfiguration() { }
      internal void ComposeParts()
      {
        //An aggregate catalog that combines multiple catalogs
        AggregateCatalog _catalog = new AggregateCatalog();
        //Create the CompositionContainer with the parts in the catalog
        _catalog.Catalogs.Add(new DirectoryCatalog(System.IO.Path.GetDirectoryName(typeof(UANetworkingConfigurationUnitTest).Assembly.Location)));
        m_Container = new CompositionContainer(_catalog);
        //Fill the imports of this object
        m_Container.ComposeParts(this);
      }
      private CompositionContainer m_Container = null;
    }
    #endregion

  }
}
