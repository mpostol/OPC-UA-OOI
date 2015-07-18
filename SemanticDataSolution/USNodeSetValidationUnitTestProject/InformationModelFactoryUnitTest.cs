
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{

  [TestClass]
  public class InformationModelFactoryUnitTest
  {

    [ClassInitialize]
    public static void InformationModelFactoryClassInitialize(TestContext context)
    {
      m_FactoryBase = new InformationModelFactoryBase();
      Assert.IsNotNull(m_FactoryBase);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportReferenceTypeFactoryTestMethod()
    {
      IReferenceTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IReferenceTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportObjectTypeFactoryTestMethod2()
    {
      IObjectTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IObjectTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportVariableTypeFactoryTestMethod2()
    {
      IVariableTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IVariableTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelIExportDataTypeFactoryFactoryTestMethod2()
    {
      IDataTypeFactory _new = m_FactoryBase.NewExportNodeFFactory<IDataTypeFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportObjectInstanceFactoryTestMethod2()
    {
      IObjectInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IObjectInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportPropertyInstanceFactoryTestMethod2()
    {
      IPropertyInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IPropertyInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportVariableInstanceFactoryTestMethod2()
    {
      IVariableInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IVariableInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportMethodInstanceFactoryTestMethod2()
    {
      IMethodInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IMethodInstanceFactory>();
      Assert.IsNotNull(_new);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void InformationModelFactoryIExportViewInstanceFactoryTestMethod2()
    {
      IViewInstanceFactory _new = m_FactoryBase.NewExportNodeFFactory<IViewInstanceFactory>();
      Assert.IsNotNull(_new);
    }

    private static InformationModelFactoryBase m_FactoryBase = null;

  }
}
