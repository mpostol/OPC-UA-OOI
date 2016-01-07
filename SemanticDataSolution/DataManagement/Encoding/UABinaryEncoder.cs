
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.Encoding
{
  /// <summary>
  /// Class UABinaryEncoder - basic implementation of the <see cref="IUAEncoder"/> that provides methods to be used to encode selected set of the OPC UA Built-in types.
  /// </summary>
  public abstract class UABinaryEncoder : IUAEncoder
  {

    #region IUAEncoder - supported types
    /// <summary>
    /// Writes the <see cref="IVariant"/> using provided encoder <see cref="IBinaryEncoder"/>
    /// </summary>
    /// <param name="encoder">The encoder to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="IVariant"/>.</param>
    public virtual void Write(IBinaryEncoder encoder, IVariant value)
    {
      // check for null.
      if (value.Value == null || value.UATypeInfo == null || value.UATypeInfo.BuiltInType == BuiltInType.Null)
      {
        encoder.Write((byte)0);
        return;
      }
      // encode enums as int32.
      byte _encodingByte = (byte)value.UATypeInfo.BuiltInType;
      if (value.UATypeInfo.BuiltInType == BuiltInType.Enumeration)
        _encodingByte = (byte)BuiltInType.Int32;
      if (value.UATypeInfo.ValueRank < 0)
      {
        encoder.Write(_encodingByte);
        WriteValue(encoder, value.UATypeInfo.BuiltInType, value.Value);
      }
      else
      {
        _encodingByte |= (byte)VariantEncodingMask.IsArray;
        Array _array = null;
        if (value.Value != null)
        {
          _array = value.Value as Array;
          if (_array == null)
            throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value.Value)} must be {nameof(IMatrix)} and cannot be null");
          if (_array.Rank > 1)
            _encodingByte |= (byte)VariantEncodingMask.ArrayDimensionsPresents;
        }
        encoder.Write(_encodingByte);
        EncodeArray(encoder, value.UATypeInfo.BuiltInType, _array);
      }
    }
    /// <summary>
    /// Encodes the <see cref="Array" /> directly if the array is one dimensional or as <see cref="UANetworking.Configuration.Serialization.BuiltInType.Variant" /> otherwise.
    /// </summary>
    /// <typeparam name="type">The type of the array element  type.</typeparam>
    /// <param name="encoder">The encoder <see cref="IBinaryEncoder" /> to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="Array" />.</param>
    /// <param name="writeValue">Thi delegate encapsulates binary encoding functionality.</param>
    /// <param name="builtInType"><see cref="BuiltInType" /> of the array item to be encoded in case of variant.</param>
    public void WriteArray<type>(IBinaryEncoder encoder, Array value, Action<type> writeValue, BuiltInType builtInType)
    {
      if (value == null)
      {
        encoder.Write((byte)0);
        return;
      }
      byte _encodingByte = (byte)builtInType;
      if (value.Rank > 1)
      {
        //Encode it as the Variant
        if (builtInType == BuiltInType.Enumeration)
          _encodingByte = (byte)BuiltInType.Int32;
        _encodingByte |= (byte)VariantEncodingMask.IsArray;
        if (value.Rank > 1)
          _encodingByte |= (byte)VariantEncodingMask.ArrayDimensionsPresents;
        encoder.Write(_encodingByte);
      }
      EncodeArray<type>(encoder.Write, value, writeValue);
    }
    /// <summary>
    /// Writes <see cref="DateTime" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public virtual void Write(IBinaryEncoder encoder, DateTime value)
    {
      encoder.Write(CommonDefinitions.GetUADataTimeTicks(value));
    }
    /// <summary>
    /// Writes a <see cref="Guid" /> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="encoder">The encoder to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="Guid"/>.</param>
    public virtual void Write(IBinaryEncoder encoder, Guid value)
    {
      encoder.Write(value.ToByteArray());
    }
    /// <summary>
    /// Writes the <c>ByteString</c> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder and an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public virtual void Write(IBinaryEncoder encoder, byte[] value)
    {
      if (value == null)
        encoder.Write((Int32)(-1));
      else
      {
        encoder.Write((Int32)value.Length);
        encoder.Write(value);
      }
    }
    /// <summary>
    /// Encodes the <see cref="string" /> as a sequence of UTF8 characters without a null terminator and preceded by the length in bytes.
    /// The length in bytes is encoded as Int32. A value of −1 is used to indicate a ‘null’ string.
    /// </summary>
    /// <param name="encoder">The encoder <see cref="IBinaryEncoder" /> to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="Guid" />.</param>
    public void Write(IBinaryEncoder encoder, string value)
    {
      if (value == null)
      {
        encoder.Write((Int32)(-1));
        return;
      }
      byte[] _bytes = new UTF8Encoding().GetBytes(value);
      Write(encoder, _bytes);
    }
    #endregion

    #region IUAEncoder - unsupported types - should be implemented by comercial products.
    /// <summary>
    /// Writes <see cref="IDataValue" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, IDataValue value);
    /// <summary>
    /// Writes <see cref="IDiagnosticInfo" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, IDiagnosticInfo value);
    /// <summary>
    /// Writes <see cref="IExpandedNodeId" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, IExpandedNodeId value);
    /// <summary>
    /// Writes <see cref="ILocalizedText" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, IExtensionObject value);
    /// <summary>
    /// Writes <see cref="DateTime" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, ILocalizedText value);
    /// <summary>
    /// Writes <see cref="INodeId" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, INodeId value);
    /// <summary>
    /// Writes <see cref="IQualifiedName" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, IQualifiedName value);
    /// <summary>
    /// Writes <see cref="IStatusCode" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, IStatusCode value);
    /// <summary>
    /// Writes <see cref="XmlElement" /> using the provided encoder <see cref="IBinaryEncoder" />.
    /// </summary>
    /// <param name="encoder">The encoder - an object implementing the <see cref="IBinaryEncoder" /> interface.</param>
    /// <param name="value">The value to be encoded.</param>
    public abstract void Write(IBinaryEncoder encoder, XmlElement value);
    #endregion

    #region private
    /// <summary>
    /// The maximum array length - could be used to apply license volume limits
    /// </summary>
    protected int MaxArrayLength = byte.MaxValue;
    private void WriteValue(IBinaryEncoder encoder, BuiltInType builtInType, object value)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          encoder.Write((Boolean)value);
          return;
        case BuiltInType.SByte:
          encoder.Write((SByte)value);
          return;
        case BuiltInType.Byte:
          encoder.Write((Byte)value);
          return;
        case BuiltInType.Int16:
          encoder.Write((Int16)value);
          return;
        case BuiltInType.UInt16:
          encoder.Write((UInt16)value);
          return;
        case BuiltInType.Int32:
          encoder.Write((Int32)value);
          return;
        case BuiltInType.UInt32:
          encoder.Write((System.UInt32)value);
          return;
        case BuiltInType.Int64:
          encoder.Write((Int64)value);
          return;
        case BuiltInType.UInt64:
          encoder.Write((UInt64)value);
          return;
        case BuiltInType.Float:
          encoder.Write((Single)value);
          return;
        case BuiltInType.Double:
          encoder.Write((Double)value);
          return;
        case BuiltInType.String:
          Write(encoder, (String)value);
          return;
        case BuiltInType.DateTime:
          Write(encoder, (DateTime)value);
          return;
        case BuiltInType.Guid:
          encoder.Write((Guid)value);
          return;
        case BuiltInType.ByteString:
          Write(encoder, (byte[])value);
          return;
        case BuiltInType.XmlElement:
          Write(encoder, (XmlElement)value);
          return;
        case BuiltInType.NodeId:
          Write(encoder, (XmlElement)value);
          return;
        case BuiltInType.ExpandedNodeId:
          Write(encoder, (IExpandedNodeId)value);
          return;
        case BuiltInType.StatusCode:
          Write(encoder, (IStatusCode)value);
          return;
        case BuiltInType.QualifiedName:
          Write(encoder, (IQualifiedName)value);
          return;
        case BuiltInType.LocalizedText:
          Write(encoder, (ILocalizedText)value);
          return;
        case BuiltInType.ExtensionObject:
          Write(encoder, (ILocalizedText)value);
          return;
        case BuiltInType.DataValue:
          Write(encoder, (IDataValue)value);
          return;
        case BuiltInType.Variant:
          Write(encoder, (IVariant)value);
          return;
        case BuiltInType.DiagnosticInfo:
          Write(encoder, (IDiagnosticInfo)value);
          return;
        case BuiltInType.Enumeration:
          encoder.Write((int)value);
          return;
        case BuiltInType.Null:
        default:
          throw new ArgumentOutOfRangeException($"Cannot encode unknown type in Variant object (0x{builtInType:X2}).");
      }
    }
    private void EncodeArray(IBinaryEncoder encoder, BuiltInType builtInType, Array value)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          EncodeArray<Boolean>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.SByte:
          EncodeArray<SByte>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.Byte:
          EncodeArray<Byte>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.Int16:
          EncodeArray<Int16>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.UInt16:
          EncodeArray<UInt16>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          EncodeArray<Int32>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.UInt32:
          EncodeArray<System.UInt32>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.Int64:
          EncodeArray<Int64>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.UInt64:
          EncodeArray<UInt64>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.Float:
          EncodeArray<Single>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.Double:
          EncodeArray<Double>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.String:
          EncodeArray<String>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.DateTime:
          EncodeArray<DateTime>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.Guid:
          EncodeArray<Guid>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.ByteString:
          EncodeArray<Byte>(encoder.Write, value, encoder.Write);
          break;
        case BuiltInType.XmlElement:
          EncodeArray<XmlElement>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.NodeId:
          EncodeArray<INodeId>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.ExpandedNodeId:
          EncodeArray<IExpandedNodeId>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.StatusCode:
          EncodeArray<IStatusCode>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.QualifiedName:
          EncodeArray<IQualifiedName>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.LocalizedText:
          EncodeArray<ILocalizedText>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.ExtensionObject:
          EncodeArray<IExtensionObject>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.DataValue:
          EncodeArray<IDataValue>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.Variant:
          EncodeArray<IVariant>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.DiagnosticInfo:
          EncodeArray<IDiagnosticInfo>(encoder.Write, value, x => Write(encoder, x));
          break;
        case BuiltInType.Null:
        default:
          break;
      };
    }
    private void EncodeArray<type>(Action<Int32> encoderInt32, Array array, Action<type> writeValue)
    {
      if (array == null)
      {
        encoderInt32(-1);
        return;
      }
      if (MaxArrayLength > 0 && MaxArrayLength < array.Length)
        throw new ArgumentOutOfRangeException(nameof(MaxArrayLength), $"MaxArrayLength {MaxArrayLength} < {array.Length}");
      encoderInt32(array.Length);
      //for (int ii = 0; ii < values.Length; ii++)
      foreach (type item in array)
        writeValue(item);
      if (array.Rank == 1)
        return;
      encoderInt32(array.Rank);
      for (int ii = 0; ii < array.Rank; ii++)
        encoderInt32(array.GetLength(ii));
    }
    #endregion

  }
}

