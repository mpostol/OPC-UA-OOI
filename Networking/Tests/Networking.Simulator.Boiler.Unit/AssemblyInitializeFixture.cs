//___________________________________________________________________________________
//
//  Copyright (C) 2018 Copyright, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.Simulator.Boiler.UnitTest.CommonServiceLocatorInstrumentation;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest
{
  [TestClass]
  public static class AssemblyInitializeFixture
  {
    [TestMethod]
    [AssemblyInitialize]
    public static void TestMethod1(TestContext context)
    {
      Logger _Logger = new Logger();
      Container _container = new Container(new Object[] { _Logger });
      ServiceLocator.SetLocatorProvider(() => _container);
      Assert.IsTrue(ServiceLocator.IsLocationProviderSet);
    }
  }
}
