
using System;
using System.Xml;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.Encoding
{
  /// <summary>
  /// Class UABinaryDecoderImplementation - limited implementation of the <see cref="UABinaryDecoder"/> for the testing purpose only.
  /// </summary>
  internal class UABinaryDecoderImplementation : UABinaryDecoder
  {
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.IDataValue" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.IDataValue" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IDataValue ReadDataValue(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.IDiagnosticInfo" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.IDiagnosticInfo" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.IExpandedNodeId" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.IExpandedNodeId" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.IExtensionObject" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.IExtensionObject" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IExtensionObject ReadExtensionObject(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.ILocalizedText" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.ILocalizedText" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override ILocalizedText ReadLocalizedText(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.INodeId" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.INodeId" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override INodeId ReadNodeId(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.IQualifiedName" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.IQualifiedName" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IQualifiedName ReadQualifiedName(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="UAOOI.Networking.SemanticData.Encoding.IStatusCode" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="UAOOI.Networking.SemanticData.Encoding.IStatusCode" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IStatusCode ReadStatusCode(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="System.Xml.XmlElement" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="UAOOI.Networking.SemanticData.Encoding.IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="System.Xml.XmlElement" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public override XmlElement ReadXmlElement(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
  }
}
