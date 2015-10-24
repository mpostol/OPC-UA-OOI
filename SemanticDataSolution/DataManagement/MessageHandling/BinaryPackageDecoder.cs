
using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class BinaryPackageDecoder - OPC UA binary package decoder.
  /// </summary>
  public abstract class BinaryPackageDecoder : BinaryMessageDecoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPackageDecoder"/> class.
    /// </summary>
    public BinaryPackageDecoder()
    {
      Header = PackageHeader.GetConsumerPackageHeader(this);
    }
    #endregion

    /// <summary>
    /// Gets or sets the header of the package.
    /// </summary>
    /// <value>The header <see cref="PackageHeader"/>.</value>
    public PackageHeader Header { get; set; }


    #region BinaryMessageDecoder
    /// <summary>
    /// Reads an 8-byte unsigned integer from the message and advances the position by eight bytes.
    /// </summary>
    /// <returns>An 8-byte unsigned integer <see cref="UInt64"/> read from this message. .</returns>
    protected override UInt64 ReadUInt64()
    {
      return UABinaryReader.ReadUInt64();
    }
    protected override UInt32 ReadUInt32()
    {
      return UABinaryReader.ReadUInt32();
    }
    protected override UInt16 ReadUInt16()
    {
      return UABinaryReader.ReadUInt16();
    }
    protected override String ReadString()
    {
      return UABinaryReader.ReadString();
    }
    protected override Single ReadSingle()
    {
      return UABinaryReader.ReadSingle();
    }
    protected override SByte ReadSByte()
    {
      return UABinaryReader.ReadSByte();
    }
    protected override Int64 ReadInt64()
    {
      return UABinaryReader.ReadInt64();
    }
    protected override Int32 ReadInt32()
    {
      return UABinaryReader.ReadInt32();
    }
    protected override Int16 ReadInt16()
    {
      return UABinaryReader.ReadInt16();
    }
    protected override Double ReadDouble()
    {
      return UABinaryReader.ReadDouble();
    }
    protected override Decimal ReadDecimal()
    {
      return Convert.ToDecimal(UABinaryReader.ReadInt64());
    }
    protected override char ReadChar()
    {
      return UABinaryReader.ReadChar();
    }
    public override Byte ReadByte()
    {
      return UABinaryReader.ReadByte();
    }
    protected override Boolean ReadBoolean()
    {
      return UABinaryReader.ReadBoolean();
    }
    protected override DateTime ReadDateTime()
    {
      return CommonDefinitions.GetUADateTime(UABinaryReader.ReadInt64());
    }
    public override Guid ReadGuid()
    {
      return UABinaryReader.ReadGuid();
    }
    #endregion

    #region private
    /// <summary>
    /// Gets the ua binary reader.
    /// </summary>
    /// <value>The ua binary reader.</value>
    protected abstract UABinaryReader UABinaryReader { get; }
    /// <summary>
    /// Called by the network handler and start Analyzing new package by awaking all readers waiting for the messages by raising the event.
    /// </summary>
    protected void OnNewPackageArrived()
    {
      Header.Synchronize();
      for (int i = Header.MessageCount; i > 0; i--)
        RaiseReadMessageCompleted();
    }
    #endregion

  }
}
