
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;

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
    public void InformationModelFactoryIExportReferenceTypeFactoryTestMethod()
    {
      IExportReferenceTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportReferenceTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelFactoryIExportObjectTypeFactoryTestMethod2()
    {
      IExportObjectTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportObjectTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelFactoryIExportVariableTypeFactoryTestMethod2()
    {
      IExportVariableTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportVariableTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelIExportDataTypeFactoryFactoryTestMethod2()
    {
      IExportDataTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportDataTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelFactoryIExportObjectInstanceFactoryTestMethod2()
    {
      IExportObjectInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportObjectInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelFactoryIExportPropertyInstanceFactoryTestMethod2()
    {
      IExportPropertyInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportPropertyInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelFactoryIExportVariableInstanceFactoryTestMethod2()
    {
      IExportVariableInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportVariableInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelFactoryIExportMethodInstanceFactoryTestMethod2()
    {
      IExportMethodInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportMethodInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    public void InformationModelFactoryIExportViewInstanceFactoryTestMethod2()
    {
      IExportViewInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IExportViewInstanceFactory>();
      Assert.IsNotNull(_new);
    }

  }
}
