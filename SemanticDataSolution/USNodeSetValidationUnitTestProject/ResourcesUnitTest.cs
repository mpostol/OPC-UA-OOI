using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class ResourcesUnitTest
  {
    [TestMethod]
    public void OpcUaNodeSet2TestMethod()
    {
      UANodeSet _standard = Extensions.LoadResource<UANodeSet>(Extensions.UADefinedTypesName);
      Assert.IsNotNull(_standard);
      Assert.IsNull(_standard.NamespaceUris);
    }
  }
}
