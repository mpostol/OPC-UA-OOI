
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.Encoding;
using System.Xml;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.UnitTest
{
  [TestClass]
  public class BinaryUDPPackageReaderTestClass
  {

    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    public void CreatorTestMethod()
    {
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x)))
      {
        Assert.IsNotNull(_reader1);
        _reader1.CallOnEnable();
      }
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x)))
      {
        Assert.IsNotNull(_reader1);
        _reader1.CallOnEnable();
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(System.Net.Sockets.SocketException))]
    public void ExclusiveAddressUseTrueTestMethod()
    {
      bool _ExclusiveAddressUse = true;
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x)))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ExclusiveAddressUse(_ExclusiveAddressUse);
        _reader1.CallOnEnable();
        using (Consumer.BinaryUDPPackageReader _reader2 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x)))
        {
          Assert.IsNotNull(_reader2);
          _reader1.ExclusiveAddressUse(_ExclusiveAddressUse);
          _reader2.CallOnEnable();
        }
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(System.Net.Sockets.SocketException))]
    public void ExclusiveAddressUseFalseTestMethod()
    {
      bool _ExclusiveAddressUse = false;
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x)))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ExclusiveAddressUse(_ExclusiveAddressUse);
        _reader1.CallOnEnable();
        using (Consumer.BinaryUDPPackageReader _reader2 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x)))
        {
          Assert.IsNotNull(_reader2);
          _reader1.ExclusiveAddressUse(_ExclusiveAddressUse);
          _reader2.CallOnEnable();
        }
      }
    }

    #region test instrumentation
    private class UADecoder : IUADecoder
    {
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

      public IVariant ReadVariant(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public XmlElement ReadXmlElement(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }
    }
    #endregion

  }
}
