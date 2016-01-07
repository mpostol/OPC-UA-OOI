
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
    /// <param name="encoding">The encoding.</param>
    /// <param name="dataSetWriterId">The data set identifier.</param>
    /// <param name="messageSequenceNumber">The message sequence number. A monotonically increasing sequence number assigned by the publisher to each message sent.</param>
    /// <param name="timeStamp">The time stamp - the time the Data was collected.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Impossible to convert null value
    /// or</exception>
    void IMessageWriter.Send
      (Func<int, IProducerBinding> producerBinding, ushort length, ulong contentMask, FieldEncodingEnum encoding, UInt16 dataSetWriterId, ushort messageSequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion)
    {
      lock (this)
      {
        if (State.State != HandlerState.Operational)
          return;
        ContentMask = contentMask;

        CreateMessage(encoding, dataSetWriterId, length, messageSequenceNumber, timeStamp, configurationVersion);
        //UInt64 _mask = 0x1;
        for (int i = 0; i < length; i++)
        {
          //TODO: Implement ContentMask https://github.com/mpostol/OPC-UA-OOI/issues/89
          //if ((ContentMask & _mask) > 0)
          //{
          IProducerBinding _pb = producerBinding(i);
          switch (encoding)
          {
            case FieldEncodingEnum.VariantFieldEncoding:
              WriteValueVariant(_pb);
              break;
            case FieldEncodingEnum.CompressedFieldEncoding:
              WriteValue(_pb);
              break;
            case FieldEncodingEnum.DataValueFieldEncoding:
              WriteDataValue(_pb);
              break;
          }
          //}
          //_mask = _mask << 1;
        }
        SendMessage();
      }
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
    public abstract void Write(ulong value);
    public abstract void Write(uint value);
    public abstract void Write(ushort value);
    public abstract void Write(float value);
    public abstract void Write(sbyte value);
    public abstract void Write(long value);
    public abstract void Write(int value);
    public abstract void Write(short value);
    public abstract void Write(double value);
    public abstract void Write(bool value);
    /// <summary>
    /// Writes a <see cref="Guid"/> to the current stream as a 16-element byte array that contains the value and advances the stream position by 16 bytes.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    public void Write(Guid value)
    {
      m_UAEncoder.Write(this, value);
    }
    public abstract void Write(byte[] value);
    /// <summary>
    /// Writes an unsigned byte to the current stream and advances the stream position by one byte.
    /// </summary>
    /// <param name="value">TThe unsigned <see cref="byte"/> to write./param>
    public abstract void Write(byte value);
    public void Write(DateTime value)
    {
      m_UAEncoder.Write(this, value);
    }
    #endregion

    #region private
    //types
    private class Variant : IVariant
    {
      public Variant(UATypeInfo typeInfo, object value)
      {
        switch (typeInfo.BuiltInType)
        {
          case BuiltInType.Null:
            throw new ArgumentOutOfRangeException(nameof(typeInfo), "Null is not permitted in the Variant");
          case BuiltInType.Boolean:
          case BuiltInType.SByte:
          case BuiltInType.Byte:
          case BuiltInType.Int16:
          case BuiltInType.UInt16:
          case BuiltInType.Int32:
          case BuiltInType.UInt32:
          case BuiltInType.Int64:
          case BuiltInType.UInt64:
          case BuiltInType.Float:
          case BuiltInType.Double:
          case BuiltInType.String:
          case BuiltInType.DateTime:
            if (value == null)
              throw new NullReferenceException("Value type cannot be null.");
            break;
          default:
            break;
        }
        UATypeInfo = typeInfo;
        Value = value;
      }
      public UATypeInfo UATypeInfo
      {
        get; private set;
      }
      public object Value
      {
        get; private set;
      }
    }
    //vars
    private IUAEncoder m_UAEncoder;
    //methods
    /// <summary>
    /// Creates the message.
    /// </summary>
    /// <param name="encoding">The selected encoding for the message.</param>
    /// <param name="dataSetWriterId">The data set writer identifier.</param>
    /// <param name="fieldCount">The field count.</param>
    /// <param name="sequenceNumber">The sequence number.</param>
    /// <param name="timeStamp">The time stamp.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    internal protected abstract void CreateMessage
      (FieldEncodingEnum encoding, UInt16 dataSetWriterId, ushort fieldCount, ushort sequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion);
    /// <summary>
    /// Finalize preparation and sends the message.
    /// </summary>
    protected abstract void SendMessage();
    private void WriteValue(IProducerBinding producerBinding)
    {
      object value = producerBinding.GetFromRepository();
      switch (producerBinding.Encoding.BuiltInType)
      {
        case BuiltInType.Boolean:
          Write((Boolean)value);
          break;
        case BuiltInType.SByte:
          Write((SByte)value);
          break;
        case BuiltInType.Byte:
          Write((Byte)value);
          break;
        case BuiltInType.DateTime:
          m_UAEncoder.Write(this, (DateTime)value);
          break;
        case BuiltInType.Double:
          Write((Double)value);
          break;
        case BuiltInType.Int16:
          Write((Int16)value);
          break;
        case BuiltInType.Enumeration:
        case BuiltInType.Int32:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Int32)value);
          else
            m_UAEncoder.WriteArray<Int32>(this, (Array)value, Write, BuiltInType.Int32);
          break;
        case BuiltInType.Int64:
          Write((Int64)value);
          break;
        case BuiltInType.Float:
          Write((Single)value);
          break;
        case BuiltInType.String:
          m_UAEncoder.Write(this, (String)value);
          break;
        case BuiltInType.UInt16:
          Write((UInt16)value);
          break;
        case BuiltInType.UInt32:
          Write((UInt32)value);
          break;
        case BuiltInType.UInt64:
          Write((UInt64)value);
          break;
        case BuiltInType.Guid:
          Write((Guid)value);
          break;
        case BuiltInType.ByteString:
          m_UAEncoder.Write(this, (byte[])value);
          break;
        case BuiltInType.XmlElement:
          m_UAEncoder.Write(this, (XmlElement)value);
          break;
        case BuiltInType.NodeId:
          m_UAEncoder.Write(this, (INodeId)value);
          break;
        case BuiltInType.ExpandedNodeId:
          m_UAEncoder.Write(this, (IExpandedNodeId)value);
          break;
        case BuiltInType.StatusCode:
          m_UAEncoder.Write(this, (IStatusCode)value);
          break;
        case BuiltInType.QualifiedName:
          m_UAEncoder.Write(this, (IQualifiedName)value);
          break;
        case BuiltInType.LocalizedText:
          m_UAEncoder.Write(this, (ILocalizedText)value);
          break;
        case BuiltInType.ExtensionObject:
          m_UAEncoder.Write(this, (IExtensionObject)value);
          break;
        case BuiltInType.DataValue:
          m_UAEncoder.Write(this, (IDataValue)value);
          break;
        case BuiltInType.Variant:
          m_UAEncoder.Write(this, (IVariant)value);
          break;
        case BuiltInType.DiagnosticInfo:
          m_UAEncoder.Write(this, (IDiagnosticInfo)value);
          break;
        case BuiltInType.Null:
        default:
          throw new ArgumentOutOfRangeException($"Impossible to convert {value} of type {producerBinding.Encoding}");
      }
    }
    private void WriteValueVariant(IProducerBinding producerBinding)
    {
      object value = producerBinding.GetFromRepository();
      Variant _variant = new Variant(new UATypeInfo(producerBinding.Encoding), value);
      m_UAEncoder.Write(this, _variant);
    }
    private void WriteDataValue(IProducerBinding _pb)
    {
      throw new NotImplementedException();
    }

    #endregion

  }

}
