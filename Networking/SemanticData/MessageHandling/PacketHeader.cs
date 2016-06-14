
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.Networking.SemanticData.Encoding;
using System.Collections.ObjectModel;

namespace UAOOI.Networking.SemanticData.MessageHandling
{

  /// <summary>
  /// Class PacketHeader - represent information in the protocol packet header.
  /// </summary>
  public abstract class PacketHeader
  {

    #region public API
    /// <summary>
    /// Gets the producer packet header.
    /// </summary>
    /// <param name="encoder">The writer.</param>
    /// <param name="producerId">The producer identifier.</param>
    /// <param name="dataSetWriterIds">The data set writer ids list. The size of the list must be equal to the <see cref="PacketHeader.MessageCount"/>.</param>
    /// <returns>An instance of the <see cref="PacketHeader"/>.</returns>
    public static PacketHeader GetProducerPacketHeader(IBinaryHeaderEncoder encoder, Guid producerId, IList<UInt16> dataSetWriterIds)
    {
      return new ProducerHeader(encoder, producerId, dataSetWriterIds);
    }
    /// <summary>
    /// Gets the consumer packet header.
    /// </summary>
    /// <param name="decoder">The reader.</param>
    /// <returns>New instance of the <see cref="PacketHeader"/>.</returns>
    public static PacketHeader GetConsumerPacketHeader(IBinaryDecoder decoder)
    {
      return new ConsumerHeader(decoder);
    }
    /// <summary>
    /// Synchronizes this instance content with the header.
    /// </summary>
    public abstract void WritePacketHeader();
    #endregion

    #region Header
    /// <summary>
    /// If implemented gets or sets the protocol version.
    /// </summary>
    /// <value>The protocol version.</value>
    public abstract byte ProtocolVersion { get; set; }
    /// <summary>
    /// If implemented gets or sets the packet flags.
    /// </summary>
    /// <value>The packet flags.</value>
    public abstract byte NetworkMessageFlags { get; set; }
    /// <summary>
    /// If implemented gets or sets the identifier of producer that sends the data.
    /// </summary>
    /// <value>The <see cref="Guid"/> representing the producer.</value>
    public abstract Guid PublisherId { get; set; }
    /// <summary>
    /// If implemented gets or sets the security token identifier.
    /// </summary>
    /// <value>The security token identifier.</value>
    public abstract UInt32 SecurityTokenId { get; set; }
    /// <summary>
    /// Gets or sets the length of the nonce used to initialize the encryption algorithm..
    /// </summary>
    /// <value>The length of the nonce.</value>
    public byte NonceLength { get; set; }
    /// <summary>
    /// Gets or sets the nonce a cryptographically random number used for exactly one packet.
    /// </summary>
    /// <value>The nonce as the array of <see cref="byte"/>.</value>
    public byte[] Nonce { get; set; }
    /// <summary>
    /// If implemented gets or sets the number of messages contained in the packet.
    /// </summary>
    /// <value>The message count.</value>
    public abstract byte MessageCount { get; }
    /// <summary>
    /// If implemented gets or sets the data set writer ids list. The size of the list is defined by the <see cref="PacketHeader.MessageCount"/>.
    /// It identifies the publisher and the message writer responsible for sending Messages for the DataSet.
    /// </summary>
    /// <value>The data set writer ids.</value>
    public abstract ReadOnlyCollection<UInt16> DataSetWriterIds { get; }
    #endregion

    #region private implementation
    private class ConsumerHeader : PacketHeader
    {

      #region constructor
      public ConsumerHeader(IBinaryDecoder reader) : base()
      {
        m_Reader = reader;
        ReadPacketHeader();
      }
      #endregion

      #region PacketHeader
      public override byte MessageCount
      {
        get { return m_MessageCount; }
      }
      public override byte NetworkMessageFlags
      {
        get; set;
      }
      public override byte ProtocolVersion
      {
        get; set;
      }
      public override Guid PublisherId
      {
        get; set;
      }
      public override UInt32 SecurityTokenId
      {
        get; set;
      }
      public override ReadOnlyCollection<UInt16> DataSetWriterIds
      {
        get { return m_DataSetWriterIds; }
      }
      public override void WritePacketHeader()
      {
        throw new ApplicationException("Consumer packet is read only");
      }
      #endregion

      #region private
      private IBinaryDecoder m_Reader;
      private ReadOnlyCollection<UInt16> m_DataSetWriterIds;
      private byte m_MessageCount;
      private void ReadPacketHeader()
      {
        ProtocolVersion = m_Reader.ReadByte();
        NetworkMessageFlags = m_Reader.ReadByte();
        PublisherId = m_Reader.ReadGuid();
        SecurityTokenId = m_Reader.ReadUInt32();
        NonceLength = m_Reader.ReadByte();
        Nonce = new byte[NonceLength];
        for (int i = 0; i < NonceLength; i++)
          Nonce[i] = m_Reader.ReadByte();
        m_MessageCount = m_Reader.ReadByte();
        List<UInt16> _ids = new List<UInt16>();
        for (int i = 0; i < MessageCount; i++)
          _ids.Add(m_Reader.ReadUInt16());
        m_DataSetWriterIds = new ReadOnlyCollection<UInt16>(_ids);
      }
      #endregion

    }
    private class ProducerHeader : PacketHeader
    {

      #region constructor
      public ProducerHeader(IBinaryHeaderEncoder writer, Guid producerId, IList<UInt16> dataSetWriterIds) : base()
      {
        if (writer == null)
          throw new ArgumentNullException(nameof(writer));
        PublisherId = producerId;
        NetworkMessageFlags = Convert.ToByte(PacketFlagsDefinitions.NetworkMessageType.RegularMessages);
        ProtocolVersion = CommonDefinitions.ProtocolVersion;
        SecurityTokenId = 0;
        NonceLength = 0;
        DataSetWriterIds = new ReadOnlyCollection<UInt16>(dataSetWriterIds);
        MessageCount = Convert.ToByte(DataSetWriterIds.Count);
        ushort _packetLength = Convert.ToUInt16(m_PacketHeaderLength + dataSetWriterIds.Count * 2);
        m_HeaderWriter = new HeaderWriter(writer, _packetLength);
      }
      #endregion

      #region PacketHeader
      /// <summary>
      /// Gets or sets the number of messages contained in the packet.
      /// </summary>
      /// <value>The message count.</value>
      public override byte MessageCount
      {
        get;
      }
      /// <summary>
      /// Gets or sets the message flags.
      /// </summary>
      /// <value>The message flags.</value>
      public override byte NetworkMessageFlags
      {
        get; set;
      }
      /// <summary>
      /// Gets or sets the protocol version.
      /// </summary>
      /// <value>The protocol version.</value>
      public override byte ProtocolVersion
      {
        get; set;
      }
      /// <summary>
      /// Gets or sets the identifier of producer that sends the data.
      /// </summary>
      /// <value>The <see cref="Guid" /> representing the producer.</value>
      public override Guid PublisherId
      {
        get; set;
      }
      /// <summary>
      /// Gets or sets the security token identifier.
      /// </summary>
      /// <value>The security token identifier.</value>
      public override UInt32 SecurityTokenId
      {
        get; set;
      }
      public override ReadOnlyCollection<UInt16> DataSetWriterIds
      {
        get;
      }
      /// <summary>
      /// Synchronizes this instance content with the header.
      /// </summary>
      public override void WritePacketHeader()
      {
        m_HeaderWriter.WriteHeader(WriteHeader);
      }
      #endregion

      #region private
      //vars
      private HeaderWriter m_HeaderWriter;
      private const ushort m_PacketHeaderLength = 20;
      //methods
      private void WriteHeader(IBinaryHeaderEncoder m_Writer, ushort dataLength)
      {
        Debug.Assert(DataSetWriterIds != null);
        Debug.Assert(DataSetWriterIds.Count == MessageCount);
        m_Writer.Write(ProtocolVersion);
        m_Writer.Write(NetworkMessageFlags);
        m_Writer.Write(PublisherId);
        m_Writer.Write(SecurityTokenId);
        m_Writer.Write(NonceLength);
        for (int i = 0; i < NonceLength; i++)
          m_Writer.Write(Nonce[i]);
        m_Writer.Write(MessageCount);
        if (MessageCount == 0)
          return;
        for (int i = 0; i < DataSetWriterIds.Count; i++)
          m_Writer.Write(DataSetWriterIds[i]);
      }
      #endregion

    }
    #endregion

  }

}
