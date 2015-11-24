
using System;
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface IUAEncoder - if implemented provides methods to be used to encode OPC UA Built-in types.
  /// </summary>
  public interface IUAEncoder
  {

    void WriteDateTime(IBinaryEncoder encoder, DateTime value);
    void WriteByteString(IBinaryEncoder encoder, byte[]  value);
    void WriteDataValue(IBinaryEncoder encoder, IDataValue value);
    void WriteDiagnosticInfo(IBinaryEncoder encoder, IDiagnosticInfo  value);
    void WriteExpandedNodeId(IBinaryEncoder encoder, IExpandedNodeId value);
    void WriteExtensionObject(IBinaryEncoder encoder, IExtensionObject value);
    void WriteLocalizedText(IBinaryEncoder encoder, ILocalizedText value);
    void WriteNodeId(IBinaryEncoder encoder, INodeId value);
    void WriteQualifiedName(IBinaryEncoder encoder, IQualifiedName value);
    void WriteXmlElement(IBinaryEncoder encoder, XmlElement value);
    void WriteStatusCode(IBinaryEncoder encoder, IStatusCode value);
    void WriteVariant(IBinaryEncoder encoder, IVariant  value);

  }

}