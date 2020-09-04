//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Provisioning.Client;
using System;
using System.Threading.Tasks;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  internal class UnassignedState : CommunicationContext.StateBase
  {
    public UnassignedState(CommunicationContext communicationContext) : base(communicationContext)
    {
    }

    #region CommunicationContext.StateBase

    public override ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => ProvisioningRegistrationStatusType.Unassigned;


    public override async Task<RegisterResult> Register()
    {
      return RegisterResult.Failed;
    }

    public override async Task<bool> Connect()
    {
      return await Task.FromException<bool>(new ApplicationException($"The operation {nameof(Connect)} is not allowed in the {nameof(UnassignedState)}"));
    }

    public override void DisconnectRequest()
    {
      throw new ApplicationException($"The operation {nameof(DisconnectRequest)} is not allowed in the {nameof(UnassignedState)}.");
    }

    public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      throw new ApplicationException($"The operation {nameof(TransferData)} is not allowed in the {nameof(UnassignedState)}");
    }

    #endregion CommunicationContext.StateBase

    #region private

    private const string GlobalDeviceEndpoint = "global.azure-devices-provisioning.net";

    #endregion private
  }
}