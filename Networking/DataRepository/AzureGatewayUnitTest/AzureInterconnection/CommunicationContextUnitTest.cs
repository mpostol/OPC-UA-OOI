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
      Mock<IAzureEnabledNetworkDevice> azureParametersFixture = new Mock<IAzureEnabledNetworkDevice>();
      using (CommunicationContext _fixture = new CommunicationContext(azureParametersFixture.Object, loggerFixture.Object))
      {
        Assert.AreEqual<ProvisioningRegistrationStatusType>(ProvisioningRegistrationStatusType.Unassigned, _fixture.GetProvisioningRegistrationStatusType);
        Assert.ThrowsException<AggregateException>(() => _fixture.Register().Wait());
        Assert.ThrowsException<AggregateException>(() => _fixture.Connect().Wait());
        Assert.ThrowsException<ApplicationException>(() => _fixture.TransferData(null, "Repository group"));
        Assert.ThrowsException<AggregateException>(() => _fixture.Connect().Wait());
        Assert.ThrowsException<ApplicationException>(() => _fixture.DisconnectRequest());
      }
    }

    [TestMethod]
    public void StateBaseTest()
    {
      Assert.ThrowsException<ArgumentNullException>(() => new StateBaseFixture(null));
      Assert.ThrowsException<ArgumentNullException>(() => new UnassignedState(null));
      Assert.ThrowsException<ArgumentNullException>(() => new AssigneddState(null));
    }

    #region instrumentation

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

      public override Task<RegisterResult> Register()
      {
        throw new NotImplementedException();
      }

      public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
      {
        throw new NotImplementedException();
      }
    }

    #endregion instrumentation
  }
}