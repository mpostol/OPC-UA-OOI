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
    [ExpectedException(typeof(NotImplementedException))]
    public void WriteByteStringTestMethod()
    {
      using (var _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
        _buffer.WriteByteString(_buffer, null);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    [ExpectedException(typeof(NotImplementedException))]
    public void WriteDataValueTestMethod()
    {
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
        _buffer.WriteDataValue(_buffer, null);
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
      _buffer.WriteGuid(_Guid);
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
      _buffer.WriteVariant(_buffer, _variant);
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
        byte[] _EncodedVGuid = null;
        Variant _variant = new Variant { UATypeInfo = new UATypeInfo(BuiltInType.DateTime), Value = _dtx.dateTime };
        _buffer.WriteVariant(_buffer, _variant);
        _buffer.Close();
        _EncodedVGuid = _stream.ToArray();
        Assert.IsNotNull(_EncodedVGuid);
        Assert.AreEqual<int>(9, _EncodedVGuid.Length);
        Assert.AreEqual<byte>((byte)BuiltInType.DateTime, _EncodedVGuid[0]);
        CollectionAssert.AreEqual(_dtx.encoding, _EncodedVGuid.ToList<byte>());
      }
    }
    #endregion

    #region testing instrumentation
    private class TestBinaryWriter : BinaryWriter, Encoding.IBinaryEncoder, IUAEncoder
    {
      public TestBinaryWriter(Stream output) : base(output)
      {

      }
      public void WriteBoolean(bool value)
      {
        Write(value);
      }
      public void WriteByte(byte value)
      {
        Write(value);
      }
      public void WriteBytes(byte[] value)
      {
        Write(value);
      }
      public void WriteDouble(double value)
      {
        Write(value);
      }
      public void WriteGuid(Guid value)
      {
        _encoder.WriteGuid(this, value);
      }
      public void WriteInt16(short value)
      {
        Write(value);
      }
      public void WriteInt32(int value)
      {
        Write(value);
      }
      public void WriteInt64(long value)
      {
        Write(value);
      }
      public void WriteSByte(sbyte value)
      {
        Write(value);
      }
      public void WriteSingle(float value)
      {
        Write(value);
      }
      public void WriteString(string value)
      {
        Write(value);
      }
      public void WriteUInt16(ushort value)
      {
        Write(value);
      }
      public void WriteUInt32(uint value)
      {
        Write(value);
      }
      public void WriteUInt64(ulong value)
      {
        Write(value);
      }
      public void WriteDateTime(IBinaryEncoder encoder, DateTime value)
      {
        _encoder.WriteDateTime(this, value);
      }
      public void WriteByteString(IBinaryEncoder encoder, byte[] value)
      {
        _encoder.WriteByteString(this, value);
      }
      public void WriteDataValue(IBinaryEncoder encoder, IDataValue value)
      {
        _encoder.WriteDataValue(this, value);
      }
      public void WriteDiagnosticInfo(IBinaryEncoder encoder, IDiagnosticInfo value)
      {
        _encoder.WriteDiagnosticInfo(this, value);
      }
      public void WriteExpandedNodeId(IBinaryEncoder encoder, IExpandedNodeId value)
      {
        _encoder.WriteExpandedNodeId(this, value);
      }
      public void WriteExtensionObject(IBinaryEncoder encoder, IExtensionObject value)
      {
        _encoder.WriteExtensionObject(this, value);
      }
      public void WriteLocalizedText(IBinaryEncoder encoder, ILocalizedText value)
      {
        Assert.AreSame(this, encoder);
        _encoder.WriteLocalizedText(this, value);
      }
      public void WriteNodeId(IBinaryEncoder encoder, INodeId value)
      {
        Assert.AreSame(this, encoder);
        _encoder.WriteNodeId(this, value);
      }
      public void WriteQualifiedName(IBinaryEncoder encoder, IQualifiedName value)
      {
        Assert.AreSame(this, encoder);
        _encoder.WriteQualifiedName(this, value);
      }
      public void WriteXmlElement(IBinaryEncoder encoder, XmlElement value)
      {
        Assert.AreSame(this, encoder);
        _encoder.WriteXmlElement(this, value);
      }
      public void WriteStatusCode(IBinaryEncoder encoder, IStatusCode value)
      {
        Assert.AreSame(this, encoder);
        _encoder.WriteStatusCode(this, value);
      }
      public void WriteVariant(IBinaryEncoder encoder, IVariant value)
      {
        Assert.AreSame(this, encoder);
        _encoder.WriteVariant(this, value);
      }
      public void WriteGuid(IBinaryEncoder encoder, Guid value)
      {
        Assert.AreSame(this, encoder);
        _encoder.WriteGuid(this, value);
      }
      IUAEncoder _encoder = new Helpers.UABinaryEncoderImplementation();
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
