
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  [TestClass]
  public class MessageReaderTestClass
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void CreatorTestMethod()
    {
      TestMessageReaderBase _bmw = new TestMessageReaderBase();
      Assert.IsNotNull(_bmw);
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void ReadMessageCompletedTestMethod()
    {
      TestMessageReaderBase _reader = new TestMessageReaderBase();
      _reader.AttachToNetwork();
      object _sender = null;
      MessageEventArg _message = null;
      _reader.ReadMessageCompleted += (x, y) => { _sender = x; _message = y; };
      Assert.IsNull(_sender);
      Assert.IsNull(_message);
      _reader.GetMessageTest(SemanticData.GetSemanticDataTest());
      Assert.IsNotNull(_sender);
      Assert.IsNotNull(_message);
      Assert.AreSame(_reader, _sender);
      Assert.AreSame(_reader, _message.MessageContent);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void IAmDestinationTestMethod()
    {
      TestMessageReaderBase _reader = new TestMessageReaderBase();
      _reader.AttachToNetwork();
      object _sender = null;
      MessageEventArg _message = null;
      _reader.ReadMessageCompleted += (x, y) => { _sender = x; _message = y; };
      Assert.IsNull(_message);
      SemanticData _id = SemanticData.GetSemanticDataTest();
      _reader.GetMessageTest(_id);
      Assert.IsNotNull(_message);
      Assert.IsTrue(_message.MessageContent.IAmDestination(_id));
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void BinaryMessageReaderTestMethod()
    {
      ISemanticData _semanticData = SemanticData.GetSemanticDataTest();
      BinaryMessageFrameDecoder _reader = new BinaryMessageFrameDecoder();
      Assert.IsNotNull(_reader);
      Assert.AreEqual<int>(0, _reader.m_NumberOfSentBytes);
      Assert.AreEqual<int>(0, _reader.m_NumberOfAttachToNetwork);
      Assert.AreEqual<int>(0, _reader.m_NumberOfSentMessages);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _reader.State.State);
      _reader.AttachToNetwork();
      Assert.AreEqual<HandlerState>(HandlerState.Operational, _reader.State.State);
      Assert.AreEqual<int>(1, _reader.m_NumberOfAttachToNetwork);
      Assert.AreEqual<int>(0, _reader.m_NumberOfSentBytes);
      Assert.AreEqual<int>(0, _reader.m_NumberOfSentMessages);
      MessageEventArg e = null;
      object[] _buffer = new object[CommonDefinitions.TestValues.Length];
      IConsumerBinding[] _bindings = new IConsumerBinding[_buffer.Length];
      Action<object, int> _assign = (x, y) => _buffer[y] = x;
      for (int i = 0; i < _buffer.Length; i++)
        _bindings[i] = new ConsumerBinding(i, _assign, CommonDefinitions.TestValues[i].GetType());
      int _redItems = 0;
      _reader.ReadMessageCompleted += (x, y) => _reader_ReadMessageCompleted(x, y, _semanticData, (z) => { _redItems++; return _bindings[z]; }, _buffer.Length);
      _reader.ReadMessage(CommonDefinitions.GetTestBinaryArray(), _semanticData);
      Assert.AreEqual<int>(1, _reader.m_NumberOfAttachToNetwork);
      Assert.AreEqual<int>(64, _reader.m_NumberOfSentBytes);
      Assert.AreEqual<int>(1, _reader.m_NumberOfSentMessages);
      Assert.AreEqual<int>(_buffer.Length, _redItems);
      object[] _shouldBeInBuffer = CommonDefinitions.TestValues;
      Assert.AreEqual<int>(_shouldBeInBuffer.Length, _buffer.Length);
      Assert.AreEqual<string>(String.Join(",", _shouldBeInBuffer), String.Join(",", _buffer));
    }
    #endregion

    #region private
    private void _reader_ReadMessageCompleted(object sender, MessageEventArg e, ISemanticData dataId, Func<int, IConsumerBinding> update, int length)
    {
      if (!e.MessageContent.IAmDestination(dataId))
        return;
      e.MessageContent.UpdateMyValues(update, length);
    }
    private class ConsumerBinding : IConsumerBinding
    {
      public ConsumerBinding(int index, Action<object, int> assignAction, Type targetType)
      {
        m_AssignAction = assignAction;
        m_Index = index;
        TargetType = targetType;
      }
      public void Assign2Repository(object value)
      {
        m_AssignAction(value, m_Index);
      }
      public System.Windows.Data.IValueConverter Converter
      {
        set { throw new NotImplementedException(); }
      }
      public Type TargetType
      {
        get;
        private set;
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
      public System.Globalization.CultureInfo Culture
      {
        set { throw new NotImplementedException(); }
      }
      public void OnEnabling()
      {
        throw new NotImplementedException();
      }
      public void OnDisabling()
      {
        throw new NotImplementedException();
      }

      private Action<object, int> m_AssignAction;
      private int m_Index;

    }
    private class TestMessageReaderBase : MessageReaderBase
    {

      #region creator
      public TestMessageReaderBase()
      {
        State = new MyState();
      }
      #endregion

      #region MessageReaderBase
      protected override UInt64 ReadUInt64()
      {
        throw new NotImplementedException();
      }
      protected override UInt32 ReadUInt32()
      {
        throw new NotImplementedException();
      }
      protected override UInt16 ReadUInt16()
      {
        throw new NotImplementedException();
      }
      protected override String ReadString()
      {
        throw new NotImplementedException();
      }
      protected override Single ReadSingle()
      {
        throw new NotImplementedException();
      }
      protected override SByte ReadSByte()
      {
        throw new NotImplementedException();
      }
      protected override Int64 ReadInt64()
      {
        throw new NotImplementedException();
      }
      protected override Int32 ReadInt32()
      {
        throw new NotImplementedException();
      }
      protected override Int16 ReadInt16()
      {
        throw new NotImplementedException();
      }
      protected override Double ReadDouble()
      {
        throw new NotImplementedException();
      }
      protected override Char ReadChar()
      {
        throw new NotImplementedException();
      }
      protected override Byte ReadByte()
      {
        throw new NotImplementedException();
      }
      protected override Boolean ReadBoolean()
      {
        throw new NotImplementedException();
      }
      protected override DateTime ReadDateTime()
      {
        return global::UAOOI.SemanticData.DataManagement.MessageHandling.CommonDefinitions.GetUADateTime(m_BinaryReader.ReadInt64());
      }
      protected override Decimal ReadDecimal()
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
      public override bool IAmDestination(ISemanticData dataId)
      {
        return dataId.Guid == m_SemanticData.Guid;
      }

      #endregion

      #region private
      private BinaryReader m_BinaryReader;
      private int m_NumberOfAttachToNetwork;
      private SemanticData m_SemanticData;
      #endregion

      internal void GetMessageTest(SemanticData semanticData)
      {
        m_SemanticData = semanticData;
        this.RaiseReadMessageCompleted();
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
    private class SemanticData : ISemanticData
    {
      internal static SemanticData GetSemanticDataTest()
      {
        return new SemanticData(null, "SymbolicName", 123, Guid.NewGuid());
      }
      public SemanticData(Uri identifier, string symbolicName, IComparable nodeId, Guid guid)
      {
        Identifier = identifier;
        SymbolicName = symbolicName;
        NodeId = nodeId;
        Guid = guid;
      }
      public Uri Identifier
      {
        get;
        private set;
      }
      public string SymbolicName
      {
        get;
        private set;
      }
      public IComparable NodeId
      {
        get;
        private set;
      }
      public Guid Guid
      {
        get;
        private set;
      }
    }
    #endregion

    #region To be promoted to the codebase
    public class BinaryMessageFrameDecoder : BinaryMessageDecoder
    {

      public BinaryMessageFrameDecoder()
      {
        State = new MyState();
      }

      #region BinaryMessageDecoder
      public override IAssociationState State
      {
        get;
        protected set;
      }
      public override void AttachToNetwork()
      {
        Assert.AreNotEqual<HandlerState>(HandlerState.Operational, State.State);
        State.Enable();
        m_NumberOfAttachToNetwork++;
      }
      /// <summary>
      /// Check if the message destination is the data set described by the <paramref name="dataId" /> of type <see cref="ISemanticData" />.
      /// </summary>
      /// <param name="dataId">The data identifier <see cref="ISemanticData" />.</param>
      /// <returns><c>true</c> if <paramref name="dataId" /> is the destination of the message, <c>false</c> otherwise.</returns>
      public override bool IAmDestination(ISemanticData dataId)
      {
        return dataId.Guid == m_SemanticData.Guid;
      }

      #endregion

      #region Message frame
      public override ulong ContentMask
      {
        get { return ulong.MaxValue; }
      }
      private ISemanticData m_SemanticData;
      #endregion

      #region tetst instrumentation
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork = 0;
      internal int m_NumberOfSentMessages = 0;
      internal void ReadMessage(byte[] buffer, ISemanticData semanticData)
      {
        m_NumberOfSentMessages++;
        m_NumberOfSentBytes += buffer.Length;
        m_SemanticData = semanticData;
        CreateReader(buffer);

        base.RaiseReadMessageCompleted();
        DisposeReader();
      }
      #endregion

    }


    #endregion
  }

}
