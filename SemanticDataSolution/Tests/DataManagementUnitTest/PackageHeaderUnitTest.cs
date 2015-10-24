using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class PackageHeaderUnitTest
  {

    #region TestMethod
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
    public void ConsumerPackageHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest();
      PackageHeader _header = PackageHeader.GetConsumerPackageHeader(_reader);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<byte>(0xff, _header.MessageCount);
      Assert.AreEqual<long>(20, _reader.Position);
      _header.MessageCount = 0x0;
      Assert.AreEqual<long>(20, _reader.Position);
      Assert.AreEqual<byte>(0x0, _header.MessageCount);
    }
    #endregion

    #region private 
    private class HeaderWriterTest : IHeaderWriter
    {
      public long Seek(int offset, SeekOrigin origin)
      {
        switch (origin)
        {
          case SeekOrigin.Begin:
            Position = offset;
            break;
          case SeekOrigin.Current:
            Position += offset;
            if (Position < 0)
              throw new ArgumentOutOfRangeException("Position");
            break;
          case SeekOrigin.End:
            throw new NotImplementedException();
        };
        return Position;
      }
      public void Write(Guid value)
      {
        Position += 16;
      }
      public void Write(byte value)
      {
        Position++;
      }
      internal long Position = 0;
    }
    private class HeaderReaderTest : IHeaderReader
    {
      public byte ReadByte()
      {
        Position++;
        return 0xff;
      }
      public Guid ReadGuid()
      {
        Position += 16;
        return Guid.NewGuid();
      }
      internal int Position = 0;
    }
    #endregion

  }

  #region To be promoted



  #endregion
}
