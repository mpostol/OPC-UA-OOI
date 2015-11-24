
using System;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

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
    /// <param name="reader">The reader <see cref="IBinaryDecoder"/> used to read the header data from the message.</param>
    /// <returns>MessageHeader.</returns>
    public static MessageHeader GetConsumerMessageHeader(IBinaryDecoder reader)
    {
      return new ConsumerMessageHeader(reader);
    }
    /// <summary>
    /// Synchronizes this instance content with the header.
    /// </summary>
    public abstract void Synchronize();
    #endregion

    #region Header
    /// <summary>
    /// Gets or sets the data set identifier <see cref="Guid"/>.
    /// </summary>
    /// <value>The <see cref="Guid"/> data set identifier.</value>
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
        m_writer.WriteGuid(DataSetId);
      }
      private IBinaryHeaderWriter m_writer;
    }
    private class ConsumerMessageHeader : MessageHeader
    {
      public ConsumerMessageHeader(IBinaryDecoder reader)
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
      private IBinaryDecoder m_reader;
    }
    private MessageHeader() { }
    #endregion

  }

}
