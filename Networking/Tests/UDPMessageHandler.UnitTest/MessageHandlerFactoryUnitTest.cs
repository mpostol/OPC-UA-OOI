using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.Networking.UDPMessageHandler.UnitTest
{
  [TestClass]
  public class MessageHandlerFactoryUnitTest
  {
    [TestMethod]
    public void UDPReaderConfigurationTestMethod()
    {
      int UDPPortNumber = 4840;
      bool JoinMulticastGroup = true;
      string DefaultMulticastGroup = "239.255.255.1";
      bool ReuseAddress = true;
      MessageHandlerFactory.UDPReaderConfiguration _configuration = MessageHandlerFactory.UDPReaderConfiguration.Parse($"{UDPPortNumber},{JoinMulticastGroup},{DefaultMulticastGroup},{ReuseAddress}");
      Assert.IsNotNull(_configuration);
      Assert.AreEqual<string>("4840,True,239.255.255.1,True", _configuration.ToString());
      Assert.AreEqual<int>(UDPPortNumber, _configuration.UDPPortNumber);
      Assert.AreEqual<bool>(JoinMulticastGroup, _configuration.JoinMulticastGroup);
      Assert.AreEqual<string>(DefaultMulticastGroup, _configuration.DefaultMulticastGroup);
      Assert.AreEqual<bool>(ReuseAddress, _configuration.ReuseAddress);
    }
  }
}
