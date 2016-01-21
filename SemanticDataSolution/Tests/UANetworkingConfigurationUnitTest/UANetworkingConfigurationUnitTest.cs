
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
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
      Assert.IsNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNotNull(_newConfiguration.TraceSource);
    }
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
    [DataContractAttribute(Name = "ConfigurationData", Namespace = CommonDefinitions.Namespace)]
    [System.SerializableAttribute()]
    [XmlRoot(Namespace = CommonDefinitions.Namespace)]
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
