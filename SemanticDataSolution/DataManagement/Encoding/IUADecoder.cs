
using System;
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface IUADecoder - if implemented provides methods to be used to decode OPC UA Built-in types using provided decoder implementing the <see cref="IBinaryDecoder"/> interface.
  /// </summary>
  public interface IUADecoder
  {
    /// <summary>
    /// Reads the <see cref="Guid"/> from UA Binary encoded as a 16-element byte array that contains the value and advances the input message position by 16 bytes.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="Guid"/> decoded from the input message.</returns>
    Guid ReadGuid(IBinaryDecoder decoder);
    /// <summary>
    /// Reads the <see cref="DateTime"/> from UA binary encoded stream of bytes as <see cref="Int64"/> that contains the value and advances the stream position by 8 bytes.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="DateTime "/> decoded from the UA binary stream of bytes.</returns>
    DateTime ReadDateTime(IBinaryDecoder decoder);
    /// <summary>
    /// Reads the string of <see cref="System.Byte"/> from UA Binary encoded as a 16-element byte array that contains the value.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="System.Byte"/> array decoded from the UA binary stream of bytes.</returns>
    byte[] ReadByteString(IBinaryDecoder decoder);
    /// <summary>
    /// Reads the <see cref="string"/> from UA binary encoded stream of bytes encoded as a sequence of UTF8 characters without a null terminator and preceded by the length in bytes.
    /// The length in bytes is encoded as Int32. A value of −1 is used to indicate a ‘null’ string.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="string"/> decoded from the UA binary stream of bytes.</returns>
    string ReadString(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="IDataValue"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="IDataValue"/> decoded from the UA binary stream of bytes.</returns>
    IDataValue ReadDataValue(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="IDiagnosticInfo"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="IDiagnosticInfo"/> decoded from the UA binary stream of bytes.</returns>
    IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="IExpandedNodeId"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="IExpandedNodeId"/> decoded from the UA binary stream of bytes.</returns>
    IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="IExtensionObject"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="IExtensionObject"/> decoded from the UA binary stream of bytes.</returns>
    IExtensionObject ReadExtensionObject(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="ILocalizedText"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="ILocalizedText"/> decoded from the UA binary stream of bytes.</returns>
    ILocalizedText ReadLocalizedText(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="INodeId"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="INodeId"/> decoded from the UA binary stream of bytes.</returns>
    INodeId ReadNodeId(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="IQualifiedName"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="IQualifiedName"/> decoded from the UA binary stream of bytes.</returns>
    IQualifiedName ReadQualifiedName(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="XmlElement"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="XmlElement"/> decoded from the UA binary stream of bytes.</returns>
    XmlElement ReadXmlElement(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="IStatusCode"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="IStatusCode"/> decoded from the UA binary stream of bytes.</returns>
    IStatusCode ReadStatusCode(IBinaryDecoder decoder);
    /// <summary>
    /// Reads an instance of <see cref="IVariant"/> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder"/> to be used to read form the stream.</param>
    /// <returns>The <see cref="IVariant"/> decoded from the UA binary stream of bytes.</returns>
    IVariant ReadVariant(IBinaryDecoder decoder);

  }
}