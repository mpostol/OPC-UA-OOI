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
    public void WriteByteStringTest()
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
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ArrayLengthOutOfRangeTest()
    {
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
      {
        Assert.IsNotNull(_buffer);
        Int32[] _value = new Int32[byte.MaxValue + 1];
        Variant _variant = new Variant { UATypeInfo = new UATypeInfo(BuiltInType.Int32, 1), Value = _value };
        _buffer.Write(_buffer, _variant);
      }
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    public void ArrayOneDimensionTest()
    {
      WriteArrayOneDimension(0);
      WriteArrayOneDimension(1);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    public void ArrayMultiDimensionTest()
    {
      byte[] _EncodedValue = null;
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
      {
        Assert.IsNotNull(_buffer);
        Int32[] _dimensions = new Int32[] { 2, 2 };
        Int32[,] _array = new Int32[,] { { 0, 1 }, { 2, 3 } };
        Assert.AreEqual<int>(2, _array.Rank);
        Assert.AreEqual<int>(4, _array.Length);
        Assert.AreEqual<int>(_dimensions.Length, _array.Rank);
        UATypeInfo _uaTypeInfo = new UATypeInfo(BuiltInType.Int32, _dimensions.Length) { ArrayDimensions = _dimensions };
        Variant _variant = new Variant { UATypeInfo = _uaTypeInfo, Value = _array };
        _buffer.Write(_buffer, _variant);
        _buffer.Close();
        _EncodedValue = _stream.ToArray();
      }
      Assert.IsNotNull(_EncodedValue);
      Assert.AreEqual<int>(33, _EncodedValue.Length);
    }
    private class Matrix : IMatrix
    {
      public Matrix(int[] dimensions, Array elements, UATypeInfo typeInfo)
      {
        Dimensions = dimensions;
        Elements = elements;
        TypeInfo = typeInfo;
      }
      public int[] Dimensions
      {
        get; private set;
      }
      public Array Elements
      {
        get; private set;
      }
      public UATypeInfo TypeInfo
      {
        get; private set;
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
      byte[] _EncodedGuid = null;
      Guid _Guid = Guid.NewGuid();
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
      {
        Assert.IsNotNull(_buffer);
        _buffer.Write(_Guid);
        _buffer.Close();
        _EncodedGuid = _stream.ToArray();
      }
      Assert.IsNotNull(_EncodedGuid);
      Assert.AreEqual<int>(16, _EncodedGuid.Length);
      Guid _recoveredGuid = new Guid(_EncodedGuid);
      Assert.AreEqual<Guid>(_Guid, _recoveredGuid);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryEncoderImplementationUnitTest")]
    public void VariantGuidTestMethod()
    {
      byte[] _EncodedVGuid = null;
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
      {
        Assert.IsNotNull(_buffer);
        Variant _variant = new Variant { UATypeInfo = new UATypeInfo(BuiltInType.Guid), Value = CommonDefinitions.TestGuid };
        _buffer.Write(_buffer, _variant);
        _buffer.Close();
        _EncodedVGuid = _stream.ToArray();
      }
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
      public void Write(IBinaryEncoder encoder, string value)
      {
        Assert.AreSame(this, encoder);
        _encoder.Write(this, value);
      }
      public void WriteArray<type>(IBinaryEncoder encoder, Array value, Action<type> writeValue, BuiltInType builtInType)
      {
        throw new NotImplementedException();
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
    private static void WriteArrayOneDimension(int rank)
    {
      byte[] _EncodedValue = null;
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryWriter _buffer = new TestBinaryWriter(_stream))
      {
        Assert.IsNotNull(_buffer);
        Int32[] _value = new Int32[] { 0, 1, 2, 3, 4 };
        Variant _variant = new Variant { UATypeInfo = new UATypeInfo(BuiltInType.Int32, rank), Value = _value };
        _buffer.Write(_buffer, _variant);
        _buffer.Close();
        _EncodedValue = _stream.ToArray();
      }
      Assert.IsNotNull(_EncodedValue);
      string _EncodedValueString = String.Join(", ", _EncodedValue);
      string _expectedString = "134, 5, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0";
      Assert.AreEqual<string>(_expectedString, _EncodedValueString);
      Assert.AreEqual<int>(25, _EncodedValue.Length);
      Assert.AreEqual<int>(0, _EncodedValue[5]);
      Assert.AreEqual<int>(1, _EncodedValue[9]);
      Assert.AreEqual<int>(2, _EncodedValue[13]);
      Assert.AreEqual<int>(3, _EncodedValue[17]);
      Assert.AreEqual<int>(4, _EncodedValue[21]);
    }
    #endregion

  }

}
