using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;
using System.Xml;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using System.Linq;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class UABinaryEncoderImplementationUnitTest
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    public void WriteByteStringTestMethod()
    {
      using (var _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
      {
        _buffer.Write(_buffer, new byte[10]);
        _buffer.Close();
        byte[] _Encoded = _stream.ToArray();
        Assert.AreEqual<int>(14, _Encoded.Length);
      }
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    [ExpectedException(typeof(NotImplementedException))]
    public void WriteDataValueTestMethod()
    {
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
        _buffer.Write(_buffer, (IDataValue)null);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    public void GuidTestMethod()
    {
      MemoryStream _stream = new MemoryStream();
      TestBinaryWriter _buffer = new TestBinaryWriter(_stream);
      Assert.IsNotNull(_buffer);
      byte[] _EncodedGuid = null;
      Guid _Guid;
      _Guid = Guid.NewGuid();
      _buffer.Write(_Guid);
      _buffer.Close();
      _EncodedGuid = _stream.ToArray();
      Assert.IsNotNull(_EncodedGuid);
      Assert.AreEqual<int>(16, _EncodedGuid.Length);
      Guid _recoveredGuid = new Guid(_EncodedGuid);
      Assert.AreEqual<Guid>(_Guid, _recoveredGuid);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    public void VariantGuidTestMethod()
    {
      MemoryStream _stream = new MemoryStream();
      TestBinaryWriter _buffer = new TestBinaryWriter(_stream);
      Assert.IsNotNull(_buffer);
      byte[] _EncodedVGuid = null;
      Variant _variant = new Variant { UATypeInfo = new UATypeInfo(BuiltInType.Guid), Value = CommonDefinitions.TestGuid };
      _buffer.Write(_buffer, _variant);
      _buffer.Close();
      _EncodedVGuid = _stream.ToArray();
      Assert.IsNotNull(_EncodedVGuid);
      Assert.AreEqual<int>(17, _EncodedVGuid.Length);
      ArraySegment<byte> _segment = new ArraySegment<byte>(_EncodedVGuid, 1, 16);
      Assert.AreEqual<byte>((byte)BuiltInType.Guid, _EncodedVGuid[0]);
      CollectionAssert.AreEqual(CommonDefinitions.TestGuid.ToByteArray(), _segment.ToList<byte>());
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    public void VariantDateTimeTestMethod()
    {
      foreach (CommonDefinitions.DateTimeVariantEncoding _dtx in CommonDefinitions.DateTimeTestingValues)
      {
        MemoryStream _stream = new MemoryStream();
        TestBinaryWriter _buffer = new TestBinaryWriter(_stream);
        Assert.IsNotNull(_buffer);
        Variant _variant = new Variant { UATypeInfo = new UATypeInfo(BuiltInType.DateTime), Value = _dtx.dateTime };
        _buffer.Write(_buffer, _variant);
        _buffer.Close();
        byte[] _EncodedVariant = _stream.ToArray();
        Assert.IsNotNull(_EncodedVariant);
        Assert.AreEqual<int>(9, _EncodedVariant.Length);
        Assert.AreEqual<byte>((byte)BuiltInType.DateTime, _EncodedVariant[0]);
        CollectionAssert.AreEqual(_dtx.encoding, _EncodedVariant.ToList<byte>());
      }
    }
    #endregion

    #region testing instrumentation
    private class TestBinaryWriter : BinaryWriter, IBinaryEncoder, IUAEncoder
    {

      #region creator
      public TestBinaryWriter(Stream output) : base(output) { }
      #endregion

      #region IBinaryEncoder
      public void Write(Guid value)
      {
        _encoder.Write(this, value);
      }
      public void Write(DateTime value)
      {
        throw new NotImplementedException();
      }
      #endregion

      #region IUAEncoder
      public void Write(IBinaryEncoder encoder, DateTime value)
      {
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, byte[] value)
      {
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, IDataValue value)
      {
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, IDiagnosticInfo value)
      {
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, IExpandedNodeId value)
      {
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, IExtensionObject value)
      {
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, ILocalizedText value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, INodeId value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, IQualifiedName value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, XmlElement value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, IStatusCode value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, IVariant value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      public void Write(IBinaryEncoder encoder, Guid value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      #endregion

      #region private 
      IUAEncoder _encoder = new Helpers.UABinaryEncoderImplementation();
      #endregion

    }
    private class Variant : IVariant
    {
      public UATypeInfo UATypeInfo
      {
        get; set;
      }

      public object Value
      {
        get; set;
      }
    }
    #endregion

  }

}
