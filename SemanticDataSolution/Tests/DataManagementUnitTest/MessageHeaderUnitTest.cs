
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
      Assert.AreEqual<long>(16, _writer.Position);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageHeaderUnitTest")]
    public void ConsumerMessageHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest();
      MessageHeader _header = MessageHeader.GetConsumerMessageHeader(_reader);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<long>(16, _reader.Position);
      Assert.AreEqual<Guid>(MessageHandling.CommonDefinitions.ProducerId, _header.DataSetId);
      _header.Synchronize();
      Assert.AreEqual<long>(32, _reader.Position);
    }

    /// <summary>
    /// Class MessageHeader  represent information in the protocol message header.
    /// </summary>
    public abstract class MessageHeader
    {
      #region API
      /// <summary>
      /// Gets the producer message header.
      /// </summary>
      /// <param name="writer">The writer <see cref="IBinaryHeaderWriter"/> to populate the payload with the header information.</param>
      /// <returns>MessageHeader.</returns>
      public static MessageHeader GetProducerMessageHeader(IBinaryHeaderWriter writer)
      {
        return new ProducerMessageHeader(writer);
      }
      /// <summary>
      /// Gets the consumer message header.
      /// </summary>
      /// <param name="reader">The reader <see cref="IBinaryHeaderReader"/> used to read the header data from the message.</param>
      /// <returns>MessageHeader.</returns>
      public static MessageHeader GetConsumerMessageHeader(IBinaryHeaderReader reader)
      {
        return new ConsumerMessageHeader(reader);          
      }
      /// <summary>
      /// Synchronizes this instance content with the header.
      /// </summary>
      public abstract void Synchronize();
      #endregion

      #region Header
      public abstract Guid DataSetId { get; set; }
      #endregion

      #region private
      private class ProducerMessageHeader : MessageHeader
      {
        public ProducerMessageHeader(IBinaryHeaderWriter writer)
        {
          this.m_writer = writer;
        }
        public override Guid DataSetId
        {
          get; set;
        }
        public override void Synchronize()
        {
          m_writer.Seek(0, System.IO.SeekOrigin.Begin);
          m_writer.Write(DataSetId);
          m_writer.Seek(0, System.IO.SeekOrigin.End);
        }
        private IBinaryHeaderWriter m_writer;
      }
      private class ConsumerMessageHeader : MessageHeader
      {
        public ConsumerMessageHeader(IBinaryHeaderReader reader)
        {
          m_reader = reader;
        }
        public override Guid DataSetId
        {
          get; set;
        }
        public override void Synchronize()
        {
          DataSetId = m_reader.ReadGuid();
        }
        private IBinaryHeaderReader m_reader;
      }
      private MessageHeader() { } 
      #endregion
    }
  }
}
