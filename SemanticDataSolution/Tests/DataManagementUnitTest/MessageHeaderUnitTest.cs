
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
      HeaderWriterTest _writer = new HeaderWriterTest();
      MessageHeader _header = MessageHeader.GetProducerMessageHeader(_writer);
      Assert.IsNotNull(_header);
      _header.DataSetId = Guid.NewGuid();
      _header.Synchronize();
      Assert.AreEqual<long>(16, _writer.Position);
      _header.Synchronize();
      Assert.AreEqual<long>(32, _writer.Position);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageHeaderUnitTest")]
    public void ConsumerMessageHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest();
      MessageHeader _header = MessageHeader.GetConsumerMessageHeader(_reader);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<long>(16, _reader.m_Position);
      Assert.AreEqual<Guid>(CommonDefinitions.ProducerId, _header.DataSetId);
      _header.Synchronize();
      Assert.AreEqual<long>(32, _reader.m_Position);
    }

  }
}
