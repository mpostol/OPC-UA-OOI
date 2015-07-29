
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{
  [TestClass]
  public class UAModelContextUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentNullException))]
    [TestCategory("Code")]
    public void CreateUAModelContextNodeAliasNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      UAModelContext _mc = new UAModelContext(null, _tm.NamespaceUris, _as);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void CreateUAModelContextModelNamespaceUrisNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      UAModelContext _mc = new UAModelContext(_tm.Aliases, null, _as);
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void CreateUAModelContextAddressSpaceContextNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      UAModelContext _mc = new UAModelContext(_tm.Aliases, _tm.NamespaceUris, null);
      Assert.IsNotNull(_mc);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void CreateUAModelContext()
    { 
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      UAModelContext _mc = new UAModelContext(_tm.Aliases, _tm.NamespaceUris, _as);
      Assert.IsNotNull(_mc);
    }
  }
}
