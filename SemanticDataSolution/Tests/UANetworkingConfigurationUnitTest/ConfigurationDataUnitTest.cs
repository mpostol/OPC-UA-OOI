
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
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
    #region private
    private class LocalConfigurationData : ConfigurationData
    {
      internal static LocalConfigurationData Loader()
      {
        return new LocalConfigurationData();
      }
      private LocalConfigurationData()
      {

      }
    }

    #endregion
  }
}
