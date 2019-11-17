//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
      Assert.AreEqual<int>(1, _diagnosticCounter);
      Assert.AreEqual<int>(0, _listOfTraceMessage.Count);
    }
    [TestMethod]
    [TestCategory("Helpers")]
    public void TraceDiagnosticTestMethod2()
    {
      List<TraceMessage> _listOfTraceMessage = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, "Test string"), _listOfTraceMessage, ref _diagnosticCounter);
      Assert.AreEqual<int>(1, _listOfTraceMessage.Count);
      Assert.AreEqual<int>(1, _diagnosticCounter);
    }
    [TestMethod]
    [TestCategory("Helpers")]
    public void TraceDiagnosticTestMethod3()
    {
      List<TraceMessage> _listOfTraceMessage = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdInvalidSyntax, "NodeIdInvalidSyntax"), _listOfTraceMessage, ref _diagnosticCounter);
      Assert.AreEqual<int>(1, _listOfTraceMessage.Count);
      Assert.AreEqual<int>(1, _diagnosticCounter);
    }
  }
}
