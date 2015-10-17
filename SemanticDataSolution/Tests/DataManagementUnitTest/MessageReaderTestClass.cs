
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class MessageReaderTestClass
  {
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
    [TestCategory("DataManagement_MessageWriter")]
    public void ReadMessageCompletedTestMethod()
    {
      TestMessageReaderBase _reader = new TestMessageReaderBase();
      _reader.AttachToNetwork();
      object _sender = null;
      MessageEventArg _message = null;
      _reader.ReadMessageCompleted += (x, y) => { _sender = x; _message = y; };
      Assert.IsNull(_sender);
      Assert.IsNull(_message);
      _reader.GetMessageTest();
      Assert.IsNotNull(_sender);
      Assert.IsNotNull(_message);
      Assert.AreSame(_reader, _sender);
      Assert.AreSame(_reader, _message.MessageContent);
    }
    public abstract class MessageReaderBase : IMessageReader, IPeriodicDataMessage
    {

      #region IMessageReader
      public abstract IAssociationState State
      {
        get;
        protected set;
      }
      bool IPeriodicDataMessage.IAmDestination(ISemanticData dataId)
      {
        throw new NotImplementedException();
      }
      public abstract void AttachToNetwork();
      public event EventHandler<MessageEventArg> ReadMessageCompleted;
      #endregion

      #region IPeriodicDataMessage
      void IPeriodicDataMessage.UpdateMyValues(Func<int, IConsumerBinding> update, int length)
      {
        UInt64 _mask = 0x1;
        int _associationIndex = 0;
        for (int i = 0; i < length; i++)
        {
          if ((ContentFilter & _mask) > 0)
          {
            IConsumerBinding _binding = update(_associationIndex);
            Read(_binding);
          }
          _associationIndex++;
          _mask = _mask << 1;
        }
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
            _value = CommonDefinitions.GetUADateTime(_r.ReadInt64());
            break;
          case TypeCode.Decimal:
            return false;
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

      protected abstract object ReadUInt64();
      protected abstract object ReadUInt32();
      protected abstract object ReadUInt16();
      protected abstract object ReadString();
      protected abstract object ReadSingle();
      protected abstract object ReadSByte();
      protected abstract object ReadInt64();
      protected abstract object ReadInt32();
      protected abstract object ReadInt16();
      protected abstract object ReadDouble();
      protected abstract object ReadChar();
      protected abstract object ReadByte();
      protected abstract object ReadBoolean();
      protected abstract ulong ContentFilter { get; set; }
      protected void RaiseReadMessageCompleted()
      {
        EventHandler<MessageEventArg> _handler = ReadMessageCompleted;
        if (_handler == null)
          return;
        ReadMessageCompleted(this, new MessageEventArg(this));
      }

      #region test instrumentation
      internal virtual void GetMessageTest()
      {
        Assert.IsNotNull(ReadMessageCompleted);
        this.RaiseReadMessageCompleted();
      }
      #endregion
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
      #endregion

      #region private
      private int m_NumberOfAttachToNetwork;
      #endregion

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

  }

}
