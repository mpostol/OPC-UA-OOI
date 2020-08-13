//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test
{

  [TestClass]
  public class PartDataManagementSetupUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      Assert.Inconclusive("Reference to the CommonServiceLocator must be added");
      Assert.ThrowsException<InvalidOperationException>(() => new PartDataManagementSetup());
    }
  }

}
