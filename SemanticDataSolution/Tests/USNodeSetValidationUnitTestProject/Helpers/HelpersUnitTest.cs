
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers
{
  [TestClass]
  public class HelpersUnitTest
  {
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TraceDiagnosticTestMethod1()
    {
      List<TraceMessage> _listOfTraceMessage = null;
      int diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.DiagnosticTraceMessage("Test string"), _listOfTraceMessage, ref diagnosticCounter);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void TraceDiagnosticTestMethod2()
    {
      List<TraceMessage> _listOfTraceMessage = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.DiagnosticTraceMessage("Test string"), _listOfTraceMessage, ref _diagnosticCounter);
      Assert.AreEqual<int>(0, _listOfTraceMessage.Count);
      Assert.AreEqual<int>(1, _diagnosticCounter);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void TraceDiagnosticTestMethod3()
    {
      List<TraceMessage> _listOfTraceMessage = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      TraceHelper.TraceDiagnostic(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdInvalidSyntax, "NodeIdInvalidSyntax"), _listOfTraceMessage, ref _diagnosticCounter);
      Assert.AreEqual<int>(1, _listOfTraceMessage.Count);
      Assert.AreEqual<int>(0, _diagnosticCounter);
    }
  }
}
