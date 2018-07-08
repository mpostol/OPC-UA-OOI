
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UnitTest.DataSerialization
{
  [TestClass]
  public class QualifiedNameUnitTest
  {
    [TestMethod]
    public void NotEqualOperatorTest()
    {
      QualifiedName _qualifiedName = null;
      Assert.IsFalse(_qualifiedName != null);
      _qualifiedName = new QualifiedName("name", 1);
      Assert.IsTrue(_qualifiedName != null);
    }
    [TestMethod]
    public void EqualOperatorTest()
    {
      QualifiedName _qualifiedName = null;
      Assert.IsTrue(_qualifiedName == null);
      _qualifiedName = new QualifiedName("name", 1);
      Assert.IsFalse(_qualifiedName == null);
    }
    [TestMethod]
    public void EqualsTest()
    {
      QualifiedName _qualifiedName = new QualifiedName("name", 1);
      Assert.IsTrue(_qualifiedName.NamespaceIndexSpecified);
      Assert.IsFalse(_qualifiedName.Equals(null));
      Assert.IsTrue(_qualifiedName.Equals(_qualifiedName));
      QualifiedName _qualifiedNameSecond = new QualifiedName("name", 1);
      Assert.IsTrue(_qualifiedName.Equals(_qualifiedNameSecond));
      _qualifiedNameSecond = new QualifiedName("NAME", 1);
      Assert.IsFalse(_qualifiedName.Equals(_qualifiedNameSecond));
      _qualifiedNameSecond = new QualifiedName("name", 0);
      Assert.IsFalse(_qualifiedName.Equals(_qualifiedNameSecond));
    }
  }
}
