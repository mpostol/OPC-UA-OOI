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
    public void IntegrationServicesCreatorTestMethod()
    {
      IntegrationServices _is = new IntegrationServices();
      Assert.IsNotNull(_is);
    }
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void IntegrationServicesRemoveAssociationNullTestMethod()
    {
      IntegrationServices _is = new IntegrationServices();
      Assert.IsNotNull(_is);
      _is.RemoveAssociation(null);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IntegrationServicesRemoveAssociationNotRegisteredTestMethod()
    {
      IntegrationServices _is = new IntegrationServices();
      Assert.IsNotNull(_is);
      _is.RemoveAssociation(new TestAssociation());
    }
  }

  internal class TestAssociation : IAssociation
  {
    public ISemanticData DataDescriptor
    {
      get { throw new NotImplementedException(); }
    }
    public IAssociationState State
    {
      get { throw new NotImplementedException(); }
    }

    public event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;

    public IEndPointConfiguration Address
    {
      get { throw new NotImplementedException(); }
    }

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
    IEnumerator System.Collections.IEnumerable.GetEnumerator()
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
  }
}
