
using System;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryDecoder - wrapper of <see cref="UABinaryReader"/> supporting OPC UA binary encoding.
  /// </summary>
  public abstract class BinaryDecoder : BinaryPackageDecoder, IDisposable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPackageDecoder" /> class is to be used by the package level decoding.
    /// </summary>
    /// <param name="uaDecoder">The ua decoder to be used fo decode UA Built-in data types.</param>
    public BinaryDecoder(IUADecoder uaDecoder) : base(uaDecoder) { }
    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposedValue)
        return;
      if (disposing)
      {
        UABinaryReader _lc = m_UABinaryReader;
        if (_lc != null)
          _lc.Close();
        m_UABinaryReader = null;
      }
      disposedValue = true;
    }
    // This code added to correctly implement the disposable pattern.
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion

    #region BinaryPackageDecoder
    /// <summary>
    /// Reads an 8-byte unsigned integer from the message and advances the position by eight bytes.
    /// </summary>
    /// <returns>An 8-byte unsigned integer <see cref="UInt64"/> read from this message. .</returns>
    public override UInt64 ReadUInt64()
    {
      return m_UABinaryReader.ReadUInt64();
    }
    public override UInt32 ReadUInt32()
    {
      return m_UABinaryReader.ReadUInt32();
    }
    public override UInt16 ReadUInt16()
    {
      return m_UABinaryReader.ReadUInt16();
    }
    public override String ReadString()
    {
      return m_UABinaryReader.ReadString();
    }
    public override Single ReadSingle()
    {
      return m_UABinaryReader.ReadSingle();
    }
    public override SByte ReadSByte()
    {
      return m_UABinaryReader.ReadSByte();
    }
    public override Int64 ReadInt64()
    {
      return m_UABinaryReader.ReadInt64();
    }
    public override Int32 ReadInt32()
    {
      return m_UABinaryReader.ReadInt32();
    }
    public override Int16 ReadInt16()
    {
      return m_UABinaryReader.ReadInt16();
    }
    public override Double ReadDouble()
    {
      return m_UABinaryReader.ReadDouble();
    }
    public override char ReadChar()
    {
      return m_UABinaryReader.ReadChar();
    }
    public override Byte ReadByte()
    {
      return m_UABinaryReader.ReadByte();
    }
    public override Boolean ReadBoolean()
    {
      return m_UABinaryReader.ReadBoolean();
    }
    public override DateTime ReadDateTime()
    {
      return m_UABinaryReader.ReadDateTime();
    }
    public override Guid ReadGuid()
    {
      return m_UABinaryReader.ReadGuid();
    }
    public override byte[] ReadBytes(int count)
    {
      return m_UABinaryReader.ReadBytes(count);
    }
    #endregion

    #region private
    /// <summary>
    /// Called when new frame has arrived.
    /// </summary>
    /// <param name="uaBinaryReader">
    /// The UA binary reader an instance of <see cref="UABinaryReader"/> 
    /// created after new frame has been arrived.
    /// </param>
    /// <remarks> Just after processing the object is disposed.</remarks>
    protected void OnNewFrameArrived(UABinaryReader uaBinaryReader)
    {
      m_UABinaryReader = uaBinaryReader;
      OnNewPackageArrived();
      m_UABinaryReader.Dispose();
      m_UABinaryReader = null; ;
    }
    private UABinaryReader m_UABinaryReader;
    #endregion

  }

}
