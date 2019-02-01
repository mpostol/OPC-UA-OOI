//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory;

namespace UAOOI.Networking.SemanticData.UnitTest
{

  [TestClass]
  public class BinaryDecoderUnitTest
  {

    [TestMethod]
    public void DisposeTest()
    {
      BinaryDataTransferGraphReceiverFixture _BinaryDataTransferGraphReceiverFixture = new DTGFixture();
      using (BinaryDecoder _reader = new BinaryDecoder(_BinaryDataTransferGraphReceiverFixture, new Helpers.UABinaryDecoderImplementation())) { }
      Assert.AreEqual<int>(1, _BinaryDataTransferGraphReceiverFixture.DisposeCount);
    }
    [TestMethod]
    [TestCategory("UAOOI.Networking.SemanticData")]
    public void DataTransferTest()
    {
      uint _dataId = CommonDefinitions.DataSetId;
      BinaryDataTransferGraphReceiverFixture _BinaryDataTransferGraphReceiverFixture = new DTGFixture();
      using (BinaryDecoder _reader = new BinaryDecoder(_BinaryDataTransferGraphReceiverFixture, new Helpers.UABinaryDecoderImplementation()))
      {
        Assert.IsNotNull(_reader);
        Assert.AreEqual<int>(0, _BinaryDataTransferGraphReceiverFixture.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _BinaryDataTransferGraphReceiverFixture.NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _BinaryDataTransferGraphReceiverFixture.m_NumberOfSentMessages);
        Assert.AreEqual<HandlerState>(HandlerState.Disabled, _BinaryDataTransferGraphReceiverFixture.State.State);
        _reader.AttachToNetwork();
        _reader.State.Enable();
        Assert.AreEqual<HandlerState>(HandlerState.Operational, _BinaryDataTransferGraphReceiverFixture.State.State);
        Assert.AreEqual<int>(1, _BinaryDataTransferGraphReceiverFixture.NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _BinaryDataTransferGraphReceiverFixture.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _BinaryDataTransferGraphReceiverFixture.m_NumberOfSentMessages);
        object[] _buffer = new object[CommonDefinitions.TestValues.Length];
        IConsumerBinding[] _bindings = new IConsumerBinding[_buffer.Length];
        Action<object, int> _assign = (x, y) => _buffer[y] = x;
        for (int i = 0; i < _buffer.Length; i++)
          _bindings[i] = new ConsumerBinding(i, _assign, Type.GetTypeCode(CommonDefinitions.TestValues[i].GetType()));
        int _redItems = 0;
        _reader.ReadMessageCompleted += (x, y) => _reader_ReadMessageCompleted(x, y, _dataId, (z) => { _redItems++; return _bindings[z]; }, _buffer.Length);
        _BinaryDataTransferGraphReceiverFixture.SendUDPMessage(CommonDefinitions.GetTestBinaryArrayVariant(), _dataId);
        Assert.AreEqual<int>(1, _BinaryDataTransferGraphReceiverFixture.NumberOfAttachToNetwork);
        Assert.AreEqual<int>(116, _BinaryDataTransferGraphReceiverFixture.m_NumberOfSentBytes);
        Assert.AreEqual<int>(1, _BinaryDataTransferGraphReceiverFixture.m_NumberOfSentMessages);
        //test packet content
        PacketHeader _readerHeader = _reader.Header;
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
      }
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
    private class DTGFixture : BinaryDataTransferGraphReceiverFixture { }
    #endregion

  }

}
