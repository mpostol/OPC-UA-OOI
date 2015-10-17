
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  [TestClass]
  public class MessageWriterUnitTest
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
      ((IMessageWriter)_bmw).Send(x => _binding, 1);
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
      ((IMessageWriter)_bmw).Send(x => _binding, 1);
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
      ((IMessageWriter)_bmw).Send((x) => { _binding.Value = m_TestValues[x]; _sentItems++; return _binding; }, m_TestValues.Length);
      Assert.AreEqual(m_TestValues.Length, _sentItems);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    public void BinaryMessageWriterTestMethod()
    {
      BinaryMessageWriter _bmw = new BinaryMessageWriter();
      Assert.AreEqual<int>(0, _bmw.m_NumberOfSentBytes);
      Assert.AreEqual<int>(0, _bmw.m_NumberOfAttachToNetwork);
      Assert.AreEqual<int>(0, _bmw.m_NumberOfSentMessages);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _bmw.State.State);
      _bmw.AttachToNetwork();
      Assert.AreEqual<HandlerState>(HandlerState.Operational, _bmw.State.State);
      Assert.AreEqual<int>(1, _bmw.m_NumberOfAttachToNetwork);
      Assert.AreEqual<int>(0, _bmw.m_NumberOfSentBytes);
      Assert.AreEqual<int>(0, _bmw.m_NumberOfSentMessages);
      ProducerBinding _binding = new ProducerBinding();
      _binding.Value = String.Empty;
      int _sentItems = 0;
      ((IMessageWriter)_bmw).Send((x) => { _binding.Value = m_TestValues[x]; _sentItems++; return _binding; }, m_TestValues.Length);
      Assert.AreEqual(m_TestValues.Length, _sentItems);
      Assert.AreEqual<int>(1, _bmw.m_NumberOfAttachToNetwork);
      Assert.AreEqual<int>(67, _bmw.m_NumberOfSentBytes);
      Assert.AreEqual<int>(1, _bmw.m_NumberOfSentMessages);
      byte[] _shouldBeInBuffer = new byte[]	
      {
          0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //UInt64
          0x7b, 0x00, 0x00, 0x00,                             //UInt32
          0x7b, 0x00,                                         //UInt16
          0x03, 0x31, 0x32, 0x33,                             //string
          0x00, 0x00, 0xf6, 0x42,                             //Float
          0x7b,                                               //sbyte
          0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //Int64
          0x7b, 0x00, 0x00, 0x00,                             //Int32
          0x7b, 0x00,                                         //Int16
          0x00, 0x00, 0x00, 0x00, 0x00, 0xc0, 0x5e, 0x40,     //Double
          0x7b, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //Int32
          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     //DateTime
          0x7b,                                               //Byte
          0x01,                                               //boolean
          0x41,                                               //Char
          0x01, 0x02, 0x03,                                   //byte[]
      };
      byte[] _isInBuffer = _bmw.Buffer;
      CollectionAssert.AreEqual(_bmw.Buffer, _shouldBeInBuffer);
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
      protected override void Write(byte[] value, object parameter)
      {
        Assert.IsInstanceOfType(value, typeof(byte[]));
      }
      protected override void CreateMessage(int length)
      {
        MassageCreated = true;
      }
      protected override void SendMessage() { }
      #endregion

      #region test infrastructure
      internal bool MassageCreated = false;
      #endregion

    }
    private class BinaryMessageWriter : MessageWriterBase
    {

      #region creator
      public BinaryMessageWriter()
      {
        State = new MyState();
      }
      #endregion

      #region MessageWriterBase
      public override IAssociationState State
      {
        get;
        protected set;
      }
      public override void AttachToNetwork()
      {
        Assert.AreNotEqual<HandlerState>(HandlerState.Operational, State.State);
        State.Enable();
        m_NumberOfAttachToNetwork++;
      }
      protected override void SendMessage()
      {
        Assert.IsNotNull(m_BinaryWriter);
        m_BinaryWriter.Close();
        SendMessage(m_Output.ToArray());
        m_BinaryWriter.Dispose();
        m_BinaryWriter = null;
      }

      protected override void CreateMessage(int length)
      {
        m_Output = new MemoryStream();
        m_BinaryWriter = new BinaryWriter(m_Output);
      }
      protected override void WriteUInt64(ulong value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteUInt32(uint value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteUInt16(ushort value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteString(string value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteSingle(float value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteSByte(sbyte value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteInt64(long value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteInt32(int value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteInt16(short value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteDouble(double value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteDecimal(decimal value, object parameter)
      {
        long _valueLong = Convert.ToInt64(value);
        m_BinaryWriter.Write(_valueLong);
      }
      protected override void WriteDateTime(DateTime value, object parameter)
      {
        if (value.Kind == DateTimeKind.Local)
          value = value.ToUniversalTime();
        m_BinaryWriter.Write(CommonDefinitions.GetUADataTimeTicks(value));
      }
      protected override void WriteByte(byte value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteBool(bool value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void WriteChar(char value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      protected override void Write(byte[] value, object parameter)
      {
        m_BinaryWriter.Write(value);
      }
      #endregion

      #region private
      //vars
      private BinaryWriter m_BinaryWriter;
      private MemoryStream m_Output;
      //methods
      protected virtual void SendMessage(byte[] buffer)
      {
        m_NumberOfSentMessages++;
        m_NumberOfSentBytes += buffer.Length;
      }
      #endregion

      #region tetst instrumentation
      internal int m_NumberOfSentMessages = 0;
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork;
      internal byte[] Buffer { get { return m_Output.ToArray(); } }
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
    private object[] m_TestValues = new object[] { (ulong)123, (uint)123, (ushort)123, "123", (float)123, (sbyte)123, (long)123, (int)123, (short)123, (double)123, (decimal)123, new DateTime(1601, 1, 1), (byte)123, true, 'A', new byte[] { 1, 2, 3 } };
    #endregion

  }

}
