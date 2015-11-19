
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class MessageReaderBase - helper class providing basic implementation of the <see cref="IMessageReader"/> interface
  /// </summary>
  public abstract class MessageReaderBase : IMessageReader
  {

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
    public abstract ulong ContentMask { get; } //TODO Must be added to teh message.
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
          Read(_binding);
        }
        _mask = _mask << 1;
      }
    }
    #endregion

    #region private

    #region Reader
    protected abstract UInt64 ReadUInt64();
    protected abstract UInt32 ReadUInt32();
    protected abstract UInt16 ReadUInt16();
    protected abstract String ReadString();
    protected abstract Single ReadSingle();
    protected abstract SByte ReadSByte();
    protected abstract Int64 ReadInt64();
    protected abstract Int32 ReadInt32();
    protected abstract Int16 ReadInt16();
    protected abstract Double ReadDouble();
    protected abstract char ReadChar();
    public abstract Byte ReadByte();
    protected abstract Boolean ReadBoolean();
    protected abstract DateTime ReadDateTime();
    /// <summary>
    /// Reads the decimal.
    /// </summary>
    /// <remarks>Only OPC UA types are allowed. To be removed.</remarks>
    /// <returns>Decimal.</returns>
    protected abstract Decimal ReadDecimal();
    public abstract Guid ReadGuid();
    #endregion

    /// <summary>
    /// Gets the message header.
    /// </summary>
    /// <value>The message header <see cref="MessageHeader"/>.</value>
    public abstract MessageHeader MessageHeader { get; }
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
    private void Read(IConsumerBinding binding)
    {
      if (!IsValueIConvertible(binding))
        throw new ArgumentOutOfRangeException(string.Format("Impossible to convert the type {0}", binding.TargetType));
    }
    private bool IsValueIConvertible(IConsumerBinding binding)
    {
      object _value = null;
      switch (binding.TargetType)
      {
        case BuiltInType.Null:
          return false;
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
        case BuiltInType.XmlElement:
        case BuiltInType.NodeId:
        case BuiltInType.ExpandedNodeId:
        case BuiltInType.StatusCode:
        case BuiltInType.QualifiedName:
        case BuiltInType.LocalizedText:
        case BuiltInType.ExtensionObject:
        case BuiltInType.DataValue:
        case BuiltInType.Variant:
        case BuiltInType.DiagnosticInfo:
        default:
          return false;
      }
      binding.Assign2Repository(_value);
      return true;
    }
    #endregion

  }

}
