using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CAS.UA.IServerConfiguration;
using System.Xml;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{
  [TestClass]
  [DeploymentItem(@"TestData\", @"TestData\")]
  public class UANetworkingConfigurationUnitTest
  {

    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void CreatorTestMethod()
    {
      UANetworkingConfiguration _newConfiguration = new UANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void CreateDefaultConfigurationTestMethod()
    {
      UANetworkingConfiguration _newConfiguration = new UANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      _newConfiguration.CreateDefaultConfiguration();
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ReadSaveConfigurationTestMethod()
    {
      UANetworkingConfiguration _newConfiguration = new UANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationData.xml");
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
      _newConfiguration.SaveConfiguration(String.Empty, _fi);
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);

    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(NotImplementedException))]
    public void EditConfigurationTestMethod()
    {
      UANetworkingConfiguration _newConfiguration = new UANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      _newConfiguration.EditConfiguration();
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void GetInstanceConfigurationTestMethod()
    {
      UANetworkingConfiguration _newConfiguration = new UANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      INodeDescriptor _nd = new NodeDescriptor();
      _newConfiguration.GetInstanceConfiguration(_nd);
    }

    private class NodeDescriptor : INodeDescriptor
    {
      public string BindingDescription
      {
        get
        {
          throw new NotImplementedException();
        }
      }

      public XmlQualifiedName DataType
      {
        get
        {
          throw new NotImplementedException();
        }
      }

      public bool InstanceDeclaration
      {
        get
        {
          throw new NotImplementedException();
        }
      }

      public InstanceNodeClassesEnum NodeClass
      {
        get
        {
          throw new NotImplementedException();
        }
      }

      public XmlQualifiedName NodeIdentifier
      {
        get
        {
          throw new NotImplementedException();
        }
      }
    }
  }
}
