
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.UDPMessageHandler.Configuration;

namespace UAOOI.Networking.UDPMessageHandler.UnitTest.Configuration
{
  [TestClass]
  public class UDPReaderConfigurationUnitTest
  {
    [TestMethod]
    public void UDPMulticastReaderConfigurationTest()
    {
      int UDPPortNumber = 4840;
      bool JoinMulticastGroup = true;
      string DefaultMulticastGroup = "239.255.255.1";
      bool ReuseAddress = true;
      UDPReaderConfiguration _configuration = UDPReaderConfiguration.Parse($"{UDPPortNumber},{JoinMulticastGroup},{DefaultMulticastGroup},{ReuseAddress}");
      Assert.IsNotNull(_configuration);
      Assert.AreEqual<string>("4840,True,239.255.255.1,True", _configuration.ToString());
      Assert.AreEqual<int>(UDPPortNumber, _configuration.UDPPortNumber);
      Assert.AreEqual<string>(DefaultMulticastGroup, _configuration.DefaultMulticastGroup.ToString());
      Assert.AreEqual<bool>(ReuseAddress, _configuration.ReuseAddress);
    }
    [TestMethod]
    public void UDPReaderConfigurationTest()
    {
      int UDPPortNumber = 4840;
      bool JoinMulticastGroup = false;
      string DefaultMulticastGroup = "239.255.255.1";
      bool ReuseAddress = true;
      UDPReaderConfiguration _configuration = UDPReaderConfiguration.Parse($"{UDPPortNumber},{JoinMulticastGroup},{DefaultMulticastGroup},{ReuseAddress}");
      Assert.IsNotNull(_configuration);
      Assert.AreEqual<string>("4840,False,,True", _configuration.ToString());
      Assert.AreEqual<int>(UDPPortNumber, _configuration.UDPPortNumber);
      Assert.IsNull(_configuration.DefaultMulticastGroup);
      Assert.AreEqual<bool>(ReuseAddress, _configuration.ReuseAddress);

    }
  }
}
