using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class ContextUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void CreateContextTestMethod()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      UAModelContext<ModelDesign> _mc = new UAModelContext<ModelDesign>(_tm.Aliases, _tm.NamespaceUris, null);
      Assert.IsNotNull(_mc);
      Assert.IsNull(_mc.GetAddressSpaceContext);
    }
  }
}
