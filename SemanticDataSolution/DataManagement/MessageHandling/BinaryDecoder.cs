using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryDecoder - wrapper of <see cref="UABinaryReader"/> supporting OPC UA binary encoding.
  /// </summary>
  public abstract class BinaryDecoder : BinaryPackageDecoder, IDisposable
  {

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          UABinaryReader _lc = m_UABinaryReader;
          if (_lc != null)
            _lc.Close();
          m_UABinaryReader = null;
        }
        disposedValue = true;
      }
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

    #region BinaryMessageDecoder
    /// <summary>
    /// Reads an 8-byte unsigned integer from the message and advances the position by eight bytes.
    /// </summary>
    /// <returns>An 8-byte unsigned integer <see cref="UInt64"/> read from this message. .</returns>
    protected override UInt64 ReadUInt64()
    {
      return m_UABinaryReader.ReadUInt64();
    }
    protected override UInt32 ReadUInt32()
    {
      return m_UABinaryReader.ReadUInt32();
    }
    protected override UInt16 ReadUInt16()
    {
      return m_UABinaryReader.ReadUInt16();
    }
    protected override String ReadString()
    {
      return m_UABinaryReader.ReadString();
    }
    protected override Single ReadSingle()
    {
      return m_UABinaryReader.ReadSingle();
    }
    protected override SByte ReadSByte()
    {
      return m_UABinaryReader.ReadSByte();
    }
    protected override Int64 ReadInt64()
    {
      return m_UABinaryReader.ReadInt64();
    }
    protected override Int32 ReadInt32()
    {
      return m_UABinaryReader.ReadInt32();
    }
    protected override Int16 ReadInt16()
    {
      return m_UABinaryReader.ReadInt16();
    }
    protected override Double ReadDouble()
    {
      return m_UABinaryReader.ReadDouble();
    }
    protected override Decimal ReadDecimal()
    {
      return Convert.ToDecimal(m_UABinaryReader.ReadInt64());
    }
    protected override char ReadChar()
    {
      return m_UABinaryReader.ReadChar();
    }
    public override Byte ReadByte()
    {
      return m_UABinaryReader.ReadByte();
    }
    protected override Boolean ReadBoolean()
    {
      return m_UABinaryReader.ReadBoolean();
    }
    protected override DateTime ReadDateTime()
    {
      return m_UABinaryReader.ReadDateTime();
    }
    public override Guid ReadGuid()
    {
      return m_UABinaryReader.ReadGuid();
    }
    #endregion

    #region private
    /// <summary>
    /// Called when new frame has arrived.
    /// </summary>
    /// <param name="uaBinaryReader">The UA binary reader <see cref="UABinaryReader"/>.</param>
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
