
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  internal class HeaderWriterTest : IBinaryHeaderWriter
  {

    #region constructor
    public HeaderWriterTest(Action<Int64> callback, long startPosition)
    {
      b_Position = startPosition;
      m_callBack = callback;
    }
    public HeaderWriterTest(Action<Int64> callback) : this(callback, 0) { }
    #endregion

    #region IBinaryHeaderWriter
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
          Position = End + offset;
          if (Position < 0)
            throw new ArgumentOutOfRangeException("Position");
          break;
      };
      return Position;
    }
    public void Write(Guid value)
    {
      m_callBack(Position);
      Position += 16;
    }
    public void Write(byte value)
    {
      m_callBack(Position);
      Position++;
    }
    public void Write(int value)
    {
      m_callBack(Position);
      Position += 4;
    }
    public void Write(bool value)
    {
      m_callBack(Position);
      throw new NotImplementedException();
    }
    public void Write(sbyte value)
    {
      m_callBack(Position);
      throw new NotImplementedException();
    }
    public void Write(short value)
    {
      throw new NotImplementedException();
    }
    public void Write(ushort value)
    {
      m_callBack(Position);
      Position += 2;
    }
    public void Write(uint value)
    {
      m_callBack(Position);
      Position += 4;
    }
    public void Write(long value)
    {
      throw new NotImplementedException();
    }
    public void Write(ulong value)
    {
      throw new NotImplementedException();
    }
    public void Write(float value)
    {
      throw new NotImplementedException();
    }
    public void Write(double value)
    {
      throw new NotImplementedException();
    }
    public void Write(string value)
    {
      throw new NotImplementedException();
    }
    public void Write(byte[] value)
    {
      throw new NotImplementedException();
    }
    public void Write(DateTime value)
    {
      m_callBack(Position);
      Position += 8;
    }
    #endregion

    internal long End = 0;
    internal long Position
    {
      get { return b_Position; }
      set
      {
        b_Position = value;
        if (b_Position > End)
          End = Position;
      }
    }
    private long b_Position = 0;
    private Action<Int64> m_callBack = null;

  }
  internal class HeaderReaderTest : IBinaryDecoder
  {

    public HeaderReaderTest(long startPosition)
    {
      m_Position = startPosition;
    }
    public HeaderReaderTest() : this(0) { }
    public byte ReadByte()
    {
      byte _pos = Convert.ToByte(m_Position);
      m_Position++;
      return _pos;
    }
    public Guid ReadGuid()
    {
      m_Position += 16;
      return CommonDefinitions.TestGuid;
    }
    int IBinaryDecoder.ReadInt32()
    {
      int _pos = Convert.ToInt32(m_Position);
      m_Position += 4;
      return _pos;
    }
    bool IBinaryDecoder.ReadBoolean()
    {
      throw new NotImplementedException();
    }
    sbyte IBinaryDecoder.ReadSByte()
    {
      throw new NotImplementedException();
    }
    short IBinaryDecoder.ReadInt16()
    {
      throw new NotImplementedException();
    }
    ushort IBinaryDecoder.ReadUInt16()
    {
      ushort _pos = Convert.ToUInt16(m_Position);
      m_Position += 2;
      return _pos;
    }
    uint IBinaryDecoder.ReadUInt32()
    {
      uint _pos = Convert.ToUInt32(m_Position);
      m_Position += 4;
      return _pos;
    }
    long IBinaryDecoder.ReadInt64()
    {
      throw new NotImplementedException();
    }
    ulong IBinaryDecoder.ReadUInt64()
    {
      throw new NotImplementedException();
    }
    float IBinaryDecoder.ReadSingle()
    {
      throw new NotImplementedException();
    }
    double IBinaryDecoder.ReadDouble()
    {
      throw new NotImplementedException();
    }
    string IBinaryDecoder.ReadString()
    {
      throw new NotImplementedException();
    }
    public byte[] ReadBytes(int count)
    {
      throw new NotImplementedException();
    }
    public DateTime ReadDateTime()
    {
      m_Position += 8;
      return CommonDefinitions.TestMinimalDateTime;
    }

    internal long m_Position = 0;

  }

}
