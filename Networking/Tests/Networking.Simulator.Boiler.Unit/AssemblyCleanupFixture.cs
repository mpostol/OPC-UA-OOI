//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.Simulator.Boiler.UnitTest.CommonServiceLocatorInstrumentation;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest
{
  [TestClass]
  public static class AssemblyCleanupFixture
  {
    [TestMethod]
    [AssemblyCleanup]
    public static void AssemblyCleanupTest()
    {
      Assert.IsFalse(Logger.Singleton.CheckForErrors());
    }
  }
}
