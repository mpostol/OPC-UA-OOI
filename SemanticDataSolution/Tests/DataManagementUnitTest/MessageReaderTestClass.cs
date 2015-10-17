
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class MessageReaderTestClass
  {
    [TestMethod]
    public void TestMethod1()
    {

    }
    public abstract class MessageReaderBase : IMessageReader, IPeriodicDataMessage
    {

      #region IMessageReader
      public event EventHandler<MessageEventArg> ReadMessageCompleted;
      public IAssociationState State
      {
        get { throw new NotImplementedException(); }
      }
      public void AttachToNetwork()
      {
        throw new NotImplementedException();
      }
      #endregion
      private void RaiseReadMessageCompleted()
      {
        EventHandler<MessageEventArg> _handler = ReadMessageCompleted;
        if (_handler == null)
          return;
        ReadMessageCompleted(this, new MessageEventArg(this));
      }
      #region IPeriodicDataMessage
      void IPeriodicDataMessage.UpdateMyValues(Func<int, IConsumerBinding> update, int length)
      {
        UInt64 _mask = 0x1;
        int _associationIndex = 0;
        for (int i = 0; i < length; i++)
        {
          if ((ContentFilter & _mask) > 0)
          {
            IConsumerBinding _binding = update(_associationIndex);
            Read(_binding);
          }
          _associationIndex++;
          _mask = _mask << 1;
        }
      }
      private void Read(IConsumerBinding binding)
      {
        if (!IsValueIConvertible(binding))
          throw new ArgumentOutOfRangeException(string.Format("Impossible to convert the type {0}", binding.TargetType.Name));
      }
      private bool IsValueIConvertible(IConsumerBinding binding)
      {
        object _value = null;
        System.IO.BinaryReader _r = null;
        switch (Type.GetTypeCode(binding.TargetType))
        {
          case TypeCode.Boolean:
            _value = ReadBoolean();
            break;
          case TypeCode.Byte:
            _value = ReadByte();
            break;
          case TypeCode.Char:
            _value = ReadChar();
            break;
          case TypeCode.DBNull:
            return false;
          case TypeCode.DateTime:
            _value = CommonDefinitions.GetUADateTime(_r.ReadInt64());
            break;
          case TypeCode.Decimal:
            return false;
          case TypeCode.Double:
            _value = ReadDouble();
            break;
          case TypeCode.Empty:
            return false;
          case TypeCode.Int16:
            _value = ReadInt16();
            break;
          case TypeCode.Int32:
            _value = ReadInt32();
            break;
          case TypeCode.Int64:
            _value = ReadInt64();
            break;
          case TypeCode.Object:
            return false;
          case TypeCode.SByte:
            _value = ReadSByte();
            break;
          case TypeCode.Single:
            _value = ReadSingle();
            break;
          case TypeCode.String:
            _value = ReadString();
            break;
          case TypeCode.UInt16:
            _value = ReadUInt16();
            break;
          case TypeCode.UInt32:
            _value = ReadUInt32();
            break;
          case TypeCode.UInt64:
            _value = ReadUInt64();
            break;
          default:
            return false;
        }
        binding.Assign2Repository(_value);
        return true;
      }
      #endregion

      protected abstract object ReadUInt64();
      protected abstract object ReadUInt32();
      protected abstract object ReadUInt16();
      protected abstract object ReadString();
      protected abstract object ReadSingle();
      protected abstract object ReadSByte();
      protected abstract object ReadInt64();
      protected abstract object ReadInt32();
      protected abstract object ReadInt16();
      protected abstract object ReadDouble();
      protected abstract object ReadChar();
      protected abstract object ReadByte();
      protected abstract object ReadBoolean();
      bool IPeriodicDataMessage.IAmDestination(ISemanticData dataId)
      {
        throw new NotImplementedException();
      }


      protected abstract ulong ContentFilter { get; set; }
    }
    private class TestMessageReaderBase : MessageReaderBase
    {

      #region MessageReaderBase
      protected override object ReadUInt64()
      {
        throw new NotImplementedException();
      }
      protected override object ReadUInt32()
      {
        throw new NotImplementedException();
      }
      protected override object ReadUInt16()
      {
        throw new NotImplementedException();
      }
      protected override object ReadString()
      {
        throw new NotImplementedException();
      }
      protected override object ReadSingle()
      {
        throw new NotImplementedException();
      }
      protected override object ReadSByte()
      {
        throw new NotImplementedException();
      }
      protected override object ReadInt64()
      {
        throw new NotImplementedException();
      }
      protected override object ReadInt32()
      {
        throw new NotImplementedException();
      }
      protected override object ReadInt16()
      {
        throw new NotImplementedException();
      }
      protected override object ReadDouble()
      {
        throw new NotImplementedException();
      }
      protected override object ReadChar()
      {
        throw new NotImplementedException();
      }
      protected override object ReadByte()
      {
        throw new NotImplementedException();
      }
      protected override object ReadBoolean()
      {
        throw new NotImplementedException();
      }
      protected override ulong ContentFilter
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
      #endregion

    }
  }

}
