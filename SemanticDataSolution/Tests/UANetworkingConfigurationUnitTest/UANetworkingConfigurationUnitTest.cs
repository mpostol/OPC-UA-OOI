
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{

  [TestClass]
  [DeploymentItem(@"TestData\", @"TestData\")]
  public class UANetworkingConfigurationUnitTest
  {

    #region TestClass
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void CreatorTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
    }
    //TODO to be moved to DataBinding
    //[TestMethod]
    //[TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    //public void CreateDefaultConfigurationTestMethod()
    //{
    //  DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
    //  Assert.IsNotNull(_newConfiguration);
    //  _newConfiguration.CreateDefaultConfiguration();
    //  Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
    //  ConfigurationData _CurrentConfiguration = _newConfiguration.CurrentConfiguration;
    //  Assert.IsNotNull(_CurrentConfiguration.DataSets);
    //  Assert.AreEqual<int>(0, _CurrentConfiguration.DataSets.Length);
    //  Assert.IsNotNull(_CurrentConfiguration.MessageHandlers);
    //  Assert.AreEqual<int>(0, _CurrentConfiguration.MessageHandlers.Length);
    //}
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ReadSaveConfigurationTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataConsumer.xml");
      Assert.IsTrue(_configFile.Exists);
      bool _ConfigurationFileChanged = false;
      _newConfiguration.OnModified += (x, y) => { Assert.IsTrue(y.ConfigurationFileChanged); _ConfigurationFileChanged = y.ConfigurationFileChanged; };
      _newConfiguration.ReadConfiguration(_configFile);
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);

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
    //TODO to be moved to DataBinding
    //[TestMethod]
    //[TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    //[ExpectedException(typeof(ArgumentNullException))]
    //public void GetInstanceConfigurationNullTestMethod()
    //{
    //  DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
    //  Assert.IsNotNull(_newConfiguration);
    //  IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(null);
    //}
    /// <summary>
    /// Gets the instance configuration no configuration test method.
    /// </summary>
    //todo - MOVE
    //[TestMethod]
    //[TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    //public void GetInstanceConfigurationNoConfigurationTestMethod()
    //{
    //  DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
    //  Assert.IsNotNull(_newConfiguration);
    //  NodeDescriptor _nd = new NodeDescriptor();
    //  IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
    //  Assert.IsNotNull(_newInstanceConfiguration);
    //  IInstanceConfiguration _nxtInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
    //  Assert.AreNotSame(_newInstanceConfiguration, _nxtInstanceConfiguration);
    //  Assert.AreEqual<string>(_newInstanceConfiguration.ToString(), _nxtInstanceConfiguration.ToString());
    //}
    //[TestMethod]
    //[TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    //public void GetInstanceConfigurationTestMethod()
    //{
    //  //create hard coded configuration 
    //  DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
    //  Assert.IsNotNull(_newConfiguration);
    //  _newConfiguration.DefaultConfigurationLoader = ReferenceConfiguration.LoadConsumer;
    //  bool _ConfigurationFileChanged = false;
    //  _newConfiguration.OnModified += (x, y) => { Assert.IsTrue(y.ConfigurationFileChanged); _ConfigurationFileChanged = y.ConfigurationFileChanged; };
    //  _newConfiguration.CreateDefaultConfiguration();
    //  Assert.IsTrue(_ConfigurationFileChanged);
    //  Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
    //  //test GetInstanceConfiguration
    //  INodeDescriptor _nd = new NodeDescriptor(new XmlQualifiedName("NodeDescriptor", "NodeDescriptorNS"));
    //  IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
    //  Assert.IsNotNull(_newInstanceConfiguration);
    //}
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void DefaultFileNameTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      string _fileName = _newConfiguration.DefaultConfigurationFileName;
      FileInfo _fi = new FileInfo(_fileName);
      Assert.AreEqual<string>("", _fi.Extension);
      Assert.AreEqual<string>("UANetworkingConfiguration", _fi.Name);
    }
    #endregion

    #region private
    private class NodeDescriptorTest : NodeDescriptor
    {
      public NodeDescriptorTest() : this(new XmlQualifiedName(DateTime.Today.ToString(), "UAOOI.SemanticData.UANetworking.Configuration.UnitTest")) { }
      public NodeDescriptorTest(XmlQualifiedName nodeIdentifier)
      {
        b_NodeIdentifier = nodeIdentifier;
        BindingDescription = NodeIdentifier.Name;
        DataType = new XmlQualifiedName("DataType", "UAOOI.SemanticData.UANetworking.Configuration.UnitTest");
        InstanceDeclaration = false;
        NodeClass = InstanceNodeClassesEnum.Object;
      }
      XmlQualifiedName b_NodeIdentifier = null;
    }
    private class DerivedUANetworkingConfiguration : UANetworkingConfiguration<ConfigurationData>
    {
      public DerivedUANetworkingConfiguration()
      {
        //InstanceConfigurationFactory = new Exports.InstanceConfigurationFactory();
        TraceSource = new Exports.TraceSourceBase();
        //ConfigurationEditor = new Exports.ConfigurationEditorBase();
      }
    }
    #endregion

  }
}
