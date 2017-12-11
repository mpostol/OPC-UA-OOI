
using System;
using System.Net;
using System.Threading;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.Encoding;

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
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
      {
        Assert.IsNotNull(_reader1);
        _reader1.State.Enable();
        Assert.IsNotNull(_reader1.MulticastGroup);
      }
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
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
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        using (BinaryUDPPackageReader _reader2 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
        {
          Assert.IsNotNull(_reader2);
          _reader2.ReuseAddress = _ExclusiveAddressUse;
          _reader2.State.Enable();
        }
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(System.Net.Sockets.SocketException))]
    public void ExclusiveAddressUseFalseTestMethod()
    {
      bool _ExclusiveAddressUse = false;
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
      {
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        using (BinaryUDPPackageReader _reader2 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
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
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
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
      using (BinaryUDPPackageReader _reader1 = new BinaryUDPPackageReader(new UADecoder(), LocalUDPConfiguration.GetReaderConfiguration()))
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
    private class UADecoder : IUADecoder
    {
      public Array ReadArray<type>(IBinaryDecoder decoder, Func<type> readValue, bool arrayDimensionsPresents)
      {
        throw new NotImplementedException();
      }
      public byte[] ReadByteString(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }
      public IDataValue ReadDataValue(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public DateTime ReadDateTime(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IExtensionObject ReadExtensionObject(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public Guid ReadGuid(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public ILocalizedText ReadLocalizedText(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public INodeId ReadNodeId(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IQualifiedName ReadQualifiedName(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IStatusCode ReadStatusCode(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public string ReadString(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }
      public IVariant ReadVariant(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }
      public XmlElement ReadXmlElement(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }
    }
    private static class LocalUDPConfiguration
    {
      internal static MessageHandlerFactory.UDPReaderConfiguration GetReaderConfiguration()
      {
        bool _ExclusiveAddressUse = true;
        int UDPPortNumber = 4840;
        bool JoinMulticastGroup = true;
        string DefaultMulticastGroup = "239.255.255.1";
        return MessageHandlerFactory.UDPReaderConfiguration.Parse($"{UDPPortNumber},{JoinMulticastGroup},{DefaultMulticastGroup},{_ExclusiveAddressUse}");
      }
    }
    #endregion

  }
}
