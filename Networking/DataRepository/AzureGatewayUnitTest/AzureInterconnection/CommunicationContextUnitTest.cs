//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test.AzureInterconnection
{
  [TestClass]
  public class CommunicationContextUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      Mock<ILogger<CommunicationContext>> loggerFixture = new Mock<ILogger<CommunicationContext>>();
      AzureDeviceParameters azureParametersFixture = AzureDeviceParameters.Parse(String.Empty);
      Mock<IDTOProvider> IDTOProviderFixture = new Mock<IDTOProvider>();
      Assert.ThrowsException<ArgumentNullException>(() => new CommunicationContext(null, "qwerty", azureParametersFixture, loggerFixture.Object));
      Assert.ThrowsException<ArgumentNullException>(() => new CommunicationContext(IDTOProviderFixture.Object, "qwerty", null, loggerFixture.Object));
      Assert.ThrowsException<ArgumentNullException>(() => new CommunicationContext(IDTOProviderFixture.Object, "qwerty", azureParametersFixture, null));
      CommunicationContext _fixture = new CommunicationContext(IDTOProviderFixture.Object, "qwerty", azureParametersFixture, loggerFixture.Object);
      Assert.ThrowsException<ApplicationException>(() => _fixture.DisconnectRequest());
    }
  }
}
