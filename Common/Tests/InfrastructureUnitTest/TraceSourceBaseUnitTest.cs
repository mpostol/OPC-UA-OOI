
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using UAOOI.Common.Infrastructure.Diagnostic;

namespace UAOOI.Common.Infrastructure.UnitTest
{
  [TestClass]
  public class TraceSourceBaseUnitTest
  {
    [TestMethod]
    public void CreationStateTestMethod()
    {
      TraceSourceBase _trace = new TraceSourceBase();
      _trace.TraceData(TraceEventType.Critical, 0, null);
    }
  }
}
