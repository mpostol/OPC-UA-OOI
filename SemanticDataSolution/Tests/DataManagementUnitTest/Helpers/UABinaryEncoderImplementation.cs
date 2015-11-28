
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
    public override void Write(IBinaryEncoder encoder, byte[] value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, IDataValue value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, IDiagnosticInfo value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, IExpandedNodeId value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, IExtensionObject value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, ILocalizedText value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, INodeId value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, IQualifiedName value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, IStatusCode value)
    {
      throw new NotImplementedException();
    }
    public override void Write(IBinaryEncoder encoder, IVariant value)
    {
      base.Write(encoder, value);
    }
    public override void Write(IBinaryEncoder encoder, XmlElement value)
    {
      throw new NotImplementedException();
    }

  }
}
