
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using UAOOI.SemanticData.UnitTest.Helpers;


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
      IAddressSpaceContext _as = new AddressSpaceContext(x => { Helpers.TraceHelper.TraceDiagnostic(x, _trace, ref _diagnosticCounter); });
      Assert.IsNotNull(_as);
      InformationModelFactoryBase _md = new InformationModelFactoryBase();
      _as.ImportUANodeSet(_ns.NamespaceUris[0], x => x.ImportNodeSet(_ns, true), _md);
      Assert.IsNotNull(_md);
      Assert.Inconclusive();
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }

  }
}
