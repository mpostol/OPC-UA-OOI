using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UnitTest
{

  [TestClass]
  public class ResourcesUnitTest
  {
    [TestMethod]
    public void OpcUaNodeSet2TestMethod()
    {
      UANodeSet _standard = Extensions.LoadResource<UANodeSet>(UANodeSet.UTUADefinedTypesName);
      Assert.IsNotNull(_standard);
      Assert.IsNull(_standard.NamespaceUris);
    }
    [TestMethod]
    public void ReadUADefinedTypesTestMethod()
    {
      UANodeSet _standard = UANodeSet.ReadUADefinedTypes();
      Assert.IsNotNull(_standard);
      Assert.IsNull(_standard.NamespaceUris);
    }

  }
}
