//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers
{
  [TestClass]
  public class HelpersUnitTest
  {
    [TestMethod]
    [TestCategory("Helpers")]
    [ExpectedException(typeof(NullReferenceException))]
    public void TraceDiagnosticTestMethod1()
    {
      List<TraceMessage> _listOfTraceMessage = null;
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, "Test string"), _listOfTraceMessage, ref _diagnosticCounter);
    }

    [TestMethod]
    [TestCategory("Helpers")]
    public void TraceNonCategorizedTest()
    {
      List<TraceMessage> _listOfTraceMessage = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, "Test string"), _listOfTraceMessage, ref _diagnosticCounter);
      Assert.AreEqual<int>(1, _listOfTraceMessage.Count);
      Assert.AreEqual<int>(1, _diagnosticCounter);
    }

    [TestMethod]
    [TestCategory("Helpers")]
    public void TraceNodeIdInvalidSyntaxTest()
    {
      List<TraceMessage> _listOfTraceMessage = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdInvalidSyntax, "NodeIdInvalidSyntax"), _listOfTraceMessage, ref _diagnosticCounter);
      Assert.AreEqual<int>(1, _listOfTraceMessage.Count);
      Assert.AreEqual<int>(1, _diagnosticCounter);
    }
    [TestMethod]
    [TestCategory("Helpers")]
    public void TraceDiagnosticInformationTest()
    {
      List<TraceMessage> _listOfTraceMessage = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.BuildErrorTraceMessage(BuildError.DiagnosticInformation, "NodeIdInvalidSyntax"), _listOfTraceMessage, ref _diagnosticCounter);
      Assert.AreEqual<int>(0, _listOfTraceMessage.Count);
      Assert.AreEqual<int>(1, _diagnosticCounter);
    }
  }
}