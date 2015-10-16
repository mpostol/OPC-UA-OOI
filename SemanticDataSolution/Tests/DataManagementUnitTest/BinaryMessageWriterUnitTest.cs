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
      UDPMessageWriter _bmw = new UDPMessageWriter();
      Assert.IsNotNull(_bmw);
      _bmw.AttachToNetwork();
      Assert.IsTrue(_bmw.State.State == HandlerState.Operational);
    }
    [TestMethod]
    [TestCategory("DataManagement_BinaryMessageWriter")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ObjectTestMethod()
    {
      UDPMessageWriter _bmw = new UDPMessageWriter();
      _bmw.AttachToNetwork();
      ProducerBinding _binding = new ProducerBinding();
      _binding.Value = new TestClass();
      ((IMessageWriter)_bmw).Send(x => _binding, 1);
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
    public abstract class BinaryMessageWriter : IMessageWriter
    {

      public BinaryMessageWriter()
      {
        State = new MyState();
      }
      #region IMessageWriter
      public void Send(Func<int, IProducerBinding> producerBinding, int length)
      {
        if (State.State != HandlerState.Operational)
          return;
        CreateMessage(length);
        for (int i = 0; i < length; i++)
        {
          IProducerBinding _pb = producerBinding(i);
          object _value = _pb.GetFromRepository();
          Type _type = _value.GetType();
          if (_type == typeof(byte[]))
            Write((byte[])_value, _pb.Parameter);
          else if (!IsIConvertible(_value, _pb.Parameter))
            throw new ArgumentOutOfRangeException(string.Format("Imposible to convert {0}", _value));
        }
      }
      public IAssociationState State
      {
        get;
        private set;
      }
      public void AttachToNetwork()
      {
        Assert.AreNotEqual<HandlerState>(HandlerState.Operational, State.State);
        State.Enable();
      }
      #endregion

      #region private

      #region Writer
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
    public class UDPMessageWriter : BinaryMessageWriter
    {

      private System.IO.BinaryWriter m_BinaryWriter;

      protected override void WriteUInt64(ulong value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteUInt32(uint value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteUInt16(ushort value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteString(string value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteSingle(float value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteSByte(sbyte value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteInt64(long value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteInt32(int value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteInt16(short value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteDouble(double value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteDecimal(decimal value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteDateTime(DateTime dateTime, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteByte(byte value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteBool(bool value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void WriteChar(char value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void Write(byte[] value, object parameter)
      {
        throw new NotImplementedException();
      }

      protected override void CreateMessage(int length)
      {
        MeassageCreated = true;
      }
      internal bool MeassageCreated = false;
    }
  }
}
