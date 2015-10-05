using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerAssociationUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociation")]
    [ExpectedException(typeof(NotImplementedException))]
    public void ProducerAssociationCreatorTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SD(), "DataManagement_ProducerAssociation");
      Assert.IsNotNull(_npa);
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
  }
}
