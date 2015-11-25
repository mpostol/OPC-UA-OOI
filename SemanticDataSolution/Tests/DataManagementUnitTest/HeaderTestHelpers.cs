
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  internal class HeaderWriterTest : IBinaryHeaderWriter
  {
    #region constructor
    public HeaderWriterTest(long startPosition)
    {
      b_Position = startPosition;
    }
    public HeaderWriterTest() : this(0) { }
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
    public void WriteGuid(Guid value)
    {
      Position += 16;
    }
    public void WriteByte(byte value)
    {
      Position++;
    }
    public void WriteInt32(int value)
    {
      throw new NotImplementedException();
    }

    public void WriteBoolean(bool value)
    {
      throw new NotImplementedException();
    }

    public void WriteSByte(sbyte value)
    {
      throw new NotImplementedException();
    }

    public void WriteInt16(short value)
    {
      throw new NotImplementedException();
    }

    public void WriteUInt16(ushort value)
    {
      throw new NotImplementedException();
    }

    public void WriteUInt32(uint value)
    {
      throw new NotImplementedException();
    }

    public void WriteInt64(long value)
    {
      throw new NotImplementedException();
    }

    public void WriteUInt64(ulong value)
    {
      throw new NotImplementedException();
    }

    public void WriteSingle(float value)
    {
      throw new NotImplementedException();
    }

    public void WriteDouble(double value)
    {
      throw new NotImplementedException();
    }

    public void WriteString(string value)
    {
      throw new NotImplementedException();
    }

    public void WriteBytes(byte[] value)
    {
      throw new NotImplementedException();
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
      m_Position++;
      return 0xff;
    }
    public Guid ReadGuid()
    {
      m_Position += 16;
      return CommonDefinitions.TestGuid;
    }
    int IBinaryDecoder.ReadInt32()
    {
      throw new NotImplementedException();
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
      throw new NotImplementedException();
    }

    uint IBinaryDecoder.ReadUInt32()
    {
      throw new NotImplementedException();
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

    internal long m_Position = 0;

  }

}
