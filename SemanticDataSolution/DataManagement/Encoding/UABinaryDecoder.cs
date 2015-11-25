
using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.DataManagement.MessageHandling;
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
    /// Reads the variant using the provided <see cref="IBinaryDecoder"/>.
    /// </summary>
    /// <param name="decoder">The decoder to be used to read <see cref="IVariant"/> from the input byte stream.</param>
    /// <returns>Returns am instance of the <see cref="IVariant"/> encapsulating the received value.</returns>
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
        int length = decoder.ReadInt32();
        if (length < 0)
          return value;
        Array array = ReadArray(decoder, length, builtInType);
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
    /// Reads the <see cref="Guid"/> from UA binary encoded as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <returns>The <see cref="Guid"/> decoded from the UA binary encoded <see cref="Stream"/>.</returns>
    public Guid ReadGuid(IBinaryDecoder decoder)
    {
      int m_EncodedGuidLength = 16;
      byte[] bytes = decoder.ReadBytes(m_EncodedGuidLength);
      return new Guid(bytes);
    }
    /// <summary>
    /// Reads the <see cref="DateTime"/> from UA binary encoded as <see cref="Int64"/> that contains the value and advances the stream position by 8 bytes.
    /// </summary>
    /// <returns>The <see cref="DateTime "/> decoded from the UA binary encoded <see cref="Stream"/>.</returns>
    public DateTime ReadDateTime(IBinaryDecoder decoder)
    {
      return CommonDefinitions.GetUADateTime(decoder.ReadInt64());
    }
    #endregion

    #region IUADecoder - unsupported types
    public abstract byte[] ReadByteString(IBinaryDecoder decoder);
    public abstract IDataValue ReadDataValue(IBinaryDecoder decoder);
    public abstract IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder);
    public abstract IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder);
    public abstract IExtensionObject ReadExtensionObject(IBinaryDecoder decoder);
    public abstract ILocalizedText ReadLocalizedText(IBinaryDecoder decoder);
    public abstract INodeId ReadNodeId(IBinaryDecoder decoder);
    public abstract IQualifiedName ReadQualifiedName(IBinaryDecoder decoder);
    public abstract XmlElement ReadXmlElement(IBinaryDecoder decoder);
    public abstract IStatusCode ReadStatusCode(IBinaryDecoder decoder);
    #endregion

    #region private
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
        m_BuiltInType = builtInType;
        int _length = 1;
        for (int _ix = 0; _ix < dimensions.Length; _ix++)
          _length *= dimensions[_ix];
        if (_length != Dimensions.Length)
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

      private BuiltInType m_BuiltInType;

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
          return new Variant(encoder.ReadString(), encodingByte);
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
          throw new ArgumentOutOfRangeException($"Cannot decode unknown type in Variant object (0x{encodingByte:X2}).");
      }
    }
    private Array ReadArray(IBinaryDecoder encoder, int length, BuiltInType builtInType)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          return ReadArray<bool>(length, encoder.ReadBoolean);
        case BuiltInType.SByte:
          return ReadArray<sbyte>(length, encoder.ReadSByte);
        case BuiltInType.Byte:
          return ReadArray<byte>(length, encoder.ReadByte);
        case BuiltInType.Int16:
          return ReadArray<short>(length, encoder.ReadInt16);
        case BuiltInType.UInt16:
          return ReadArray<ushort>(length, encoder.ReadUInt16);
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          return ReadArray<int>(length, encoder.ReadInt32);
        case BuiltInType.UInt32:
          return ReadArray<uint>(length, encoder.ReadUInt32);
        case BuiltInType.Int64:
          return ReadArray<long>(length, encoder.ReadInt64);
        case BuiltInType.UInt64:
          return ReadArray<ulong>(length, encoder.ReadUInt64);
        case BuiltInType.Float:
          return ReadArray<float>(length, encoder.ReadSingle);
        case BuiltInType.Double:
          return ReadArray<double>(length, encoder.ReadDouble);
        case BuiltInType.String:
          return ReadArray<string>(length, encoder.ReadString);
        case BuiltInType.DateTime:
          return ReadArray<DateTime>(length, () => ReadDateTime(encoder));
        case BuiltInType.Guid:
          return ReadArray<Guid>(length, encoder.ReadGuid);
        case BuiltInType.ByteString:
          return ReadArray<byte>(length, encoder.ReadByte);
        case BuiltInType.XmlElement:
          return ReadArray<XmlElement>(length, () => ReadXmlElement(encoder));
        case BuiltInType.NodeId:
          return ReadArray<INodeId>(length, () => ReadNodeId(encoder));
        case BuiltInType.ExpandedNodeId:
          return ReadArray<IExpandedNodeId>(length, () => ReadExpandedNodeId(encoder));
        case BuiltInType.StatusCode:
          return ReadArray<IStatusCode>(length, () => ReadStatusCode(encoder));
        case BuiltInType.QualifiedName:
          return ReadArray<IQualifiedName>(length, () => ReadQualifiedName(encoder));
        case BuiltInType.LocalizedText:
          return ReadArray<ILocalizedText>(length, () => ReadLocalizedText(encoder));
        case BuiltInType.ExtensionObject:
          return ReadArray<IExtensionObject>(length, () => ReadExtensionObject(encoder));
        case BuiltInType.DataValue:
          return ReadArray<IDataValue>(length, () => ReadDataValue(encoder));
        case BuiltInType.Variant:
          return ReadArray<IVariant>(length, () => ReadVariant(encoder));
        default:
          throw new ArgumentOutOfRangeException($"Cannot decode unknown type in Variant object (0x{(int)builtInType:X2}).");
      }
    }
    private Array ReadArray<type>(int length, Func<type> readValue)
    {
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
