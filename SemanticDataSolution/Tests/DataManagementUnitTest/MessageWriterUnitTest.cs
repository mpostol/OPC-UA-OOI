
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
      ((IMessageWriter)_bmw).Send(x => _binding, 1, UInt64.MaxValue);
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
      ((IMessageWriter)_bmw).Send(x => _binding, 1, UInt64.MaxValue);
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
      ((IMessageWriter)_bmw).Send((x) => { _binding.Value = CommonDefinitions.TestValues[x]; _sentItems++; return _binding; }, CommonDefinitions.TestValues.Length, UInt64.MaxValue);
      Assert.AreEqual(CommonDefinitions.TestValues.Length, _sentItems);
    }
    [TestMethod]
    [TestCategory("DataManagement_MessageWriter")]
    public void BinaryMessageWriterTestMethod()
    {
      BinaryMessagePackageEncoder _writer = new BinaryMessagePackageEncoder();
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
      ((IMessageWriter)_writer).Send((x) => { _binding.Value = CommonDefinitions.TestValues[x]; _sentItems++; return _binding; }, CommonDefinitions.TestValues.Length, UInt64.MaxValue);
      Assert.AreEqual(CommonDefinitions.TestValues.Length, _sentItems);
      Assert.AreEqual<int>(1, _writer.m_NumberOfAttachToNetwork);
      Assert.AreEqual<int>(64, _writer.m_NumberOfSentBytes);
      Assert.AreEqual<int>(1, _writer.m_NumberOfSentMessages);
      byte[] _shouldBeInBuffer = CommonDefinitions.GetTestBinaryArray();
      CollectionAssert.AreEqual(_writer.m_Buffer, _shouldBeInBuffer);
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
    #endregion

    #region to be promoted to the codebase
    public class BinaryMessagePackageEncoder : BinaryMessageEncoder
    {

      #region creator
      public BinaryMessagePackageEncoder()
      {
        State = new MyState();
      }
      #endregion

      #region MyRegion
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
      protected override void EncodeHeaders()
      {
        //TODO must be implemented after definition of the details by the specyficatiopn;
      }
      protected override void SendMessage(byte[] buffer)
      {
        m_NumberOfSentMessages++;
        m_NumberOfSentBytes += buffer.Length;
        m_Buffer = new byte[buffer.Length];
        buffer.CopyTo(m_Buffer, 0);
      }
      #endregion

      #region tetst instrumentation
      internal int m_NumberOfSentMessages = 0;
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork;
      internal byte[] m_Buffer = null;
      #endregion

    }

    #endregion
  }

}
