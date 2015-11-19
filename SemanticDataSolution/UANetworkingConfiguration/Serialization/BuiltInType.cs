
namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{
  /// <summary>
  /// The set of built-in data types for UA type descriptions - see Part 6 5.1.2.
  /// </summary>
  /// <remarks>
  /// An enumeration that lists all of the built-in data types for OPC UA Type Descriptions.
  /// </remarks>
  public enum BuiltInType : int
  {
    /// <summary>
    /// An invalid or unspecified value.
    /// </summary>
    Null = 0,
    /// <summary>
    /// A boolean logic value (true or false) - A two-state logical value (true or false).
    /// </summary>
    Boolean = 1,
    /// <summary>
    /// An 8 bit signed integer value. An integer value between −128 and 127.
    /// </summary>
    SByte = 2,
    /// <summary>
    /// An 8 bit unsigned integer value. An integer value between 0 and 255.
    /// </summary>
    Byte = 3,
    /// <summary>
    /// A 16 bit signed integer value. An integer value between 0 and 65 535.
    /// </summary>
    Int16 = 4,
    /// <summary>
    /// A 16 bit unsigned integer value. An integer value between 0 and 65 535.
    /// </summary>
    UInt16 = 5,
    /// <summary>
    /// A 32 bit signed integer value. An integer value between −2 147 483 648 and 2 147 483 647.
    /// </summary>
    Int32 = 6,
    /// <summary>
    /// A 32 bit unsigned integer value. An integer value between 0 and 4 294 967 295.
    /// </summary>
    UInt32 = 7,
    /// <summary>
    /// A 64 bit signed integer value. An integer value between −9 223 372 036 854 775 808 and 9 223 372 036 854 775 807
    /// </summary>
    Int64 = 8,
    /// <summary>
    /// A 64 bit unsigned integer value. An integer value between 0 and 18 446 744 073 709 551 615.
    /// </summary>
    UInt64 = 9,
    /// <summary>
    /// An IEEE single precision (32 bit) floating point value. An IEEE single precision (32 bit) floating point value.
    /// </summary>
    Float = 10,
    /// <summary>
    /// An IEEE double precision (64 bit) floating point value. An IEEE double precision (64 bit) floating point value.
    /// </summary>
    Double = 11,
    /// <summary>
    /// A sequence of Unicode characters.
    /// </summary>
    String = 12,
    /// <summary>
    /// An instance in time.
    /// </summary>
    DateTime = 13,
    /// <summary>
    /// A 128-bit globally unique identifier.
    /// </summary>
    Guid = 14,
    /// <summary>
    /// A sequence of bytes.
    /// </summary>
    ByteString = 15,
    /// <summary>
    /// An XML element.
    /// </summary>
    XmlElement = 16,
    /// <summary>
    /// An identifier for a node in the address space of an OPC UA Server.
    /// </summary>
    NodeId = 17,
    /// <summary>
    /// A node id that stores the namespace URI instead of the namespace index.
    /// </summary>
    ExpandedNodeId = 18,
    /// <summary>
    /// A structured result code.
    /// </summary>
    StatusCode = 19,
    /// <summary>
    /// A string qualified with a namespace.
    /// </summary>
    QualifiedName = 20,
    /// <summary>
    /// A localized text string with an locale identifier.
    /// </summary>
    LocalizedText = 21,
    /// <summary>
    /// A structure that contains an application specific data type that may not be recognized by the receiver.
    /// </summary>
    ExtensionObject = 22,
    /// <summary>
    /// A data value with an associated quality and time stamp.
    /// </summary>
    DataValue = 23,
    /// <summary>
    /// Any of the other built-in types - a union of all of the types specified above.
    /// </summary>
    Variant = 24,
    /// <summary>
    /// A diagnostic information associated with a result code.
    /// </summary>
    DiagnosticInfo = 25,

  }

}
