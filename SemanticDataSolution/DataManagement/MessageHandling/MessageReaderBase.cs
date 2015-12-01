
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
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
    /// Check if the message destination is the data set described by the <paramref name="dataId" /> of type <see cref="ISemanticData" />.
    /// </summary>
    /// <param name="dataId">The data identifier <see cref="ISemanticData" />.</param>
    /// <returns><c>true</c> if <paramref name="dataId" /> is the destination of the message, <c>false</c> otherwise.</returns>
    bool IMessageReader.IAmDestination(ISemanticData dataId)
    {
      return dataId.Guid == MessageHeader.DataSetId;
    }
    /// <summary>
    /// Updates my values using inverse of control pattern.
    /// </summary>
    /// <param name="update">Captures a delegated used to update the consumer variables using values decoded form the message.</param>
    /// <param name="length">Number of items in the data set.</param>
    void IMessageReader.UpdateMyValues(Func<int, IConsumerBinding> update, int length)
    {
      UInt64 _mask = 0x1;
      for (int i = 0; i < length; i++)
      {
        if ((ContentMask & _mask) > 0)
        {
          IConsumerBinding _binding = update(i);
          m_ReadValueDelegate(_binding);
        }
        _mask = _mask << 1;
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
    //methods
    /// <summary>
    /// Gets the message header.
    /// </summary>
    /// <value>The message header <see cref="MessageHeader"/>.</value>
    protected abstract MessageHeader MessageHeader { get; }
    /// <summary>
    /// Raises the read message completed event.
    /// </summary>
    protected void RaiseReadMessageCompleted()
    {
      if (this.State.State != HandlerState.Operational)
        return;
      EventHandler<MessageEventArg> _handler = ReadMessageCompleted;
      if (_handler == null)
        return;
      ReadMessageCompleted(this, new MessageEventArg(this));
    }
    private void ReadValue(IConsumerBinding consumerBinding)
    {
      object _value = null;
      switch (consumerBinding.Encoding)
      {
        case BuiltInType.Boolean:
          _value = ReadBoolean();
          break;
        case BuiltInType.SByte:
          _value = ReadSByte();
          break;
        case BuiltInType.Byte:
          _value = ReadByte();
          break;
        case BuiltInType.Int16:
          _value = ReadInt16();
          break;
        case BuiltInType.UInt16:
          _value = ReadUInt16();
          break;
        case BuiltInType.Int32:
          _value = ReadInt32();
          break;
        case BuiltInType.UInt32:
          _value = ReadUInt32();
          break;
        case BuiltInType.Int64:
          _value = ReadInt64();
          break;
        case BuiltInType.UInt64:
          _value = ReadUInt64();
          break;
        case BuiltInType.Float:
          _value = ReadSingle();
          break;
        case BuiltInType.Double:
          _value = ReadDouble();
          break;
        case BuiltInType.String:
          _value = ReadString();
          break;
        case BuiltInType.DateTime:
          _value = ReadDateTime();
          break;
        case BuiltInType.Guid:
          _value = ReadGuid();
          break;
        case BuiltInType.ByteString:
          m_UADecoder.ReadByteString(this);
          break;
        case BuiltInType.XmlElement:
          _value = m_UADecoder.ReadXmlElement(this);
          break;
        case BuiltInType.NodeId:
          _value = m_UADecoder.ReadNodeId(this);
          break;
        case BuiltInType.ExpandedNodeId:
          _value = m_UADecoder.ReadExpandedNodeId(this);
          break;
        case BuiltInType.StatusCode:
          _value = m_UADecoder.ReadStatusCode(this);
          break;
        case BuiltInType.QualifiedName:
          _value = m_UADecoder.ReadQualifiedName(this);
          break;
        case BuiltInType.LocalizedText:
          _value = m_UADecoder.ReadLocalizedText(this);
          break;
        case BuiltInType.ExtensionObject:
          _value = m_UADecoder.ReadExtensionObject(this);
          break;
        case BuiltInType.DataValue:
          _value = m_UADecoder.ReadDataValue(this);
          break;
        case BuiltInType.Variant:
          _value = m_UADecoder.ReadVariant(this);
          break;
        case BuiltInType.DiagnosticInfo:
          _value = m_UADecoder.ReadDiagnosticInfo(this);
          break;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Impossible to convert the type {0}", consumerBinding.Encoding));
      }
      consumerBinding.Assign2Repository(_value);
    }
    private void ReadValueVariant(IConsumerBinding consumerBinding)
    {
      IVariant _ret = m_UADecoder.ReadVariant(this);
      consumerBinding.Assign2Repository(_ret.Value);
    }
    #endregion

  }

}
