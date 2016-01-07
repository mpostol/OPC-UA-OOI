
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Class UABinaryDecoder - basic implementation of the <see cref="IUADecoder"/> that provides methods to be used to decode selected set of the OPC UA Built-in types.
  /// </summary>
  /// <remarks>
  /// <note>
  /// It is expected that full featured implementation of this call will be injected by the user by this library. 
  /// The library supports decoding only primitive types.
  /// </note>
  /// </remarks>
  public abstract class UABinaryDecoder : IUADecoder
  {

    #region IUADecoder - supported types
    /// <summary>
    /// Reads an instance of <see cref="IVariant" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="IVariant" /> decoded from the UA binary stream of bytes.</returns>
    public IVariant ReadVariant(IBinaryDecoder decoder)
    {
      byte encodingByte = decoder.ReadByte(); //Read the EncodingMask
      if (encodingByte == 0x0)
        return null;
      Variant value = null;
      BuiltInType builtInType = (BuiltInType)(encodingByte & (byte)VariantEncodingMask.TypeIdMask);
      if ((encodingByte & (byte)VariantEncodingMask.IsArray) == 0)
        value = ReadValue(decoder, builtInType);
      else
      {
        Array array = DecodeArray(decoder, builtInType);
        List<Int32> _dimensions = null;
        if ((encodingByte & (byte)VariantEncodingMask.ArrayDimensionsPresents) != 0)
          _dimensions = ReadDimensions(decoder);
        if (_dimensions != null && _dimensions.Count > 0)
          value = new Variant(new Matrix(array, builtInType, _dimensions.ToArray()));
        else
          value = new Variant(new Matrix(array, builtInType));
      }
      return value;
    }
    /// <summary>
    /// Reads an array of the specified type <paramref name="uaTypeInfo" /> and wraps it in the <see cref="IMatrix" /> object.
    /// </summary>
    /// <typeparam name="type">The type of the <see cref="IMatrix.Elements" /> element.</typeparam>
    /// <param name="decoder">The decoder to be used to recover the array from the binary stream.</param>
    /// <param name="readValue">This delegate encapsulates binary decoding functionality of the array element.</param>
    /// <param name="uaTypeInfo"><see cref="BuiltInType" /> of the array to be decoded used in case the array is multidimensional and must be decoded as the variant.</param>
    /// <returns>An instance of <see cref="IMatrix" /> capturing the an array recovered from the message.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Encountered unexpected array rank.</exception>
    public IMatrix ReadArray<type>(IBinaryDecoder decoder, Func<type> readValue, UATypeInfo uaTypeInfo)
    {
      IMatrix _ret = null;
      if (uaTypeInfo.ValueRank == 1)
        _ret = new Matrix(DecodeArray<type>(decoder.ReadInt32, readValue), uaTypeInfo.BuiltInType);
      else if (uaTypeInfo.ValueRank > 1)
      {
        IVariant _variant = ReadVariant(decoder);
        _ret = (IMatrix)_variant.Value;
      }
      else
        throw new ArgumentOutOfRangeException($"{nameof(uaTypeInfo.ValueRank)}", $"{nameof(uaTypeInfo)} must represent an array and {nameof(uaTypeInfo.ValueRank)} >= 1 ");
      return _ret;
    }
    /// <summary>
    /// Reads the <see cref="Guid" /> from UA Binary encoded as a 16-element byte array that contains the value and advances the input message position by 16 bytes.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="Guid" /> decoded from the input message.</returns>
    public Guid ReadGuid(IBinaryDecoder decoder)
    {
      return CommonDefinitions.ReadGuid(decoder);
    }
    /// <summary>
    /// Reads the <see cref="DateTime" /> from UA binary encoded stream of bytes as <see cref="Int64" /> that contains the value and advances the stream position by 8 bytes.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="DateTime " /> decoded from the UA binary stream of bytes.</returns>
    public DateTime ReadDateTime(IBinaryDecoder decoder)
    {
      return CommonDefinitions.GetUADateTime(decoder.ReadInt64());
    }
    /// <summary>
    /// Reads the string od bytes from UA Binary encoded as a 16-element byte array that contains the value.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="System.Byte" /> array decoded from the UA binary stream of bytes.</returns>
    public byte[] ReadByteString(IBinaryDecoder decoder)
    {
      Int32 _length = decoder.ReadInt32();
      if (_length < 0)
        return null;
      return decoder.ReadBytes(_length);
    }
    /// <summary>
    /// Reads the <see cref="string" /> from UA binary encoded stream of bytes encoded as a sequence of UTF8 characters without a null terminator and preceded by the length in bytes.
    /// The length in bytes is encoded as Int32. A value of −1 is used to indicate a ‘null’ string.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="string" /> decoded from the UA binary stream of bytes.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public string ReadString(IBinaryDecoder decoder)
    {
      int length = decoder.ReadInt32();
      if (length == -1)
        return null;
      byte[] bytes = decoder.ReadBytes(length);
      return new UTF8Encoding().GetString(bytes, 0, bytes.Length);
    }
    #endregion

    #region IUADecoder - unsupported types
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="IDataValue" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="IDataValue" /> decoded from the UA binary stream of bytes.</returns>
    public abstract IDataValue ReadDataValue(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="IDiagnosticInfo" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="IDiagnosticInfo" /> decoded from the UA binary stream of bytes.</returns>
    public abstract IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="IExpandedNodeId" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="IExpandedNodeId" /> decoded from the UA binary stream of bytes.</returns>
    public abstract IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="IExtensionObject" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="IExtensionObject" /> decoded from the UA binary stream of bytes.</returns>
    public abstract IExtensionObject ReadExtensionObject(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="ILocalizedText" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="ILocalizedText" /> decoded from the UA binary stream of bytes.</returns>
    public abstract ILocalizedText ReadLocalizedText(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="INodeId" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="INodeId" /> decoded from the UA binary stream of bytes.</returns>
    public abstract INodeId ReadNodeId(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="IQualifiedName" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="IQualifiedName" /> decoded from the UA binary stream of bytes.</returns>
    public abstract IQualifiedName ReadQualifiedName(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="XmlElement" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="XmlElement" /> decoded from the UA binary stream of bytes.</returns>
    public abstract XmlElement ReadXmlElement(IBinaryDecoder decoder);
    /// <summary>
    /// If implemented by a derived class reads an instance of <see cref="IStatusCode" /> from UA Binary encoded stream.
    /// </summary>
    /// <param name="decoder">The decoder <see cref="IBinaryDecoder" /> to be used to read form the stream.</param>
    /// <returns>The <see cref="IStatusCode" /> decoded from the UA binary stream of bytes.</returns>
    public abstract IStatusCode ReadStatusCode(IBinaryDecoder decoder);
    #endregion

    #region private
    //types
    private class Variant : IVariant
    {
      internal Variant(object value, UATypeInfo type)
      {
        Value = value;
        UATypeInfo = type;
      }
      public Variant(object value, BuiltInType type) : this(value, new UATypeInfo(type)) { }
      public Variant(Matrix value) : this(value, value.TypeInfo) { }
      public object Value
      {
        get; private set;
      }
      public UATypeInfo UATypeInfo
      {
        get; private set;
      }

    }
    private class Matrix : IMatrix
    {

      /// <summary>
      /// Initializes a new instance of the <see cref="Matrix"/> class with a multidimensional array.
      /// </summary>
      /// <param name="array">The array.</param>
      /// <param name="builtInType">Type of the built in.</param>
      public Matrix(Array array, BuiltInType builtInType) : this(array, builtInType, array.Length) { }
      /// <summary>
      /// Initializes a new instance of the <see cref="Matrix"/> class with a one dimensional array and a list of dimensions.
      /// </summary>
      /// <param name="array">The array.</param>
      /// <param name="builtInType">Type of the built in.</param>
      /// <param name="dimensions">The value.</param>
      public Matrix(Array array, BuiltInType builtInType, params int[] dimensions)
      {
        Elements = array;
        Dimensions = dimensions;
        int _length = 1;
        for (int _ix = 0; _ix < dimensions.Length; _ix++)
          _length *= dimensions[_ix];
        if (_length != array.Length)
          throw new ArgumentException("The number of elements in the array does not match the dimensions.");
        TypeInfo = new UATypeInfo(builtInType, Dimensions.Length);
      }
      #region IMatrix
      public int[] Dimensions
      {
        get; private set;
      }
      public Array Elements
      {
        get; private set;
      }
      public UATypeInfo TypeInfo { get; private set; }
      #endregion

    }
    //vars
    /// <summary>
    /// The maximum array length - could be used to apply license volume limits.
    /// </summary>
    private int MaxArrayLength = 2;
    //methods
    private Variant ReadValue(IBinaryDecoder encoder, BuiltInType encodingByte)
    {
      switch (encodingByte)
      {
        case BuiltInType.Boolean:
          return new Variant(encoder.ReadBoolean(), encodingByte);
        case BuiltInType.SByte:
          return new Variant(encoder.ReadSByte(), encodingByte);
        case BuiltInType.Byte:
          return new Variant(encoder.ReadByte(), encodingByte);
        case BuiltInType.Int16:
          return new Variant(encoder.ReadInt16(), encodingByte);
        case BuiltInType.UInt16:
          return new Variant(encoder.ReadUInt16(), encodingByte);
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          return new Variant(encoder.ReadInt32(), encodingByte);
        case BuiltInType.UInt32:
          return new Variant(encoder.ReadUInt32(), encodingByte);
        case BuiltInType.Int64:
          return new Variant(encoder.ReadInt64(), encodingByte);
        case BuiltInType.UInt64:
          return new Variant(encoder.ReadUInt64(), encodingByte);
        case BuiltInType.Float:
          return new Variant(encoder.ReadSingle(), encodingByte);
        case BuiltInType.Double:
          return new Variant(encoder.ReadDouble(), encodingByte);
        case BuiltInType.String:
          return new Variant(ReadString(encoder), encodingByte);
        case BuiltInType.DateTime:
          return new Variant(ReadDateTime(encoder), encodingByte);
        case BuiltInType.Guid:
          return new Variant(ReadGuid(encoder), encodingByte);
        case BuiltInType.ByteString:
          return new Variant(ReadByteString(encoder), encodingByte);
        case BuiltInType.XmlElement:
          return new Variant(ReadXmlElement(encoder), encodingByte);
        case BuiltInType.NodeId:
          return new Variant(ReadNodeId(encoder), encodingByte);
        case BuiltInType.ExpandedNodeId:
          return new Variant(ReadExpandedNodeId(encoder), encodingByte);
        case BuiltInType.StatusCode:
          return new Variant(ReadStatusCode(encoder), encodingByte);
        case BuiltInType.QualifiedName:
          return new Variant(ReadQualifiedName(encoder), encodingByte);
        case BuiltInType.LocalizedText:
          return new Variant(ReadLocalizedText(encoder), encodingByte);
        case BuiltInType.ExtensionObject:
          return new Variant(ReadExtensionObject(encoder), encodingByte);
        case BuiltInType.DataValue:
          return new Variant(ReadDataValue(encoder), encodingByte);
        default:
          throw new ArgumentOutOfRangeException($"Cannot decode unknown type in Variant object (0x{encodingByte:X}).");
      }
    }
    private Array DecodeArray(IBinaryDecoder decoder, BuiltInType builtInType)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          return DecodeArray<bool>(decoder.ReadInt32, decoder.ReadBoolean);
        case BuiltInType.SByte:
          return DecodeArray<sbyte>(decoder.ReadInt32, decoder.ReadSByte);
        case BuiltInType.Byte:
          return DecodeArray<byte>(decoder.ReadInt32, decoder.ReadByte);
        case BuiltInType.Int16:
          return DecodeArray<short>(decoder.ReadInt32, decoder.ReadInt16);
        case BuiltInType.UInt16:
          return DecodeArray<ushort>(decoder.ReadInt32, decoder.ReadUInt16);
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          return DecodeArray<int>(decoder.ReadInt32, decoder.ReadInt32);
        case BuiltInType.UInt32:
          return DecodeArray<uint>(decoder.ReadInt32, decoder.ReadUInt32);
        case BuiltInType.Int64:
          return DecodeArray<long>(decoder.ReadInt32, decoder.ReadInt64);
        case BuiltInType.UInt64:
          return DecodeArray<ulong>(decoder.ReadInt32, decoder.ReadUInt64);
        case BuiltInType.Float:
          return DecodeArray<float>(decoder.ReadInt32, decoder.ReadSingle);
        case BuiltInType.Double:
          return DecodeArray<double>(decoder.ReadInt32, decoder.ReadDouble);
        case BuiltInType.String:
          return DecodeArray<string>(decoder.ReadInt32, () => ReadString(decoder));
        case BuiltInType.DateTime:
          return DecodeArray<DateTime>(decoder.ReadInt32, () => ReadDateTime(decoder));
        case BuiltInType.Guid:
          return DecodeArray<Guid>(decoder.ReadInt32, decoder.ReadGuid);
        case BuiltInType.ByteString:
          return DecodeArray<byte>(decoder.ReadInt32, decoder.ReadByte);
        case BuiltInType.XmlElement:
          return DecodeArray<XmlElement>(decoder.ReadInt32, () => ReadXmlElement(decoder));
        case BuiltInType.NodeId:
          return DecodeArray<INodeId>(decoder.ReadInt32, () => ReadNodeId(decoder));
        case BuiltInType.ExpandedNodeId:
          return DecodeArray<IExpandedNodeId>(decoder.ReadInt32, () => ReadExpandedNodeId(decoder));
        case BuiltInType.StatusCode:
          return DecodeArray<IStatusCode>(decoder.ReadInt32, () => ReadStatusCode(decoder));
        case BuiltInType.QualifiedName:
          return DecodeArray<IQualifiedName>(decoder.ReadInt32, () => ReadQualifiedName(decoder));
        case BuiltInType.LocalizedText:
          return DecodeArray<ILocalizedText>(decoder.ReadInt32, () => ReadLocalizedText(decoder));
        case BuiltInType.ExtensionObject:
          return DecodeArray<IExtensionObject>(decoder.ReadInt32, () => ReadExtensionObject(decoder));
        case BuiltInType.DataValue:
          return DecodeArray<IDataValue>(decoder.ReadInt32, () => ReadDataValue(decoder));
        case BuiltInType.Variant:
          return DecodeArray<IVariant>(decoder.ReadInt32, () => ReadVariant(decoder));
        default:
          throw new ArgumentOutOfRangeException($"Cannot decode unknown type in Variant object (0x{(int)builtInType:X2}).");
      }
    }
    private Array DecodeArray<type>(Func<Int32> readInt32, Func<type> readValue)
    {
      int length = readInt32();
      if (length < 0)
        return null;
      type[] values = new type[length];
      for (int ii = 0; ii < values.Length; ii++)
        values[ii] = readValue();
      Array array = values;
      return array;
    }
    private List<int> ReadDimensions(IBinaryDecoder encoder)
    {
      int length = encoder.ReadInt32();
      if (length < 0)
        return null;
      if (MaxArrayLength > 0 && MaxArrayLength < length)
        throw new ArgumentOutOfRangeException(nameof(MaxArrayLength), $"Unsupported array length {length}");
      List<Int32> values = new List<Int32>(length);
      for (int ii = 0; ii < length; ii++)
        values.Add(encoder.ReadInt32());
      return values;
    }
    #endregion

  }
}
