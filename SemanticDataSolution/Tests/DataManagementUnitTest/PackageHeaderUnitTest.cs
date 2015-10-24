using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class PackageHeaderUnitTest
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataManagement_PackageHeaderUnitTest")]
    public void ProducerPackageHeaderTestMethod()
    {
      HeaderWriterTest _writer = new HeaderWriterTest();
      PackageHeader _header = PackageHeader.GetProducerPackageHeader(_writer);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<byte>(0, _header.MessageCount);
      Assert.AreEqual<long>(20, _writer.Position);
      _header.MessageCount = 0xff;
      Assert.AreEqual<long>(20, _writer.Position);
      Assert.AreEqual<byte>(255, _header.MessageCount);
    }
    [TestMethod]
    public void ConsumerPackageHeaderTestMethod()
    {
      HeaderReaderTest _reader = new HeaderReaderTest();
      PackageHeader _header = PackageHeader.GetConsumerPackageHeader(_reader);
      Assert.IsNotNull(_header);
      _header.Synchronize();
      Assert.AreEqual<byte>(0xff, _header.MessageCount);
      Assert.AreEqual<long>(20, _reader.Position);
      _header.MessageCount = 0x0;
      Assert.AreEqual<long>(20, _reader.Position);
      Assert.AreEqual<byte>(0x0, _header.MessageCount);
    }
    #endregion

    #region private 
    private class HeaderWriterTest : IHeaderWriter
    {
      public long Seek(int offset, SeekOrigin origin)
      {
        switch (origin)
        {
          case SeekOrigin.Begin:
            Position = offset;
            break;
          case SeekOrigin.Current:
            Position += offset;
            if (Position < 0)
              throw new ArgumentOutOfRangeException("Position");
            break;
          case SeekOrigin.End:
            throw new NotImplementedException();
        };
        return Position;
      }
      public void Write(Guid value)
      {
        Position += 16;
      }
      public void Write(byte value)
      {
        Position++;
      }
      internal long Position = 0;
    }
    private class HeaderReaderTest : IHeaderReader
    {
      public byte ReadByte()
      {
        Position++;
        return 0xff;
      }
      public Guid ReadGuid()
      {
        Position += 16;
        return Guid.NewGuid();
      }
      internal int Position = 0;
    }
    #endregion

  }

  #region To be promoted
  /// <summary>
  /// Enum MessageFlag - the message control bits.
  /// </summary>
  [Flags]
  public enum MessageFlag
  {
    /// <summary>
    /// The metadata - not implemented 
    /// </summary>
    Metadata = 0x0,
    /// <summary>
    /// The periodic data
    /// </summary>
    PeriodicData = 0x1,
  }
  /// <summary>
  /// Interface IHeaderWriter - instance of this interface is used to manage the message and package headers content by the writer.
  /// </summary>
  public interface IHeaderWriter
  {
    /// <summary>
    /// Sets the position within the current stream.
    /// </summary>
    /// <param name="offset">
    /// A byte offset relative to origin.
    /// </param>
    /// <param name="origin">
    /// A field of <see cref="System.IO.SeekOrigin"/> indicating the reference point from which the new position is to be obtained..
    /// </param>
    /// <returns>The position with the current stream as <see cref="System.Int64"/>.</returns>
    long Seek(int offset, SeekOrigin origin);
    /// <summary>
    /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
    /// </summary>
    /// <param name="value">TThe unsigned <see cref="byte"/> to write./param>
    void Write(byte value);
    /// <summary>
    /// Writes a <see cref="Guid"/> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    void Write(Guid value);
  }
  /// <summary>
  /// Interface IHeaderReader - instance of this interface is used to manage the message and package headers content by the reader.
  /// </summary>
  public interface IHeaderReader
  {
    Guid ReadGuid();
    byte ReadByte();
  }
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
    /// <returns>PackageHeader.</returns>
    public static PackageHeader GetProducerPackageHeader(IHeaderWriter writer)
    {
      return new ProducerPackageHeader(writer);
    }
    /// <summary>
    /// Gets the consumer package header.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>PackageHeader.</returns>
    public static PackageHeader GetConsumerPackageHeader(IHeaderReader reader)
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
      public ConsumerPackageHeader(IHeaderReader reader) : base()
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
      private IHeaderReader m_Reader;
      #endregion

    }
    private class ProducerPackageHeader : PackageHeader
    {
      #region constructor
      public ProducerPackageHeader(IHeaderWriter writer) : base()
      {
        m_Writer = writer;
        PublisherId = Guid.NewGuid();
        b_MessageCount = 0;
        MessageFlags = Convert.ToByte(MessageFlag.PeriodicData);
        ProtocolVersion = 0;
        SecurityTokenId = 0;
      }
      #endregion

      #region PackageHeader
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
          SavePosition();
          m_Writer.Write(b_MessageCount);
          RestorePosition();
        }
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
      private IHeaderWriter m_Writer;
      private long m_MessageCountPosition = 0;
      private byte b_MessageCount = 0;
      private long m_CurrentPosition = 0;
      //methods
      private long SavePosition()
      {
        m_CurrentPosition = m_Writer.Seek(0, SeekOrigin.Current);
        return m_CurrentPosition;
      }
      private long RestorePosition()
      {
        return m_Writer.Seek((int)m_CurrentPosition, SeekOrigin.Begin);
      }
      #endregion

    }
    #endregion

  }
  #endregion
}
