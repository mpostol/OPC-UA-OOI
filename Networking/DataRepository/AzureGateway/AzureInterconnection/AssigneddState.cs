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
  internal class AssigneddState : CommunicationContext.StateBase
  {
    #region constructor

    public AssigneddState(CommunicationContext communicationContext) : base(communicationContext)
    {
    }

    #endregion constructor

    #region CommunicationContext.StateBase

    public override ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => ProvisioningRegistrationStatusType.Assigned;

    public override async Task<bool> Connect()
    {
      return false;
    }

    public override void DisconnectRequest()
    {
      throw new NotImplementedException();
    }

    public override async Task<RegisterResult> Register()
    {
      return await Task.FromException<RegisterResult>(new ApplicationException($"The operation {nameof(Register)} is not allowed in the {nameof(AssigneddState)}"));
    }

    public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      throw new ApplicationException($"The operation {nameof(TransferData)} is not allowed in the {nameof(AssigneddState)}");
    }

    #endregion CommunicationContext.StateBase
  }
}