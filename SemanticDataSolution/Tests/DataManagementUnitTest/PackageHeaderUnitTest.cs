
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class PackageHeaderUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_PackageHeaderUnitTest")]
    public void ProducerPackageHeaderTestMethod()
    {
      HeaderWriterTest _writer = new HeaderWriterTest();
      PackageHeader _header = PackageHeader.GetProducerPackageHeader(_writer);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<byte>(0, _header.MessageCount);
      Assert.AreEqual<long>(20, _writer.Position);
      _header.MessageCount = 0xff;
      Assert.AreEqual<long>(20, _writer.Position);
      Assert.AreEqual<byte>(255, _header.MessageCount);
    }
    [TestMethod]
    [TestCategory("DataManagement_PackageHeaderUnitTest")]
    public void ConsumerPackageHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest();
      PackageHeader _header = PackageHeader.GetConsumerPackageHeader(_reader);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<byte>(0xff, _header.MessageCount);
      Assert.AreEqual<byte>(0xff, _header.MessageFlags);
      Assert.AreEqual<byte>(0xff, _header.ProtocolVersion);
      Assert.AreEqual<byte>(0xff, _header.SecurityTokenId);
      Assert.AreEqual<Guid>(MessageHandling.CommonDefinitions.ProducerId, _header.PublisherId);
      Assert.AreEqual<long>(20, _reader.Position);
      _header.MessageCount = 0x0;
      Assert.AreEqual<long>(20, _reader.Position);
      Assert.AreEqual<byte>(0x0, _header.MessageCount);
    }

  }

}
