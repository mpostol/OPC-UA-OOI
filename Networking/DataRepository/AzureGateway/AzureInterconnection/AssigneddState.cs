//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Logging;
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
      Logger.LogDebug("Successfully provisioned device. Creating client.");
      IAuthenticationMethod auth;
      switch (SecurityProvider)
      {
        case SecurityProviderTpm tpmSecurity:
          auth = new DeviceAuthenticationWithTpm(AzureEnabledNetworkDevice.AzureDeviceParameters.AzureDeviceId, tpmSecurity);
          break;

        case SecurityProviderX509 certificateSecurity:
          auth = new DeviceAuthenticationWithX509Certificate(AzureEnabledNetworkDevice.AzureDeviceParameters.AzureDeviceId, certificateSecurity.GetAuthenticationCertificate());
          break;

        case SecurityProviderSymmetricKey symmetricKeySecurity:
          auth = new DeviceAuthenticationWithRegistrySymmetricKey(AzureEnabledNetworkDevice.AzureDeviceParameters.AzureDeviceId, symmetricKeySecurity.GetPrimaryKey());
          break;

        default:
          Logger.LogError("Specified security provider is unknown.");
          throw new NotSupportedException("Unknown authentication type.");
      }
      DeviceClient = DeviceClient.Create(DeviceRegistrationResult.AssignedHub, auth, AzureEnabledNetworkDevice.AzureDeviceParameters.TransportType);
      await DeviceClient.OpenAsync().ConfigureAwait(false);
      AzureEnabledNetworkDevice.DeviceClient = DeviceClient;
      return true;
    }

    public override void DisconnectRequest()
    {
      throw new NotImplementedException();
    }

    public override async Task<bool> Register()
    {
      throw new NotImplementedException();
    }

    public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      throw new NotImplementedException();
    }

    #endregion CommunicationContext.StateBase
  }
}