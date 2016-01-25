using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UAOOI.Configuration.Networking.Upgrade.Re_l1_00_16;
using UAOOI.SemanticData.UANetworking.Configuration.Serializers;
using NewConfigurationData = UAOOI.SemanticData.UANetworking.Configuration.Serialization.ConfigurationData;

namespace UAOOI.Configuration.Networking.Upgrade.UnitTest
{
  [TestClass]
  public class Re_l1_00_16UnitTest
  {
    [TestMethod]
    public void AfterCreationStateTest()
    {
      ConfigurationData _newInstance = new ConfigurationData();
      Assert.IsNull(_newInstance.DataSets);
      Assert.IsNull(_newInstance.MessageHandlers);
    }
    [TestMethod]
    public void ReadXmlTestMethod()
    {
      NewMethod(@"TestingData\ConfigurationDataConsumer.xml", @"NewConfigurationDataConsumer.xml");
      NewMethod(@"TestingData\ConfigurationDataProducer.xml", @"NewConfigurationDataProducer.xml");
    }

    private void NewMethod(string inFileName, string outFileName)
    {
      var _trace = new UAOOI.Common.Infrastructure.Diagnostic.TraceSourceBase();
      FileInfo _file2Covert = new FileInfo(inFileName);
      Assert.IsTrue(_file2Covert.Exists);
      ConfigurationData _oldConfiguration = XmlDataContractSerializers.Load<ConfigurationData>(_file2Covert, _trace.TraceData);
      Assert.IsNotNull(_oldConfiguration);
      NewConfigurationData _newConfiguration = Import(_oldConfiguration);
      Assert.IsNotNull(_newConfiguration);
      FileInfo _file2Save = new FileInfo(outFileName);
      XmlDataContractSerializers.Save<NewConfigurationData>(_file2Save, _newConfiguration, _trace.TraceData);
    }
    private NewConfigurationData Import(ConfigurationData _oldConfiguration)
    {
      NewConfigurationData _ret = new NewConfigurationData() { };
      return _ret;
    }
  }
}
