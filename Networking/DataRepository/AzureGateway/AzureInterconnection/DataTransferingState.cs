//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  internal class DataTransferingState : CommunicationContext.StateBase
  {
    public DataTransferingState( CommunicationContext communicationContext) : base(communicationContext)
    {
    }

    #region CommunicationContext.StateBase

    public override ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => throw new NotImplementedException();

    public override Task<bool> Connect()
    {
      throw new ApplicationException($"The operation {nameof(Connect)} is not allowed in the {nameof(DataTransferingState)}");
    }

    public override Task<RegisterResult> Register()
    {
      throw new ApplicationException($"The operation {nameof(Connect)} is not allowed in the {nameof(DataTransferingState)}");
    }

    public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      throw new ApplicationException($"The operation {nameof(Connect)} is not allowed in the {nameof(DataTransferingState)}");
    }

    public override void DisconnectRequest()
    {
    }

    #endregion CommunicationContext.StateBase

    #region private


    #endregion private
  }
}