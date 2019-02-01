//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UAOOI.Networking.UDPMessageHandler.Configuration;

namespace UAOOI.Networking.UDPMessageHandler.UnitTest
{

  [TestClass]
  public class BinaryUDPPackageReaderTestClass
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    public void CreatorTestMethod()
    {
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
      {
        Assert.IsNotNull(_reader1);
        _reader1.State.Enable();
        Assert.IsNotNull(_reader1.MulticastGroup);
      }
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
      {
        Assert.IsNotNull(_reader1);
        _reader1.State.Enable();
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    public void ExclusiveAddressUseTrueTestMethod()
    {
      bool _ExclusiveAddressUse = true;
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        using (BinaryUDPPackageReader _reader2 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
        {
          Assert.IsNotNull(_reader2);
          _reader2.ReuseAddress = _ExclusiveAddressUse;
          _reader2.State.Enable();
        }
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(SocketException))]
    public void ExclusiveAddressUseFalseTestMethod()
    {
      bool _ExclusiveAddressUse = false;
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
      {
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        using (BinaryUDPPackageReader _reader2 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
        {
          _reader2.ReuseAddress = _ExclusiveAddressUse;
          _reader2.State.Enable();
        }
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ExclusiveAddressOperationalTestMethod()
    {
      bool _ExclusiveAddressUse = true;
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
      {
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        _reader1.ReuseAddress = _ExclusiveAddressUse;
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ExclusiveMulticastGroupTestMethod()
    {
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(LocalUDPConfiguration.GetReaderConfiguration()))
      {
        try
        {
          _reader1.State.Enable();
          Thread.Sleep(200);
        }
        catch (Exception _ex)
        {
          Assert.IsNotNull(_ex);
          Assert.Fail();
        }
        _reader1.MulticastGroup = IPAddress.Parse("239.0.0.1");
      }
    }
    #endregion

    #region test instrumentation
    private static class LocalUDPConfiguration
    {
      internal static UDPReaderConfiguration GetReaderConfiguration()
      {
        bool _ExclusiveAddressUse = true;
        int UDPPortNumber = 4840;
        bool JoinMulticastGroup = true;
        string DefaultMulticastGroup = "239.255.255.1";
        return UDPReaderConfiguration.Parse($"{UDPPortNumber},{JoinMulticastGroup},{DefaultMulticastGroup},{_ExclusiveAddressUse}");
      }
    }
    #endregion

  }

}
