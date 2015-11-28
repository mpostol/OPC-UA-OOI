
using System;
using System.Collections.Generic;
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
    /// Writes the variant using provided encoder <see cref="IBinaryEncoder"/>
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
      object valueToEncode = value.Value;
      if (value.UATypeInfo.ValueRank < 0)
      {
        encoder.Write(_encodingByte);
        WriteValue(encoder, value.UATypeInfo.BuiltInType, valueToEncode);
      }
      else
      {
        IMatrix matrix = null;
        _encodingByte |= (byte)VariantEncodingMask.IsArray;
        if (value.UATypeInfo.ValueRank > 1)
        {
          _encodingByte |= (byte)VariantEncodingMask.ArrayDimensionsPresents;
          matrix = (IMatrix)valueToEncode;
          valueToEncode = matrix.Elements;
        }
        encoder.Write(_encodingByte);
        WriteArray(encoder, value.UATypeInfo.BuiltInType, valueToEncode);
        if (value.UATypeInfo.ValueRank > 1)
          WriteDimensions(encoder, matrix.Dimensions);
      }
    }
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
    #endregion

    #region IUAEncoder - unsupported types - should be implemented by comercial products.
    public abstract void Write(IBinaryEncoder encoder, byte[] value);
    public abstract void Write(IBinaryEncoder encoder, IDataValue value);
    public abstract void Write(IBinaryEncoder encoder, IDiagnosticInfo value);
    public abstract void Write(IBinaryEncoder encoder, IExpandedNodeId value);
    public abstract void Write(IBinaryEncoder encoder, IExtensionObject value);
    public abstract void Write(IBinaryEncoder encoder, ILocalizedText value);
    public abstract void Write(IBinaryEncoder encoder, INodeId value);
    public abstract void Write(IBinaryEncoder encoder, IQualifiedName value);
    public abstract void Write(IBinaryEncoder encoder, IStatusCode value);
    public abstract void Write(IBinaryEncoder encoder, XmlElement value);
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
          encoder.Write((UInt32)value);
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
          encoder.Write((String)value);
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
    private void WriteArray(IBinaryEncoder encoder, BuiltInType builtInType, object value)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          WriteArray<Boolean>(encoder, value, encoder.Write);
          break;
        case BuiltInType.SByte:
          WriteArray<SByte>(encoder, value, encoder.Write);
          break;
        case BuiltInType.Byte:
          WriteArray<Byte>(encoder, value, encoder.Write);
          break;
        case BuiltInType.Int16:
          WriteArray<Int16>(encoder, value, encoder.Write);
          break;
        case BuiltInType.UInt16:
          WriteArray<UInt16>(encoder, value, encoder.Write);
          break;
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          WriteArray<Int32>(encoder, value, encoder.Write);
          break;
        case BuiltInType.UInt32:
          WriteArray<UInt32>(encoder, value, encoder.Write);
          break;
        case BuiltInType.Int64:
          WriteArray<Int64>(encoder, value, encoder.Write);
          break;
        case BuiltInType.UInt64:
          WriteArray<UInt64>(encoder, value, encoder.Write);
          break;
        case BuiltInType.Float:
          WriteArray<Single>(encoder, value, encoder.Write);
          break;
        case BuiltInType.Double:
          WriteArray<Double>(encoder, value, encoder.Write);
          break;
        case BuiltInType.String:
          WriteArray<String>(encoder, value, encoder.Write);
          break;
        case BuiltInType.DateTime:
          WriteArray<DateTime>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.Guid:
          WriteArray<Guid>(encoder, value, encoder.Write);
          break;
        case BuiltInType.ByteString:
          WriteArray<Byte>(encoder, value, encoder.Write);
          break;
        case BuiltInType.XmlElement:
          WriteArray<XmlElement>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.NodeId:
          WriteArray<INodeId>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.ExpandedNodeId:
          WriteArray<IExpandedNodeId>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.StatusCode:
          WriteArray<IStatusCode>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.QualifiedName:
          WriteArray<IQualifiedName>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.LocalizedText:
          WriteArray<ILocalizedText>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.ExtensionObject:
          WriteArray<IExtensionObject>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.DataValue:
          WriteArray<IDataValue>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.Variant:
          WriteArray<IVariant>(encoder, value, x => Write(encoder, x));
          break;
        case BuiltInType.DiagnosticInfo:
          WriteArray<IDiagnosticInfo>(encoder, value, x => Write(encoder, x));
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
        encoder.Write(-1);
        return true;
      }
      if (MaxArrayLength > 0 && MaxArrayLength < values.Count)
        throw new ArgumentOutOfRangeException(nameof(MaxArrayLength), $"MaxArrayLength {MaxArrayLength} < {values.Count}");
      // write length.
      encoder.Write(values.Count);
      return values.Count == 0;
    }
    private void WriteDimensions(IBinaryEncoder encoder, int[] dimensions)
    {
      // write dimensions array length.
      if (WriteArrayLength(encoder, dimensions))
        return;
      // write dimensions.
      for (int ii = 0; ii < dimensions.Length; ii++)
        encoder.Write(dimensions[ii]);
    }
    #endregion

  }
}

