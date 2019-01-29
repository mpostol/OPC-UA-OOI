//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.SemanticData.UnitTest.Helpers;

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
      uint _dataId = CommonDefinitions.DataSetId;
      List<string> _Events = new List<string>();
      using (BinaryUDPPackageReader _reader = new BinaryUDPPackageReader(_port, z => _Events.Add(z)))
      {
        Assert.IsNotNull(_reader);
        Assert.AreEqual<int>(0, _reader.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _reader.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _reader.m_NumberOfSentMessages);
        //Assert.AreEqual<HandlerState>(HandlerState.Disabled, _reader.State.State);
        //_reader.AttachToNetwork();
        //Assert.AreEqual<HandlerState>(HandlerState.Operational, _reader.State.State);
        Assert.AreEqual<int>(1, _reader.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _reader.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _reader.m_NumberOfSentMessages);
        object[] _buffer = new object[CommonDefinitions.TestValues.Length];
        IConsumerBinding[] _bindings = new IConsumerBinding[_buffer.Length];
        Action<object, int> _assign = (x, y) => _buffer[y] = x;
        for (int i = 0; i < _buffer.Length; i++)
          _bindings[i] = new ConsumerBinding(i, _assign, Type.GetTypeCode(CommonDefinitions.TestValues[i].GetType()));
        int _redItems = 0;
        _reader.m_BinaryDecoder.ReadMessageCompleted += (x, y) => _reader_ReadMessageCompleted(x, y, _dataId, (z) => { _redItems++; return _bindings[z]; }, _buffer.Length);
        _reader.TestBinaryStreamObserver.SendUDPMessage(CommonDefinitions.GetTestBinaryArrayVariant(), _dataId);
        Assert.AreEqual<int>(1, _reader.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(116, _reader.m_NumberOfSentBytes);
        Assert.AreEqual<int>(1, _reader.m_NumberOfSentMessages);
        Thread.Sleep(1500);
        foreach (string _item in _Events)
          Console.WriteLine(_item);
        //test packet content
        PacketHeader _readerHeader = _reader.m_BinaryDecoder.Header;
        Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, _readerHeader.PublisherId);
        Assert.AreEqual<byte>(MessageHandling.CommonDefinitions.ProtocolVersion, _readerHeader.ProtocolVersion);
        Assert.AreEqual<byte>(0, _readerHeader.NetworkMessageFlags);
        Assert.AreEqual<uint>(0, _readerHeader.SecurityTokenId);
        Assert.AreEqual<ushort>(1, _readerHeader.NonceLength);
        Assert.AreEqual<int>(1, _readerHeader.Nonce.Length);
        Assert.AreEqual<byte>(0xcc, _readerHeader.Nonce[0]);
        Assert.AreEqual<ushort>(1, _readerHeader.MessageCount);
        Assert.AreEqual<int>(1, _readerHeader.DataSetWriterIds.Count);
        Assert.AreEqual<uint>(CommonDefinitions.DataSetId, _readerHeader.DataSetWriterIds[0]);

        Assert.AreEqual<int>(_buffer.Length, _redItems);
        object[] _shouldBeInBuffer = CommonDefinitions.TestValues;
        Assert.AreEqual<int>(_shouldBeInBuffer.Length, _buffer.Length);
        Assert.AreEqual<string>(string.Join(",", _shouldBeInBuffer), string.Join(",", _buffer));
        Assert.AreEqual<byte>(1, _readerHeader.MessageCount);
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
        set => throw new NotImplementedException();
      }
      public UATypeInfo Encoding
      {
        get;
        private set;
      }
      public object Parameter
      {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
      }
      public CultureInfo Culture
      {
        set => throw new NotImplementedException();
      }

      DataRepository.IValueConverter IBinding.Converter { set => throw new NotImplementedException(); }
      public object FallbackValue { set => throw new NotImplementedException(); }

      public void OnEnabling()
      {
        throw new NotImplementedException();
      }
      public void OnDisabling()
      {
        throw new NotImplementedException();
      }
      private readonly Action<object, int> m_AssignAction;
      private readonly int m_Index;

    }
    private void _reader_ReadMessageCompleted(object sender, MessageEventArg e, uint dataId, Func<int, IConsumerBinding> update, int length)
    {
      Assert.AreEqual<uint>(dataId, e.DataSetId);
      e.MessageContent.UpdateMyValues(update, length);
    }
    private sealed class BinaryUDPPackageReader : IDisposable
    {

      public BinaryUDPPackageReader(int port, Action<string> trace)
      {
        m_BinaryDecoder = new BinaryDecoder(TestBinaryStreamObserver = new BinaryStreamObserver(this), new Helpers.UABinaryDecoderImplementation());
        m_Trace = trace;
      }

      #region BinaryDecoder
      public BinaryStreamObserver TestBinaryStreamObserver { get; set; }
      internal class BinaryStreamObserver : IBinaryDataTransferGraphReceiver
      {
        public BinaryStreamObserver(BinaryUDPPackageReader binaryUDPPackageReader)
        {
          this.binaryUDPPackageReader = binaryUDPPackageReader;
        }
        public IAssociationState State { get; set; } = new MyState();
        public event EventHandler<byte[]> OnNewFrameArrived;
        public void AttachToNetwork()
        {
          this.binaryUDPPackageReader.m_NumberOfAttachToNetwork++;
        }
        public void Dispose()
        {
          throw new NotImplementedException();
        }
        internal void SendUDPMessage(byte[] buffer, uint semanticData)
        {
          //string m_RemoteHostName = "localhost";
          //// Get DNS host information.
          //IPHostEntry m_HostInfo = Dns.GetHostEntry(m_RemoteHostName);
          //// Get the DNS IP addresses associated with the host.
          //Assert.AreEqual<int>(2, m_HostInfo.AddressList.Length);
          //// Get first IPAddress in list return by DNS.
          //IPAddress m_IPAddresses = m_HostInfo.AddressList.Where<IPAddress>(x => x.AddressFamily == AddressFamily.InterNetwork).First<IPAddress>();
          //Assert.IsNotNull(m_IPAddresses);
          //IPEndPoint _IPEndPoint = new IPEndPoint(m_IPAddresses, _RemoteHostPortNumber);
          //using (UdpClient _myClient = new UdpClient())
          OnNewFrameArrived.Invoke(this, buffer);
          binaryUDPPackageReader.m_NumberOfSentMessages++;
          binaryUDPPackageReader.m_NumberOfSentBytes += buffer.Length;
          binaryUDPPackageReader.m_SemanticData = semanticData;
        }

        private readonly BinaryUDPPackageReader binaryUDPPackageReader;
      }
      internal readonly BinaryDecoder m_BinaryDecoder;
      #endregion

      #region private
      private uint m_SemanticData;
      #endregion

      #region tetst instrumentation
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork = 0;
      internal int m_NumberOfSentMessages = 0;
      private readonly Action<string> m_Trace;
      #endregion

      public void Dispose() { }


    }
    #endregion

  }

}
