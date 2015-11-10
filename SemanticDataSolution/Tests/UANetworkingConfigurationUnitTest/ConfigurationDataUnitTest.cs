
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using UAOOI.DataBindings.Serializers;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using System.Linq;
using System.Xml;
using CAS.UA.IServerConfiguration;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{

  [TestClass]
  public class ConfigurationDataUnitTest
  {
    #region TestMethod
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void LoadTestMethod()
    {
      LocalConfigurationData _configuration = ConfigurationData.Load<LocalConfigurationData>(LocalConfigurationData.Loader);
      Assert.IsNotNull(_configuration);
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void ConfigurationDataConsumerTestMethod()
    {
      ConfigurationData _consumer = ReferenceConfiguration.LoadConsumer();
      FileInfo _fileInfo = new FileInfo(@"ConfigurationDataConsumer.coded.xml");
      XmlDataContractSerializers.Save<ConfigurationData>(_fileInfo, _consumer, (x, y, z) => { Console.WriteLine(z); });
      _fileInfo.Refresh();
      Assert.IsTrue(_fileInfo.Exists);
      ConfigurationData _mirror = XmlDataContractSerializers.Load<ConfigurationData>(_fileInfo, (x, y, z) => { Console.WriteLine(z); });
      Compare(_consumer, _mirror);
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void ConfigurationDataProducerTestMethod()
    {
      ConfigurationData _Producer = ReferenceConfiguration.LoadProducer();
      FileInfo _fileInfo = new FileInfo(@"ConfigurationDataProducer.coded.xml");
      XmlDataContractSerializers.Save<ConfigurationData>(_fileInfo, _Producer, (x, y, z) => { Console.WriteLine(z); });
      _fileInfo.Refresh();
      Assert.IsTrue(_fileInfo.Exists);
      ConfigurationData _mirror = XmlDataContractSerializers.Load<ConfigurationData>(_fileInfo, (x, y, z) => { Console.WriteLine(z); });
      Compare(_Producer, _mirror);
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void ConfigurationDataOnSaveTestMethod()
    {
      LocalConfigurationData _configuration = new LocalConfigurationData();
      LocalConfigurationData.Save<LocalConfigurationData>(_configuration, (x) => { Assert.AreEqual(1, x.OnSavingCount); });
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void ConfigurationDataOnLoadTestMethod()
    {
      LocalConfigurationData _configuration = new LocalConfigurationData();
      LocalConfigurationData _new = LocalConfigurationData.Load<LocalConfigurationData>(() => _configuration);
      Assert.AreEqual(1, _configuration.OnLoadedCount);
    }
    #endregion

    #region private
    private class LocalConfigurationData : ConfigurationData
    {
      #region test data
      internal int OnLoadedCount = 0;
      internal int OnSavingCount = 0;

      #endregion
      internal static LocalConfigurationData Loader()
      {
        return new LocalConfigurationData();
      }
      internal LocalConfigurationData() { }
      protected override void OnLoaded()
      {
        base.OnLoaded();
        OnLoadedCount++;
      }
      protected override void OnSaving()
      {
        base.OnSaving();
        OnSavingCount++;
      }
    }
    private void Compare(ConfigurationData _consumer, ConfigurationData _mirror)
    {
      Assert.AreEqual<int>(_consumer.DataSets.Length, _mirror.DataSets.Length);
      Assert.AreEqual<int>(_consumer.MessageHandlers.Length, _mirror.MessageHandlers.Length);
      Compare(_consumer.DataSets, _mirror.DataSets);
    }
    private void Compare(DataSetConfiguration[] dataSets1, DataSetConfiguration[] dataSets2)
    {
      Dictionary<string, DataSetConfiguration> _dataSets2Dictionary = dataSets2.ToDictionary<DataSetConfiguration, string>(x => x.AssociationName);
      foreach (DataSetConfiguration item in dataSets1)
        Compare(item, _dataSets2Dictionary[item.AssociationName]);
    }
    private void Compare(DataSetConfiguration item1, DataSetConfiguration item2)
    {
      Assert.AreEqual<AssociationRole>(item1.AssociationRole, item2.AssociationRole);
      Assert.AreEqual<string>(item1.DataSymbolicName, item2.DataSymbolicName);
      Assert.AreEqual<string>(item1.Guid, item2.Guid);
      Assert.AreEqual<string>(item1.InformationModelURI, item2.InformationModelURI);
      Assert.AreEqual<string>(item1.RepositoryGroup, item2.RepositoryGroup);
      Compare(item1.Root, item2.Root);
    }
    private void Compare(NodeDescriptor item1, NodeDescriptor item2)
    {
      if (item1 == null && item2 == null)
        return;
      Assert.IsNotNull(item1);
      Assert.IsNotNull(item2);
      Assert.AreEqual<string>(item1.BindingDescription, item2.BindingDescription);
      Assert.AreEqual<XmlQualifiedName>(item1.DataType, item2.DataType);
      Assert.AreEqual<bool>(item1.InstanceDeclaration, item2.InstanceDeclaration);
      Assert.AreEqual<InstanceNodeClassesEnum>(item1.NodeClass, item2.NodeClass);
      Assert.AreEqual<XmlQualifiedName>(item1.NodeIdentifier, item2.NodeIdentifier);
      Assert.AreEqual<string>(item1.ToString(), item2.ToString());
    }
    #endregion
  }
}
