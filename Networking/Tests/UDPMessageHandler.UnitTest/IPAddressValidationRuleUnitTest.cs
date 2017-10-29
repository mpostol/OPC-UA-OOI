
using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.Networking.UDPMessageHandler.UnitTest
{
  [TestClass]
  public class IPAddressValidationRuleUnitTest
  {
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NullStringTestMethod()
    {
      IPAddress _res = IPAddressValidationRule.ValidateIP(null);
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    public void WrongStringTestMethod()
    {
      IPAddress _res = IPAddressValidationRule.ValidateIP("123");
      Assert.IsNotNull(_res);
      Assert.AreEqual<AddressFamily>(AddressFamily.InterNetwork, _res.AddressFamily);
      Assert.AreEqual<string>("0.0.0.123", _res.ToString());
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void WrongAddressTestMethod()
    {
      IPAddress _res = IPAddressValidationRule.ValidateIP("139.255.255.999");
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    public void ValidateMulticastTest()
    {
      IPAddress _res = IPAddressValidationRule.ValidateIP("239.255.255.1");
      Assert.IsNotNull(_res);
      Assert.AreEqual<AddressFamily>(AddressFamily.InterNetwork, _res.AddressFamily);
      IPAddressValidationRule.ValidateMulticast(_res);

      _res = IPAddressValidationRule.ValidateIP("239.255.255.255");
      Assert.IsNotNull(_res);
      Assert.AreEqual<AddressFamily>(AddressFamily.InterNetwork, _res.AddressFamily);
      IPAddressValidationRule.ValidateMulticast(_res);

      _res = IPAddressValidationRule.ValidateIP("224.0.0.0");
      Assert.IsNotNull(_res);
      Assert.AreEqual<AddressFamily>(AddressFamily.InterNetwork, _res.AddressFamily);
      IPAddressValidationRule.ValidateMulticast(_res);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void WrongMulticast240_0_0_0_TestMethod()
    {
      IPAddress _res = IPAddressValidationRule.ValidateIP("240.0.0.0");
      Assert.IsNotNull(_res);
      Assert.AreEqual<AddressFamily>(AddressFamily.InterNetwork, _res.AddressFamily);
      IPAddressValidationRule.ValidateMulticast(_res);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void WrongMulticast240_255_255_255_TestMethod()
    {
      IPAddress _res = IPAddressValidationRule.ValidateIP("240.255.255.255");
      Assert.IsNotNull(_res);
      Assert.AreEqual<AddressFamily>(AddressFamily.InterNetwork, _res.AddressFamily);
      IPAddressValidationRule.ValidateMulticast(_res);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void WrongMulticast223_0_0_0_TestMethod()
    {
      IPAddress _res = IPAddressValidationRule.ValidateIP("223.0.0.0");
      Assert.IsNotNull(_res);
      Assert.AreEqual<AddressFamily>(AddressFamily.InterNetwork, _res.AddressFamily);
      IPAddressValidationRule.ValidateMulticast(_res);
    }

  }
}
