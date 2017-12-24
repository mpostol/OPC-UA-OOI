using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.Diagnostics;
using UAOOI.Networking.UDPMessageHandler.Configuration;

namespace UAOOI.Networking.UDPMessageHandler.UnitTest
{
  [TestClass]
  public class MessageHandlerFactoryUnitTest
  {

    [TestMethod]
    public void UDPWriterConfigurationTest()
    {
      int UDPPortNumber = 4840;
      string RemoteHostName = "localhost";
      UDPWriterConfiguration _configuration = UDPWriterConfiguration.Parse($"{UDPPortNumber},{RemoteHostName}");
      Assert.IsNotNull(_configuration);
      Assert.AreEqual<string>("4840,localhost", _configuration.ToString());
      Assert.AreEqual<int>(UDPPortNumber, _configuration.UDPPortNumber);
      Assert.AreEqual<string>(RemoteHostName, _configuration.RemoteHostName);
    }
    [TestMethod]
    public void MessageHandlerFactoryBaseTest()
    {
      Assert.IsTrue(typeof(INetworkingEventSourceProvider).IsAssignableFrom( typeof(MessageHandlerFactory)));
    }
  }
}
