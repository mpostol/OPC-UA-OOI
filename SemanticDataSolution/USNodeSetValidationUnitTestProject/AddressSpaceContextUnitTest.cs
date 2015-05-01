using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UnitTest.Helpers;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class AddressSpaceContextUnitTest
  {
    [TestMethod]
    public void AddressSpaceContextUnitTestTestMethod()
    {
      AddressSpaceContext<ModelDesign> _as = new AddressSpaceContext<ModelDesign>(x => { });
      Assert.IsNotNull(_as);
    }
  }
}
