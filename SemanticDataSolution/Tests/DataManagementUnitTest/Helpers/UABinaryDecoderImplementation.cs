
using System;
using System.Xml;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Helpers
{

  /// <summary>
  /// Class UABinaryDecoderImplementation - limited implementation of the <see cref="UABinaryDecoder"/> for the testing purpose only.
  /// </summary>
  internal class UABinaryDecoderImplementation : UABinaryDecoder
  {

    #region Encoding.UABinaryDecoder
    public override byte[] ReadByteString(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override IDataValue ReadDataValue(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override IExtensionObject ReadExtensionObject(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override ILocalizedText ReadLocalizedText(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override INodeId ReadNodeId(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override IQualifiedName ReadQualifiedName(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override IStatusCode ReadStatusCode(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    public override XmlElement ReadXmlElement(IBinaryDecoder decoder)
    {
      throw new NotImplementedException();
    }
    #endregion

  }

}
