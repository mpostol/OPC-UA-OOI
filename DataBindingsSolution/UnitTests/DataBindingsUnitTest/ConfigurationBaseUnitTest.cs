
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.DataBindings.UnitTest
{
  [TestClass]
  public class ConfigurationBaseUnitTest
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void CreatorTestMethod()
    {
      DerivedTest _newConfiguration = new DerivedTest();
      Assert.IsNotNull(_newConfiguration);
    }
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void RaiseOnChangeNullTestMethod()
    {
      DerivedTest _instance = new DerivedTest();
      int _OnModifiedCalled = 0;
      _instance.OnModified += (x, y) => { Assert.IsTrue(y.ConfigurationFileChanged); _OnModifiedCalled++; };
      _instance.CurrentConfiguration = new ConfigurationData();
      Assert.AreEqual<int>(1, _OnModifiedCalled);
      _instance.CurrentConfiguration = _instance.CurrentConfiguration;
      Assert.AreEqual<int>(1, _OnModifiedCalled);
    }
    #endregion
    
    #region private
    private class DerivedTest : ConfigurationBase<ConfigurationData>
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
