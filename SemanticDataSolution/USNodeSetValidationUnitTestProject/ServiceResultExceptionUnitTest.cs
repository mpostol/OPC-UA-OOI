using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class ServiceResultExceptionUnitTest
  {
    [TestMethod]
    public void ServiceResultExceptionCreateTestMethod()
    {
      Assert.IsNotNull(new ServiceResultException());
    }
    [TestMethod]
    public void ServiceResultExceptionCreateWithMessageTestMethod()
    {
      TraceMessage traceMessage = TraceMessage.BuildErrorTraceMessage(BuildError.BadNodeIdInvalid, "BuildError_BadNodeIdInvalid");
      ServiceResultException _ex = new ServiceResultException(traceMessage, "test message");
      Assert.IsNotNull(_ex);
      Assert.IsNotNull(_ex.TraceMessage);
      Assert.IsNull(_ex.InnerException);
      Assert.AreEqual<string>("test message", _ex.Message);
      Assert.IsNotNull(_ex.TraceMessage);
      Assert.AreEqual<Focus>(BuildError.BadNodeIdInvalid.Focus, _ex.TraceMessage.BuildError.Focus);
      Assert.AreEqual<TraceEventType>(TraceEventType.Information, _ex.TraceMessage.TraceLevel);
    }
  }
}
