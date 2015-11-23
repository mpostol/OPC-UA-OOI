
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface IUADecoder - if implemented provides methods to be used to decode OPC UA Built-in types.
  /// </summary>
  public interface IUADecoder
  {

    byte[] ReadBytes(IBinaryDecoder encoder);
    void ReadByteString(IBinaryDecoder messageReaderBase);
    IDataValue ReadDataValue(IBinaryDecoder encoder);
    IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder messageReaderBase);
    IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder encoder);
    IExtensionObject ReadExtensionObject(IBinaryDecoder encoder);
    ILocalizedText ReadLocalizedText(IBinaryDecoder encoder);
    INodeId ReadNodeId(IBinaryDecoder encoder);
    IQualifiedName ReadQualifiedName(IBinaryDecoder encoder);
    XmlElement ReadXmlElement(IBinaryDecoder encoder);
    IStatusCode ReadStatusCode(IBinaryDecoder encoder);
    IVariant ReadVariant(IBinaryDecoder encoder);

  }
}