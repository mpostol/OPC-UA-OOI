
using System;

namespace UAOOI.Networking.SemanticData.Encoding
{

  //types
  [Flags]
  internal enum VariantEncodingMask
  {

    /// <summary>
    /// True if an array of values is encoded.
    /// </summary>
    IsArray = 0x80,
    /// <summary>
    /// True if the Array Dimensions field is encoded
    /// </summary>
    ArrayDimensionsPresents = 0x40,
    /// <summary>
    /// The type mask of the Built-in Type Id
    /// </summary>
    TypeIdMask = 0x3F

  }
}
