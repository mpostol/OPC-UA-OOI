
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using UAOOI.DataBindings;
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
      Assert.IsNotNull(_newConfiguration.Tracer);
      _newConfiguration.Tracer(System.Diagnostics.TraceEventType.Verbose, 0, "Do nothing and keep alive.");
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void CreateDefaultConfigurationTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
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
    public void ReadSaveConfigurationTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
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
      Assert.IsFalse(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      _fi.Refresh();
      Assert.IsTrue(_fi.Exists);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetInstanceConfigurationNullTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(null);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void GetInstanceConfigurationNoConfigurationTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      INodeDescriptor _nd = new NodeDescriptor();
      IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
      Assert.IsNull(_newInstanceConfiguration);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void GetInstanceConfigurationTestMethod()
    {
      //create hard coded configuration 
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      _newConfiguration.DefaultConfigurationLoader = ReferenceConfiguration.LoadConsumer;
      bool _ConfigurationFileChanged = false;
      _newConfiguration.OnModified += (x, y) => { Assert.IsTrue(y.ConfigurationFileChanged); _ConfigurationFileChanged = y.ConfigurationFileChanged; };
      _newConfiguration.CreateDefaultConfiguration();
      Assert.IsTrue(_ConfigurationFileChanged);
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      //test GetInstanceConfiguration
      INodeDescriptor _nd = new NodeDescriptor() { NodeIdentifier = new System.Xml.XmlQualifiedName("NodeDescriptor", "NodeDescriptorNS") };
      IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
      Assert.IsNotNull(_newInstanceConfiguration);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void DefaultFileNameTestMethod()
    {
      DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
      Assert.IsNotNull(_newConfiguration);
      string _fileName = _newConfiguration.DefaultFileName;
      FileInfo _fi = new FileInfo(_fileName);
      Assert.AreEqual<string>(".uasconfig", _fi.Extension);
      Assert.AreEqual<string>("UANetworkingConfiguration.uasconfig", _fi.Name);
    }

    #endregion

    #region private
    private class NodeDescriptor : NodeDescriptorBase
    {
      public override string BindingDescription
      {
        get
        {
          throw new NotImplementedException();
        }

        set
        {
          throw new NotImplementedException();
        }
      }

      public override XmlQualifiedName DataType
      {
        get
        {
          throw new NotImplementedException();
        }

        set
        {
          throw new NotImplementedException();
        }
      }
      public override bool InstanceDeclaration
      {
        get
        {
          throw new NotImplementedException();
        }

        set
        {
          throw new NotImplementedException();
        }
      }
      public override InstanceNodeClassesEnum NodeClass
      {
        get
        {
          throw new NotImplementedException();
        }

        set
        {
          throw new NotImplementedException();
        }
      }
      public override XmlQualifiedName NodeIdentifier
      {
        get; set;
      }

    }
    private class DerivedUANetworkingConfiguration : UANetworkingConfiguration<ConfigurationData>
    {
      public override void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
      {
        throw new NotImplementedException();
      }
      public override void EditConfiguration()
      {
        throw new NotImplementedException();
      }
    }
    #endregion

  }
}
