using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class AssociationUnitTest
  {
    #region test
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    [TestCategory("DataManagement_Association")]
    public void AssociationCreatorTestMethod1()
    {
      TestAssociation _nt = new TestAssociation(null, null);
    }
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    [TestCategory("DataManagement_Association")]
    public void AssociationCreatorTestMethod2()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, null);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestCategory("DataManagement_Association")]
    public void AssociationCreatorTestMethod3()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "allisName");
      _nt = new TestAssociation(_testISemanticData, "allisName");
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void AssociationCreatorTestMethod4()
    {
      string _alias = "AssociationCreatorTestMethod4";
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, _alias);
      Assert.IsNotNull(_nt);
      Assert.IsNull(_nt.Address);
      Assert.IsNotNull(_nt.DataDescriptor);
      Assert.IsNotNull(_nt.DataDescriptor.Identifier);
      Assert.IsNotNull(_nt.DefaultConfiguration);
      Assert.IsNotNull(_nt.State);
      Assert.AreEqual<HandlerState>(HandlerState.NoConfiguration, _nt.State.State);
      Assert.AreEqual<string>(_alias, _nt.ToString());
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void AssociationCompareToTestMethod()
    {
      ISemanticData _testISemanticData0 = new TestISemanticData("TestISemanticData1", 0);
      Assert.IsNotNull(_testISemanticData0);
      TestAssociation _nt0 = new TestAssociation(_testISemanticData0, "AssociationCompareToTestMethod0");
      Assert.IsNotNull(_nt0);
      Assert.AreEqual<int>(0, _nt0.CompareTo(_nt0));
      ISemanticData _testISemanticData1 = new TestISemanticData("TestISemanticData0", 0);
      Assert.IsNotNull(_testISemanticData1);
      TestAssociation _nt1 = new TestAssociation(_testISemanticData1, "AssociationCompareToTestMethod1");
      Assert.IsNotNull(_nt1);
      Assert.AreEqual<int>(1, _nt1.CompareTo(_nt0));
      Assert.AreEqual<int>(-1, _nt0.CompareTo(_nt1));
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AssociationStateDisableTestMethod()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationStateDisableTestMethod");
      Assert.IsNotNull(_nt);
      _nt.State.Disable();
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AssociationStateEnableTestMethod()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationStateEnableTestMethod");
      Assert.IsNotNull(_nt);
      _nt.State.Enable();
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void AssociationInitializeMethod()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationInitializeMethod");
      Assert.IsNotNull(_nt);
      int _eventsCount = 0;
      IAssociationState _lastState = null;
      _nt.StateChangedEventHandler += (x, y) => { _eventsCount++; _lastState = y.State; };
      Assert.AreEqual<HandlerState>(HandlerState.NoConfiguration, _nt.State.State);
      Assert.AreEqual<int>(0, _eventsCount);
      Assert.IsNull(_lastState);
      _nt.Initialize();
      Assert.AreEqual<int>(1, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _lastState.State);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _nt.State.State);
      _nt.State.Enable();
      Assert.AreEqual<int>(2, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Operational, _lastState.State);
      Assert.AreEqual<HandlerState>(HandlerState.Operational, _nt.State.State);
      _nt.State.Disable();
      Assert.AreEqual<int>(3, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _lastState.State);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _nt.State.State);
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AssociationInitializeMethod2()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationInitializeMethod2", false);
      Assert.IsNotNull(_nt);
      int _eventsCount = 0;
      IAssociationState _lastState = null;
      _nt.StateChangedEventHandler += (x, y) => { _eventsCount++; _lastState = y.State; };
      Assert.AreEqual<HandlerState>(HandlerState.NoConfiguration, _nt.State.State);
      _nt.Initialize();
      Assert.AreEqual<int>(1, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Error, _lastState.State);
      Assert.AreEqual<HandlerState>(HandlerState.Error, _nt.State.State);
      _nt.State.Enable();
    }
    #endregion

    #region private
    private class TestAssociation : Association
    {
      public TestAssociation(ISemanticData data, string aliasName, bool success)
        : base(data, PersistentConfiguration.GetDataSet(), aliasName, new IBF(), new IEF())
      {
        Address = null;
        m_Success = success;
      }
      public TestAssociation(ISemanticData data, string aliasName)
        : this(data, aliasName, true)
      { }
      protected override ISemanticDataItemConfiguration GetDefaultConfiguration()
      {
        return new TestISemanticDataItemConfiguration();
      }
      public override IEndPointConfiguration Address
      {
        get;
        set;
      }
      protected override void InitializeCommunication()
      {
        if (!m_Success)
          throw new InvalidOperationException("Wrong configuration");
      }
      protected override void OnEnabling() { }
      protected override void OnDisabling() { }
      private bool m_Success = false;

    }
    private class DataBrokerFactory : IBindingFactory
    {
      public IBinding GetBinding(string repositoryGroup, string variableName)
      {
        return new Binding<int>(x => { });
      }
    }
    private class TestISemanticData : ISemanticData
    {
      public TestISemanticData()
        : this("SymbolicName".AddId(_count), _count)
      {
        _count++;
      }
      public TestISemanticData(string symbolicName, IComparable nodeId)
      {
        Identifier = new Uri(@"Http://commsvr.com");
        SymbolicName = symbolicName;
        NodeId = nodeId;
      }
      public Uri Identifier
      {
        get;
        private set;
      }
      public string SymbolicName
      {
        get;
        private set;
      }
      public IComparable NodeId
      {
        get;
        private set;
      }
      private static int _count = 0;
    }
    private class TestISemanticDataItemConfiguration : ISemanticDataItemConfiguration
    {
      public bool State
      {
        get { throw new NotImplementedException(); }
      }
      public void Enable()
      {
        throw new NotImplementedException();
      }
      public void Disable()
      {
        throw new NotImplementedException();
      }
    }
    private class IBF : IBindingFactory
    {

      public IBinding GetBinding(string repositoryGroup, string variableName)
      {
        return new MyBinding();
      }
      private class MyBinding : IBinding
      {

        public System.Windows.Data.IValueConverter Converter
        {
          set {  }
        }

        public Type TargetType
        {
          get { throw new NotImplementedException(); }
        }

        public object Parameter
        {
          set {  }
        }

        public System.Globalization.CultureInfo Culture
        {
          set {  }
        }

        public void Assign2Repository(object value)
        {
          throw new NotImplementedException();
        }
      }

    }
    private class IEF : IEncodingFactory
    {

      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        converter.Culture = null;
        converter.Converter = null;
        converter.Parameter = null;
      }
    }
    #endregion

  }
}
