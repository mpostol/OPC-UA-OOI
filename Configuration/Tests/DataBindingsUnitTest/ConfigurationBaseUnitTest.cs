
using System;
using System.IO;
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.DataBindings.UnitTest
{
  [TestClass]
  [DeploymentItem(@"..\..\..\NetworkingUnitTest\TestData\", @"TestData\")]
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
      Assert.IsNotNull(_newConfiguration.TraceSource);
    }
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    public void RaiseOnChangeNullTestMethod()
    {
      ConfigurationBaseDerivedTest _instance = new ConfigurationBaseDerivedTest();
      int _OnModifiedCalled = 0;
      _instance.OnModified += (x, y) => { _OnModifiedCalled++; Assert.IsTrue(y.ConfigurationFileChanged); };
      _instance.CurrentConfiguration = new ConfigurationData();
      Assert.AreEqual<int>(1, _OnModifiedCalled);
      _instance.CurrentConfiguration = _instance.CurrentConfiguration;
      Assert.AreEqual<int>(1, _OnModifiedCalled);
      _instance.CurrentConfiguration = new ConfigurationData();
      Assert.AreEqual<int>(2, _OnModifiedCalled);
      _instance.CreateDefaultConfiguration();
      Assert.AreEqual<int>(3, _OnModifiedCalled);
    }
    [TestMethod]
    [TestCategory("DataBindings_ConfigurationBaseUnitTest")]
    //[DeploymentItem(@"..\..\..\NetworkingUnitTest\TestData\", @"TestData\")]
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
      /// <summary>
      /// Gets the default name of the file.
      /// </summary>
      /// <value>The default name of the file.</value>
      /// <exception cref="NotImplementedException"></exception>
      public override string DefaultFileName
      {
        get
        {
          throw new NotImplementedException();
        }
      }
      /// <summary>
      /// Creates the default configuration.
      /// </summary>
      public override void CreateDefaultConfiguration()
      {
        CurrentConfiguration = new ConfigurationData();
      }
      /// <summary>
      /// Creates automatically the instance configurations on the best effort basis.
      /// </summary>
      /// <param name="descriptors">The descriptors of nodes.</param>
      /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
      /// <param name="CancelWasPressed">if set to <c>true</c> cancel was pressed.</param>
      /// <exception cref="NotImplementedException"></exception>
      public override void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
      {
        throw new NotImplementedException();
      }
      /// <summary>
      /// Gets the configuration editor - user interface to edit the plug-in configuration file.
      /// </summary>
      /// <returns>Represents a window or dialog box that makes up an application's user interface to be used to edit configuration file.</returns>
      /// <exception cref="NotImplementedException"></exception>
      public override void EditConfiguration()
      {
        throw new NotImplementedException();
      }
      /// <summary>
      /// Gets the instance to be used by a user to configure the selected node.
      /// </summary>
      /// <param name="descriptor">Provides identifying description of the node to be configured.</param>
      /// <returns>Returned object provides access to the instance node configuration edition functionality.</returns>
      /// <exception cref="NotImplementedException"></exception>
      public override IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
      {
        throw new NotImplementedException();
      }
      /// <summary>
      /// Saves the configuration file to a specified location.
      /// </summary>
      /// <param name="solutionFilePath">The solution file path.</param>
      /// <param name="configurationFile">The configuration file.</param>
      /// <exception cref="NotImplementedException"></exception>
      /// <remarks><paramref name="solutionFilePath" /> is to be used to create relative file path to configuration files used by the plug-in.</remarks>
      public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
      {
        throw new NotImplementedException();
      }
    }
    #endregion

  }
}
