
using System;
using System.Xml;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Helpers
{
  /// <summary>
  /// Class UABinaryEncoderImplementation - limited implementation of the <see cref="UABinaryEncoder"/> for the testing purpose only.
  /// </summary>
  internal class UABinaryEncoderImplementation : UABinaryEncoder
  {
    public override void WriteByteString(IBinaryEncoder encoder, byte[] value)
    {
      throw new NotImplementedException();
    }
    public override void WriteDataValue(IBinaryEncoder encoder, IDataValue value)
    {
      throw new NotImplementedException();
    }
    public override void WriteDiagnosticInfo(IBinaryEncoder encoder, IDiagnosticInfo value)
    {
      throw new NotImplementedException();
    }
    public override void WriteExpandedNodeId(IBinaryEncoder encoder, IExpandedNodeId value)
    {
      throw new NotImplementedException();
    }
    public override void WriteExtensionObject(IBinaryEncoder encoder, IExtensionObject value)
    {
      throw new NotImplementedException();
    }
    public override void WriteGuid(IBinaryEncoder encoder, Guid value)
    {
      throw new NotImplementedException();
    }
    public override void WriteLocalizedText(IBinaryEncoder encoder, ILocalizedText value)
    {
      throw new NotImplementedException();
    }
    public override void WriteNodeId(IBinaryEncoder encoder, INodeId value)
    {
      throw new NotImplementedException();
    }
    public override void WriteQualifiedName(IBinaryEncoder encoder, IQualifiedName value)
    {
      throw new NotImplementedException();
    }
    public override void WriteStatusCode(IBinaryEncoder encoder, IStatusCode value)
    {
      throw new NotImplementedException();
    }
    public override void WriteVariant(IBinaryEncoder encoder, IVariant value)
    {
      base.WriteVariant(encoder, value);
    }
    public override void WriteXmlElement(IBinaryEncoder encoder, XmlElement value)
    {
      throw new NotImplementedException();
    }

  }
}
