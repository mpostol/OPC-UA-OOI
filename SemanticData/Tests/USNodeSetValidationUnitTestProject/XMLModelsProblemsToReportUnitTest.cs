//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
      Uri model = _as.ImportUANodeSet(_testDataFileInfo);
      _trace.Clear();
      _as.ValidateAndExportModel(model);
      foreach (TraceMessage item in _trace)
        Debug.WriteLine(item.ToString());
      //TODO ADI model from Embedded example import fails #509
      Assert.AreEqual<int>(3, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual<int>(3, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NodeClass).Count<TraceMessage>());
      Assert.AreEqual<int>(3, _trace.Where<TraceMessage>(x => x.BuildError.Identifier.Trim().Contains("P3-0502020001")).Count<TraceMessage>());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NonCategorized).Count<TraceMessage>());
    }

    [TestMethod]
    public void eoursel510Test()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\eoursel510\Opc.Ua.NodeSet2.TriCycleType_V1.1.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      IAddressSpaceContext _as = new AddressSpaceContext(z => _trace.Add(z));
      Uri model = _as.ImportUANodeSet(_testDataFileInfo);
      //Extensions is omitted during the import
      List<TraceMessage> importUANodeSetXML = _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.XML).ToList<TraceMessage>();
      Assert.AreEqual<int>(1, importUANodeSetXML.Count);
      Assert.AreEqual<string>("P0-0001010000", importUANodeSetXML[0].BuildError.Identifier);
      Assert.IsTrue(importUANodeSetXML[0].Message.Contains("Extensions is omitted during the import"));
      _trace.Clear();
      _as.ValidateAndExportModel(model);
      foreach (TraceMessage item in _trace)
        Debug.WriteLine(item.ToString());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.DataEncoding).Count<TraceMessage>());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.DataType).Count<TraceMessage>());
      Assert.AreEqual<int>(3, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Naming).Count<TraceMessage>());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NodeClass).Count<TraceMessage>());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NonCategorized).Count<TraceMessage>());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Reference).Count<TraceMessage>());
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus == Focus.XML).Count<TraceMessage>());
    }
  }
}