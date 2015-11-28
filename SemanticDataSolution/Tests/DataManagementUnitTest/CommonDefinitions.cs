
using System;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  internal static class CommonDefinitions
  {

    internal static byte[] GetTestBinaryArray()
    {
      return new byte[]
      {
          //Package header
          0xf3, 0x5d, 0x19, 0xa6, 0x30, 0x0b, 0x25, 0x4c, 0x8b, 0xf8, 0x45, 0xb0, 0x76, 0x40, 0x21, 0x16, //guid - PublisherId
          0x01,                                               //byte MessageFlags
          0x00,                                               //byte ProtocolVersion
          0x00,                                               //byte SecurityTokenId
          0x01,                                               //byte MessageCount
          //Message header
          0xf3, 0x5d, 0x19, 0xa6, 0x30, 0x0b, 0x25, 0x4c, 0x8b, 0xf8, 0x45, 0xb0, 0x76, 0x40, 0x21, 0x16, //guid - PublisherId
          //Message content
          0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //UInt64
          0x7b, 0x00, 0x00, 0x00,                             //UInt32
          0x7b, 0x00,                                         //UInt16
          0x03, 0x31, 0x32, 0x33,                             //string
          0x00, 0x00, 0xf6, 0x42,                             //Float
          0x7b,                                               //sbyte
          0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //Int64
          0x7b, 0x00, 0x00, 0x00,                             //Int32
          0x7b, 0x00,                                         //Int16
          0x00, 0x00, 0x00, 0x00, 0x00, 0xc0, 0x5e, 0x40,     //Double
          //0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //decimal
          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //DateTime
          0x7b,                                               //Byte
          0x01,                                               //boolean
          //0x41,                                               //Char
          //0x01, 0x02, 0x03,                                   //byte[]
      };
    }
    internal struct DateTimeVariantEncoding
    {
      internal DateTime dateTime;
      internal byte[] encoding;
    }
    internal static readonly DateTime TestMinimalDateTime = new DateTime(1601, 1, 1);
    internal static readonly DateTime TestMaximumDateTime = new DateTime(9999, 12, 31, 23, 59, 59);
    internal static readonly Guid TestGuid = new Guid("A6195DF3-0B30-4C25-8BF8-45B076402116");
    internal static readonly byte[] TestGuidVariant = new byte[] { (byte)BuiltInType.Guid, 0xf3, 0x5d, 0x19, 0xa6, 0x30, 0x0b, 0x25, 0x4c, 0x8b, 0xf8, 0x45, 0xb0, 0x76, 0x40, 0x21, 0x16 };
    internal static DateTimeVariantEncoding[] DateTimeTestingValues = new DateTimeVariantEncoding[]
    {
      new DateTimeVariantEncoding() {  dateTime = TestMinimalDateTime, encoding = new byte[] { (byte)BuiltInType.DateTime, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
      new DateTimeVariantEncoding() {  dateTime = TestMaximumDateTime, encoding = new byte[] { (byte)BuiltInType.DateTime, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x7f } },
      new DateTimeVariantEncoding() {  dateTime = new DateTime(9999, 12, 31, 23, 59, 58), encoding = new byte[] { (byte)BuiltInType.DateTime, 0x00, 0x13, 0x8f,0xd0, 0x5e, 0x5a, 0xc8, 0x24 } }
    };
    /// <summary>
    /// The producer identifier - should be moved to the configuration - see spec for current definition.
    /// </summary>
    internal static object[] TestValues = new object[]
      {
        (ulong)123, (uint)123, (ushort)123, "123",
        (float)123, (sbyte)123, (long)123, (int)123,
        (short)123, (double)123, /*(decimal)123,*/ TestMinimalDateTime,
        (byte)123, true/*, 'A'*/}; //, new byte[] { 1, 2, 3 } };
  }
}
