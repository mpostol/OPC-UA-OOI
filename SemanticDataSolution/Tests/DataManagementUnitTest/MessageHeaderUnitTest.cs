
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class MessageHeaderUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_MessageHeaderUnitTest")]
    public void ProducerMessageHeaderTestMethod1()
    {
      long _fieldNumber = 0;
      long[] _position = new long[] { 0, 2, 3, 4, 6, 7, 8, 16 };
      HeaderWriterTest _writer = new HeaderWriterTest(x => Assert.AreEqual<long>(_position[_fieldNumber++], x));
      MessageHeader _header = MessageHeader.GetProducerMessageHeader(_writer);
      Assert.IsNotNull(_header);
      _header.MessageType = MessageHeader.MessageTypeEnum.DataDeltaFrame;
      _header.MessageSequenceNumber = 8;
      _header.ConfigurationVersion = new MessageHeader.ConfigurationVersionDataType() { MajorVersion = 6, MinorVersion = 7 };
      _header.TimeStamp = CommonDefinitions.TestMinimalDateTime;
      _header.FieldCount = 16;
      _header.Synchronize();
      Assert.AreEqual<long>(8, _fieldNumber);
      Assert.AreEqual<long>(18, _writer.Position);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageHeaderUnitTest")]
    public void ConsumerMessageHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest();
      MessageHeader _header = MessageHeader.GetConsumerMessageHeader(_reader);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<ushort>(0, _header.MessageLength);
      Assert.AreEqual<ushort>(2, (byte)_header.MessageType);
      Assert.AreEqual<ushort>(3, _header.MessageFlags);
      Assert.AreEqual<ushort>(4, _header.MessageSequenceNumber);
      Assert.AreEqual<ushort>(6, _header.ConfigurationVersion.MajorVersion);
      Assert.AreEqual<ushort>(7, _header.ConfigurationVersion.MinorVersion);
      Assert.AreEqual<ushort>(16, _header.FieldCount);
      Assert.AreEqual<DateTime>(CommonDefinitions.TestMinimalDateTime, _header.TimeStamp);
      Assert.AreEqual<long>(18, _reader.m_Position);
    }

  }
}
