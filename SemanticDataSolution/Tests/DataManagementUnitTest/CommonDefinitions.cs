
using System;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  internal static class CommonDefinitions
  {

    internal static byte[] GetTestBinaryArrayVariant()
    {
      return new byte[]
      {
          //Package header 0-25
          0xf3, 0x5d, 0x19, 0xa6, 0x30, 0x0b, 0x25, 0x4c, 0x8b, 0xf8, 0x45, 0xb0, 0x76, 0x40, 0x21, 0x16, //guid - PublisherId
          110,                                                //byte ProtocolVersion
          0x00,                                               //byte MessageFlags
          0x00,                                               //byte SecurityTokenId
          1,                                                  //byte NonceLength
          0xCC,                                               //Byte[NonceLength] Nonce 
          0x01,                                               //byte MessageCount
          0x7F, 0x4B, 0xFB , 0xBA,                            //UInt32[MessageCount] DataSetWriterIds
          //Message header 26-43
          0x1,                                                //MessageType 
          0x1,                                                //EncodingFlags
          68, 00,                                             // MessageLength
          0, 0,                                               //MessageSequenceNumber 
          0, 0,                                               //ConfigurationVersion
          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //TimeStamp
          13, 0,                                              // FieldCount
          //Message content 60 - 126
          (byte)BuiltInType.UInt64, 0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,    //UInt64
          (byte)BuiltInType.UInt32, 0x7b, 0x00, 0x00, 0x00,                            //UInt32
          (byte)BuiltInType.UInt16, 0x7b, 0x00,                                        //UInt16
          (byte)BuiltInType.String, 0x03, 0x31, 0x32, 0x33,                            //string
          (byte)BuiltInType.Float, 0x00, 0x00, 0xf6, 0x42,                             //Float
          (byte)BuiltInType.SByte, 0x7b,                                               //sbyte
          (byte)BuiltInType.Int64, 0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //Int64
          (byte)BuiltInType.Int32, 0x7b, 0x00, 0x00, 0x00,                             //Int32
          (byte)BuiltInType.Int16, 0x7b, 0x00,                                         //Int16
          (byte)BuiltInType.Double, 0x00, 0x00, 0x00, 0x00, 0x00, 0xc0, 0x5e, 0x40,    //Double
          (byte)BuiltInType.DateTime, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,  //DateTime
          (byte)BuiltInType.Byte, 0x7b,                                                //Byte
          (byte)BuiltInType.Boolean, 0x01,                                             //boolean
      };
    }
    internal static byte[] GetTestBinaryArrayVariant4Consumer()
    {
      return new byte[]
      {
          //Package header 0-25
          0xf3, 0x5d, 0x19, 0xa6, 0x30, 0x0b, 0x25, 0x4c, 0x8b, 0xf8, 0x45, 0xb0, 0x76, 0x40, 0x21, 0x16, //guid - PublisherId
          110,                                                //byte ProtocolVersion
          0x00,                                               //byte MessageFlags
          0x00,                                               //byte SecurityTokenId
          0,                                                  //byte NonceLength
          //0xCC,                                             Byte[NonceLength] Nonce 
          0x01,                                               //byte MessageCount
          127, 75, 251 , 186,                                 //UInt32[MessageCount] DataSetWriterIds
          //Message header 26-43
          0x1,                                                //MessageType 
          0x1,                                                //EncodingFlags
          68, 00,                                             //MessageLength
          0, 0,                                               //MessageSequenceNumber 
          0, 0,                                               //ConfigurationVersion
          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //TimeStamp
          13, 0,                                              // FieldCount
          //Message content 60 - 126
          (byte)BuiltInType.UInt64, 0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,    //UInt64
          (byte)BuiltInType.UInt32, 0x7b, 0x00, 0x00, 0x00,                            //UInt32
          (byte)BuiltInType.UInt16, 0x7b, 0x00,                                        //UInt16
          (byte)BuiltInType.String, 0x03, 0x31, 0x32, 0x33,                            //string
          (byte)BuiltInType.Float, 0x00, 0x00, 0xf6, 0x42,                             //Float
          (byte)BuiltInType.SByte, 0x7b,                                               //sbyte
          (byte)BuiltInType.Int64, 0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //Int64
          (byte)BuiltInType.Int32, 0x7b, 0x00, 0x00, 0x00,                             //Int32
          (byte)BuiltInType.Int16, 0x7b, 0x00,                                         //Int16
          (byte)BuiltInType.Double, 0x00, 0x00, 0x00, 0x00, 0x00, 0xc0, 0x5e, 0x40,    //Double
          (byte)BuiltInType.DateTime, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,  //DateTime
          (byte)BuiltInType.Byte, 0x7b,                                                //Byte
          (byte)BuiltInType.Boolean, 0x01,                                             //boolean
      };
    }

    internal const uint DataSetId = 3137031039;
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
