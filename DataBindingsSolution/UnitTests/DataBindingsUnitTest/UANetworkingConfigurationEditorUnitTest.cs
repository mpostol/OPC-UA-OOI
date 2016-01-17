
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace UAOOI.DataBindings.UnitTest
{

  [TestClass]
  public class UANetworkingConfigurationEditorUnitTest
  {

    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void GetIServerConfigurationTestMethod()
    {
      FileInfo _fileInfo = new FileInfo("UAOOI.DataBindings.dll");
      Assert.IsTrue(_fileInfo.Exists);
      Assembly _pluginAssembly = null;
      IConfiguration _serverConfiguration = null;
      GetIServerConfiguration(_fileInfo, out _pluginAssembly, out _serverConfiguration);
      Assert.IsNotNull(_pluginAssembly);
      Assert.IsNotNull(_serverConfiguration);
      UANetworkingConfigurationEditor _editor = (UANetworkingConfigurationEditor)_serverConfiguration;
      Assert.IsNotNull(_editor);
      Assert.IsNotNull(_editor.ConfigurationEditor);
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void GetMyIServerConfigurationTestMethod()
    {
      FileInfo _fileInfo = new FileInfo("UAOOI.DataBindings.dll");
      Assert.IsTrue(_fileInfo.Exists);
      Assembly _pluginAssembly = null;
      IConfiguration _serverConfiguration = null;
      GetIServerConfiguration(_fileInfo, out _pluginAssembly, out _serverConfiguration);
      Assert.IsNotNull(_pluginAssembly);
      Assert.IsNotNull(_serverConfiguration);
      UANetworkingConfigurationEditor _editor = (UANetworkingConfigurationEditor)_serverConfiguration;
      Assert.IsNotNull(_editor);
      Assert.IsNotNull(_editor.ConfigurationEditor);
    }
    private static void GetIServerConfiguration(FileInfo info, out Assembly pluginAssembly, out IConfiguration serverConfiguration)
    {
      string iName = typeof(IConfiguration).ToString();
      pluginAssembly = Assembly.LoadFrom(info.FullName);
      serverConfiguration = null;
      foreach (Type pluginType in pluginAssembly.GetExportedTypes())
        //Only look at public types
        if (pluginType.IsPublic && !pluginType.IsAbstract && pluginType.GetInterface(iName) != null)
          try
          {
            serverConfiguration = (IConfiguration)Activator.CreateInstance(pluginType);
          }
          catch (TargetInvocationException _ex)
          {
            throw new ApplicationException(String.Format("The server configuration plug-in {0}/{1} cannot be loaded. Contact the vendor to get current version of this component", pluginType.FullName, info.Name), _ex);
          }
    }
  }

}
