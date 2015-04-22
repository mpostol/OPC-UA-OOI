using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAS.UA.IServerConfiguration;
using System.IO;

namespace CAS.UAOOI.DataBindings.UnitTest
{
  [TestClass]
  public class ConfigurationBaseUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void CreateDefaultConfigurationTestMethod()
    {
      ConfigurationBase _mc = new ConfigurationBase();
      Assert.IsNotNull(_mc);
      _mc.CreateDefaultConfiguration();
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void CreateInstanceConfigurationsTestMethod()
    {
      ConfigurationBase _mc = new ConfigurationBase();
      Assert.IsNotNull(_mc);
      bool cancelWasPressed = false;
      _mc.CreateInstanceConfigurations(new NodeDescriptor[] { NodeDescriptor.GetTestInstance(), NodeDescriptor.GetTestInstance() }, true, out cancelWasPressed);
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void DefaultFileNameTestMethod()
    {
      ConfigurationBase _mc = new ConfigurationBase();
      Assert.IsNotNull(_mc);
      string _dfn = _mc.DefaultFileName;
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void EditConfigurationTestMethod()
    {
      ConfigurationBase _mc = new ConfigurationBase();
      Assert.IsNotNull(_mc);
      _mc.EditConfiguration();
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void GetInstanceConfigurationTestMethod()
    {
      ConfigurationBase _mc = new ConfigurationBase();
      Assert.IsNotNull(_mc);
      IInstanceConfiguration _ic = _mc.GetInstanceConfiguration(NodeDescriptor.GetTestInstance());
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void ReadConfigurationTestMethod()
    {
      ConfigurationBase _mc = new ConfigurationBase();
      Assert.IsNotNull(_mc);
      FileInfo _fi = new FileInfo(@"BleBle.txt");
      Assert.IsFalse(_fi.Exists);
      _mc.ReadConfiguration(_fi);
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void SaveConfigurationTestMethod()
    {
      ConfigurationBase _mc = new ConfigurationBase();
      Assert.IsNotNull(_mc);
      _mc.SaveConfiguration(@"solutionFilePath", new FileInfo(@"configurationFile"));
    }
  }
}
