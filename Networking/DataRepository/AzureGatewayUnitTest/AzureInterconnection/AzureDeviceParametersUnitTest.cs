//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using Microsoft.Azure.Devices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test.AzureInterconnection
{
  [TestClass]
  public class AzureDeviceParametersUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      AzureDeviceParameters instaneToTest = AzureDeviceParameters.Parse(String.Empty);
      Assert.IsNotNull(instaneToTest);
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzureDeviceId));
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzurePrimaryKey));
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzureScopeId));
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzureSecondaryKey));
      Assert.AreEqual<TimeSpan>(TimeSpan.FromSeconds(1.0), instaneToTest.PublishingInterval);
      Assert.AreEqual<TransportType>(default(TransportType), instaneToTest.TransportType);
    }
  }
}
