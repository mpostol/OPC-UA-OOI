
using System;
using System.Xml;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class MessageWriterBase - helper class that provides basic implementation of the <see cref="IMessageWriter"/>.
  /// </summary>
  public abstract class MessageWriterBase : IMessageWriter, IBinaryEncoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageWriterBase"/> class providing basic implementation of the <see cref="IMessageWriter"/>.
    /// </summary>
    /// <param name="uaEncoder">The ua encoder.</param>
    public MessageWriterBase(IUAEncoder uaEncoder)
    {
      if (uaEncoder == null)
        throw new ArgumentNullException(nameof(uaEncoder));
      m_UAEncoder = uaEncoder;
    }
    #endregion

    #region IMessageWriter
    /// <summary>
    /// Gets the content mask. The content mast read from the message or provided by the writer.
    /// The order of the bits starting from the least significant bit matches the order of the data items
    /// within the data set.
    /// </summary>
    /// <value>The content mask represented as unsigned number <see cref="UInt64" />. The order of the bits starting from the least significant
    /// bit matches the order of the data items within the data set.</value>
    public ulong ContentMask
    {
      get;
      private set;
    }
    /// <summary>
    /// Sends the data described by a data set collection to remote destination.
    /// </summary>
    /// <param name="producerBinding">Encapsulates functionality used by the <see cref="IMessageWriter" /> to collect all the data (data set items) required to prepare new message and send it over the network.</param>
    /// <param name="length">Number of items to be send used to calculate the length of the message.</param>
    /// <param name="contentMask">The content mask represented as unsigned number <see cref="UInt64" />. The order of the bits starting from the least significant
    /// bit matches the order of the data items within the data set.</param>
    /// <param name="semanticData">An instance of <see cref="ISemanticData" /> that represents a data item conforming to the UA Semantic Data paradigm.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Impossible to convert null value
    /// or</exception>
    void IMessageWriter.Send(Func<int, IProducerBinding> producerBinding, int length, ulong contentMask, ISemanticData semanticData)
    {
      if (State.State != HandlerState.Operational)
        return;
      ContentMask = contentMask;
      CreateMessage(length, semanticData.Guid);
      UInt64 _mask = 0x1;
      for (int i = 0; i < length; i++)
      {
        if ((ContentMask & _mask) > 0)
        {
          IProducerBinding _pb = producerBinding(i);
          WriteValue(_pb);
        }
        _mask = _mask << 1;
      }
      SendMessage();
    }
    /// <summary>
    /// If implemented in derived class gets the state machine for this instance.
    /// </summary>
    /// <value>An object of <see cref="IAssociationState" /> providing implementation of the state machine governing this instance behavior.</value>
    public abstract IAssociationState State
    {
      get;
      protected set;
    }
    /// <summary>
    /// Attaches to network - initialize the underlying protocol stack and establish the connection with the broker is applicable.
    /// </summary>
    /// <remarks>Depending on the message transport layer type implementation of this function varies.</remarks>
    public abstract void AttachToNetwork();
    #endregion

    #region IBinaryEncoder
    public abstract void WriteUInt64(ulong value);
    public abstract void WriteUInt32(uint value);
    public abstract void WriteUInt16(ushort value);
    public abstract void WriteString(string value);
    public abstract void WriteSingle(float value);
    public abstract void WriteSByte(sbyte value);
    public abstract void WriteInt64(long value);
    public abstract void WriteInt32(int value);
    public abstract void WriteInt16(short value);
    public abstract void WriteDouble(double value);
    public abstract void WriteBoolean(bool value);
    /// <summary>
    /// Writes a <see cref="Guid"/> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    public void WriteGuid(Guid value)
    {
      m_UAEncoder.WriteGuid(this, value);
    }
    public abstract void WriteBytes(byte[] value);
    /// <summary>
    /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
    /// </summary>
    /// <param name="value">TThe unsigned <see cref="byte"/> to write./param>
    public abstract void WriteByte(byte value);
    #endregion

    #region private
    protected abstract void CreateMessage(int length, Guid dataSetId);
    protected abstract void SendMessage();
    private IUAEncoder m_UAEncoder;
    private void WriteValue(IProducerBinding _pb)
    {
      object value = _pb.GetFromRepository();
      switch (_pb.Encoding)
      {
        case BuiltInType.Boolean:
          WriteBoolean((Boolean)value);
          break;
        case BuiltInType.SByte:
          WriteSByte((SByte)value);
          break;
        case BuiltInType.Byte:
          WriteByte((Byte)value);
          break;
        case BuiltInType.DateTime:
          m_UAEncoder.WriteDateTime(this, (DateTime)value);
          break;
        case BuiltInType.Double:
          WriteDouble((Double)value);
          break;
        case BuiltInType.Int16:
          WriteInt16((Int16)value);
          break;
        case BuiltInType.Enumeration:
        case BuiltInType.Int32:
          WriteInt32((Int32)value);
          break;
        case BuiltInType.Int64:
          WriteInt64((Int64)value);
          break;
        case BuiltInType.Float:
          WriteSingle((Single)value);
          break;
        case BuiltInType.String:
          WriteString((String)value);
          break;
        case BuiltInType.UInt16:
          WriteUInt16((UInt16)value);
          break;
        case BuiltInType.UInt32:
          WriteUInt32((UInt32)value);
          break;
        case BuiltInType.UInt64:
          WriteUInt64((UInt64)value);
          break;
        case BuiltInType.Guid:
          WriteGuid((Guid)value);
          break;
        case BuiltInType.ByteString:
          m_UAEncoder.WriteByteString(this, (byte[])value);
          break;
        case BuiltInType.XmlElement:
          m_UAEncoder.WriteXmlElement(this, (XmlElement)value);
          break;
        case BuiltInType.NodeId:
          m_UAEncoder.WriteNodeId(this, (INodeId)value);
          break;
        case BuiltInType.ExpandedNodeId:
          m_UAEncoder.WriteExpandedNodeId(this, (IExpandedNodeId)value);
          break;
        case BuiltInType.StatusCode:
          m_UAEncoder.WriteStatusCode(this, (IStatusCode)value);
          break;
        case BuiltInType.QualifiedName:
          m_UAEncoder.WriteQualifiedName(this, (IQualifiedName)value);
          break;
        case BuiltInType.LocalizedText:
          m_UAEncoder.WriteLocalizedText(this, (ILocalizedText)value);
          break;
        case BuiltInType.ExtensionObject:
          m_UAEncoder.WriteExtensionObject(this, (IExtensionObject)value);
          break;
        case BuiltInType.DataValue:
          m_UAEncoder.WriteDataValue(this, (IDataValue)value);
          break;
        case BuiltInType.Variant:
          m_UAEncoder.WriteVariant(this, (IVariant)value);
          break;
        case BuiltInType.DiagnosticInfo:
          m_UAEncoder.WriteDiagnosticInfo(this, (IDiagnosticInfo)value);
          break;
        case BuiltInType.Null:
        default:
          throw new ArgumentOutOfRangeException($"Impossible to convert {value} of type {_pb.Encoding}");
      }
    }
    #endregion

  }

}
