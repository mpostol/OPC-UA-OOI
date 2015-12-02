
using System;
using System.IO;
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
    /// struct ConfigurationVersionDataType - this data type is used to indicate configuration changes in the information send by the message producer. 
    /// </summary>
    public struct ConfigurationVersionDataType
    {
      /// <summary>
      /// Gets or sets the major version. 
      /// </summary>
      /// <remarks>
      /// The major number reflects the primary format of the DataSet and must be equal for both producer and consumer.
      /// Removing fields from the DataSet, reordering fields, adding fields in between other fields or a DataType 
      /// change in fields shall result in an update of the MajorVersion. The initial value for the MajorVersion is 0. 
      /// If the MajorVersion is incremented, the MinorVersion shall be set to 0.
      /// An overflow of the MajorVersion is treated like any other major version change and requires a meta data exchange.
      /// </remarks>
      /// <value>The major version.</value>
      public byte MajorVersion { get; set; }
      /// <summary>
      /// Gets or sets the minor version.
      /// </summary>
      /// <remarks>The minor number reflects backward compatible changes of the DataSet like adding a field at the end of the DataSet.
      /// The initial value for the MinorVersion is 0. The MajorVersion shall be incremented after an overflow of the MinorVersion.</remarks>
      /// <value>The minor version.</value>
      public byte MinorVersion { get; set; }
    }
    /// <summary>
    /// Enum MessageTypeEnum - The type of the message.
    /// </summary>
    public enum MessageTypeEnum : byte
    {
      /// <summary>
      /// The data key frame
      /// </summary>
      DataKeyFrame = 0x1,
      /// <summary>
      /// The data delta frame
      /// </summary>
      DataDeltaFrame = 0x2,
      /// <summary>
      /// The event frame
      /// </summary>
      Event = 0x3,
      /// <summary>
      /// The keep alive frame
      /// </summary>
      KeepAlive = 0x4,
      /// <summary>
      /// The data set metadata frame
      /// </summary>
      DataSetMetadata = 0x5,
    }
    /// <summary>
    /// Gets or sets the data set identifier <see cref="Guid"/>.
    /// </summary>
    /// <value>The <see cref="Guid"/> data set identifier.</value>
    public abstract Guid DataSetId { get; set; }
    /// <summary>
    /// Gets or sets the length of the message.
    /// </summary>
    /// <value>The length of the message data structure including the header information and length field.</value>
    public abstract UInt16 MessageLength { get; }
    /// <summary>
    /// Gets or sets the type of the message.
    /// </summary>
    /// <value>The type of the message.</value>
    public abstract MessageTypeEnum MessageType { get; set; }
    /// <summary>
    /// Gets or sets the message flags.
    /// </summary>
    /// <value>The message flags are not defined yet.</value>
    public abstract Byte MessageFlags { get; set; }
    /// <summary>
    /// Gets or sets the message sequence number.
    /// </summary>
    /// <remarks>
    /// A receiver shall ignore older messages than the last sequence processed. Receivers need to be aware of sequence numbers roll over.
    /// </remarks>
    /// <value>The message sequence number. A monotonically increasing sequence number assigned by the publisher to each message sent.
    /// </value>
    public abstract UInt16 MessageSequenceNumber { get; set; }
    /// <summary>
    /// Gets or sets the configuration version.
    /// </summary>
    /// <value>The configuration version used as consistency check for the metadata about the published variables.</value>
    public abstract ConfigurationVersionDataType ConfigurationVersion { get; set; }
    /// <summary>
    /// Gets or sets the time stamp of th data contained in the message.
    /// </summary>
    /// <value>The time the Data was collected.</value>
    public abstract DateTime TimeStamp { get; set; }
    /// <summary>
    /// Gets or sets the field count.
    /// </summary>
    /// <value>Number of fields of the DataSet contained in the Message.</value>
    public abstract UInt16 FieldCount { get; set; }
    #endregion

    #region private
    private class ProducerMessageHeader : MessageHeader
    {

      #region creator
      public ProducerMessageHeader(IBinaryHeaderWriter writer)
      {
        m_HeaderWriter = new HeaderWriter(writer, m_PackageHeaderLength);
      }
      #endregion

      #region MessageHeader
      public override Guid DataSetId
      {
        get; set;
      }
      public override ushort MessageLength
      {
        get { throw new NotImplementedException(); }
      }
      public override MessageTypeEnum MessageType
      {
        get; set;
      }
      public override byte MessageFlags
      {
        get; set;
      }
      public override ushort MessageSequenceNumber
      {
        get; set;
      }
      public override ConfigurationVersionDataType ConfigurationVersion
      {
        get; set;
      }
      public override DateTime TimeStamp
      {
        get; set;
      }
      public override ushort FieldCount
      {
        get; set;
      }
      public override void Synchronize()
      {
        m_HeaderWriter.WriteHeader(WriteHeader);
      }
      #endregion

      #region private
      private const ushort m_PackageHeaderLength = 18;
      private HeaderWriter m_HeaderWriter;
      private void WriteHeader(IBinaryHeaderWriter writer, ushort messageLength)
      {
        writer.Write(DataSetId);
        writer.Write(messageLength);
        writer.Write((byte)MessageType);
        writer.Write(MessageFlags);
        writer.Write(MessageSequenceNumber);
        writer.Write(ConfigurationVersion.MajorVersion);
        writer.Write(ConfigurationVersion.MinorVersion);
        writer.Write(TimeStamp);
        writer.Write(FieldCount);
      }
      #endregion

    }
    private class ConsumerMessageHeader : MessageHeader
    {

      #region creator
      public ConsumerMessageHeader(IBinaryDecoder reader)
      {
        m_reader = reader;
      }
      #endregion

      #region MessageHeader
      public override Guid DataSetId
      {
        get; set;
      }
      public override ushort MessageLength
      {
        get { return m_MessageLength; } 
      }
      public override MessageTypeEnum MessageType
      {
        get; set;
      }
      public override byte MessageFlags
      {
        get; set;
      }
      public override ushort MessageSequenceNumber
      {
        get; set;
      }
      public override ConfigurationVersionDataType ConfigurationVersion
      {
        get; set;
      }
      public override DateTime TimeStamp
      {
        get; set;
      }
      public override ushort FieldCount
      {
        get; set;
      }
      public override void Synchronize()
      {
        ConfigurationVersionDataType _cv = new ConfigurationVersionDataType() { MajorVersion = 0, MinorVersion = 0 };
        DataSetId = m_reader.ReadGuid();
        m_MessageLength = m_reader.ReadUInt16();
        MessageType = (MessageTypeEnum)m_reader.ReadByte();
        MessageFlags = m_reader.ReadByte();
        MessageSequenceNumber = m_reader.ReadUInt16();
        _cv.MajorVersion = m_reader.ReadByte();
        _cv.MinorVersion = m_reader.ReadByte();
        TimeStamp = m_reader.ReadDateTime();
        FieldCount = m_reader.ReadUInt16();
        ConfigurationVersion = _cv;
      }
      #endregion

      #region private
      private IBinaryDecoder m_reader;
      private ushort m_MessageLength;
      #endregion

    }
    private MessageHeader() { }
    #endregion

  }

}
