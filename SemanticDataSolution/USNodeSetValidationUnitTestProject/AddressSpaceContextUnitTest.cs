
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using UAOOI.SemanticData.UnitTest.Helpers;


namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class AddressSpaceContextUnitTest
  {

    [TestMethod]
    public void AddressSpaceContextUTTryGetUANodeContextTestMethod()
    {
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      Assert.IsNotNull(_as);
      Assert.IsNotNull(_as.UTTryGetUANodeContext(VariableTypes.PropertyType));
    }

    [TestMethod]
    public void AddressSpaceContextUnitTestTestMethod()
    {
      UANodeSet _ns = TestData.CreateNodeSetModel();
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.IsTrue(_ns.NamespaceUris.Length >= 1, "Wrong test data - NamespaceUris must contain more then 2 items");
      IAddressSpaceContext _as = new AddressSpaceContext(x => { Helpers.TraceHelper.TraceDiagnostic(x, _trace, ref _diagnosticCounter); });
      Assert.IsNotNull(_as);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ImportUANodeSet(_ns);
      Assert.IsNotNull(_ns);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ValidateAndExportModel(_ns.NamespaceUris[0]);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }

  }
}
