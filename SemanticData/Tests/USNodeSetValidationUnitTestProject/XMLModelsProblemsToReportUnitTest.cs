//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  [DeploymentItem(@"XMLModels\ProblemsToReport", @"ProblemsToReport\")]
  public class XMLModelsProblemsToReportUnitTest
  {
    [TestMethod]
    public void ADITest()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\ADI#509\Opc.Ua.Adi.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      IAddressSpaceContext _as = new AddressSpaceContext(z => _trace.Add(z));
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ValidateAndExportModel(@"http://opcfoundation.org/UA/ADI/");
      IEnumerable<TraceMessage> vitalMessageserrors = _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic);
      IEnumerable<TraceMessage> focusNodeClass = _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NodeClass);
      Assert.IsFalse(focusNodeClass.Where<TraceMessage>(x => !x.BuildError.Identifier.Trim().Contains("P3-0502020001")).Any<TraceMessage>());

      //Assert.Inconclusive("The import returns unexpected errors.");

      Assert.AreEqual<int>(6, vitalMessageserrors.Count<TraceMessage>());

      Assert.AreEqual<int>(3, focusNodeClass.Count<TraceMessage>());
      Debug.WriteLine(nameof(Focus.NodeClass));
      foreach (TraceMessage item in focusNodeClass)
        Debug.WriteLine(item.ToString());

      IEnumerable<TraceMessage> focusNonCategorized = _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NonCategorized);
      Assert.AreEqual<int>(2, focusNonCategorized.Count<TraceMessage>());
      Debug.WriteLine(nameof(Focus.NonCategorized));
      foreach (TraceMessage item in focusNonCategorized)
        Debug.WriteLine(item.ToString());
    }
    [TestMethod]
    public void eoursel510Test()
    {

      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\eoursel510\Opc.Ua.NodeSet2.TriCycleType_V1.1.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      IAddressSpaceContext _as = new AddressSpaceContext(z => _trace.Add(z));
      _as.ImportUANodeSet(_testDataFileInfo);
      //Extensions is omitted during the import
      Assert.AreEqual<int>(10, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.XML).Count<TraceMessage>());
      _as.ValidateAndExportModel();
      Assert.AreEqual<int>(16, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.XML).Count<TraceMessage>());

    }
  }
}