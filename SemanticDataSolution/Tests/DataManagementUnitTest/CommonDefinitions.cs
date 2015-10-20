
using System;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  internal static class CommonDefinitions
  {
    
    internal static byte[] GetTestBinaryArray()
    {
      return new byte[]	
      {
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
          0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //Int32
          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //DateTime
          0x7b,                                               //Byte
          0x01,                                               //boolean
          0x41,                                               //Char
          //0x01, 0x02, 0x03,                                   //byte[]
      };

    }
    internal static object[] TestValues = new object[] 
      { 
        (ulong)123, (uint)123, (ushort)123, "123", 
        (float)123, (sbyte)123, (long)123, (int)123, 
        (short)123, (double)123, (decimal)123, new DateTime(1601, 1, 1), 
        (byte)123, true, 'A'}; //, new byte[] { 1, 2, 3 } };
  }
}
