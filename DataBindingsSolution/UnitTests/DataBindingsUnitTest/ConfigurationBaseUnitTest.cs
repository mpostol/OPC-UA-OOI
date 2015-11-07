
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
    public void CreatorTestMethod()
    {
      DerivedTest _newConfiguration = new DerivedTest();
      Assert.IsNotNull(_newConfiguration);
      Assert.IsNotNull(_newConfiguration.Tracer);
      _newConfiguration.Tracer(System.Diagnostics.TraceEventType.Verbose, 0, "Do nothing and keep alive.");
    }
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void DefaultFileNameTestMethod()
    {
      DerivedTest _mc = new DerivedTest();
      Assert.IsNotNull(_mc);
      string _fileName = _mc.DefaultFileName;
      FileInfo _fi = new FileInfo(_fileName);
      Assert.AreEqual<string>(".uasconfig", _fi.Extension);
      Assert.AreEqual<string>("DefaultConfigurationFileName.uasconfig", _fi.Name);
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

    #region private
    private class ConfigurationData { }
    private class DerivedTest : ConfigurationBase<ConfigurationData>
    {
      public DerivedTest() : base(() => null) { }
      protected override string DefaultConfigurationFileName
      {
        get
        {
          return "DefaultConfigurationFileName";
        }
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

    }
    #endregion

  }
}
