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
    public void ProducerPackageHeaderCreatorTestMethod()
    {
      HeaderWriterTest _writer = new HeaderWriterTest();
      PackageHeader _header = PackageHeader.GetProducerPackageHeader(_writer);
      Assert.IsNotNull(_header);
      _header.Synchonize();
      Assert.AreEqual<byte>(0, _header.MessageCount);
      Assert.AreEqual<long>(20, _writer.m_Position);
      _header.MessageCount = 0xff;
      Assert.AreEqual<long>(20, _writer.m_Position);
      Assert.AreEqual<byte>(255, _header.MessageCount);
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
            m_Position = offset;
            break;
          case SeekOrigin.Current:
            m_Position += offset;
            if (m_Position < 0)
              throw new ArgumentOutOfRangeException("Position");
            break;
          case SeekOrigin.End:
            throw new NotImplementedException();
        };
        return m_Position;
      }
      public void Write(Guid value)
      {
        m_Position += 16;
      }
      public void Write(byte value)
      {
        m_Position++;
      }

      #region private
      internal long m_Position = 0;
      #endregion
    }
    #endregion
  }

  #region To be promoted
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

  [Flags]
  public enum MessageFlag
  {
    Metadata = 0x0,
    PeriodicData = 0x1,
  }
  public abstract class PackageHeader
  {
    public static PackageHeader GetProducerPackageHeader(IHeaderWriter writer)
    {
      return new ProducerPackageHeader(writer);
    }

    public abstract void Synchonize();

    #region Header
    public abstract Guid PublisherId { get; set; }
    public abstract byte MessageFlags { get; set; }
    public abstract byte ProtocolVersion { get; set; }
    public abstract byte SecurityTokenId { get; set; }
    public abstract byte MessageCount { get; set; }
    #endregion

    #region private
    private class ProducerPackageHeader : PackageHeader
    {

      public ProducerPackageHeader(IHeaderWriter writer) : base()
      {
        m_Writer = writer;
        PublisherId = Guid.NewGuid();
        b_MessageCount = 0;
        MessageFlags = Convert.ToByte(MessageFlag.PeriodicData);
        ProtocolVersion = 0;
        SecurityTokenId = 0;
      }
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
      public override void Synchonize()
      {
        m_Writer.Write(PublisherId);
        m_Writer.Write(MessageFlags);
        m_Writer.Write(ProtocolVersion);
        m_Writer.Write(SecurityTokenId);
        m_MessageCountPosition = SavePosition();
        m_Writer.Write(MessageCount);
      }

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
