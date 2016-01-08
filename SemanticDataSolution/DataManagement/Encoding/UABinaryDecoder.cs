
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
        Array array = DecodeArray(decoder, builtInType, ((encodingByte & (byte)VariantEncodingMask.ArrayDimensionsPresents) != 0));
        value = new Variant(array, builtInType);
      }
      return value;
    }
    /// <summary>
    /// Reads an array of the specified type <paramref name="uaTypeInfo" /> and wraps it in the <see cref="IMatrix" /> object.
    /// </summary>
    /// <typeparam name="type">The type of the <see cref="IMatrix.Elements" /> element.</typeparam>
    /// <param name="decoder">The decoder to be used to recover the array from the binary stream.</param>
    /// <param name="readValue">This delegate encapsulates binary decoding functionality of the array element.</param>
    /// <param name="arrayDimensionsPresents">if set to <c>true</c> the rank of the array is greater than 1 and dimensions are present in the encoded stream.</param>
    /// <returns>An instance of <see cref="IMatrix" /> capturing the an array recovered from the message.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Encountered unexpected array rank.</exception>
    public Array ReadArray<type>(IBinaryDecoder decoder, Func<type> readValue, bool arrayDimensionsPresents)
    {
      Array _ret = null;
      if (!arrayDimensionsPresents)
        _ret = DecodeArray<type>(decoder, readValue, arrayDimensionsPresents);
      else
      {
        IVariant _variant = ReadVariant(decoder);
        _ret = (Array)_variant.Value;
      }
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
      public Variant(Array value, BuiltInType type) : this(value, new UATypeInfo(type, value.Rank)) { }
      public Variant(object value, BuiltInType type) : this(value, new UATypeInfo(type)) { }
      public object Value
      {
        get; private set;
      }
      public UATypeInfo UATypeInfo
      {
        get; private set;
      }
      private Variant(object value, UATypeInfo type)
      {
        Value = value;
        UATypeInfo = type;
      }
    }
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
    private Array DecodeArray(IBinaryDecoder decoder, BuiltInType builtInType, bool arrayDimensionsPresents)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          return DecodeArray<bool>(decoder, decoder.ReadBoolean, arrayDimensionsPresents);
        case BuiltInType.SByte:
          return DecodeArray<sbyte>(decoder, decoder.ReadSByte, arrayDimensionsPresents);
        case BuiltInType.Byte:
          return DecodeArray<byte>(decoder, decoder.ReadByte, arrayDimensionsPresents);
        case BuiltInType.Int16:
          return DecodeArray<short>(decoder, decoder.ReadInt16, arrayDimensionsPresents);
        case BuiltInType.UInt16:
          return DecodeArray<ushort>(decoder, decoder.ReadUInt16, arrayDimensionsPresents);
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          return DecodeArray<int>(decoder, decoder.ReadInt32, arrayDimensionsPresents);
        case BuiltInType.UInt32:
          return DecodeArray<uint>(decoder, decoder.ReadUInt32, arrayDimensionsPresents);
        case BuiltInType.Int64:
          return DecodeArray<long>(decoder, decoder.ReadInt64, arrayDimensionsPresents);
        case BuiltInType.UInt64:
          return DecodeArray<ulong>(decoder, decoder.ReadUInt64, arrayDimensionsPresents);
        case BuiltInType.Float:
          return DecodeArray<float>(decoder, decoder.ReadSingle, arrayDimensionsPresents);
        case BuiltInType.Double:
          return DecodeArray<double>(decoder, decoder.ReadDouble, arrayDimensionsPresents);
        case BuiltInType.String:
          return DecodeArray<string>(decoder, () => ReadString(decoder), arrayDimensionsPresents);
        case BuiltInType.DateTime:
          return DecodeArray<DateTime>(decoder, () => ReadDateTime(decoder), arrayDimensionsPresents);
        case BuiltInType.Guid:
          return DecodeArray<Guid>(decoder, decoder.ReadGuid, arrayDimensionsPresents);
        case BuiltInType.ByteString:
          return DecodeArray<byte>(decoder, decoder.ReadByte, arrayDimensionsPresents);
        case BuiltInType.XmlElement:
          return DecodeArray<XmlElement>(decoder, () => ReadXmlElement(decoder), arrayDimensionsPresents);
        case BuiltInType.NodeId:
          return DecodeArray<INodeId>(decoder, () => ReadNodeId(decoder), arrayDimensionsPresents);
        case BuiltInType.ExpandedNodeId:
          return DecodeArray<IExpandedNodeId>(decoder, () => ReadExpandedNodeId(decoder), arrayDimensionsPresents);
        case BuiltInType.StatusCode:
          return DecodeArray<IStatusCode>(decoder, () => ReadStatusCode(decoder), arrayDimensionsPresents);
        case BuiltInType.QualifiedName:
          return DecodeArray<IQualifiedName>(decoder, () => ReadQualifiedName(decoder), arrayDimensionsPresents);
        case BuiltInType.LocalizedText:
          return DecodeArray<ILocalizedText>(decoder, () => ReadLocalizedText(decoder), arrayDimensionsPresents);
        case BuiltInType.ExtensionObject:
          return DecodeArray<IExtensionObject>(decoder, () => ReadExtensionObject(decoder), arrayDimensionsPresents);
        case BuiltInType.DataValue:
          return DecodeArray<IDataValue>(decoder, () => ReadDataValue(decoder), arrayDimensionsPresents);
        case BuiltInType.Variant:
          return DecodeArray<IVariant>(decoder, () => ReadVariant(decoder), arrayDimensionsPresents);
        default:
          throw new ArgumentOutOfRangeException($"Cannot decode unknown type in Variant object (0x{(int)builtInType:X2}).");
      }
    }
    private Array DecodeArray<type>(IBinaryDecoder decoder, Func<type> readValue, bool arrayDimensionsPresents)
    {
      int length = decoder.ReadInt32();
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof(length));
      Array _ret;
      type[] values = new type[length];
      for (int ii = 0; ii < length; ii++)
        values[ii] = readValue();
      int[] _dimensions = null;
      if (arrayDimensionsPresents)
      {
        _dimensions = ReadDimensions(decoder);
        _ret = Array.CreateInstance(typeof(type), _dimensions);
        CopyValues(_ret, values);
      }
      else
        _ret = values;
      return _ret;
    }
    private void CopyValues<type>(Array _ret, type[] values)
    {
      throw new NotImplementedException();
    }
    private int[] ReadDimensions(IBinaryDecoder decoder)
    {
      int length = decoder.ReadInt32();
      if (length < 0)
        return null;
      if (MaxArrayLength > 0 && MaxArrayLength < length)
        throw new ArgumentOutOfRangeException(nameof(MaxArrayLength), $"Unsupported array length {length}");
      List<Int32> values = new List<Int32>(length);
      for (int ii = 0; ii < length; ii++)
        values.Add(decoder.ReadInt32());
      return values.ToArray();
    }
    #endregion

  }
}
