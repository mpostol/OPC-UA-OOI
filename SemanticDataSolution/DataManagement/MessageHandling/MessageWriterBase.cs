
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class MessageWriterBase - helper class that provides basic implementation of the <see cref="IMessageWriter"/>
  /// </summary>
  public abstract class MessageWriterBase : IMessageWriter
  {

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
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Impossible to convert null value
    /// or
    /// </exception>
    void IMessageWriter.Send(Func<int, IProducerBinding> producerBinding, int length, ulong contentMask)
    {
      if (State.State != HandlerState.Operational)
        return;
      ContentMask = contentMask;
      CreateMessage(length);
      UInt64 _mask = 0x1;
      for (int i = 0; i < length; i++)
      {
        if ((ContentMask & _mask) > 0)
        {
          IProducerBinding _pb = producerBinding(i);
          object _value = _pb.GetFromRepository();
          if (_value == null)
            throw new ArgumentOutOfRangeException("Impossible to convert null value");
          Type _type = _value.GetType();
          if (!IsValueIConvertible(_value, _pb.Parameter))
            throw new ArgumentOutOfRangeException(string.Format("Impossible to convert {0}", _value));
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

    #region private

    #region Writer
    protected abstract void WriteUInt64(ulong value, object parameter);
    protected abstract void WriteUInt32(uint value, object parameter);
    protected abstract void WriteUInt16(ushort value, object parameter);
    protected abstract void WriteString(string value, object parameter);
    protected abstract void WriteSingle(float value, object parameter);
    protected abstract void WriteSByte(sbyte value, object parameter);
    protected abstract void WriteInt64(long value, object parameter);
    protected abstract void WriteInt32(int value, object parameter);
    protected abstract void WriteInt16(short value, object parameter);
    protected abstract void WriteDouble(double value, object parameter);
    protected abstract void WriteDecimal(decimal value, object parameter);
    protected abstract void WriteDateTime(DateTime dateTime, object parameter);
    protected abstract void WriteByte(byte value, object parameter);
    protected abstract void WriteBool(bool value, object parameter);
    protected abstract void WriteChar(char value, object parameter);
    #endregion

    protected abstract void CreateMessage(int length);
    protected abstract void SendMessage();
    private bool IsValueIConvertible(object value, object parameter)
    {
      IConvertible _cv = value as IConvertible;
      if (_cv == null)
        return false;
      switch (_cv.GetTypeCode())
      {
        case TypeCode.Boolean:
          WriteBool((Boolean)value, parameter);
          break;
        case TypeCode.Byte:
          WriteByte((Byte)value, parameter);
          break;
        case TypeCode.Char:
          WriteChar((Char)value, parameter);
          break;
        case TypeCode.DBNull:
          throw new ArgumentOutOfRangeException("the value cannot be TypeCode.DBNull");
        case TypeCode.DateTime:
          WriteDateTime((DateTime)value, parameter);
          break;
        case TypeCode.Decimal:
          WriteDecimal((Decimal)value, parameter);
          break;
        case TypeCode.Double:
          WriteDouble((Double)value, parameter);
          break;
        case TypeCode.Empty:
          throw new ArgumentOutOfRangeException("the value cannot be TypeCode.Empty");
        case TypeCode.Int16:
          WriteInt16((Int16)value, parameter);
          break;
        case TypeCode.Int32:
          WriteInt32((Int32)value, parameter);
          break;
        case TypeCode.Int64:
          WriteInt64((Int64)value, parameter);
          break;
        case TypeCode.Object:
          return false;
        case TypeCode.SByte:
          WriteSByte((SByte)value, parameter);
          break;
        case TypeCode.Single:
          WriteSingle((Single)value, parameter);
          break;
        case TypeCode.String:
          WriteString((String)value, parameter);
          break;
        case TypeCode.UInt16:
          WriteUInt16((UInt16)value, parameter);
          break;
        case TypeCode.UInt32:
          WriteUInt32((UInt32)value, parameter);
          break;
        case TypeCode.UInt64:
          WriteUInt64((UInt64)value, parameter);
          break;
        default:
          return false;
      }
      return true;
    }

    #endregion

  }
}
