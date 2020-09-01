//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Provisioning.Client;
using System.Threading.Tasks;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  internal interface IStateBase
  {
    Task<bool> Register(IAzureEnabledNetworkDevice device);

    Task<bool> Connect();

    void TransferData(IDTOProvider dataProvider, string repositoryGroup);

    void DisconnectRequest();

    ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType { get; }
  }
}