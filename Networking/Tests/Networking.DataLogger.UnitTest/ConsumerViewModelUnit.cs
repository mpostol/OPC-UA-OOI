
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.DataLogger.UnitTest.Instrumentation;

namespace UAOOI.Networking.DataLogger.UnitTest
{

  [TestClass]
  public class ConsumerViewModelUnit
  {
    [TestMethod]
    public void ConstructorTest()
    {
      TestConsumerViewModel _viewModel = new TestConsumerViewModel();
    }
    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void TraceTest()
    {
      TestConsumerViewModel _viewModel = new TestConsumerViewModel();
      _viewModel.Trace("");
    }

  }
}
