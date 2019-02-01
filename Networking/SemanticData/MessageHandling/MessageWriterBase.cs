//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System;
using System.Xml;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class MessageWriterBase - helper class that provides basic implementation of the <see cref="IMessageWriter"/>.
  /// </summary>
  public abstract class MessageWriterBase : MessageHandler, IMessageWriter, IBinaryEncoder
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
    public override ulong ContentMask
    {
      get;
      protected set;
    }
    /// <summary>
    /// Sends the data described by a data set collection to remote destination.
    /// </summary>
    /// <param name="producerBinding">Encapsulates functionality used by the <see cref="IMessageWriter" /> to collect all the data (data set items) required to prepare new message and send it over the network.</param>
    /// <param name="length">Number of items to be send used to calculate the length of the message.</param>
    /// <param name="contentMask">The content mask represented as unsigned number <see cref="UInt64" />. The order of the bits starting from the least significant
    /// bit matches the order of the data items within the data set.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="dataSelector">The data selector.</param>
    /// <param name="messageSequenceNumber">The message sequence number. A monotonically increasing sequence number assigned by the publisher to each message sent.</param>
    /// <param name="timeStamp">The time stamp - the time the Data was collected.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Impossible to convert null value
    /// or</exception>
    void IMessageWriter.Send
      (Func<int, IProducerBinding> producerBinding, ushort length, ulong contentMask, FieldEncodingEnum encoding, DataSelector dataSelector,
       ushort messageSequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion)
    {
      lock (this)
      {
        if (State.State != HandlerState.Operational)
          return;
        ContentMask = contentMask;
        CreateMessage(encoding, dataSelector.PublisherId, dataSelector.DataSetWriterId, length, messageSequenceNumber, timeStamp, configurationVersion);
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
    /// <param name="value">TThe unsigned <see cref="byte"/> to write.</param>
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
    /// <param name="producerId">The producer identifier.</param>
    /// <param name="dataSetWriterId">The data set writer identifier.</param>
    /// <param name="fieldCount">The field count.</param>
    /// <param name="sequenceNumber">The sequence number.</param>
    /// <param name="timeStamp">The time stamp.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    internal protected abstract void CreateMessage
      (FieldEncodingEnum encoding, Guid producerId, UInt16 dataSetWriterId, ushort fieldCount, ushort sequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion);
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
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Boolean)value);
          else
            m_UAEncoder.WriteArray<Boolean>(this, (Array)value, Write, BuiltInType.Boolean);
          break;
        case BuiltInType.SByte:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((SByte)value);
          else
            m_UAEncoder.WriteArray<SByte>(this, (Array)value, Write, BuiltInType.SByte);
          break;
        case BuiltInType.Byte:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Byte)value);
          else
            m_UAEncoder.WriteArray<Byte>(this, (Array)value, Write, BuiltInType.Byte);
          break;
        case BuiltInType.DateTime:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (DateTime)value);
          else
            m_UAEncoder.WriteArray<DateTime>(this, (Array)value, Write, BuiltInType.DateTime);
          break;
        case BuiltInType.Double:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Double)value);
          else
            m_UAEncoder.WriteArray<Double>(this, (Array)value, Write, BuiltInType.Double);
          break;
        case BuiltInType.Int16:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Int16)value);
          else
            m_UAEncoder.WriteArray<Int16>(this, (Array)value, Write, BuiltInType.Int16);
          break;
        case BuiltInType.Enumeration:
        case BuiltInType.Int32:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Int32)value);
          else
            m_UAEncoder.WriteArray<Int32>(this, (Array)value, Write, BuiltInType.Int32);
          break;
        case BuiltInType.Int64:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Int64)value);
          else
            m_UAEncoder.WriteArray<Int64>(this, (Array)value, Write, BuiltInType.Int64);
          break;
        case BuiltInType.Float:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((Single)value);
          else
            m_UAEncoder.WriteArray<Single>(this, (Array)value, Write, BuiltInType.Float);
          break;
        case BuiltInType.String:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (String)value);
          else
            m_UAEncoder.WriteArray<String>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.String);
          break;
        case BuiltInType.UInt16:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((UInt16)value);
          else
            m_UAEncoder.WriteArray<UInt16>(this, (Array)value, Write, BuiltInType.UInt16);
          break;
        case BuiltInType.UInt32:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((UInt32)value);
          else
            m_UAEncoder.WriteArray<UInt32>(this, (Array)value, Write, BuiltInType.UInt32);
          break;
        case BuiltInType.UInt64:
          if (producerBinding.Encoding.ValueRank < 0)
            Write((UInt64)value);
          else
            m_UAEncoder.WriteArray<UInt64>(this, (Array)value, Write, BuiltInType.UInt64);
          break;
        case BuiltInType.Guid:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (Guid)value);
          else
            m_UAEncoder.WriteArray<Guid>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.Guid);
          break;
        case BuiltInType.ByteString:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (byte[])value);
          else
            m_UAEncoder.WriteArray<byte[]>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.ByteString);
          break;
        case BuiltInType.XmlElement:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (XmlElement)value);
          else
            m_UAEncoder.WriteArray<XmlElement>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.XmlElement);
          break;
        case BuiltInType.NodeId:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (INodeId)value);
          else
            m_UAEncoder.WriteArray<INodeId>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.NodeId);
          break;
        case BuiltInType.ExpandedNodeId:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (IExpandedNodeId)value);
          else
            m_UAEncoder.WriteArray<IExpandedNodeId>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.ExpandedNodeId);
          break;
        case BuiltInType.StatusCode:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (IStatusCode)value);
          else
            m_UAEncoder.WriteArray<IStatusCode>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.StatusCode);
          break;
        case BuiltInType.QualifiedName:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (IQualifiedName)value);
          else
            m_UAEncoder.WriteArray<IQualifiedName>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.QualifiedName);
          break;
        case BuiltInType.LocalizedText:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (ILocalizedText)value);
          else
            m_UAEncoder.WriteArray<ILocalizedText>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.LocalizedText);
          break;
        case BuiltInType.ExtensionObject:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (IExtensionObject)value);
          else
            m_UAEncoder.WriteArray<IExtensionObject>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.ExtensionObject);
          break;
        case BuiltInType.DataValue:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (IDataValue)value);
          else
            m_UAEncoder.WriteArray<IDataValue>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.DataValue);
          break;
        case BuiltInType.Variant:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (IVariant)value);
          else
            m_UAEncoder.WriteArray<IVariant>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.Variant);
          break;
        case BuiltInType.DiagnosticInfo:
          if (producerBinding.Encoding.ValueRank < 0)
            m_UAEncoder.Write(this, (IDiagnosticInfo)value);
          else
            m_UAEncoder.WriteArray<IDiagnosticInfo>(this, (Array)value, x => m_UAEncoder.Write(this, x), BuiltInType.DiagnosticInfo);
          break;
        case BuiltInType.Null:
        default:
          throw new ArgumentOutOfRangeException($"Impossible to convert {value} of type {producerBinding.Encoding}");
      }
    }
    private void WriteValueVariant(IProducerBinding producerBinding)
    {
      object value = producerBinding.GetFromRepository();
      Variant _variant = new Variant(producerBinding.Encoding, value);
      m_UAEncoder.Write(this, _variant);
    }
    private void WriteDataValue(IProducerBinding _pb)
    {
      throw new NotImplementedException();
    }

    #endregion

  }

}
