
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.DataLogger.UnitTest.Instrumentation;

namespace UAOOI.Networking.DataLogger.UnitTest
{
  [TestClass]
  public class DataConsumerUnitTest
  {

    [TestMethod]
    public void ConstructorTest()
    {
      TestConsumerViewModel _viewModel = new TestConsumerViewModel();
      DataConsumer _DataConsumer = new DataConsumer(_viewModel);
    }
  }
}
