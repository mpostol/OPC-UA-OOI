
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;
using System.Windows.Data;
using System.Globalization;

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
      Object[] _buffor = new object[2];
      Guid _guid = Guid.NewGuid();
      PeriodicDataMessage _newMessage = new PeriodicDataMessage(new object[] { 1, "string" }, _guid);
      Assert.IsNotNull(_newMessage);
      _newMessage.UpdateMyValues(x => new Binding(y => _buffor[x] = y));
      Assert.AreEqual<int>(1, (int)_buffor[0]);
      Assert.AreEqual<string>("string", (string)_buffor[1]);
    }
    private class Binding : IBinding
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
