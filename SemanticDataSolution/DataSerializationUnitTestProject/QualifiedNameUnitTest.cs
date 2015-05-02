using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.DataSerialization.UnitTest
{
  [TestClass]
  public class QualifiedNameUnitTest
  {
    [TestMethod]
    public void QualifiedNameTestMethod1()
    {
      QualifiedName _qn = new QualifiedName("Name");
      Assert.IsNotNull(_qn);
    }
    [TestMethod]
    public void QualifiedNameParseTestMethod2()
    {
      QualifiedName _qn = QualifiedName.Parse("0:Name");
      Assert.IsNotNull(_qn);
    }
  }
}
