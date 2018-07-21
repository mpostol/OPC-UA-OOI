//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
