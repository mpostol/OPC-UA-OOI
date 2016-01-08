
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Xml;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class MessageReaderBaseTestClass
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void CreatorTestMethod()
    {
      TestMessageReaderBase _bmw = new TestMessageReaderBase(Guid.NewGuid(), FieldEncodingEnum.VariantFieldEncoding);
      Assert.IsNotNull(_bmw);
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void ReadMessageVariantFieldEncodingTestMethod()
    {
      Guid _publisherId = Guid.NewGuid();
      TestMessageReaderBase _reader = new TestMessageReaderBase(_publisherId, FieldEncodingEnum.VariantFieldEncoding);
      _reader.AttachToNetwork();
      Assert.AreEqual<HandlerState>(_reader.State.State, HandlerState.Operational);
      object _sender = null;
      MessageEventArg _message = null;
      _reader.ReadMessageCompleted += (x, y) => { _sender = x; _message = y; };
      Assert.IsNull(_sender);
      Assert.IsNull(_message);
      _reader.GetMessageTest(123);
      Assert.IsNotNull(_sender);
      Assert.IsNotNull(_message);
      Assert.AreSame(_reader, _sender);
      Assert.AreSame(_reader, _message.MessageContent);
      Assert.AreEqual<UInt32>(123, _message.DataSetId);
      Assert.AreEqual<Guid>(_publisherId, _message.ProducerId);
      _message.MessageContent.UpdateMyValues(update, 1);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void ReadMessageCompressedFieldEncodingTestMethod()
    {
      Guid _publisherId = Guid.NewGuid();
      TestMessageReaderBase _reader = new TestMessageReaderBase(_publisherId, FieldEncodingEnum.CompressedFieldEncoding);
      _reader.AttachToNetwork();
      Assert.AreEqual<HandlerState>(_reader.State.State, HandlerState.Operational);
      object _sender = null;
      MessageEventArg _message = null;
      _reader.ReadMessageCompleted += (x, y) => { _sender = x; _message = y; };
      Assert.IsNull(_sender);
      Assert.IsNull(_message);
      _reader.GetMessageTest(123);
      Assert.IsNotNull(_sender);
      Assert.IsNotNull(_message);
      Assert.AreSame(_reader, _sender);
      Assert.AreSame(_reader, _message.MessageContent);
      Assert.AreEqual<UInt32>(123, _message.DataSetId);
      Assert.AreEqual<Guid>(_publisherId, _message.ProducerId);
      _message.MessageContent.UpdateMyValues(update, 1);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    [ExpectedException(typeof(NotImplementedException))]
    public void ReadMessageDataValueFieldEncodingTestMethod()
    {
      Guid _publisherId = Guid.NewGuid();
      TestMessageReaderBase _reader = new TestMessageReaderBase(_publisherId, FieldEncodingEnum.DataValueFieldEncoding);
      _reader.AttachToNetwork();
      Assert.AreEqual<HandlerState>(_reader.State.State, HandlerState.Operational);
      object _sender = null;
      MessageEventArg _message = null;
      _reader.ReadMessageCompleted += (x, y) => { _sender = x; _message = y; };
      Assert.IsNull(_sender);
      Assert.IsNull(_message);
      _reader.GetMessageTest(123);
      Assert.IsNotNull(_sender);
      Assert.IsNotNull(_message);
      Assert.AreSame(_reader, _sender);
      Assert.AreSame(_reader, _message.MessageContent);
      Assert.AreEqual<UInt32>(123, _message.DataSetId);
      Assert.AreEqual<Guid>(_publisherId, _message.ProducerId);
      _message.MessageContent.UpdateMyValues(update, 1);
    }
    #endregion

    #region private
    //types
    private class ConsumerBinding : IConsumerBinding
    {
      public IValueConverter Converter
      {
        set
        {
          throw new NotImplementedException();
        }
      }
      public CultureInfo Culture
      {
        set
        {
          throw new NotImplementedException();
        }
      }
      public UATypeInfo Encoding
      {
        get
        {
          return new UATypeInfo(BuiltInType.Byte);
        }
      }
      public object Parameter
      {
        get
        {
          throw new NotImplementedException();
        }
        set
        {
          throw new NotImplementedException();
        }
      }
      public void Assign2Repository(object value)
      {
        Assert.AreSame(typeof(byte), value.GetType());
        Assert.AreEqual<byte>(m_TestValue, (byte)value);
      }
      public void OnDisabling()
      {
        throw new NotImplementedException();
      }
      public void OnEnabling()
      {
        throw new NotImplementedException();
      }
    }
    private class TestMessageReaderBase : MessageReaderBase
    {

      #region creator
      public TestMessageReaderBase(Guid publisherId, FieldEncodingEnum encoding) : base(new UADecoder())
      {
        State = new MyState();
        m_PublisherId = publisherId;
        m_MessageHeader = new TestMessageHeader(encoding);
      }
      #endregion

      #region MessageReaderBase
      public override UInt64 ReadUInt64()
      {
        throw new NotImplementedException();
      }
      public override UInt32 ReadUInt32()
      {
        throw new NotImplementedException();
      }
      public override UInt16 ReadUInt16()
      {
        throw new NotImplementedException();
      }
      public override String ReadString()
      {
        throw new NotImplementedException();
      }
      public override Single ReadSingle()
      {
        throw new NotImplementedException();
      }
      public override SByte ReadSByte()
      {
        throw new NotImplementedException();
      }
      public override Int64 ReadInt64()
      {
        throw new NotImplementedException();
      }
      public override Int32 ReadInt32()
      {
        throw new NotImplementedException();
      }
      public override Int16 ReadInt16()
      {
        throw new NotImplementedException();
      }
      public override Double ReadDouble()
      {
        throw new NotImplementedException();
      }
      public override Char ReadChar()
      {
        throw new NotImplementedException();
      }
      public override Byte ReadByte()
      {
        return m_TestValue;
      }
      public override Boolean ReadBoolean()
      {
        throw new NotImplementedException();
      }
      public override ulong ContentMask
      {
        get
        {
          return ulong.MaxValue;
        }
      }
      public override byte[] ReadBytes(int count)
      {
        throw new NotImplementedException();
      }
      public override void AttachToNetwork()
      {
        Assert.AreNotEqual<HandlerState>(HandlerState.Operational, State.State);
        State.Enable();
        m_NumberOfAttachToNetwork++;
      }
      public override IAssociationState State
      {
        get;
        protected set;
      }
      protected override MessageHeader MessageHeader
      {
        get
        {
          return m_MessageHeader;
        }
      }
      protected override Guid PublisherId
      {
        get { return m_PublisherId; }
      }
      internal void GetMessageTest(UInt16 dataSetId)
      {
        RaiseReadMessageCompleted(dataSetId);
      }

      protected override bool EndOfMessage()
      {
        return false;
      }
      #endregion

      #region private
      //types
      private class UADecoder : IUADecoder
      {
        public Array ReadArray<type>(IBinaryDecoder decoder, Func<type> readValue, bool arrayDimensionsPresents)
        {
          throw new NotImplementedException();
        }
        public byte[] ReadByteString(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public IDataValue ReadDataValue(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public DateTime ReadDateTime(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public IExtensionObject ReadExtensionObject(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public Guid ReadGuid(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public ILocalizedText ReadLocalizedText(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public INodeId ReadNodeId(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public IQualifiedName ReadQualifiedName(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public IStatusCode ReadStatusCode(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public string ReadString(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        public IVariant ReadVariant(IBinaryDecoder decoder)
        {
          return new TestVariant();
        }
        public XmlElement ReadXmlElement(IBinaryDecoder decoder)
        {
          throw new NotImplementedException();
        }
        private class TestVariant : IVariant
        {
          public UATypeInfo UATypeInfo
          {
            get
            {
              return new UATypeInfo(BuiltInType.Byte);
            }
          }
          public object Value
          {
            get
            {
              return m_TestValue;
            }
          }
        }

      }
      private class MyState : IAssociationState
      {

        /// <summary>
        /// Initializes a new instance of the <see cref="MyState"/> class.
        /// </summary>
        public MyState()
        {
          State = HandlerState.Disabled;
        }
        /// <summary>
        /// Gets the current state <see cref="HandlerState" /> of the <see cref="Association" /> instance.
        /// </summary>
        /// <value>The state of <see cref="HandlerState" /> type.</value>
        public HandlerState State
        {
          get;
          private set;
        }
        /// <summary>
        /// This method is used to enable a configured <see cref="Association" /> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational" /> state.
        /// In the case of an error situation, the state changes into <see cref="HandlerState.Error" />. The operation is rejected if the current <see cref="State" />  is not <see cref="HandlerState.Disabled" />.
        /// </summary>
        /// <exception cref="System.ArgumentException">Wrong state</exception>
        public void Enable()
        {
          if (State != HandlerState.Disabled)
            throw new ArgumentException("Wrong state");
          State = HandlerState.Operational;
        }
        /// <summary>
        /// This method is used to disable an already enabled <see cref="Association" /> object.
        /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled" /> or <see cref="HandlerState.NoConfiguration" />.
        /// </summary>
        /// <exception cref="System.ArgumentException">Wrong state</exception>
        public void Disable()
        {
          if (State != HandlerState.Operational)
            throw new ArgumentException("Wrong state");
          State = HandlerState.Disabled;
        }
      }
      private class TestMessageHeader : MessageHandling.MessageHeader
      {
        private FieldEncodingEnum m_Encoding;
        public TestMessageHeader(FieldEncodingEnum encoding) : base()
        {
          m_Encoding = encoding;
        }
        public override ConfigurationVersionDataType ConfigurationVersion
        {
          get
          {
            throw new NotImplementedException();
          }

          internal set
          {
            throw new NotImplementedException();
          }
        }
        public override byte EncodingFlags
        {
          get
          {
            return Convert.ToByte((byte)m_Encoding | 0x01);
          }
        }
        public override ushort FieldCount
        {
          get
          {
            throw new NotImplementedException();
          }

          internal set
          {
            throw new NotImplementedException();
          }
        }
        public override uint MessageLength
        {
          get
          {
            throw new NotImplementedException();
          }
        }
        public override ushort MessageSequenceNumber
        {
          get
          {
            throw new NotImplementedException();
          }

          internal set
          {
            throw new NotImplementedException();
          }
        }
        public override MessageTypeEnum MessageType
        {
          get
          {
            throw new NotImplementedException();
          }
        }
        public override DateTime TimeStamp
        {
          get
          {
            throw new NotImplementedException();
          }

          internal set
          {
            throw new NotImplementedException();
          }
        }
        internal override void Synchronize()
        {
          throw new NotImplementedException();
        }
      }
      //vars
      private MessageHeader m_MessageHeader;
      private int m_NumberOfAttachToNetwork;
      private Guid m_PublisherId;
      #endregion

      #region test instrumentation

      protected override void Trace(string message)
      {
        throw new NotImplementedException();
      }
      #endregion

    }
    //vars
    private const byte m_TestValue = 0xca;
    //methods
    private IConsumerBinding update(int arg)
    {
      Assert.AreEqual<int>(0, arg);
      return new ConsumerBinding();
    }
    #endregion

  }

}
