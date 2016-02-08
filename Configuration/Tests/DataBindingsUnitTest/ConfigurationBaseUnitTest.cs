
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.DataBindings.UnitTest
{
  [TestClass]
  [DeploymentItem(@"..\NetworkingUnitTest\TestData\", @"TestData\")]
  public class ConfigurationBaseUnitTest
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void CreatorTestMethod()
    {
      ConfigurationBaseDerivedTest _newConfiguration = new ConfigurationBaseDerivedTest();
      Assert.IsNotNull(_newConfiguration);
      Assert.IsNull(_newConfiguration.ConfigurationData);
      Assert.IsNull(_newConfiguration.CurrentConfiguration);
      Assert.IsNull(_newConfiguration.TraceSource);
    }
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void RaiseOnChangeNullTestMethod()
    {
      ConfigurationBaseDerivedTest _instance = new ConfigurationBaseDerivedTest();
      int _OnModifiedCalled = 0;
      _instance.OnModified += (x, y) => _OnModifiedCalled++;
      _instance.CurrentConfiguration = new ConfigurationData();
      Assert.AreEqual<int>(1, _OnModifiedCalled);
      _instance.CurrentConfiguration = _instance.CurrentConfiguration;
      Assert.AreEqual<int>(1, _OnModifiedCalled);
      _instance.CurrentConfiguration = new ConfigurationData();
      Assert.AreEqual<int>(2, _OnModifiedCalled);
    }
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    [DeploymentItem(@"..\..\..\NetworkingUnitTest\TestData\", @"TestData\")]
    public void ReadConfigurationTest()
    {
      ConfigurationBaseDerivedTest _instance = new ConfigurationBaseDerivedTest();
      _instance.TraceSource = new Common.Infrastructure.Diagnostic.TraceSourceBase();
      int _OnModifiedCalled = 0;
      _instance.OnModified += (x, y) => _OnModifiedCalled++;
      FileInfo _configurationFile = new FileInfo(@"TestData\ConfigurationDataConsumer.xml");
      Assert.IsTrue(_configurationFile.Exists);
      _instance.ReadConfiguration(_configurationFile);
      Assert.IsNotNull(_instance.CurrentConfiguration);
      Assert.AreEqual<int>(1, _OnModifiedCalled);
      _instance.CurrentConfiguration = _instance.CurrentConfiguration;
      Assert.AreEqual<int>(1, _OnModifiedCalled);
      _configurationFile = new FileInfo(@"TestData\ConfigurationDataProducer.xml");
      Assert.IsTrue(_configurationFile.Exists);
      _instance.ReadConfiguration(_configurationFile);
      Assert.AreEqual<int>(2, _OnModifiedCalled);
    }
    #endregion

    #region private
    private class ConfigurationBaseDerivedTest : ConfigurationBase<ConfigurationData>
    {
      public override string DefaultFileName
      {
        get
        {
          throw new NotImplementedException();
        }
      }

      public override void CreateDefaultConfiguration()
      {
        throw new NotImplementedException();
      }

      public override void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
      {
        throw new NotImplementedException();
      }

      public override void EditConfiguration()
      {
        throw new NotImplementedException();
      }

      public override IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
      {
        throw new NotImplementedException();
      }

      public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
      {
        throw new NotImplementedException();
      }
    }
    #endregion

  }
}
