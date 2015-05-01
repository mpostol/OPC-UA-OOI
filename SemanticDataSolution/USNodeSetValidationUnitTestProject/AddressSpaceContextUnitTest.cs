using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using UAOOI.SemanticData.UnitTest.Helpers;
using System.Linq;


namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class AddressSpaceContextUnitTest
  {
    [TestMethod]
    public void AddressSpaceContextUnitTestTestMethod()
    {
      UANodeSet _ns = TestData.CreateNodeSetModel();
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      Assert.IsTrue(_ns.NamespaceUris.Length >= 1, "Wrong test data - NamespaceUris must contain more then 2 items");
      AddressSpaceContext<ModelDesign> _as = new AddressSpaceContext<ModelDesign>(x => { Helpers.TraceHelper.TraceDiagnostic(x, _trace, ref _diagnosticCounter); });
      Assert.IsNotNull(_as);
      ModelDesign _md = _as.CreateInstance(_ns.NamespaceUris[0], x => x.ImportNodeSet(_ns, true));
      Assert.IsNotNull(_md);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
  }
}
