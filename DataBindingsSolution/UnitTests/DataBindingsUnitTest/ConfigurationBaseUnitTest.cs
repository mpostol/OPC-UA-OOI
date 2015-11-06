
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace UAOOI.DataBindings.UnitTest
{
  [TestClass]
  public class ConfigurationBaseUnitTest
  {

    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void DefaultFileNameTestMethod()
    {
      ConfigurationBase _mc = new DerivedTest();
      Assert.IsNotNull(_mc);
      string _fileName = _mc.DefaultFileName;
      FileInfo _fi = new FileInfo(_fileName);
      Assert.AreEqual<string>(".uasconfig", _fi.Extension);
    }
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void RaiseOnChangeNullTestMethod()
    {
      DerivedTest _instance = new DerivedTest();
      _instance.RaiseOnChangeEventCall();
      _instance.OnModified += (x, y) => Assert.IsTrue(y.ConfigurationFileChanged);
      _instance.RaiseOnChangeEventCall();
    }

    #region private
    private class DerivedTest : ConfigurationBase
    {
      protected override string DefaultConfigurationFileName
      {
        get
        {
          return "DefaultConfigurationFileName";
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
      public override void ReadConfiguration(FileInfo configurationFile)
      {
        throw new NotImplementedException();
      }
      public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
      {
        throw new NotImplementedException();
      }
      internal void RaiseOnChangeEventCall()
      {
        RaiseOnChangeEvent(true);
      }
    }
    #endregion

  }
}
