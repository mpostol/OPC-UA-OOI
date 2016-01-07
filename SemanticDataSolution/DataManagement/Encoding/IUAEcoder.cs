
using System;
using System.Xml;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface IUAEncoder - if implemented provides methods to be used to encode OPC UA Built-in types.
  /// </summary>
  public interface IUAEncoder
  {

    /// <summary>
    /// Writes <see cref="DateTime"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, DateTime value);
    /// <summary>
    /// Writes the <c>ByteString</c> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, byte[] value);
    /// <summary>
    /// Writes <see cref="IDataValue"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, IDataValue value);
    /// <summary>
    /// Writes <see cref="IDiagnosticInfo"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, IDiagnosticInfo value);
    /// <summary>
    /// Writes <see cref="IExpandedNodeId"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, IExpandedNodeId value);
    /// <summary>
    /// Writes <see cref="ILocalizedText"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, IExtensionObject value);
    /// <summary>
    /// Writes <see cref="DateTime"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, ILocalizedText value);
    /// <summary>
    /// Writes <see cref="INodeId"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, INodeId value);
    /// <summary>
    /// Writes <see cref="IQualifiedName"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, IQualifiedName value);
    /// <summary>
    /// Writes <see cref="XmlElement"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, XmlElement value);
    /// <summary>
    /// Writes <see cref="IStatusCode"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, IStatusCode value);
    /// <summary>
    /// Writes <see cref="IVariant"/> using the provided encoder <see cref="IBinaryEncoder"/>.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder"/> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    void Write(IBinaryEncoder encoder, IVariant value);
    /// <summary>
    /// Writes a <see cref="Guid" /> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="encoder">The encoder <see cref="IBinaryEncoder"/> to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="Guid"/>.</param>
    void Write(IBinaryEncoder encoder, Guid value);
    /// <summary>
    /// Encodes the <see cref="string"/> as a sequence of UTF8 characters without a null terminator and preceded by the length in bytes.
    /// The length in bytes is encoded as Int32. A value of −1 is used to indicate a ‘null’ string.
    /// </summary>
    /// <param name="encoder">The encoder <see cref="IBinaryEncoder"/> to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="Guid"/>.</param>
    void Write(IBinaryEncoder encoder, string value);
    /// <summary>
    /// Encodes the <see cref="Array" /> directly if the array is one dimensional or as <see cref="UANetworking.Configuration.Serialization.BuiltInType.Variant" /> otherwise.
    /// </summary>
    /// <typeparam name="type">The type of the array element type.</typeparam>
    /// <param name="encoder">The encoder <see cref="IBinaryEncoder" /> to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="Array" />.</param>
    /// <param name="writeValue">This delegate encapsulates binary encoding functionality of the array element.</param>
    /// <param name="builtInType"><see cref="BuiltInType"/> of the array item to be encoded in case the array is multidimensional and must be encoded as the variant.</param>
    void WriteArray<type>(IBinaryEncoder encoder, Array value, Action<type> writeValue, BuiltInType builtInType);

  }

}
