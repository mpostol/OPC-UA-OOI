
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.Networking.SimulatorInteroperabilityTest.UnitTest
{
  [TestClass]
  public class SimulatorViewModelUnitTest
  {

    [TestMethod]
    public void ConstructorTest()
    {

      SimulatorViewModel _simulator = new SimulatorViewModel();
      Assert.IsNotNull(_simulator.ProducerRestartCommand);

    }

  }
}
