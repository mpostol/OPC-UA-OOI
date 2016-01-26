
using System;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.MessageHandling
{

  /// <summary>
  /// Class MessageReaderBase - helper class providing basic implementation of the <see cref="IMessageReader"/> interface
  /// </summary>
  public abstract class MessageReaderBase : IMessageReader, IBinaryDecoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageReaderBase"/> class providing basic implementation of the <see cref="IMessageReader"/> interface.
    /// </summary>
    /// <param name="uaDecoder">The decoder that provides methods to be used to decode OPC UA Built-in types.</param>
    public MessageReaderBase(IUADecoder uaDecoder)
    {
      if (uaDecoder == null)
        throw new ArgumentNullException(nameof(uaDecoder));
      m_UADecoder = uaDecoder;
      m_ReadValueDelegate = ReadValueVariant;
    }
    #endregion

    #region IMessageReader
    /// <summary>
    /// Gets the content mask. The content mast read from the message or provided by the writer.
    /// The order of the bits starting from the least significant bit matches the order of the data items
    /// within the data set.
    /// </summary>
    /// <remarks>Must be added to teh message.</remarks>
    /// <value>The content mask is represented as unsigned number <see cref="UInt64" />. 
    /// The value is provided by the message.
    /// The order of the bits starting from the least significant bit matches the order of the data items within the data set.
    /// </value>
    public abstract ulong ContentMask { get; } //TODO Must be added to the message.
    /// <summary>
    ///  If implemented in derived class gets the state machine for this instance.
    /// </summary>
    /// <value>An object of <see cref="IAssociationState" /> providing implementation of the state machine governing this instance behavior.</value>
    public abstract IAssociationState State
    {
      get;
      protected set;
    }
    /// <summary>
    /// Attaches this instance to the network - initialize the underlying protocol stack and establish the connection with the broker if applicable.
    /// </summary>
    /// <remarks>Depending on the message transport layer type implementation of this function varies.</remarks>
    public abstract void AttachToNetwork();
    /// <summary>
    /// Occurs when an asynchronous operation to read a new message completes.
    /// </summary>
    public event EventHandler<MessageEventArg> ReadMessageCompleted;
    /// <summary>
    /// Updates my values using inverse of control pattern.
    /// </summary>
    /// <param name="update">Captures a delegated used to update the consumer variables using values decoded form the message.</param>
    /// <param name="length">Number of items in the data set.</param>
    void IMessageReader.UpdateMyValues(Func<int, IConsumerBinding> update, int length)
    {
      //UInt64 _mask = 0x1;
      for (int i = 0; i < length; i++)
      {
        if (EndOfMessage())
        {
          Trace($"Unexpected end of message while reading #{i} element.");
          break;
        }
        //TODO: Implement ContentMask https://github.com/mpostol/OPC-UA-OOI/issues/89
        //if ((ContentMask & _mask) > 0)
        //{
        IConsumerBinding _binding = update(i);
        switch (MessageHeader.FieldsEncoding)
        {
          case FieldEncodingEnum.VariantFieldEncoding:
            ReadValueVariant(_binding);
            break;
          case FieldEncodingEnum.CompressedFieldEncoding:
            ReadValue(_binding);
            break;
          case FieldEncodingEnum.DataValueFieldEncoding:
            ReadDataValue(_binding);
            break;
        }
        //}
        //_mask = _mask << 1;
      }
    }
    #endregion

    #region IBinaryDecoder
    public abstract UInt64 ReadUInt64();
    public abstract UInt32 ReadUInt32();
    public abstract UInt16 ReadUInt16();
    public abstract String ReadString();
    public abstract Single ReadSingle();
    public abstract SByte ReadSByte();
    public abstract Int64 ReadInt64();
    public abstract Int32 ReadInt32();
    public abstract Int16 ReadInt16();
    public abstract Double ReadDouble();
    public abstract char ReadChar();
    public abstract Byte ReadByte();
    public abstract Boolean ReadBoolean();
    public abstract byte[] ReadBytes(int count);
    public DateTime ReadDateTime()
    {
      return m_UADecoder.ReadDateTime(this);
    }
    public Guid ReadGuid()
    {
      return m_UADecoder.ReadGuid(this);
    }
    #endregion

    #region private
    //vars
    private IUADecoder m_UADecoder;
    private Action<IConsumerBinding> m_ReadValueDelegate = null;
    /// <summary>
    /// Signals the end of message.
    /// </summary>
    /// <returns><c>true</c> if there is end of message condition, <c>false</c> otherwise.</returns>
    protected abstract bool EndOfMessage();
    /// <summary>
    /// If implemented by the derived class traces the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    protected abstract void Trace(string message);
    /// <summary>
    /// Gets the publisher identifier.
    /// </summary>
    /// <value>The publisher identifier.</value>
    protected abstract Guid PublisherId { get; }
    //methods
    /// <summary>
    /// Gets the message header.
    /// </summary>
    /// <value>The message header <see cref="MessageHeader"/>.</value>
    protected abstract MessageHeader MessageHeader { get; }
    /// <summary>
    /// Raises the read message completed event.
    /// </summary>
    protected void RaiseReadMessageCompleted(UInt16 dataSetId)
    {
      if (this.State.State != HandlerState.Operational)
        return;
      ReadMessageCompleted?.Invoke(this, new MessageEventArg(this, dataSetId, PublisherId));
    }
    private void ReadValue(IConsumerBinding consumerBinding)
    {
      object _value = null;
      switch (consumerBinding.Encoding.BuiltInType)
      {
        case BuiltInType.Boolean:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadBoolean();
          else
            _value = m_UADecoder.ReadArray(this, ReadBoolean, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.SByte:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadSByte();
          else
            _value = m_UADecoder.ReadArray(this, ReadSByte, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Byte:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadByte();
          else
            _value = m_UADecoder.ReadArray(this, ReadByte, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Int16:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadInt16();
          else
            _value = m_UADecoder.ReadArray(this, ReadInt16, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.UInt16:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadUInt16();
          else
            _value = m_UADecoder.ReadArray(this, ReadUInt16, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Int32:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadInt32();
          else
            _value = m_UADecoder.ReadArray(this, ReadInt32, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.UInt32:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadUInt32();
          else
            _value = m_UADecoder.ReadArray(this, ReadUInt32, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Int64:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadInt64();
          else
            _value = m_UADecoder.ReadArray(this, ReadInt64, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.UInt64:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadUInt64();
          else
            _value = m_UADecoder.ReadArray(this, ReadUInt64, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Float:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadSingle();
          else
            _value = m_UADecoder.ReadArray(this, ReadSingle, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Double:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = ReadDouble();
          else
            _value = m_UADecoder.ReadArray(this, ReadDouble, consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.String:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadString(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadString(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.DateTime:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadDateTime(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadDateTime(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Guid:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadGuid(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadGuid(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.ByteString:
          if (consumerBinding.Encoding.ValueRank < 0)
            m_UADecoder.ReadByteString(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadByteString(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.XmlElement:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadXmlElement(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadXmlElement(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.NodeId:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadNodeId(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadNodeId(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.ExpandedNodeId:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadExpandedNodeId(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadExpandedNodeId(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.StatusCode:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadStatusCode(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadStatusCode(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.QualifiedName:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadQualifiedName(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadQualifiedName(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.LocalizedText:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadLocalizedText(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadLocalizedText(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.ExtensionObject:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadExtensionObject(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadExtensionObject(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.DataValue:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadDataValue(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadDataValue(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.Variant:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadVariant(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadVariant(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        case BuiltInType.DiagnosticInfo:
          if (consumerBinding.Encoding.ValueRank < 0)
            _value = m_UADecoder.ReadDiagnosticInfo(this);
          else
            _value = m_UADecoder.ReadArray(this, () => m_UADecoder.ReadDiagnosticInfo(this), consumerBinding.Encoding.ValueRank > 1);
          break;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Impossible to convert the type {0}", consumerBinding.Encoding));
      }
      consumerBinding.Assign2Repository(_value);
    }
    private void ReadValueVariant(IConsumerBinding consumerBinding)
    {
      IVariant _ret = m_UADecoder.ReadVariant(this);
      AssertTypeMach(_ret.UATypeInfo, consumerBinding.Encoding);
      consumerBinding.Assign2Repository(_ret.Value);
    }
    private void ReadDataValue(IConsumerBinding _binding)
    {
      throw new NotImplementedException();
    }
    private void AssertTypeMach(UATypeInfo uATypeInfo, UATypeInfo encoding)
    {
      //TODO MessageReaderBase.AssertTypeMach - must be implemented
    }
    #endregion

  }

}
