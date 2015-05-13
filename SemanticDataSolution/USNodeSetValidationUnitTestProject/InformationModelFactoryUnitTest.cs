using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UnitTest
{

  [TestClass]
  public class InformationModelFactoryUnitTest
  {
    private static InformationModelFactoryBase m_FactoryBase = null;

    [ClassInitialize]
    public static void InformationModelFactoryClassInitialize(TestContext context)
    {
      m_FactoryBase = new InformationModelFactoryBase();
      Assert.IsNotNull(m_FactoryBase);
    }
    [TestMethod]
    public void InformationModelFactoryTestMethod2()
    {
      IExportReferenceTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportReferenceTypeFactory>();
      Assert.IsNotNull(_new);
    }

  }
}
