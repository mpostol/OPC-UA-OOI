using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class BinaryMessageWriterUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_BinaryMessageWriter")]
    public void CreatorTestMethod1()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      Assert.IsNotNull(_bmw);
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
    }
    [TestMethod]
    [TestCategory("DataManagement_BinaryMessageWriter")]
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
    [TestCategory("DataManagement_BinaryMessageWriter")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void NullableTestMethod()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      _bmw.AttachToNetwork();
      ProducerBinding _binding = new ProducerBinding();
      _binding.Value = new Nullable<float>();
      ((IMessageWriter)_bmw).Send(x => _binding, 1);
    }
    [TestMethod]
    [TestCategory("DataManagement_BinaryMessageWriter")]
    public void SendTestMethod()
    {
      TypesMessageWriter _bmw = new TypesMessageWriter();
      _bmw.AttachToNetwork();
      ProducerBinding _binding = new ProducerBinding();
      _binding.Value = String.Empty;
      object[] _values = new object[] { (ulong)123, (uint)123, (ushort)123, "123", (float)123, (sbyte)123, (long)123, (int)123, (short)123, (double)123, (decimal)123, new DateTime(123, 10, 1), (byte)123, true, 'A', new byte[] { 1, 2, 3 } };
      ((IMessageWriter)_bmw).Send((x) => { _binding.Value = _values[x]; return _binding; }, _values.Length);
    }

    private class TestClass
    { }
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

    /// <summary>
    /// Class MessageWriterBase - helper class that provides basic implementation of the <see cref="IMessageWriter"/>
    /// </summary>
    public abstract class MessageWriterBase : IMessageWriter
    {

      #region IMessageWriter
      /// <summary>
      /// Sends the data described by a data set collection to remote destination.
      /// </summary>
      /// <param name="producerBinding">Encapsulates functionality used by the <see cref="IMessageWriter" /> to collect all the data (data set items) required to prepare new message and send it over the network.</param>
      /// <param name="length">Number of items to be send used to calculate the length of the message.</param>
      /// <exception cref="System.ArgumentOutOfRangeException">
      /// Impossible to convert null value
      /// or
      /// </exception>
      void IMessageWriter.Send(Func<int, IProducerBinding> producerBinding, int length)
      {
        if (State.State != HandlerState.Operational)
          return;
        CreateMessage(length);
        for (int i = 0; i < length; i++)
        {
          IProducerBinding _pb = producerBinding(i);
          object _value = _pb.GetFromRepository();
          if (_value == null)
            throw new ArgumentOutOfRangeException("Impossible to convert null value");
          Type _type = _value.GetType();
          if (_type == typeof(byte[]))
            Write((byte[])_value, _pb.Parameter);
          else if (!IsIConvertible(_value, _pb.Parameter))
            throw new ArgumentOutOfRangeException(string.Format("Impossible to convert {0}", _value));
        }
        SendMessage();
      }
      /// <summary>
      /// If implemented in derived class gets the the state machine for this instance.
      /// </summary>
      /// <value>An object of <see cref="IAssociationState" /> providing implementation of the state machine governing this instance behavior.</value>
      public abstract IAssociationState State
      {
        get;
        protected set;
      }
      /// <summary>
      /// Attaches to network - initialize the underlying protocol stack and establish the connection with the broker is applicable.
      /// </summary>
      /// <remarks>Depending on the message transport layer type implementation of this function varies.</remarks>
      public abstract void AttachToNetwork();
      #endregion

      #region private

      #region Writer
      protected abstract void SendMessage();
      protected abstract void CreateMessage(int length);
      protected abstract void WriteUInt64(ulong value, object parameter);
      protected abstract void WriteUInt32(uint value, object parameter);
      protected abstract void WriteUInt16(ushort value, object parameter);
      protected abstract void WriteString(string value, object parameter);
      protected abstract void WriteSingle(float value, object parameter);
      protected abstract void WriteSByte(sbyte value, object parameter);
      protected abstract void WriteInt64(long value, object parameter);
      protected abstract void WriteInt32(int value, object parameter);
      protected abstract void WriteInt16(short value, object parameter);
      protected abstract void WriteDouble(double value, object parameter);
      protected abstract void WriteDecimal(decimal value, object parameter);
      protected abstract void WriteDateTime(DateTime dateTime, object parameter);
      protected abstract void WriteByte(byte value, object parameter);
      protected abstract void WriteBool(bool value, object parameter);
      protected abstract void WriteChar(char value, object parameter);
      protected abstract void Write(byte[] value, object parameter);
      #endregion

      private bool IsIConvertible(object value, object parameter)
      {
        IConvertible _cv = value as IConvertible;
        if (_cv == null)
          return false;
        switch (_cv.GetTypeCode())
        {
          case TypeCode.Boolean:
            WriteBool((Boolean)value, parameter);
            break;
          case TypeCode.Byte:
            WriteByte((Byte)value, parameter);
            break;
          case TypeCode.Char:
            WriteChar((Char)value, parameter);
            break;
          case TypeCode.DBNull:
            throw new ArgumentOutOfRangeException("the value cannot be TypeCode.DBNull");
          case TypeCode.DateTime:
            WriteDateTime((DateTime)value, parameter);
            break;
          case TypeCode.Decimal:
            WriteDecimal((Decimal)value, parameter);
            break;
          case TypeCode.Double:
            WriteDouble((Double)value, parameter);
            break;
          case TypeCode.Empty:
            throw new ArgumentOutOfRangeException("the value cannot be TypeCode.Empty");
          case TypeCode.Int16:
            WriteInt16((Int16)value, parameter);
            break;
          case TypeCode.Int32:
            WriteInt32((Int32)value, parameter);
            break;
          case TypeCode.Int64:
            WriteInt64((Int64)value, parameter);
            break;
          case TypeCode.Object:
            return false;
          case TypeCode.SByte:
            WriteSByte((SByte)value, parameter);
            break;
          case TypeCode.Single:
            WriteSingle((Single)value, parameter);
            break;
          case TypeCode.String:
            WriteString((String)value, parameter);
            break;
          case TypeCode.UInt16:
            WriteUInt16((UInt16)value, parameter);
            break;
          case TypeCode.UInt32:
            WriteUInt32((UInt32)value, parameter);
            break;
          case TypeCode.UInt64:
            WriteUInt64((UInt64)value, parameter);
            break;
          default:
            return false;
        }
        return true;
      }

      #endregion

    }
    public class TypesMessageWriter : MessageWriterBase
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
      internal bool MassageCreated = false;
      #endregion

      private System.IO.BinaryWriter m_BinaryWriter;
      /// <summary>
      /// Class MyState.
      /// </summary>
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
      protected override void SendMessage()
      {
      }


    }
  }
}
