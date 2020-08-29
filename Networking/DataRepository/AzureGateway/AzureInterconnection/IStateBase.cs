//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  internal interface IStateBase
  {
    void Register();

    void Connect();

    void TransferData(IDTOProvider dataProvider, string repositoryGroup);

    void DisconnectRequest();
  }
}