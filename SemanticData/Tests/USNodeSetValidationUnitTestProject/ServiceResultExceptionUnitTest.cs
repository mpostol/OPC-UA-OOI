using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{
  [TestClass]
  public class ServiceResultExceptionUnitTest
  {
    [TestMethod]
    [TestCategory("Code")]
    public void ServiceResultExceptionCreateTestMethod()
    {
      ServiceResultException _ex = new ServiceResultException();
      Assert.IsNotNull(_ex);
      Assert.IsNull(_ex.InnerException);
      Assert.IsNull(_ex.TraceMessage);
      Assert.IsFalse(String.IsNullOrEmpty(_ex.Message), _ex.Message);
      Assert.IsTrue(_ex.Message.Contains("UAOOI.SemanticData.UANodeSetValidation.ServiceResultException"));
    }
    [TestMethod]
    [TestCategory("Code")]
    public void ServiceResultExceptionCreateWithMessageTestMethod()
    {
      TraceMessage traceMessage = TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdInvalidSyntax, "BuildError_BadNodeIdInvalid");
      ServiceResultException _ex = new ServiceResultException(traceMessage, "test message");
      Assert.IsNotNull(_ex);
      Assert.IsNotNull(_ex.TraceMessage);
      Assert.IsNull(_ex.InnerException);
      Assert.AreEqual<string>("test message", _ex.Message);
      Assert.IsNotNull(_ex.TraceMessage);
      Assert.AreEqual<Focus>(BuildError.NodeIdInvalidSyntax.Focus, _ex.TraceMessage.BuildError.Focus);
      Assert.AreEqual<TraceEventType>(TraceEventType.Information, _ex.TraceMessage.TraceLevel);
    }
  }
}
