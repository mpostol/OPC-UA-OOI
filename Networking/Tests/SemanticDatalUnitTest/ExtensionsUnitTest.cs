
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.SemanticData.UnitTest
{
  [TestClass]
  public class ExtensionsUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_ExtensionsUnitTest")]
    public void IncRollOverTestMethod()
    {
      Assert.AreEqual<ushort>(0, ushort.MaxValue.IncRollOver());
      Assert.AreEqual<ushort>(1, ((ushort)0).IncRollOver());
      Assert.AreEqual<ushort>(ushort.MaxValue / 2 + 1, ((ushort)(ushort.MaxValue / 2)).IncRollOver());
    }
  }
}
