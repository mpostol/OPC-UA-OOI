
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.DataBindings.Serializers;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{

  [TestClass]
  public class ConfigurationDataUnitTest
  {
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
      FileInfo _consumerFile = new FileInfo(@"ConfigurationDataConsumer.xml");
      DataBindings.Serializers.DataContractSerializers.Save<ConfigurationData>(_consumerFile, _consumer, (x, y, z) => { Console.WriteLine(z); });
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void ConfigurationDataProducerTestMethod()
    {
      ConfigurationData _Producer = ReferenceConfiguration.LoadProducer();
      FileInfo _consumerFile = new FileInfo(@"ConfigurationDataProducer.xml");
      DataBindings.Serializers.DataContractSerializers.Save<ConfigurationData>(_consumerFile, _Producer, (x, y, z) => { Console.WriteLine(z); });
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
      LocalConfigurationData _new = LocalConfigurationData.Load<LocalConfigurationData>( () =>  _configuration  );
      Assert.AreEqual(1, _configuration.OnLoadedCount);
    }

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
      internal LocalConfigurationData(){}
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

    #endregion
  }
}
