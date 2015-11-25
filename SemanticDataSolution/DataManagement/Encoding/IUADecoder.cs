
using System;
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface IUADecoder - if implemented provides methods to be used to decode OPC UA Built-in types.
  /// </summary>
  public interface IUADecoder
  {
    /// <summary>
    /// Reads the <see cref="Guid"/> from UA Binary encoded as a 16-element byte array that contains the value and advances the input message position by 16 bytes.
    /// </summary>
    /// <returns>The <see cref="Guid"/> decoded from the input message.</returns>
    Guid ReadGuid(IBinaryDecoder decoder);
    DateTime ReadDateTime(IBinaryDecoder decoder);
    byte[] ReadByteString(IBinaryDecoder decoder);
    IDataValue ReadDataValue(IBinaryDecoder decoder);
    IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder);
    IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder);
    IExtensionObject ReadExtensionObject(IBinaryDecoder decoder);
    ILocalizedText ReadLocalizedText(IBinaryDecoder decoder);
    INodeId ReadNodeId(IBinaryDecoder decoder);
    IQualifiedName ReadQualifiedName(IBinaryDecoder decoder);
    XmlElement ReadXmlElement(IBinaryDecoder decoder);
    IStatusCode ReadStatusCode(IBinaryDecoder decoder);
    IVariant ReadVariant(IBinaryDecoder decoder);

  }
}