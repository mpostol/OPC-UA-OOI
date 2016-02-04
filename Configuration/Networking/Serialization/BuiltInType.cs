
using System.Runtime.Serialization;

namespace UAOOI.Configuration.Networking.Serialization
{
  /// <summary>
  /// The set of built-in data types for UA type descriptions - see Part 6 5.1.2.
  /// </summary>
  /// <remarks>
  /// An enumeration that lists all of the built-in data types for OPC UA Type Descriptions.
  /// </remarks>
  [DataContractAttribute(Name = "BuiltInType", Namespace = CommonDefinitions.Namespace)]
  public enum BuiltInType : int
  {
    /// <summary>
    /// An invalid or unspecified value.
    /// </summary>
    [EnumMemberAttribute()]
    Null = 0,
    /// <summary>
    /// A boolean logic value (true or false) - A two-state logical value (true or false).
    /// </summary>
    [EnumMemberAttribute()]
    Boolean = 1,
    /// <summary>
    /// An 8 bit signed integer value. An integer value between −128 and 127.
    /// </summary>
    [EnumMemberAttribute()]
    SByte = 2,
    /// <summary>
    /// An 8 bit unsigned integer value. An integer value between 0 and 255.
    /// </summary>
    [EnumMemberAttribute()]
    Byte = 3,
    /// <summary>
    /// A 16 bit signed integer value. An integer value between 0 and 65 535.
    /// </summary>
    [EnumMemberAttribute()]
    Int16 = 4,
    /// <summary>
    /// A 16 bit unsigned integer value. An integer value between 0 and 65 535.
    /// </summary>
    [EnumMemberAttribute()]
    UInt16 = 5,
    /// <summary>
    /// A 32 bit signed integer value. An integer value between −2 147 483 648 and 2 147 483 647.
    /// </summary>
    [EnumMemberAttribute()]
    Int32 = 6,
    /// <summary>
    /// A 32 bit unsigned integer value. An integer value between 0 and 4 294 967 295.
    /// </summary>
    [EnumMemberAttribute()]
    UInt32 = 7,
    /// <summary>
    /// A 64 bit signed integer value. An integer value between −9 223 372 036 854 775 808 and 9 223 372 036 854 775 807
    /// </summary>
    [EnumMemberAttribute()]
    Int64 = 8,
    /// <summary>
    /// A 64 bit unsigned integer value. An integer value between 0 and 18 446 744 073 709 551 615.
    /// </summary>
    [EnumMemberAttribute()]
    UInt64 = 9,
    /// <summary>
    /// An IEEE single precision (32 bit) floating point value. An IEEE single precision (32 bit) floating point value.
    /// </summary>
    [EnumMemberAttribute()]
    Float = 10,
    /// <summary>
    /// An IEEE double precision (64 bit) floating point value. An IEEE double precision (64 bit) floating point value.
    /// </summary>
    [EnumMemberAttribute()]
    Double = 11,
    /// <summary>
    /// A sequence of Unicode characters.
    /// </summary>
    [EnumMemberAttribute()]
    String = 12,
    /// <summary>
    /// An instance in time.
    /// </summary>
    [EnumMemberAttribute()]
    DateTime = 13,
    /// <summary>
    /// A 128-bit globally unique identifier.
    /// </summary>
    [EnumMemberAttribute()]
    Guid = 14,
    /// <summary>
    /// A sequence of bytes.
    /// </summary>
    [EnumMemberAttribute()]
    ByteString = 15,
    /// <summary>
    /// An XML element.
    /// </summary>
    [EnumMemberAttribute()]
    XmlElement = 16,
    /// <summary>
    /// An identifier for a node in the address space of an OPC UA Server.
    /// </summary>
    [EnumMemberAttribute()]
    NodeId = 17,
    /// <summary>
    /// A node id that stores the namespace URI instead of the namespace index.
    /// </summary>
    [EnumMemberAttribute()]
    ExpandedNodeId = 18,
    /// <summary>
    /// A structured result code.
    /// </summary>
    [EnumMemberAttribute()]
    StatusCode = 19,
    /// <summary>
    /// A string qualified with a namespace.
    /// </summary>
    [EnumMemberAttribute()]
    QualifiedName = 20,
    /// <summary>
    /// A localized text string with an locale identifier.
    /// </summary>
    [EnumMemberAttribute()]
    LocalizedText = 21,
    /// <summary>
    /// A structure that contains an application specific data type that may not be recognized by the receiver.
    /// </summary>
    [EnumMemberAttribute()]
    ExtensionObject = 22,
    /// <summary>
    /// A data value with an associated quality and time stamp.
    /// </summary>
    [EnumMemberAttribute()]
    DataValue = 23,
    /// <summary>
    /// Any of the other built-in types - a union of all of the types specified above.
    /// </summary>
    [EnumMemberAttribute()]
    Variant = 24,
    /// <summary>
    /// A diagnostic information associated with a result code.
    /// </summary>
    [EnumMemberAttribute()]
    DiagnosticInfo = 25,
    /// <summary>
    /// The enumeration
    /// </summary>
    [EnumMemberAttribute()]
    Enumeration = 26,
  }

}
