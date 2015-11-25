
using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.DataManagement.MessageHandling;
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
    /// Writes the variant using provided encoder <see cref="IBinaryEncoder"/>
    /// </summary>
    /// <param name="encoder">The encoder to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="IVariant"/>.</param>
    public virtual void WriteVariant(IBinaryEncoder encoder, IVariant value)
    {
      // check for null.
      if (value.Value == null || value.UATypeInfo == null || value.UATypeInfo.BuiltInType == BuiltInType.Null)
      {
        encoder.WriteByte(0);
        return;
      }
      // encode enums as int32.
      byte _encodingByte = (byte)value.UATypeInfo.BuiltInType;
      if (value.UATypeInfo.BuiltInType == BuiltInType.Enumeration)
        _encodingByte = (byte)BuiltInType.Int32;
      object valueToEncode = value.Value;
      if (value.UATypeInfo.ValueRank < 0)
      {
        encoder.WriteByte(_encodingByte);
        WriteValue(encoder, value.UATypeInfo.BuiltInType, valueToEncode);
      }
      else
      {
        if (value.UATypeInfo.ValueRank >= 0)
        {
          IMatrix matrix = null;
          _encodingByte |= (byte)VariantEncodingMask.IsArray;
          if (value.UATypeInfo.ValueRank > 1)
          {
            _encodingByte |= (byte)VariantEncodingMask.ArrayDimensionsPresents;
            matrix = (IMatrix)valueToEncode;
            valueToEncode = matrix.Elements;
          }
          encoder.WriteByte(_encodingByte);
          WriteArray(encoder, value.UATypeInfo.BuiltInType, valueToEncode);
          if (value.UATypeInfo.ValueRank > 1)
            WriteDimensions(encoder, matrix.Dimensions);
        }
      }
    }
    public virtual void WriteDateTime(IBinaryEncoder encoder, DateTime value)
    {
      encoder.WriteInt64(CommonDefinitions.GetUADataTimeTicks(value));
    }
    /// <summary>
    /// Writes a <see cref="Guid" /> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="encoder">The encoder to write the value encapsulated in this instance.</param>
    /// <param name="value">The value to be encoded as an instance of <see cref="Guid"/>.</param>
    public virtual void WriteGuid(IBinaryEncoder encoder, Guid value)
    {
      encoder.WriteBytes(value.ToByteArray());
    }
    #endregion

    #region IUAEncoder - unsupported types - should be implemented by comercial products.
    public abstract void WriteByteString(IBinaryEncoder encoder, byte[] value);
    public abstract void WriteDataValue(IBinaryEncoder encoder, IDataValue value);
    public abstract void WriteDiagnosticInfo(IBinaryEncoder encoder, IDiagnosticInfo value);
    public abstract void WriteExpandedNodeId(IBinaryEncoder encoder, IExpandedNodeId value);
    public abstract void WriteExtensionObject(IBinaryEncoder encoder, IExtensionObject value);
    public abstract void WriteLocalizedText(IBinaryEncoder encoder, ILocalizedText value);
    public abstract void WriteNodeId(IBinaryEncoder encoder, INodeId value);
    public abstract void WriteQualifiedName(IBinaryEncoder encoder, IQualifiedName value);
    public abstract void WriteStatusCode(IBinaryEncoder encoder, IStatusCode value);
    public abstract void WriteXmlElement(IBinaryEncoder encoder, XmlElement value);
    #endregion

    #region private
    //vars
    /// <summary>
    /// The maximum array length - could be used to apply license volume limits
    /// </summary>
    protected int MaxArrayLength = 2;
    private void WriteValue(IBinaryEncoder encoder, BuiltInType builtInType, object value)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          encoder.WriteBoolean((Boolean)value);
          return;
        case BuiltInType.SByte:
          encoder.WriteSByte((SByte)value);
          return;
        case BuiltInType.Byte:
          encoder.WriteByte((Byte)value);
          return;
        case BuiltInType.Int16:
          encoder.WriteInt16((Int16)value);
          return;
        case BuiltInType.UInt16:
          encoder.WriteUInt16((UInt16)value);
          return;
        case BuiltInType.Int32:
          encoder.WriteInt32((Int32)value);
          return;
        case BuiltInType.UInt32:
          encoder.WriteUInt32((UInt32)value);
          return;
        case BuiltInType.Int64:
          encoder.WriteInt64((Int64)value);
          return;
        case BuiltInType.UInt64:
          encoder.WriteUInt64((UInt64)value);
          return;
        case BuiltInType.Float:
          encoder.WriteSingle((Single)value);
          return;
        case BuiltInType.Double:
          encoder.WriteDouble((Double)value);
          return;
        case BuiltInType.String:
          encoder.WriteString((String)value);
          return;
        case BuiltInType.DateTime:
          WriteDateTime(encoder, (DateTime)value);
          return;
        case BuiltInType.Guid:
          encoder.WriteGuid((Guid)value);
          return;
        case BuiltInType.ByteString:
          WriteByteString(encoder, (byte[])value);
          return;
        case BuiltInType.XmlElement:
          WriteXmlElement(encoder, (XmlElement)value);
          return;
        case BuiltInType.NodeId:
          WriteXmlElement(encoder, (XmlElement)value);
          return;
        case BuiltInType.ExpandedNodeId:
          WriteExpandedNodeId(encoder, (IExpandedNodeId)value);
          return;
        case BuiltInType.StatusCode:
          WriteStatusCode(encoder, (IStatusCode)value);
          return;
        case BuiltInType.QualifiedName:
          WriteQualifiedName(encoder, (IQualifiedName)value);
          return;
        case BuiltInType.LocalizedText:
          WriteLocalizedText(encoder, (ILocalizedText)value);
          return;
        case BuiltInType.ExtensionObject:
          WriteLocalizedText(encoder, (ILocalizedText)value);
          return;
        case BuiltInType.DataValue:
          WriteDataValue(encoder, (IDataValue)value);
          return;
        case BuiltInType.Variant:
          WriteVariant(encoder, (IVariant)value);
          return;
        case BuiltInType.DiagnosticInfo:
          WriteDiagnosticInfo(encoder, (IDiagnosticInfo)value);
          return;
        case BuiltInType.Enumeration:
          encoder.WriteInt32((int)value);
          return;
        case BuiltInType.Null:
        default:
          throw new ArgumentOutOfRangeException($"Cannot encode unknown type in Variant object (0x{builtInType:X2}).");
      }
    }
    private void WriteArray(IBinaryEncoder encoder, BuiltInType builtInType, object value)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          WriteArray<Boolean>(encoder, value, encoder.WriteBoolean);
          break;
        case BuiltInType.SByte:
          WriteArray<SByte>(encoder, value, encoder.WriteSByte);
          break;
        case BuiltInType.Byte:
          WriteArray<Byte>(encoder, value, encoder.WriteByte);
          break;
        case BuiltInType.Int16:
          WriteArray<Int16>(encoder, value, encoder.WriteInt16);
          break;
        case BuiltInType.UInt16:
          WriteArray<UInt16>(encoder, value, encoder.WriteUInt16);
          break;
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          WriteArray<Int32>(encoder, value, encoder.WriteInt32);
          break;
        case BuiltInType.UInt32:
          WriteArray<UInt32>(encoder, value, encoder.WriteUInt32);
          break;
        case BuiltInType.Int64:
          WriteArray<Int64>(encoder, value, encoder.WriteInt64);
          break;
        case BuiltInType.UInt64:
          WriteArray<UInt64>(encoder, value, encoder.WriteUInt64);
          break;
        case BuiltInType.Float:
          WriteArray<Single>(encoder, value, encoder.WriteSingle);
          break;
        case BuiltInType.Double:
          WriteArray<Double>(encoder, value, encoder.WriteDouble);
          break;
        case BuiltInType.String:
          WriteArray<String>(encoder, value, encoder.WriteString);
          break;
        case BuiltInType.DateTime:
          WriteArray<DateTime>(encoder, value, x => WriteDateTime(encoder, x));
          break;
        case BuiltInType.Guid:
          WriteArray<Guid>(encoder, value, encoder.WriteGuid);
          break;
        case BuiltInType.ByteString:
          WriteArray<Byte>(encoder, value, encoder.WriteByte);
          break;
        case BuiltInType.XmlElement:
          WriteArray<XmlElement>(encoder, value, x => WriteXmlElement(encoder, x));
          break;
        case BuiltInType.NodeId:
          WriteArray<INodeId>(encoder, value, x => WriteNodeId(encoder, x));
          break;
        case BuiltInType.ExpandedNodeId:
          WriteArray<IExpandedNodeId>(encoder, value, x => WriteExpandedNodeId(encoder, x));
          break;
        case BuiltInType.StatusCode:
          WriteArray<IStatusCode>(encoder, value, x => WriteStatusCode(encoder, x));
          break;
        case BuiltInType.QualifiedName:
          WriteArray<IQualifiedName>(encoder, value, x => WriteQualifiedName(encoder, x));
          break;
        case BuiltInType.LocalizedText:
          WriteArray<ILocalizedText>(encoder, value, x => WriteLocalizedText(encoder, x));
          break;
        case BuiltInType.ExtensionObject:
          WriteArray<IExtensionObject>(encoder, value, x => WriteExtensionObject(encoder, x));
          break;
        case BuiltInType.DataValue:
          WriteArray<IDataValue>(encoder, value, x => WriteDataValue(encoder, x));
          break;
        case BuiltInType.Variant:
          WriteArray<IVariant>(encoder, value, x => WriteVariant(encoder, x));
          break;
        case BuiltInType.DiagnosticInfo:
          WriteArray<IDiagnosticInfo>(encoder, value, x => WriteDiagnosticInfo(encoder, x));
          break;
        case BuiltInType.Null:
        default:
          break;
      };
    }
    private void WriteArray<type>(IBinaryEncoder encoder, object values, Action<type> writeValue)
    {
      type[] _array = values as type[];
      if (WriteArrayLength(encoder, _array))
        return;
      for (int ii = 0; ii < _array.Length; ii++)
        writeValue(_array[ii]);
    }
    private bool WriteArrayLength<type>(IBinaryEncoder encoder, IList<type> values)
    {
      // check for null.
      if (values == null)
      {
        encoder.WriteInt32(-1);
        return true;
      }
      if (MaxArrayLength > 0 && MaxArrayLength < values.Count)
        throw new ArgumentOutOfRangeException(nameof(MaxArrayLength), $"MaxArrayLength {MaxArrayLength} < {values.Count}");
      // write length.
      encoder.WriteInt32(values.Count);
      return values.Count == 0;
    }
    private void WriteDimensions(IBinaryEncoder encoder, int[] dimensions)
    {
      // write dimensions array length.
      if (WriteArrayLength(encoder, dimensions))
        return;
      // write dimensions.
      for (int ii = 0; ii < dimensions.Length; ii++)
        encoder.WriteInt32(dimensions[ii]);
    }
    #endregion

  }
}

