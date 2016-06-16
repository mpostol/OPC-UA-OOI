
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Sockets;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.MessageHandling;
using System.Linq;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.UnitTest
{

  [TestClass]
  public class MessageWriterTestClass
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    public void CreatorTestMethod1()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      Assert.IsNotNull(_bmw);
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ObjectTestMethod()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      _bmw.AttachToNetwork();
      ProducerBinding _binding = new ProducerBinding();
      _binding.Value = new TestClass();
      ((IMessageWriter)_bmw).Send(x => _binding, 1, UInt64.MaxValue, FieldEncodingEnum.VariantFieldEncoding, TestDataSelector, 0, DateTime.UtcNow,
        new ConfigurationVersionDataType() { MajorVersion = 0, MinorVersion = 0 });
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    [ExpectedException(typeof(NullReferenceException))]
    public void NullableTestMethod()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
      ProducerBinding _binding = new ProducerBinding(BuiltInType.Float);
      _binding.Value = new Nullable<float>();
      ((IMessageWriter)_bmw).Send(x => _binding, 1, UInt64.MaxValue, FieldEncodingEnum.VariantFieldEncoding, TestDataSelector, 0, DateTime.UtcNow, new ConfigurationVersionDataType() { MajorVersion = 0, MinorVersion = 0 });
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    public void SendTestMethod()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
      ProducerBinding _binding = new ProducerBinding();
      _binding.Value = String.Empty;
      int _sentItems = 0;
      ((IMessageWriter)_bmw).Send((x) => { _binding.Value = CommonDefinitions.TestValues[x]; _sentItems++; return _binding; },
                                   Convert.ToUInt16(CommonDefinitions.TestValues.Length),
                                   UInt64.MaxValue,
                                   FieldEncodingEnum.VariantFieldEncoding,
                                   TestDataSelector,
                                   0,
                                   DateTime.UtcNow,
                                   new ConfigurationVersionDataType() { MajorVersion = 0, MinorVersion = 0 }
                                   );
      Assert.AreEqual(CommonDefinitions.TestValues.Length, _sentItems);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    public void BinaryUDPPackageWriterTestMethod()
    {
      int _port = 35678;
      using (BinaryUDPPackageWriter _writer = new BinaryUDPPackageWriter("localhost", _port, new Helpers.UABinaryEncoderImplementation()))
      {
        Assert.AreEqual<int>(0, _writer.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _writer.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _writer.m_NumberOfSentMessages);
        Assert.AreEqual<HandlerState>(HandlerState.Disabled, _writer.State.State);
        _writer.AttachToNetwork();
        Assert.AreEqual<HandlerState>(HandlerState.Operational, _writer.State.State);
        Assert.AreEqual<int>(1, _writer.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(0, _writer.m_NumberOfSentBytes);
        Assert.AreEqual<int>(0, _writer.m_NumberOfSentMessages);
        ProducerBinding _binding = new ProducerBinding() { Value = String.Empty };
        int _sentItems = 0;
        Guid m_Guid = CommonDefinitions.TestGuid;
        DataSelector _testDataSelector = new DataSelector() { DataSetWriterId = CommonDefinitions.DataSetId, PublisherId = CommonDefinitions.TestGuid  };
        ((IMessageWriter)_writer).Send((x) => { _binding.Value = CommonDefinitions.TestValues[x]; _sentItems++; return _binding; },
                                        Convert.ToUInt16(CommonDefinitions.TestValues.Length),
                                        UInt64.MaxValue,
                                        FieldEncodingEnum.VariantFieldEncoding,
                                        _testDataSelector,
                                        0,
                                        CommonDefinitions.TestMinimalDateTime, new ConfigurationVersionDataType() { MajorVersion = 0, MinorVersion = 0 }
                                       );
        Assert.AreEqual(CommonDefinitions.TestValues.Length, _sentItems);
        Assert.AreEqual<int>(1, _writer.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(115, _writer.m_NumberOfSentBytes);
        Assert.AreEqual<int>(1, _writer.m_NumberOfSentMessages);
        byte[] _shouldBeInBuffer = CommonDefinitions.GetTestBinaryArrayVariant4Consumer();
        byte[] _outputBuffer = _writer.DoUDPRead();
        CollectionAssert.AreEqual(_outputBuffer, _shouldBeInBuffer);
      }
    }
    #endregion

    #region private
    private class TestClass { }
    private readonly DataSelector TestDataSelector = new DataSelector() { PublisherId = Guid.NewGuid(), DataSetWriterId = UInt16.MaxValue };
    private class ProducerBinding : IProducerBinding
    {

      internal object Value;
      private BuiltInType _builtInType;

      public ProducerBinding(BuiltInType builtInType)
      {
        _builtInType = builtInType;
      }
      public ProducerBinding() { }

      #region IProducerBinding
      public bool NewValue
      {
        get { return true; }
      }
      public object GetFromRepository()
      {
        return Value;
      }
      public System.Windows.Data.IValueConverter Converter
      {
        set { throw new NotImplementedException(); }
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
        get
        {
          return null;
        }
        set { }
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
      public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
      #endregion

    }
    private class TypesMessageWriter : MessageWriterBase
    {

      #region creator
      public TypesMessageWriter() : base(new Helpers.UABinaryEncoderImplementation())
      {
        State = new MyState();
      }
      #endregion

      #region BinaryMessageWriter
      public override IAssociationState State
      {
        get;
        protected set;
      }
      public override void AttachToNetwork()
      {
        Assert.AreNotEqual<HandlerState>(HandlerState.Operational, State.State);
        State.Enable();
      }
      public override void Write(ulong value)
      {
        Assert.IsInstanceOfType(value, typeof(ulong));
      }
      public override void Write(uint value)
      {
        Assert.IsInstanceOfType(value, typeof(uint));
      }
      public override void Write(ushort value)
      {
        Assert.IsInstanceOfType(value, typeof(ushort));
      }
      public override void Write(float value)
      {
        Assert.IsInstanceOfType(value, typeof(float));
      }
      public override void Write(sbyte value)
      {
        Assert.IsInstanceOfType(value, typeof(sbyte));
      }
      public override void Write(long value)
      {
        Assert.IsInstanceOfType(value, typeof(long));
      }
      public override void Write(int value)
      {
        Assert.IsInstanceOfType(value, typeof(int));
      }
      public override void Write(short value)
      {
        Assert.IsInstanceOfType(value, typeof(short));
      }
      public override void Write(double value)
      {
        Assert.IsInstanceOfType(value, typeof(double));
      }
      public override void Write(byte value)
      {
        Assert.IsInstanceOfType(value, typeof(byte));
      }
      public override void Write(bool value)
      {
        Assert.IsInstanceOfType(value, typeof(bool));
      }
      public override void Write(byte[] value)
      {
        Assert.IsInstanceOfType(value, typeof(byte[]));
      }
      protected internal override void CreateMessage
        (FieldEncodingEnum encoding, Guid prodicerId, ushort dataSetWriterId, ushort fieldCount, ushort sequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion)
      {
        MassageCreated = true;
      }
      protected override void SendMessage() { }
      #endregion

      #region test infrastructure
      internal bool MassageCreated = false;
      #endregion

    }
    internal class SemanticDataTest : ISemanticData
    {
      public SemanticDataTest(Guid guid)
      {
        Guid = guid;
      }
      public Guid Guid
      {
        get; private set;
      }
      public Uri Identifier
      {
        get
        {
          throw new NotImplementedException();
        }
      }
      public IComparable NodeId
      {
        get
        {
          throw new NotImplementedException();
        }
      }
      public string SymbolicName
      {
        get
        {
          throw new NotImplementedException();
        }
      }
    }
    #endregion

  }

  internal class MyState : IAssociationState
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

  #region to be promoted to the codebase
  public sealed class BinaryUDPPackageWriter : BinaryEncoder
  {

    #region creator
    public BinaryUDPPackageWriter(string remoteHostName, int port, IUAEncoder uaEncoder) :
      base(uaEncoder, MessageLengthFieldTypeEnum.TwoBytes)
    {
      State = new MyState();
      m_RemoteHostName = remoteHostName;
      m_Port = port;
    }
    #endregion

    #region BinaryMessageEncoder
    public override IAssociationState State
    {
      get;
      protected set;
    }
    public override void AttachToNetwork()
    {
      // Get DNS host information.
      m_HostInfo = Dns.GetHostEntry(m_RemoteHostName);
      // Get the DNS IP addresses associated with the host.
      Assert.AreEqual<int>(2, m_HostInfo.AddressList.Length);
      // Get first IPAddress in list return by DNS.
      m_IPAddresses = m_HostInfo.AddressList.Where<IPAddress>(x => x.AddressFamily == AddressFamily.InterNetwork).First<IPAddress>();
      Assert.IsNotNull(m_IPAddresses);
      m_UdpClient = new UdpClient(m_Port);
      Assert.AreNotEqual<HandlerState>(HandlerState.Operational, State.State);
      State.Enable();
      m_NumberOfAttachToNetwork++;
    }
    protected override void SendFrame(byte[] buffer)
    {
      m_NumberOfSentBytes += buffer.Length;
      m_NumberOfSentMessages++;
      try
      {
        IPEndPoint _IPEndPoint = new IPEndPoint(m_IPAddresses, m_Port);
        m_UdpClient.Send(buffer, buffer.Length, _IPEndPoint);
      }
      catch (SocketException e)
      {
        Console.WriteLine("SocketException caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
        throw;
      }
      catch (ArgumentNullException e)
      {
        Console.WriteLine("ArgumentNullException caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
        throw;
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine("NullReferenceException caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
        throw;
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
        throw;
      }
    }

    #endregion

    #region private
    private UdpClient m_UdpClient;
    private IPAddress m_IPAddresses;
    private IPHostEntry m_HostInfo;
    private int m_Port = 4800;
    private string m_RemoteHostName;
    #endregion

    #region tetst instrumentation
    internal int m_NumberOfSentMessages = 0;
    internal int m_NumberOfSentBytes = 0;
    internal int m_NumberOfAttachToNetwork;
    internal byte[] DoUDPRead()
    {
      Byte[] _receiverBytes = null;
      try
      {
        IPEndPoint _IPEndPoint = null;
        _receiverBytes = m_UdpClient.Receive(ref _IPEndPoint);
        Assert.IsNotNull(_IPEndPoint);
      } // End of the try block.
      catch (SocketException e)
      {
        Console.WriteLine("SocketException caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
      }
      catch (ArgumentNullException e)
      {
        Console.WriteLine("ArgumentNullException caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine("NullReferenceException caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception caught!!!");
        Console.WriteLine("Source : " + e.Source);
        Console.WriteLine("Message : " + e.Message);
      }
      return _receiverBytes;

    }
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      m_UdpClient.Close();
    }

    #endregion

  }
  #endregion

}
