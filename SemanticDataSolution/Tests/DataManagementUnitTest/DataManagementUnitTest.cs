
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class DataManagementUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement")]
    public void IntegrationServicesCreatorTestMethod()
    {
      IntegrationServices _is = new IntegrationServices();
      Assert.IsNotNull(_is);
    }
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    [TestCategory("DataManagement")]
    public void IntegrationServicesRemoveAssociationNullTestMethod()
    {
      IntegrationServices _is = new IntegrationServices();
      Assert.IsNotNull(_is);
      _is.RemoveAssociation(null);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestCategory("DataManagement")]
    public void IntegrationServicesRemoveAssociationNotRegisteredTestMethod()
    {
      IntegrationServices _is = new IntegrationServices();
      Assert.IsNotNull(_is);
      _is.RemoveAssociation(new TestAssociation());
    }
  }

  internal class TestAssociation : Association
  {
    public TestAssociation()
      : base(new SD(), "DataManagementUnitTest".AddId(PersistentConfiguration.AssociationId))
    {

    }
    public IEnumerator<string> GetEnumerator()
    {
      throw new NotImplementedException();
    }
    public void Disable()
    {
      throw new NotImplementedException();
    }
    public void Enable()
    {
      throw new NotImplementedException();
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

    private class SD : ISemanticData
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

    protected internal override void AddMessageHandler(IMessageHandler messageHandler)
    {
      throw new NotImplementedException();
    }
  }

}
