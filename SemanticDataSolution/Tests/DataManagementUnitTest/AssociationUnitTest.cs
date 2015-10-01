using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationCreatorTestMethod4");
      Assert.IsNotNull(_nt);
      Assert.AreEqual<int>(0, _nt.CompareTo(_nt));
      TestAssociation _nt1 = new TestAssociation(_testISemanticData, "AssociationCreatorTestMethod3");
      Assert.IsNotNull(_nt);
      Assert.AreEqual<int>(-1, _nt1.CompareTo(_nt));
      Assert.AreEqual<int>(1, _nt.CompareTo(_nt1));
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
      public TestAssociation(ISemanticData data, string aliasName, bool sucess)
        : base(data, aliasName)
      {
        Address = null;
        m_Success = sucess;
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
    private class TestISemanticData : ISemanticData
    {

      public Uri Identifier
      {
        get { return new Uri(@"Http://commsvr.com"); }
      }
      public string SymbolicName
      {
        get { throw new NotImplementedException(); }
      }
      public IComparable NodeId
      {
        get { throw new NotImplementedException(); }
      }
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
    #endregion

  }
}
