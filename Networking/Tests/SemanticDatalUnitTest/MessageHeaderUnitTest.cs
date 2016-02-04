
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.MessageHandling;
using System.IO;
using System.Linq;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.UnitTest
{
  [TestClass]
  public class MessageHeaderUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_MessageHeaderUnitTest")]
    public void ProducerMessageHeaderTestMethod1()
    {
      byte[] _output = null;
      using (MemoryStream _outputStream = new MemoryStream())
      using (HeaderBinaryWriter _writer = new HeaderBinaryWriter(_outputStream))
      {
        MessageHeader _header = MessageHeader.GetProducerMessageHeader
          (_writer, FieldEncodingEnum.VariantFieldEncoding, MessageLengthFieldTypeEnum.TwoBytes, MessageTypeEnum.DataDeltaFrame, new ConfigurationVersionDataType() { MajorVersion = 7, MinorVersion = 8 });
        Assert.IsNotNull(_header);
        //Default values
        Assert.AreEqual<ushort>(1, _header.EncodingFlags);
        Assert.AreEqual<MessageTypeEnum>(MessageTypeEnum.DataDeltaFrame, _header.MessageType);
        Assert.AreEqual<ushort>(0, _header.MessageSequenceNumber);
        Assert.AreEqual<ushort>(7, _header.ConfigurationVersion.MajorVersion);
        Assert.AreEqual<ushort>(8, _header.ConfigurationVersion.MinorVersion);
        SetupProducerHeaderFields(_header);
        _header.Synchronize();
        _writer.Flush();
        _output = _outputStream.ToArray();
      }
      Assert.AreEqual<int>(18, _output.Length);
      byte[] _expected = new byte[] {
        0x02, //MessageType 
        0x01, //EncodingFlags
        0x12, 0x00, // MessageLength
        0x08, 0x00, //MessageSequenceNumber
        0x06, 0x07, //ConfigurationVersion
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //TimeStamp
        0x10, 0x00 // FieldCount
      };
      CollectionAssert.AreEqual(_expected, _output);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageHeaderUnitTest")]
    public void ProducerMessageLengthTestMethod1()
    {
      string m_Date = System.DateTime.Today.ToShortDateString();
      byte[] _output = null;
      using (MemoryStream _outputStream = new MemoryStream())
      using (HeaderBinaryWriter _writer = new HeaderBinaryWriter(_outputStream))
      {
        MessageHeader _header = MessageHeader.GetProducerMessageHeader
          (_writer, FieldEncodingEnum.VariantFieldEncoding, MessageLengthFieldTypeEnum.TwoBytes, MessageTypeEnum.DataDeltaFrame, new ConfigurationVersionDataType() { MajorVersion = 7, MinorVersion = 8 });
        Assert.IsNotNull(_header);
        SetupProducerHeaderFields(_header);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _writer.Write(UInt32.MaxValue);
        _header.Synchronize();
        _writer.Flush();
        _output = _outputStream.ToArray();
      }
      Assert.AreEqual<int>(58, _output.Length);
      byte[] _expected = new byte[] {
        0x02, //MessageType 
        0x01, //EncodingFlags
        0x3A, 0x00, // MessageLength
        0x08, 0x00, // MessageSequenceNumber
        0x06, 0x07, // ConfigurationVersion
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //TimeStamp
        0x10, 0x00, // FieldCount
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF,
        0xFF, 0xFF, 0xFF, 0xFF
      };
      CollectionAssert.AreEqual(_expected, _output);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageHeaderUnitTest")]
    public void ConsumerMessageHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest();
      MessageHeader _header = MessageHeader.GetConsumerMessageHeader(_reader);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<ushort>(0, (byte)_header.MessageType);
      Assert.AreEqual<ushort>(1, _header.EncodingFlags);
      Assert.AreEqual<UInt32>(2, _header.MessageLength);
      Assert.AreEqual<ushort>(4, _header.MessageSequenceNumber);
      Assert.AreEqual<ushort>(6, _header.ConfigurationVersion.MajorVersion);
      Assert.AreEqual<ushort>(7, _header.ConfigurationVersion.MinorVersion);
      //Assert.AreEqual<ushort>(16, _header.FieldCount);
      //Assert.AreEqual<DateTime>(CommonDefinitions.TestMinimalDateTime, _header.TimeStamp);
      Assert.AreEqual<long>(8, _reader.m_Position);
      Assert.AreEqual<FieldEncodingEnum>(FieldEncodingEnum.VariantFieldEncoding, _header.FieldsEncoding);
    }

    #region instrumentation
    private class HeaderBinaryWriter : BinaryWriter, IBinaryHeaderEncoder
    {
      public HeaderBinaryWriter(Stream output) : base(output) { }
      public void Write(DateTime value)
      {
        Write(Encoding.CommonDefinitions.GetUADataTimeTicks(value));
      }
      public void Write(Guid value)
      {
        Write(value.ToByteArray());
      }
    }
    private static void SetupProducerHeaderFields(MessageHeader _header)
    {
      _header.MessageSequenceNumber = 8;
      _header.ConfigurationVersion = new ConfigurationVersionDataType() { MajorVersion = 6, MinorVersion = 7 };
      _header.TimeStamp = CommonDefinitions.TestMinimalDateTime;
      _header.FieldCount = 16;
    }
    #endregion
  }
}
