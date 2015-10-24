
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

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
    /// <value>The content mask is represented as unsigned number <see cref="UInt64" />. 
    /// The value is provided by the message.
    /// The order of the bits starting from the least significant bit matches the order of the data items within the data set.</value>
    public abstract ulong ContentMask { get; }
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
    /// Attaches to network - initialize the underlying protocol stack and establish the connection with the broker is applicable.
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
    public abstract bool IAmDestination(ISemanticData dataId);
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
    protected abstract Decimal ReadDecimal();
    public abstract Guid ReadGuid();
    #endregion

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
        throw new ArgumentOutOfRangeException(string.Format("Impossible to convert the type {0}", binding.TargetType.Name));
    }
    private bool IsValueIConvertible(IConsumerBinding binding)
    {
      object _value = null;
      System.IO.BinaryReader _r = null;
      switch (Type.GetTypeCode(binding.TargetType))
      {
        case TypeCode.Boolean:
          _value = ReadBoolean();
          break;
        case TypeCode.Byte:
          _value = ReadByte();
          break;
        case TypeCode.Char:
          _value = ReadChar();
          break;
        case TypeCode.DBNull:
          return false;
        case TypeCode.DateTime:
          _value = ReadDateTime();
          break;
        case TypeCode.Decimal:
          _value = ReadDecimal();
          break;
        case TypeCode.Double:
          _value = ReadDouble();
          break;
        case TypeCode.Empty:
          return false;
        case TypeCode.Int16:
          _value = ReadInt16();
          break;
        case TypeCode.Int32:
          _value = ReadInt32();
          break;
        case TypeCode.Int64:
          _value = ReadInt64();
          break;
        case TypeCode.Object:
          return false;
        case TypeCode.SByte:
          _value = ReadSByte();
          break;
        case TypeCode.Single:
          _value = ReadSingle();
          break;
        case TypeCode.String:
          _value = ReadString();
          break;
        case TypeCode.UInt16:
          _value = ReadUInt16();
          break;
        case TypeCode.UInt32:
          _value = ReadUInt32();
          break;
        case TypeCode.UInt64:
          _value = ReadUInt64();
          break;
        default:
          return false;
      }
      binding.Assign2Repository(_value);
      return true;
    }

    #endregion



  }

}
