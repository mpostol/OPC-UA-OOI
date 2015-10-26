
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Sockets;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using System.Linq;

namespace UAOOI.SemanticData.DataManagement.UnitTest
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
      ((IMessageWriter)_bmw).Send(x => _binding, 1, UInt64.MaxValue, new SemanticDataTest(Guid.NewGuid()));
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void NullableTestMethod()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
      ProducerBinding _binding = new ProducerBinding();
      _binding.Value = new Nullable<float>();
      ((IMessageWriter)_bmw).Send(x => _binding, 1, UInt64.MaxValue, new SemanticDataTest(Guid.NewGuid()));
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
                                   CommonDefinitions.TestValues.Length,
                                   UInt64.MaxValue,
                                   new SemanticDataTest(Guid.NewGuid())
                                   );
      Assert.AreEqual(CommonDefinitions.TestValues.Length, _sentItems);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    public void BinaryUDPPackageWriterTestMethod()
    {
      int _port = 35678;
      using (BinaryUDPPackageWriter _writer = new BinaryUDPPackageWriter("localhost", _port))
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
        ProducerBinding _binding = new ProducerBinding();
        _binding.Value = String.Empty;
        int _sentItems = 0;
        Guid m_Guid = CommonDefinitions.TestGuid;
        ((IMessageWriter)_writer).Send((x) => { _binding.Value = CommonDefinitions.TestValues[x]; _sentItems++; return _binding; },
                                       CommonDefinitions.TestValues.Length,
                                       UInt64.MaxValue,
                                       new SemanticDataTest(m_Guid));
        Assert.AreEqual(CommonDefinitions.TestValues.Length, _sentItems);
        Assert.AreEqual<int>(1, _writer.m_NumberOfAttachToNetwork);
        Assert.AreEqual<int>(100, _writer.m_NumberOfSentBytes);
        Assert.AreEqual<int>(1, _writer.m_NumberOfSentMessages);
        byte[] _shouldBeInBuffer = CommonDefinitions.GetTestBinaryArray();
        byte[] _outputBuffer = _writer.DoUDPRead();
        CollectionAssert.AreEqual(_outputBuffer, _shouldBeInBuffer);
      }
    }
    #endregion

    #region private
    private class TestClass { }
    private class ProducerBinding : IProducerBinding
    {

      internal object Value;

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
      public Type TargetType
      {
        get { throw new NotImplementedException(); }
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
      public TypesMessageWriter()
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
      protected override void WriteUInt64(ulong value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(ulong));
      }
      protected override void WriteUInt32(uint value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(uint));
      }
      protected override void WriteUInt16(ushort value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(ushort));
      }
      protected override void WriteString(string value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(string));
      }
      protected override void WriteSingle(float value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(float));
      }
      protected override void WriteSByte(sbyte value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(sbyte));
      }
      protected override void WriteInt64(long value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(long));
      }
      protected override void WriteInt32(int value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(int));
      }
      protected override void WriteInt16(short value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(short));
      }
      protected override void WriteDouble(double value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(double));
      }
      protected override void WriteDecimal(decimal value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(decimal));
      }
      protected override void WriteDateTime(DateTime value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(DateTime));
      }
      protected override void WriteByte(byte value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(byte));
      }
      protected override void WriteBool(bool value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(bool));
      }
      protected override void WriteChar(char value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(char));
      }
      protected override void CreateMessage(int length, Guid dataSetId)
      {
        MassageCreated = true;
      }
      protected override void SendMessage() { }
      public override void Write(byte value)
      {
        throw new NotImplementedException();
      }
      public override void Write(Guid value)
      {
        throw new NotImplementedException();
      }

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
    public BinaryUDPPackageWriter(string remoteHostName, int port) : base(CommonDefinitions.TestGuid)
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
