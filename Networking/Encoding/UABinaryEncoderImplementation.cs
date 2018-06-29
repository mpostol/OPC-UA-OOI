
using System;
using System.Xml;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.Encoding
{
  /// <summary>
  /// Class UABinaryEncoderImplementation - limited implementation of the <see cref="UABinaryEncoder"/> for the testing purpose only.
  /// </summary>
  internal class UABinaryEncoderImplementation : UABinaryEncoder
  {
    /// <summary>
    /// Writes <see cref="UAOOI.Networking.SemanticData.Encoding.IDataValue" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, IDataValue value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="UAOOI.Networking.SemanticData.Encoding.IDiagnosticInfo" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, IDiagnosticInfo value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="UAOOI.Networking.SemanticData.Encoding.IExpandedNodeId" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, IExpandedNodeId value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="UAOOI.Networking.SemanticData.Encoding.ILocalizedText" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, IExtensionObject value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="System.DateTime" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, ILocalizedText value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="UAOOI.Networking.SemanticData.Encoding.INodeId" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, INodeId value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="UAOOI.Networking.SemanticData.Encoding.IQualifiedName" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, IQualifiedName value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="UAOOI.Networking.SemanticData.Encoding.IStatusCode" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, IStatusCode value)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Writes <see cref="System.Xml.XmlElement" /> using the provided encoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(IBinaryEncoder encoder, XmlElement value)
    {
      throw new NotImplementedException();
    }

  }

}
