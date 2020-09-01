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
      CommunicationContext _fixture = new CommunicationContext(loggerFixture.Object);
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