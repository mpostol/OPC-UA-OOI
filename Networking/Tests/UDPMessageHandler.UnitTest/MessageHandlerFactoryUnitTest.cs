using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.Diagnostics;
using UAOOI.Networking.UDPMessageHandler.Configuration;

namespace UAOOI.Networking.UDPMessageHandler.UnitTest
{
  [TestClass]
  public class MessageHandlerFactoryUnitTest
  {

    [TestMethod]
    public void MessageHandlerFactoryBaseTest()
    {
      Assert.IsTrue(typeof(INetworkingEventSourceProvider).IsAssignableFrom( typeof(MessageHandlerFactory)));
    }
  }
}
