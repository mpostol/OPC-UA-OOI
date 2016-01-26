
using System;

namespace UAOOI.Configuration.Networking.Serialization
{
  /// <summary>
  /// Enum FieldEncodingEnum - 
  /// </summary>
  public enum FieldEncodingEnum : Byte
  {
    /// <summary>
    /// The variant encoding - The DataSetFields are encoded as Variant. The Variant can contain a StatusCode instead of the expected DataType if the 
    /// status of the field is Bad. The Variant can contain a DataValue if the status of the field is Uncertain.
    /// </summary>
    VariantFieldEncoding = 0x0,
    /// <summary>
    /// The compressed field encoding - The DataSet fields are encoded using the data types defined in the configuration.
    /// </summary>
    CompressedFieldEncoding = 0x4,
    /// <summary>
    /// The data value field encoding - The DataSet fields are encoded as DataValue. This option is set if the DataSet is configured to send more than the Value.
    /// </summary>
    DataValueFieldEncoding = 0x8
  }
}
