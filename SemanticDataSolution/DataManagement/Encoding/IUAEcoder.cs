
using System;
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface IUAEncoder - if implemented provides methods to be used to encode OPC UA Built-in types.
  /// </summary>
  public interface IUAEncoder
  {

    void Write(IBinaryEncoder encoder, DateTime value);
    void Write(IBinaryEncoder encoder, byte[]  value);
    void Write(IBinaryEncoder encoder, IDataValue value);
    void Write(IBinaryEncoder encoder, IDiagnosticInfo  value);
    void Write(IBinaryEncoder encoder, IExpandedNodeId value);
    void Write(IBinaryEncoder encoder, IExtensionObject value);
    void Write(IBinaryEncoder encoder, ILocalizedText value);
    void Write(IBinaryEncoder encoder, INodeId value);
    void Write(IBinaryEncoder encoder, IQualifiedName value);
    void Write(IBinaryEncoder encoder, XmlElement value);
    void Write(IBinaryEncoder encoder, IStatusCode value);
    void Write(IBinaryEncoder encoder, IVariant  value);
    void Write(IBinaryEncoder encoder, Guid value);

  }

}