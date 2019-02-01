//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory;

namespace UAOOI.Networking.SemanticData.UnitTest
{

  [TestClass]
  public class BinaryEncoderTest
  {

    #region TestMethods
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    public void BinaryUDPPackageWriterTestMethod()
    {
      BinaryDataTransferGraphSenderFixture _binaryStreamObservable = new BinaryDataTransferGraphSenderTest();
      using (BinaryEncoder _writer = new BinaryEncoder(_binaryStreamObservable, new Helpers.UABinaryEncoderImplementation(), MessageLengthFieldTypeEnum.TwoBytes))
      {
        Assert.AreEqual<int>(0, _binaryStreamObservable.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _binaryStreamObservable.NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _binaryStreamObservable.m_NumberOfSentMessages);
        Assert.AreEqual<HandlerState>(HandlerState.Disabled, _binaryStreamObservable.State.State);
        _writer.AttachToNetwork();
        _writer.State.Enable();
        Assert.AreEqual<HandlerState>(HandlerState.Operational, _binaryStreamObservable.State.State);
        Assert.AreEqual<int>(1, _binaryStreamObservable.NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _binaryStreamObservable.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _binaryStreamObservable.m_NumberOfSentMessages);
        ProducerBindingFixture _binding = new ProducerBindingFixture() { Value = string.Empty };
        int _sentItems = 0;
        Guid m_Guid = CommonDefinitions.TestGuid;
        DataSelector _testDataSelector = new DataSelector() { DataSetWriterId = CommonDefinitions.DataSetId, PublisherId = CommonDefinitions.TestGuid };
        ((IMessageWriter)_writer).Send((x) => { _binding.Value = CommonDefinitions.TestValues[x]; _sentItems++; return _binding; },
                                        Convert.ToUInt16(CommonDefinitions.TestValues.Length),
                                        ulong.MaxValue,
                                        FieldEncodingEnum.VariantFieldEncoding,
                                        _testDataSelector,
                                        0,
                                        CommonDefinitions.TestMinimalDateTime, new ConfigurationVersionDataType() { MajorVersion = 0, MinorVersion = 0 }
                                        );
        Assert.AreEqual(CommonDefinitions.TestValues.Length, _sentItems);
        Assert.AreEqual<int>(1, _binaryStreamObservable.NumberOfAttachToNetwork);
        Assert.AreEqual<int>(115, _binaryStreamObservable.m_NumberOfSentBytes);
        Assert.AreEqual<int>(1, _binaryStreamObservable.m_NumberOfSentMessages);
        byte[] _shouldBeInBuffer = CommonDefinitions.GetTestBinaryArrayVariant4Consumer();
        CollectionAssert.AreEqual(_binaryStreamObservable.Buffer, _shouldBeInBuffer);
      }
    }
    #endregion

    #region private
    private class ProducerBindingFixture : IProducerBinding
    {

      internal object Value;
      private readonly BuiltInType _builtInType;

      public ProducerBindingFixture(BuiltInType builtInType)
      {
        _builtInType = builtInType;
      }
      public ProducerBindingFixture() { }

      #region IProducerBinding
      public bool NewValue => true;
      public object GetFromRepository()
      {
        return Value;
      }
      public IValueConverter Converter
      {
        set => throw new NotImplementedException();
      }
      public UATypeInfo Encoding
      {
        get
        {
          if (Value == null)
            return new UATypeInfo(_builtInType);
          switch (Type.GetTypeCode(Value.GetType()))
          {
            case TypeCode.Boolean:
              return new UATypeInfo(BuiltInType.Boolean);
            case TypeCode.SByte:
              return new UATypeInfo(BuiltInType.SByte);
            case TypeCode.Byte:
              return new UATypeInfo(BuiltInType.Byte);
            case TypeCode.Int16:
              return new UATypeInfo(BuiltInType.Int16);
            case TypeCode.UInt16:
              return new UATypeInfo(BuiltInType.UInt16);
            case TypeCode.Int32:
              return new UATypeInfo(BuiltInType.Int32);
            case TypeCode.UInt32:
              return new UATypeInfo(BuiltInType.UInt32);
            case TypeCode.Int64:
              return new UATypeInfo(BuiltInType.Int64);
            case TypeCode.UInt64:
              return new UATypeInfo(BuiltInType.UInt64);
            case TypeCode.Single:
              return new UATypeInfo(BuiltInType.Float);
            case TypeCode.Double:
              return new UATypeInfo(BuiltInType.Double);
            case TypeCode.DateTime:
              return new UATypeInfo(BuiltInType.DateTime);
            case TypeCode.String:
              return new UATypeInfo(BuiltInType.String);
            default:
              throw new ArgumentOutOfRangeException(nameof(Value));
          }
          throw new ArgumentOutOfRangeException(nameof(Value));
        }
      }
      public object Parameter
      {
        get => null;
        set { }
      }
      public System.Globalization.CultureInfo Culture
      {
        set => throw new NotImplementedException();
      }

      public object FallbackValue { set => throw new NotImplementedException(); }

      public void OnEnabling()
      {
        throw new NotImplementedException();
      }
      public void OnDisabling()
      {
        throw new NotImplementedException();
      }
      public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
      #endregion

    }
    private class BinaryDataTransferGraphSenderTest : BinaryDataTransferGraphSenderFixture { }
    #endregion

  }

}

