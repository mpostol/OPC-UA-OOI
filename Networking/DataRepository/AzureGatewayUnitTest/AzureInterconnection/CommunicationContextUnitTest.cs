//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
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
      using (CommunicationContext _fixture = new CommunicationContext(loggerFixture.Object))
      {
        Assert.AreEqual<ProvisioningRegistrationStatusType>(ProvisioningRegistrationStatusType.Unassigned, _fixture.GetProvisioningRegistrationStatusType);
        Assert.ThrowsException<AggregateException>(() => _fixture.Register(null).Wait());
        Assert.ThrowsException<AggregateException>(() => _fixture.Connect().Wait());
        Assert.ThrowsException<ApplicationException>(() => _fixture.TransferData(null, "Repository group"));
        Assert.ThrowsException<AggregateException>(() => _fixture.Connect().Wait());
      }
    }

    private class StateBaseFixture : CommunicationContext.StateBase
    {
      public StateBaseFixture(CommunicationContext communicationContext) : base(communicationContext)
      {
      }

      public override ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => throw new NotImplementedException();

      public override Task<bool> Connect()
      {
        throw new NotImplementedException();
      }

      public override void DisconnectRequest()
      {
        throw new NotImplementedException();
      }

      public override Task<bool> Register(IAzureEnabledNetworkDevice device)
      {
        throw new NotImplementedException();
      }

      public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
      {
        throw new NotImplementedException();
      }
    }
  }
}