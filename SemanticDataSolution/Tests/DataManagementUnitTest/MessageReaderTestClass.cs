
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.DataRepository;
using System.IO;

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
    #endregion

    #region private
    private class TestMessageReaderBase : MessageReaderBase
    {

      #region creator
      public TestMessageReaderBase()
      {
        State = new MyState();
      }
      #endregion

      #region MessageReaderBase
      protected override object ReadUInt64()
      {
        throw new NotImplementedException();
      }
      protected override object ReadUInt32()
      {
        throw new NotImplementedException();
      }
      protected override object ReadUInt16()
      {
        throw new NotImplementedException();
      }
      protected override object ReadString()
      {
        throw new NotImplementedException();
      }
      protected override object ReadSingle()
      {
        throw new NotImplementedException();
      }
      protected override object ReadSByte()
      {
        throw new NotImplementedException();
      }
      protected override object ReadInt64()
      {
        throw new NotImplementedException();
      }
      protected override object ReadInt32()
      {
        throw new NotImplementedException();
      }
      protected override object ReadInt16()
      {
        throw new NotImplementedException();
      }
      protected override object ReadDouble()
      {
        throw new NotImplementedException();
      }
      protected override object ReadChar()
      {
        throw new NotImplementedException();
      }
      protected override object ReadByte()
      {
        throw new NotImplementedException();
      }
      protected override object ReadBoolean()
      {
        throw new NotImplementedException();
      }
      protected override DateTime ReadDateTime()
      {
        return CommonDefinitions.GetUADateTime(m_BinaryReader.ReadInt64());
      }
      protected override ulong ContentFilter
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

  }

}
