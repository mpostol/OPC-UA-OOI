using System;

using System.IO;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  internal class HeaderWriterTest : IBinaryHeaderWriter
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
          Position = End + offset;
          if (Position < 0)
            throw new ArgumentOutOfRangeException("Position");
          break;
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
  internal class HeaderReaderTest : IBinaryHeaderReader
  {
    public byte ReadByte()
    {
      Position++;
      return 0xff;
    }
    public Guid ReadGuid()
    {
      Position += 16;
      return MessageHandling.CommonDefinitions.ProducerId;
    }
    internal int Position = 0;
  }

}
