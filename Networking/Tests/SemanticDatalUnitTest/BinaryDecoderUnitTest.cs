
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Data;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.UnitTest
{

  [TestClass]
  public class BinaryDecoderUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_BinaryDecoderUnitTest")]
    public void BinaryUDPPackageReaderTestMethod()
    {
      int _port = 35678;
      UInt32 _dataId = CommonDefinitions.DataSetId;
      List<string> _Events = new List<string>();
      using (BinaryUDPPackageReader _reader = new BinaryUDPPackageReader(_port, z => _Events.Add(z)))
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
        _reader.ReadMessageCompleted += (x, y) => _reader_ReadMessageCompleted(x, y, _dataId, (z) => { _redItems++; return _bindings[z]; }, _buffer.Length);
        _reader.SendUDPMessage(CommonDefinitions.GetTestBinaryArrayVariant(), _dataId, _port);
        Assert.AreEqual<int>(1, _reader.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(113, _reader.m_NumberOfSentBytes);
        Assert.AreEqual<int>(1, _reader.m_NumberOfSentMessages);
        Thread.Sleep(1500);
        foreach (string _item in _Events)
          Console.WriteLine(_item);
        //test packet content
        Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, _reader.Header.PublisherId);
        Assert.AreEqual<byte>(MessageHandling.CommonDefinitions.ProtocolVersion, _reader.Header.ProtocolVersion);
        Assert.AreEqual<byte>(0, _reader.Header.PacketFlags);
        Assert.AreEqual<UInt32>(0, _reader.Header.SecurityTokenId);
        Assert.AreEqual<ushort>(1, _reader.Header.NonceLength);
        Assert.AreEqual<int>(1, _reader.Header.Nonce.Length);
        Assert.AreEqual<byte>(0xcc, _reader.Header.Nonce[0]);
        Assert.AreEqual<ushort>(1, _reader.Header.MessageCount);
        Assert.AreEqual<int>(1, _reader.Header.DataSetWriterIds.Count);
        Assert.AreEqual<UInt32>(CommonDefinitions.DataSetId, _reader.Header.DataSetWriterIds[0]);

        Assert.AreEqual<int>(_buffer.Length, _redItems);
        object[] _shouldBeInBuffer = CommonDefinitions.TestValues;
        Assert.AreEqual<int>(_shouldBeInBuffer.Length, _buffer.Length);
        Assert.AreEqual<string>(String.Join(",", _shouldBeInBuffer), String.Join(",", _buffer));
        Assert.AreEqual<byte>(1, _reader.Header.MessageCount);
        Assert.AreEqual<int>(3, _Events.Count);
      }
      Thread.Sleep(150);
      Assert.AreEqual<int>(3, _Events.Count);
    }

    #region private test instrumentation
    private class ConsumerBinding : IConsumerBinding
    {

      public ConsumerBinding(int index, Action<object, int> assignAction, TypeCode targetType)
      {
        m_AssignAction = assignAction;
        m_Index = index;
        Encoding = new UATypeInfo(GetTargetType(targetType));
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
      public IValueConverter Converter
      {
        set { throw new NotImplementedException(); }
      }
      public UATypeInfo Encoding
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
      public CultureInfo Culture
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
    private void _reader_ReadMessageCompleted(object sender, MessageEventArg e, UInt32 dataId, Func<int, IConsumerBinding> update, int length)
    {
      Assert.AreEqual<uint>(dataId, e.DataSetId);
      //  return;
      e.MessageContent.UpdateMyValues(update, length);
    }
    private sealed class BinaryUDPPackageReader : BinaryDecoder
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
      protected override void Trace(string message)
      {
        throw new NotImplementedException();
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
          OnNewFrameArrived(new BinaryReader(_stream, System.Text.Encoding.UTF8));
          //m_Trace("BeginReceive");
          //m_UdpClient.BeginReceive(new AsyncCallback(m_ReceiveAsyncCallback), null);
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

      private UInt32 m_SemanticData;
      #endregion

      #region tetst instrumentation
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork = 0;
      internal int m_NumberOfSentMessages = 0;
      private readonly UdpClient m_UdpClient;
      private Action<string> m_Trace;

      internal void SendUDPMessage(byte[] buffer, UInt32 semanticData, int _RemoteHostPortNumber)
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
