
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
      TestMessageReaderBase _bmw = new TestMessageReaderBase(Guid.NewGuid());
      Assert.IsNotNull(_bmw);
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageReader")]
    public void ReadMessageCompletedTestMethod()
    {
      TestMessageReaderBase _reader = new TestMessageReaderBase(Guid.NewGuid());
      _reader.AttachToNetwork();
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
    }
    #endregion

    #region private
    private class TestMessageReaderBase : MessageReaderBase
    {

      #region creator
      public TestMessageReaderBase(Guid publisherId) : base(new Helpers.UABinaryDecoderImplementation())
      {
        State = new MyState();
        m_MessageHeader = MessageHeader.GetConsumerMessageHeader(this);
        m_PublisherId = publisherId;
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
        throw new NotImplementedException();
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
      #endregion

      #region private
      private MessageHeader m_MessageHeader;
      private int m_NumberOfAttachToNetwork;
      private Guid m_PublisherId;
      #endregion

      internal void GetMessageTest(UInt16 dataSetId)
      {
        RaiseReadMessageCompleted(dataSetId);
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
        return new SemanticData(null, "SymbolicName", 123, CommonDefinitions.TestGuid);
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

  }
}
