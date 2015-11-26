
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

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
    public void BinaryUDPPackageReaderTestMethod()
    {
      int _port = 35678;
      ISemanticData _semanticData = SemanticData.GetSemanticDataTest();
      List<string> m_Events = new List<string>();
      using (BinaryUDPPackageReader _reader = new BinaryUDPPackageReader(_port, z => m_Events.Add(z)))
      {
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
          _bindings[i] = new ConsumerBinding(i, _assign, Type.GetTypeCode(CommonDefinitions.TestValues[i].GetType()));
        int _redItems = 0;
        _reader.ReadMessageCompleted += (x, y) => _reader_ReadMessageCompleted(x, y, _semanticData, (z) => { _redItems++; return _bindings[z]; }, _buffer.Length);
        _reader.SendUDPMessage(CommonDefinitions.GetTestBinaryArray(), _semanticData, _port);
        Assert.AreEqual<int>(1, _reader.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(91, _reader.m_NumberOfSentBytes);
        Assert.AreEqual<int>(1, _reader.m_NumberOfSentMessages);
        Thread.Sleep(1500);
        Assert.AreEqual<int>(_buffer.Length, _redItems);
        object[] _shouldBeInBuffer = CommonDefinitions.TestValues;
        Assert.AreEqual<int>(_shouldBeInBuffer.Length, _buffer.Length);
        Assert.AreEqual<string>(String.Join(",", _shouldBeInBuffer), String.Join(",", _buffer));
        Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, _reader.Header.PublisherId);
        Assert.AreEqual<byte>(MessageHandling.CommonDefinitions.ProtocolVersion, _reader.Header.ProtocolVersion);
        Assert.AreEqual<byte>(1, _reader.Header.MessageCount);
        Assert.AreEqual<int>(4, m_Events.Count);
      }
      Thread.Sleep(150);
      Assert.AreEqual<int>(7, m_Events.Count);
      foreach (string item in m_Events)
        Console.WriteLine(item);
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

      public ConsumerBinding(int index, Action<object, int> assignAction, TypeCode targetType)
      {
        m_AssignAction = assignAction;
        m_Index = index;
        Encoding = GetTargetType(targetType);
      }
      private BuiltInType GetTargetType(TypeCode targetType)
      {
        BuiltInType _ret = default(BuiltInType);
        switch (targetType)
        {
          case TypeCode.Boolean:
            _ret = BuiltInType.Boolean;
            break;
          case TypeCode.SByte:
            _ret = BuiltInType.SByte;
            break;
          case TypeCode.Byte:
            _ret = BuiltInType.Byte;
            break;
          case TypeCode.Int16:
            _ret = BuiltInType.Int16;
            break;
          case TypeCode.UInt16:
            _ret = BuiltInType.UInt16;
            break;
          case TypeCode.Int32:
            _ret = BuiltInType.Int32;
            break;
          case TypeCode.UInt32:
            _ret = BuiltInType.UInt32;
            break;
          case TypeCode.Int64:
            _ret = BuiltInType.Int64;
            break;
          case TypeCode.UInt64:
            _ret = BuiltInType.UInt64;
            break;
          case TypeCode.Single:
            _ret = BuiltInType.Float;
            break;
          case TypeCode.Double:
            _ret = BuiltInType.Double;
            break;
          case TypeCode.DateTime:
            _ret = BuiltInType.DateTime;
            break;
          case TypeCode.String:
            _ret = BuiltInType.String;
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof(targetType));
        }
        return _ret;
      }
      public void Assign2Repository(object value)
      {
        m_AssignAction(value, m_Index);
      }
      public System.Windows.Data.IValueConverter Converter
      {
        set { throw new NotImplementedException(); }
      }
      public BuiltInType Encoding
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
      public TestMessageReaderBase() : base(new Helpers.UABinaryDecoderImplementation())
      {
        State = new MyState();
        m_MessageHeader = MessageHeader.GetConsumerMessageHeader(null);
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

      #endregion

      #region private
      private MessageHeader m_MessageHeader;
      private int m_NumberOfAttachToNetwork;
      private SemanticData m_SemanticData;
      #endregion

      internal void GetMessageTest(SemanticData semanticData)
      {
        m_SemanticData = semanticData;
        m_MessageHeader.DataSetId = m_SemanticData.Guid;
        RaiseReadMessageCompleted();
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

    #region To be promoted to the codebase

    public sealed class BinaryUDPPackageReader : BinaryDecoder
    {

      public BinaryUDPPackageReader(int port, Action<string> trace) : base(new Helpers.UABinaryDecoderImplementation())
      {
        State = new MyState();
        m_UdpClient = new UdpClient(port);
        m_Trace = trace;
      }

      #region BinaryDecoder

      /// <summary>
      /// Gets or sets the state.
      /// </summary>
      /// <value>The state.</value>
      public override IAssociationState State
      {
        get;
        protected set;
      }
      /// <summary>
      /// Attaches to network.
      /// </summary>
      public override void AttachToNetwork()
      {
        Assert.AreNotEqual<HandlerState>(HandlerState.Operational, State.State);
        State.Enable();
        m_NumberOfAttachToNetwork++;
        m_UdpClient.BeginReceive(new AsyncCallback(m_ReceiveAsyncCallback), null);
      }
      #endregion

      #region private
      /// <summary>
      /// Implements <see cref="AsyncCallback"/> for UDP begin receive.
      /// </summary>
      /// <param name="asyncResult">The asynchronous result.</param>
      private void m_ReceiveAsyncCallback(IAsyncResult asyncResult)
      {
        m_Trace("Entering m_ReceiveAsyncCallback");
        try
        {
          IPEndPoint _UEndPoint = null;
          Byte[] _receiveBytes = null;
          _receiveBytes = m_UdpClient.EndReceive(asyncResult, ref _UEndPoint);
          m_Trace(String.Format("Received length ={0}", _receiveBytes == null ? -1 : _receiveBytes.Length));
          MemoryStream _stream = new MemoryStream(_receiveBytes, 0, _receiveBytes.Length);
          OnNewFrameArrived(new BinaryReader(_stream));
          m_Trace("BeginReceive");
          m_UdpClient.BeginReceive(new AsyncCallback(m_ReceiveAsyncCallback), null);
        }
        catch (ObjectDisposedException _ex)
        {
          m_Trace(String.Format("ObjectDisposedException = {0}", _ex.Message));
        }
        catch (Exception _ex)
        {
          m_Trace(String.Format("Exception {0}, message = {1}", _ex, GetType().Name, _ex.Message));
        }
        m_Trace("Exiting m_ReceiveAsyncCallback");
      }

      private ISemanticData m_SemanticData;
      #endregion

      #region tetst instrumentation
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork = 0;
      internal int m_NumberOfSentMessages = 0;
      private readonly UdpClient m_UdpClient;
      private Action<string> m_Trace;

      internal void SendUDPMessage(byte[] buffer, ISemanticData semanticData, int _RemoteHostPortNumber)
      {
        string m_RemoteHostName = "localhost";
        // Get DNS host information.
        IPHostEntry m_HostInfo = Dns.GetHostEntry(m_RemoteHostName);
        // Get the DNS IP addresses associated with the host.
        Assert.AreEqual<int>(2, m_HostInfo.AddressList.Length);
        // Get first IPAddress in list return by DNS.
        IPAddress m_IPAddresses = m_HostInfo.AddressList.Where<IPAddress>(x => x.AddressFamily == AddressFamily.InterNetwork).First<IPAddress>();
        Assert.IsNotNull(m_IPAddresses);
        IPEndPoint _IPEndPoint = new IPEndPoint(m_IPAddresses, _RemoteHostPortNumber);
        using (UdpClient _myClient = new UdpClient())
          _myClient.Send(buffer, buffer.Length, _IPEndPoint);
        m_NumberOfSentMessages++;
        m_NumberOfSentBytes += buffer.Length;
        m_SemanticData = semanticData;
      }
      protected override void Dispose(bool disposing)
      {
        base.Dispose(disposing);
        if (disposing)
          m_UdpClient.Close();

      }

      #endregion

    }

    #endregion

  }

}
