using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class AssociationUnitTest
  {
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
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "allisName");
      Assert.IsNotNull(_nt);
      Assert.IsNotNull(_nt.DefaultConfiguration);
      Assert.IsNull(_nt.Address);
      Assert.IsNotNull(_nt.State);
      Assert.AreEqual<HandlerState>(HandlerState.NoConfiguration, _nt.State.State);
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void AssociationDefaultConfigurationTestMethod3()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "allisName2");
      Assert.IsNotNull(_nt);
    }
    private class TestAssociation : Association
    {
      public TestAssociation(ISemanticData data, string aliasName)
        : base(data, aliasName)
      {
        Address = null;
      }
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
        throw new NotImplementedException();
      }
      protected override void OnEnabling()
      {
        throw new NotImplementedException();
      }
      protected override void OnDisabling()
      {
        throw new NotImplementedException();
      }
    }
    private class TestISemanticData : ISemanticData
    {

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
  }
}
