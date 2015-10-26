
using System;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class PackageHeader - represent information in the protocol package header.
  /// </summary>
  /// <remarks>
  /// #98: PackageHeader - mus be refined
  /// Because the specification is subject of further development this class mus be refined according to further protocol modification.
  /// The following topics must be addressed:
  /// * PublisherId - how to use it and it is static so exchange it is waste of bandwidth.
  /// * Naming convention publisher => producer; subscriber => consumer
  /// * SecurityTokenId - how to use it, how to define it if producer is not OPC UA Server, why exchange it over the wire
  /// </remarks>
  public abstract class PackageHeader
  {

    #region public API
    /// <summary>
    /// Gets the producer package header.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="producerId">The producer identifier.</param>
    /// <returns>PackageHeader.</returns>
    public static PackageHeader GetProducerPackageHeader(IBinaryHeaderWriter writer, Guid producerId)
    {
      return new ProducerPackageHeader(writer, producerId);
    }
    /// <summary>
    /// Gets the consumer package header.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>PackageHeader.</returns>
    public static PackageHeader GetConsumerPackageHeader(IBinaryHeaderReader reader)
    {
      return new ConsumerPackageHeader(reader);
    }
    /// <summary>
    /// Synchronizes this instance content with the header.
    /// </summary>
    public abstract void Synchronize();
    #endregion

    #region Header
    /// <summary>
    /// Gets or sets the identifier of producer that sends the data.
    /// </summary>
    /// <value>The <see cref="Guid"/> representing the producer.</value>
    public abstract Guid PublisherId { get; set; }
    /// <summary>
    /// Gets or sets the message flags.
    /// </summary>
    /// <value>The message flags.</value>
    public abstract byte MessageFlags { get; set; }
    /// <summary>
    /// Gets or sets the protocol version.
    /// </summary>
    /// <value>The protocol version.</value>
    public abstract byte ProtocolVersion { get; set; }
    /// <summary>
    /// Gets or sets the security token identifier.
    /// </summary>
    /// <value>The security token identifier.</value>
    public abstract byte SecurityTokenId { get; set; }
    /// <summary>
    /// Gets or sets the number of messages contained in the packet.
    /// </summary>
    /// <value>The message count.</value>
    public abstract byte MessageCount { get; set; }
    #endregion

    #region private implementation
    private class ConsumerPackageHeader : PackageHeader
    {

      #region constructor
      public ConsumerPackageHeader(IBinaryHeaderReader reader) : base()
      {
        m_Reader = reader;
      }
      #endregion

      #region PackageHeader
      public override byte MessageCount
      {
        get; set;
      }
      public override byte MessageFlags
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
      public override byte SecurityTokenId
      {
        get; set;
      }
      public override void Synchronize()
      {
        PublisherId = m_Reader.ReadGuid();
        MessageFlags = m_Reader.ReadByte();
        ProtocolVersion = m_Reader.ReadByte();
        SecurityTokenId = m_Reader.ReadByte();
        MessageCount = m_Reader.ReadByte();
      }
      #endregion

      #region private
      private IBinaryHeaderReader m_Reader;
      #endregion

    }
    private class ProducerPackageHeader : PackageHeader
    {
      #region constructor
      public ProducerPackageHeader(IBinaryHeaderWriter writer, Guid producerId) : base()
      {
        m_Writer = writer;
        PublisherId = producerId;
        b_MessageCount = 0;
        MessageFlags = Convert.ToByte(MessageFlag.PeriodicData);
        ProtocolVersion = CommonDefinitions.ProtocolVersion;
        SecurityTokenId = 0;
      }
      #endregion

      #region PackageHeader
      /// <summary>
      /// Gets or sets the number of messages contained in the packet.
      /// </summary>
      /// <value>The message count.</value>
      public override byte MessageCount
      {
        get
        {
          return b_MessageCount;
        }
        set
        {
          if (value == b_MessageCount)
            return;
          b_MessageCount = value;
          SetPosition(m_MessageCountPosition);
          m_Writer.Write(b_MessageCount);
          RestorePosition();
        }
      }
      /// <summary>
      /// Gets or sets the message flags.
      /// </summary>
      /// <value>The message flags.</value>
      public override byte MessageFlags
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
      public override byte SecurityTokenId
      {
        get; set;
      }
      /// <summary>
      /// Synchronizes this instance content with the header.
      /// </summary>
      public override void Synchronize()
      {
        m_Writer.Write(PublisherId);
        m_Writer.Write(MessageFlags);
        m_Writer.Write(ProtocolVersion);
        m_Writer.Write(SecurityTokenId);
        m_MessageCountPosition = SavePosition();
        m_Writer.Write(MessageCount);
      }
      #endregion

      #region private
      //vars
      private IBinaryHeaderWriter m_Writer;
      private int m_MessageCountPosition = 0;
      private byte b_MessageCount = 0;
      //methods
      private int SavePosition()
      {
        return Convert.ToInt32(m_Writer.Seek(0, SeekOrigin.Current));
      }
      private void SetPosition(int offset)
      {
        m_Writer.Seek(offset, SeekOrigin.Begin);
      }
      private long RestorePosition()
      {
        return m_Writer.Seek(0, SeekOrigin.End);
      }
      #endregion

    }
    #endregion

  }

}
