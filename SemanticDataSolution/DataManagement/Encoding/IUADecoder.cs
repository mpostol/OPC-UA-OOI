
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface IUADecoder - if implemented provides methods to be used to decode OPC UA Built-in types.
  /// </summary>
  public interface IUADecoder
  {

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