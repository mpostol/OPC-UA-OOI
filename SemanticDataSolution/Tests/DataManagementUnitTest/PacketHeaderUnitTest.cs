
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using System.Linq;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class PacketHeaderUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_PacketHeaderUnitTest")]
    public void ProtocolVersionTestMethod()
    {
      byte[] _result = null;
      using (MemoryStream _stream = new MemoryStream())
      using (PacketWriter _writer = new PacketWriter(_stream))
      {
        PacketHeader _header = PacketHeader.GetProducerPacketHeader(_writer, CommonDefinitions.TestGuid, new UInt32[] { CommonDefinitions.DataSetId });
        Assert.IsNotNull(_header);
        Assert.AreEqual<Byte>(110, _header.ProtocolVersion);
        Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, _header.PublisherId);
        _header.PacketFlags = 0;
        _header.SecurityTokenId = 0;
        _header.NonceLength = 1;
        _header.Nonce = new byte[] { 0xcc };
        _header.WritePacketHeader();
        _writer.Flush();
        _result = _stream.ToArray();
      }
      byte[] _expected = new ArraySegment<byte>(CommonDefinitions.GetTestBinaryArrayVariant(), 0, 26).ToArray<byte>();
      CollectionAssert.AreEqual(_expected, _result);
    }
    [TestMethod]
    [TestCategory("DataManagement_PacketHeaderUnitTest")]
    public void GetConsumerPacketHeaderTestMethod()
    {
      using (MemoryStream _stream = new MemoryStream(new ArraySegment<byte>(CommonDefinitions.GetTestBinaryArrayVariant(), 0, 26).ToArray<byte>()))
      using (PacketReader _reader = new PacketReader(_stream))
      {
        PacketHeader _header = PacketHeader.GetConsumerPacketHeader(_reader);
        Assert.IsNotNull(_header);
        Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, _header.PublisherId);
        Assert.AreEqual<Byte>(110, _header.ProtocolVersion);
        Assert.AreEqual<Byte>(0, _header.PacketFlags);
        Assert.AreEqual<Byte>(0, _header.SecurityTokenId);
        Assert.AreEqual<Byte>(1, _header.NonceLength);
        CollectionAssert.AreEqual(new byte[] { 0xcc }, _header.Nonce);
        Assert.AreEqual<Byte>(1, _header.MessageCount);
        CollectionAssert.AreEqual(new UInt32[] { CommonDefinitions.DataSetId }, _header.DataSetWriterIds);
      }
    }
    [TestCategory("DataManagement_PacketHeaderUnitTest")]
    public void ProducerPacketHeaderTestMethod()
    {
      HeaderWriterTest _writer = new HeaderWriterTest(x => { }, m_StartPosition);
      PacketHeader _header = PacketHeader.GetProducerPacketHeader(_writer, CommonDefinitions.TestGuid, new System.UInt32[] { 0xFFFF });
      Assert.IsNotNull(_header);
      Assert.AreEqual<System.UInt32>(0xFFFF, _header.DataSetWriterIds[0]);
      Assert.AreEqual<byte>(1, _header.MessageCount);
      Assert.AreEqual<long>(m_StartPosition + 24, _writer.Position);
      _writer.Write(0xCCCC);
      _writer.Write(0xCCCC);
      _writer.Write(0xCCCC);
      _writer.Write(0xCCCC);
      _header.WritePacketHeader();
      Assert.AreEqual<byte>(1, _header.MessageCount);
      Assert.AreEqual<long>(m_StartPosition + 24 + 16, _writer.Position);
      _header.WritePacketHeader();
      Assert.AreEqual<long>(m_StartPosition + 24 + 16, _writer.Position);
      _writer.Write(0xCCCC);
      Assert.AreEqual<long>(m_StartPosition + 24 + 20, _writer.Position);
    }
    [TestMethod]
    [TestCategory("DataManagement_PacketHeaderUnitTest")]
    [ExpectedException(typeof(ApplicationException))]
    public void ConsumerWritePacketHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest(m_StartPosition);
      PacketHeader _header = PacketHeader.GetConsumerPacketHeader(_reader);
      Assert.IsNotNull(_header);
      _header.WritePacketHeader();
    }
    [TestMethod]
    [TestCategory("DataManagement_PacketHeaderUnitTest")]
    public void ConsumerPacketHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest(m_StartPosition);
      PacketHeader _header = PacketHeader.GetConsumerPacketHeader(_reader);
      Assert.IsNotNull(_header);
      Assert.AreEqual<byte>((byte)((byte)m_StartPosition + 16), _header.ProtocolVersion);
      Assert.AreEqual<byte>((byte)((byte)m_StartPosition + 17), _header.PacketFlags);
      Assert.AreEqual<byte>((byte)((byte)m_StartPosition + 18), _header.SecurityTokenId);
      Assert.AreEqual<byte>((byte)((byte)m_StartPosition + 19), _header.NonceLength);
      Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, _header.PublisherId);
    }

    private class PacketReader : BinaryReader, IBinaryDecoder
    {
      public PacketReader(Stream input) : base(input) { }
      public DateTime ReadDateTime()
      {
        return Encoding.CommonDefinitions.GetUADateTime(ReadInt64());
      }
      public Guid ReadGuid()
      {
        return Encoding.CommonDefinitions.ReadGuid(this);
      }
    }
    private class PacketWriter : BinaryWriter, IBinaryHeaderEncoder
    {
      public PacketWriter(Stream output) : base(output) { }
      public void Write(DateTime value)
      {
        throw new NotImplementedException();
      }
      public void Write(Guid value)
      {
        Write(value.ToByteArray());
      }
    }
    private long m_StartPosition = 10;
  }

}
