
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
