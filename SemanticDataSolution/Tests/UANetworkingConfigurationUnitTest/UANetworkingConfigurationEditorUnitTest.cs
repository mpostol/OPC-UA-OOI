
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{

  [TestClass]
  public class UANetworkingConfigurationEditorUnitTest
  {

    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void GetIServerConfigurationTestMethod()
    {
      FileInfo _fileInfo = new FileInfo("UAOOI.SemanticDataUANetworkingConfiguration.dll");
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
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void ComposePartsTestMethod()
    {
      ComposeParts();
      Assert.IsNotNull(m_Container);
      Assert.IsNotNull(m_Container.Catalog);
      m_Container.Dispose();
      Assert.IsNotNull(MyConfiguration);
      UANetworkingConfigurationEditor _editor = (UANetworkingConfigurationEditor)MyConfiguration;
      Assert.IsNotNull(_editor.ConfigurationEditor);
    }

    private void ComposeParts()
    {
      //An aggregate catalog that combines multiple catalogs
      var catalog = new AggregateCatalog();
      //Adds all the parts found in the same assembly as the UANetworkingConfigurationEditorUnitTest class
      catalog.Catalogs.Add(new AssemblyCatalog(typeof(UANetworkingConfigurationEditorUnitTest).Assembly));
      catalog.Catalogs.Add(new AssemblyCatalog(typeof(UANetworkingConfigurationEditor).Assembly));
      //Create the CompositionContainer with the parts in the catalog
      m_Container = new CompositionContainer(catalog);
      //Fill the imports of this object
      try
      {
        this.m_Container.ComposeParts(this);
      }
      catch (CompositionException compositionException)
      {
        Assert.Fail(compositionException.ToString());
      }
    }
    [Import(typeof(IConfiguration))]
    public IConfiguration MyConfiguration { get; set; }
    private CompositionContainer m_Container;
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
  [Export(typeof(IConfigurationEditor))]
  public class ConfigurationEditor : IConfigurationEditor
  {
    public void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, Action<bool> CancelWasPressed)
    {
      throw new NotImplementedException();
    }

    public void EditConfiguration(ConfigurationData configuration)
    {
      throw new NotImplementedException();
    }
  }

}
