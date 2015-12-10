
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class PacketHeaderUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_PacketHeaderUnitTest")]
    public void ProducerPacketHeaderTestMethod()
    {
      long _startPosition = 10;
      HeaderWriterTest _writer = new HeaderWriterTest(x => { }, _startPosition);
      PacketHeader _header = PacketHeader.GetProducerPacketHeader(_writer, CommonDefinitions.TestGuid, new System.UInt32[] { 0xFFFF });
      Assert.IsNotNull(_header);
      Assert.AreEqual<System.UInt32>(0xFFFF, _header.DataSetWriterIds[0]);
      Assert.AreEqual<byte>(1, _header.MessageCount);
      Assert.AreEqual<long>(_startPosition + 24, _writer.Position);
      _writer.Write(0xCCCC);
      _writer.Write(0xCCCC);
      _writer.Write(0xCCCC);
      _writer.Write(0xCCCC);
      _header.WritePacketHeader();
      Assert.AreEqual<byte>(1, _header.MessageCount);
      Assert.AreEqual<long>(_startPosition + 24 + 16, _writer.Position);
      _header.WritePacketHeader();
      Assert.AreEqual<long>(_startPosition + 24 + 16, _writer.Position);
      _writer.Write(0xCCCC);
      Assert.AreEqual<long>(_startPosition + 24 + 20, _writer.Position);
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
      Assert.AreEqual<byte>((byte)((byte)m_StartPosition + 19), _header.MessageCount);
      Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, _header.PublisherId);
      Assert.AreEqual<long>(m_StartPosition + 20 + (m_StartPosition + 19) * 4, _reader.m_Position);
    }

    private long m_StartPosition = 10;
  }

}
