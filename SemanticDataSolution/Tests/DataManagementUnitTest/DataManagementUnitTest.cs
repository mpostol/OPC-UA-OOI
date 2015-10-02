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

  internal class TestAssociation : IAssociation
  {
    public TestAssociation()
    {
      Address = null;
    }
    public ISemanticData DataDescriptor
    {
      get { throw new NotImplementedException(); }
    }
    public IAssociationState State
    {
      get { throw new NotImplementedException(); }
    }
    public event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    public ISemanticDataItemConfiguration DefaultConfiguration
    {
      get { throw new NotImplementedException(); }
    }
    public ISemanticDataItemConfiguration this[string SymbolicName]
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
    public int CompareTo(object obj)
    {
      throw new NotImplementedException();
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
    public IEndPointConfiguration Address
    {
      get;
      set;
    }
  }

}
