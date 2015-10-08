
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows.Data;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  [TestClass]
  public class PeriodicDataMessageUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_PeriodicDataMessage")]
    public void CreatorTestMethod()
    {
      PeriodicDataMessage _newMessage = new PeriodicDataMessage(new object[] { 1, "string" });
      Assert.IsNotNull(_newMessage);
    }
    [TestMethod]
    [TestCategory("DataManagement_PeriodicDataMessage")]
    public void IAmDestinationTestMethod()
    {
      Guid _guid = Guid.NewGuid();
      PeriodicDataMessage _newMessage = new PeriodicDataMessage(new object[] { 1, "string" }, _guid);
      Assert.IsNotNull(_newMessage);
      Assert.IsTrue(_newMessage.IAmDestination(new SemanticData(_guid)));
    }
    [TestMethod]
    [TestCategory("DataManagement_PeriodicDataMessage")]
    public void UpdateMyValuesTestMethod()
    {
      Object[] _buffer = new object[2];
      Guid _guid = Guid.NewGuid();
      PeriodicDataMessage _newMessage = new PeriodicDataMessage(new object[] { 1, "string" }, _guid);
      Assert.IsNotNull(_newMessage);
      _newMessage.UpdateMyValues(x => new Binding(y => _buffer[x] = y));
      Assert.AreEqual<int>(1, (int)_buffer[0]);
      Assert.AreEqual<string>("string", (string)_buffer[1]);
    }
    private class Binding : IConsumerBinding
    {
      public Binding(Action<object> action)
      {
        m_Action = action;
      }
      public IValueConverter Converter
      {
        set { ;}
        get { return null; }
      }
      public Type TargetType
      {
        get { return null; }
      }
      public object Parameter
      {
        set { ;}
        get { return null; }
      }
      public CultureInfo Culture
      {
        set { ;}
        get { return null; }
      }
      public void Assign2Repository(object value)
      {
        m_Action(value);
      }
      public void OnEnabling()
      {
        throw new NotImplementedException();
      }
      public void OnDisabling()
      {
        throw new NotImplementedException();
      }
      private Action<object> m_Action;
    }
    private class SemanticData : ISemanticData
    {
      public SemanticData(System.Guid _guid)
      {
        this.Guid = _guid;
      }

      public Uri Identifier
      {
        get { throw new NotImplementedException(); }
      }

      public string SymbolicName
      {
        get { throw new NotImplementedException(); }
      }

      public IComparable NodeId
      {
        get { throw new NotImplementedException(); }
      }

      public Guid Guid
      {
        get;
        private set;
      }
    }

  }
}
