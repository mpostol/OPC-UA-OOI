//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
      AzureDeviceParameters azureParametersFixture = AzureDeviceParameters.ParseRepositoryGroup(String.Empty);
      Mock<IDTOProvider> IDTOProviderFixture = new Mock<IDTOProvider>();
      Assert.ThrowsException<ArgumentNullException>(() => new CommunicationContext(null, "qwerty", azureParametersFixture));
      Assert.ThrowsException<ArgumentNullException>(() => new CommunicationContext(IDTOProviderFixture.Object, "qwerty", null));
      CommunicationContext _fixture = new CommunicationContext(IDTOProviderFixture.Object, "qwerty", azureParametersFixture);
      Assert.ThrowsException<ApplicationException>(() => _fixture.DisconnectRequest());
    }
  }
}