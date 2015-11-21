using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.Encoding
{
  /// <summary>
  /// The possible values for Variant encoding bits.
  /// </summary>
  [Flags]
  internal enum VariantEncodingMask
  {
    /// <summary>
    /// True if an array of values is encoded.
    /// </summary>
    IsArray = 0x80,
    /// <summary>
    /// True if the Array Dimensions field is encoded
    /// </summary>
    ArrayDimensionsPresents = 0x40,
    /// <summary>
    /// The type mask of the Built-in Type Id
    /// </summary>
    TypeIdMask = 0x3F
  }
  internal static class UABinaryDecoder
  {

    internal static Variant ReadVariant(IBinaryDecoder encoder)
    {
      byte encodingByte = encoder.ReadByte();
      Variant value = null;
        BuiltInType builtInType = (BuiltInType)(encodingByte & (byte)VariantEncodingMask.TypeIdMask);
      if ((encodingByte & (byte)VariantEncodingMask.IsArray) != 0)
      {
        // read the array length.
        int length = encoder.ReadInt32();
        if (length < 0)
          return value;
        Array array = null;
        array = GetArray(encoder, encodingByte, length, builtInType);
        if (array == null)
          throw new ArgumentOutOfRangeException($"Cannot decode array in Variant object (0x{encodingByte:X2}).");
        if ((encodingByte & (byte)VariantEncodingMask.ArrayDimensionsPresents) != 0)
        {
          List<Int32> dimensions = ReadInt32Array(encoder);
          if (dimensions != null && dimensions.Count > 0)
            value = new Variant(new Matrix(array, builtInType, dimensions.ToArray()));
          else
            value = new Variant(new Matrix(array, builtInType));
        }
        else
          value = new Variant(array);
      }
      else
        ReadValue(encoder, builtInType, value);
      return value;
    }
    internal static byte[] ReadBytes(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static DataValue ReadDataValue(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static ExtensionObject ReadExtensionObject(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static LocalizedText ReadLocalizedText(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static QualifiedName ReadQualifiedName(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static StatusCode ReadStatusCode(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static ExpandedNodeId ReadExpandedNodeId(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static NodeId ReadNodeId(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    internal static XmlElement ReadXmlElement(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }
    private static List<int> ReadInt32Array(IBinaryDecoder encoder)
    {
      throw new NotImplementedException();
    }

    #region private
    private static void ReadValue(IBinaryDecoder encoder, BuiltInType encodingByte, Variant value)
    {
      switch ((BuiltInType)encodingByte)
      {
        case BuiltInType.Null:
          value.Value = null;
          break;
        case BuiltInType.Boolean:
          value.Set(encoder.ReadBoolean());
          break;
        case BuiltInType.SByte:
          value.Set(encoder.ReadSByte());
          break;
        case BuiltInType.Byte:
          value.Set(encoder.ReadByte());
          break;
        case BuiltInType.Int16:
          value.Set(encoder.ReadInt16());
          break;
        case BuiltInType.UInt16:
          value.Set(encoder.ReadUInt16());
          break;
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          value.Set(encoder.ReadInt32());
          break;
        case BuiltInType.UInt32:
          value.Set(encoder.ReadUInt32());
          break;
        case BuiltInType.Int64:
          value.Set(encoder.ReadInt64());
          break;
        case BuiltInType.UInt64:
          value.Set(encoder.ReadUInt64());
          break;
        case BuiltInType.Float:
          value.Set(encoder.ReadSingle());
          break;
        case BuiltInType.Double:
          value.Set(encoder.ReadDouble());
          break;
        case BuiltInType.String:
          value.Set(encoder.ReadString());
          break;
        case BuiltInType.DateTime:
          value.Set(encoder.ReadDateTime());
          break;
        case BuiltInType.Guid:
          value.Set(encoder.ReadGuid());
          break;
        case BuiltInType.ByteString:
          value.Set(ReadBytes(encoder));
          break;
        case BuiltInType.XmlElement:
          value.Set(ReadXmlElement(encoder));
          break;
        case BuiltInType.NodeId:
          value.Set(ReadNodeId(encoder));
          break;
        case BuiltInType.ExpandedNodeId:
          value.Set(ReadExpandedNodeId(encoder));
          break;
        case BuiltInType.StatusCode:
          value.Set(ReadStatusCode(encoder));
          break;
        case BuiltInType.QualifiedName:
          value.Set(ReadQualifiedName(encoder));
          break;
        case BuiltInType.LocalizedText:
          value.Set(ReadLocalizedText(encoder));
          break;
        case BuiltInType.ExtensionObject:
          value.Set(ReadExtensionObject(encoder));
          break;
        case BuiltInType.DataValue:
          value.Set(ReadDataValue(encoder));
          break;
        default:
          throw new ArgumentOutOfRangeException($"Cannot decode unknown type in Variant object (0x{encodingByte:X2}).");
      }
    }

    private static Array GetArray(IBinaryDecoder encoder, byte encodingByte, int length, BuiltInType builtInType)
    {
      switch (builtInType)
      {
        case BuiltInType.Boolean:
          return ReadArray<bool>(encoder, length, encoder.ReadBoolean);
        case BuiltInType.SByte:
          return ReadArray<sbyte>(encoder, length, encoder.ReadSByte);
        case BuiltInType.Byte:
          return ReadArray<byte>(encoder, length, encoder.ReadByte);
        case BuiltInType.Int16:
          return ReadArray<short>(encoder, length, encoder.ReadInt16);
        case BuiltInType.UInt16:
          return ReadArray<ushort>(encoder, length, encoder.ReadUInt16);
        case BuiltInType.Int32:
        case BuiltInType.Enumeration:
          return ReadArray<int>(encoder, length, encoder.ReadInt32);
        case BuiltInType.UInt32:
          return ReadArray<uint>(encoder, length, encoder.ReadUInt32);
        case BuiltInType.Int64:
          return ReadArray<long>(encoder, length, encoder.ReadInt64);
        case BuiltInType.UInt64:
          return ReadArray<ulong>(encoder, length, encoder.ReadUInt64);
        case BuiltInType.Float:
          return ReadArray<float>(encoder, length, encoder.ReadSingle);
        case BuiltInType.Double:
          return ReadArray<double>(encoder, length, encoder.ReadDouble);
        case BuiltInType.String:
          return ReadArray<string>(encoder, length, encoder.ReadString);
        case BuiltInType.DateTime:
          return ReadArray<DateTime>(encoder, length, encoder.ReadDateTime);
        case BuiltInType.Guid:
          return ReadArray<Guid>(encoder, length, encoder.ReadGuid);
        case BuiltInType.ByteString:
          return ReadArray<byte>(encoder, length, encoder.ReadByte);
        case BuiltInType.XmlElement:
          return ReadArray<XmlElement>(encoder, length, () => ReadXmlElement(encoder));
        case BuiltInType.NodeId:
          return ReadArray<NodeId>(encoder, length, () => ReadNodeId(encoder));
        case BuiltInType.ExpandedNodeId:
          return ReadArray<ExpandedNodeId>(encoder, length, () => ReadExpandedNodeId(encoder));
        case BuiltInType.StatusCode:
          return ReadArray<StatusCode>(encoder, length, () => ReadStatusCode(encoder));
        case BuiltInType.QualifiedName:
          return ReadArray<QualifiedName>(encoder, length, () => ReadQualifiedName(encoder));
        case BuiltInType.LocalizedText:
          return ReadArray<LocalizedText>(encoder, length, () => ReadLocalizedText(encoder));
        case BuiltInType.ExtensionObject:
          return ReadArray<ExtensionObject>(encoder, length, () => ReadExtensionObject(encoder));
        case BuiltInType.DataValue:
          return ReadArray<DataValue>(encoder, length, () => ReadDataValue(encoder));
        case BuiltInType.Variant:
          return ReadArray<Variant>(encoder, length, () => ReadVariant(encoder));
        default:
          throw new ArgumentOutOfRangeException($"Cannot decode unknown type in Variant object (0x{encodingByte:X2}).");
      }
    }

    internal static object ReadDiagnosticInfo(MessageReaderBase messageReaderBase)
    {
      throw new NotImplementedException();
    }

    internal static void ReadByteString(MessageReaderBase messageReaderBase)
    {
      throw new NotImplementedException();
    }

    private static Array ReadArray<type>(IBinaryDecoder encoder, int length, Func<type> read)
    {
      Array array;
      type[] values = new type[length];
      for (int ii = 0; ii < values.Length; ii++)
        values[ii] = read();
      array = values;
      return array;
    }
    #endregion
  }
  internal class Variant
  {
    private Array array;
    private Matrix matrix;
    public Variant(Array array)
    {
      this.array = array;
    }
    public Variant(Matrix matrix)
    {
      this.matrix = matrix;
    }
    public object Value { get; internal set; }
    internal void Set(byte[] value)
    {
      throw new NotImplementedException();
    }
    internal void Set(sbyte value)
    {
      throw new NotImplementedException();
    }
    internal void Set(short value)
    {
      throw new NotImplementedException();
    }
    internal void Set(int value)
    {
      throw new NotImplementedException();
    }
    internal void Set(long value)
    {
      throw new NotImplementedException();
    }
    internal void Set(float value)
    {
      throw new NotImplementedException();
    }
    internal void Set(string value)
    {
      throw new NotImplementedException();
    }
    internal void Set(Guid guid)
    {
      throw new NotImplementedException();
    }

    internal void Set(XmlElement[] xmlElement)
    {
      throw new NotImplementedException();
    }

    internal void Set(ExpandedNodeId[] expandedNodeId)
    {
      throw new NotImplementedException();
    }

    internal void Set(QualifiedName[] qualifiedName)
    {
      throw new NotImplementedException();
    }

    internal void Set(ExtensionObject[] extensionObject)
    {
      throw new NotImplementedException();
    }

    internal void Set(DataValue[] dataValue)
    {
      throw new NotImplementedException();
    }

    internal void Set(LocalizedText[] localizedText)
    {
      throw new NotImplementedException();
    }

    internal void Set(StatusCode[] statusCode)
    {
      throw new NotImplementedException();
    }

    internal void Set(NodeId[] nodeId)
    {
      throw new NotImplementedException();
    }

    internal void Set(XmlElement xmlElement)
    {
      throw new NotImplementedException();
    }
    internal void Set(DateTime dateTime)
    {
      throw new NotImplementedException();
    }
    internal void Set(double value)
    {
      throw new NotImplementedException();
    }
    internal void Set(ulong value)
    {
      throw new NotImplementedException();
    }
    internal void Set(uint value)
    {
      throw new NotImplementedException();
    }
    internal void Set(ushort value)
    {
      throw new NotImplementedException();
    }
    internal void Set(byte value)
    {
      throw new NotImplementedException();
    }
    internal void Set(bool value)
    {
      throw new NotImplementedException();
    }
    internal void Set(NodeId nodeId)
    {
      throw new NotImplementedException();
    }
    internal void Set(QualifiedName qualifiedName)
    {
      throw new NotImplementedException();
    }
    internal void Set(StatusCode statusCode)
    {
      throw new NotImplementedException();
    }
    internal void Set(ExpandedNodeId expandedNodeId)
    {
      throw new NotImplementedException();
    }
    internal void Set(LocalizedText localizedText)
    {
      throw new NotImplementedException();
    }
    internal void Set(ExtensionObject extensionObject)
    {
      throw new NotImplementedException();
    }
    internal void Set(DataValue dataValue)
    {
      throw new NotImplementedException();
    }
  }
  internal class NodeId { }
  internal class ExpandedNodeId { }
  internal class StatusCode { }
  class QualifiedName
  {

  }
  class LocalizedText
  {

  }
  class ExtensionObject
  {

  }
  class DataValue
  {

  }
  class Matrix
  {
    private Array array;
    private BuiltInType builtInType;
    private int[] v;

    public Matrix(Array array, BuiltInType builtInType)
    {
      this.array = array;
      this.builtInType = builtInType;
    }

    public Matrix(Array array, BuiltInType builtInType, int[] value)
    {
      this.array = array;
      this.builtInType = builtInType;
      this.v = v;
    }
  }
}
