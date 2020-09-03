//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Logging;
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
      Logger.LogDebug($"Opening {nameof(Connect)}. Obtaining security provider for the device.");
      SecurityProvider = new SecurityProviderSymmetricKey
        (AzureEnabledNetworkDevice.AzureDeviceParameters.AzureDeviceId, AzureEnabledNetworkDevice.AzureDeviceParameters.AzurePrimaryKey, AzureEnabledNetworkDevice.AzureDeviceParameters.AzureSecondaryKey);
      ProvisioningTransportHandler _transport = null;
      try
      {
        switch (AzureEnabledNetworkDevice.AzureDeviceParameters.TransportType)
        {
          case TransportType.Amqp:
            _transport = new ProvisioningTransportHandlerAmqp();
            break;

          case TransportType.Http1:
            _transport = new ProvisioningTransportHandlerHttp();
            break;

          case TransportType.Amqp_WebSocket_Only:
            _transport = new ProvisioningTransportHandlerAmqp(TransportFallbackType.WebSocketOnly);
            break;

          case TransportType.Amqp_Tcp_Only:
            _transport = new ProvisioningTransportHandlerAmqp(TransportFallbackType.TcpOnly);
            break;

          case TransportType.Mqtt:
            _transport = new ProvisioningTransportHandlerMqtt();
            break;

          case TransportType.Mqtt_WebSocket_Only:
            _transport = new ProvisioningTransportHandlerMqtt(TransportFallbackType.WebSocketOnly);
            break;

          case TransportType.Mqtt_Tcp_Only:
            _transport = new ProvisioningTransportHandlerMqtt(TransportFallbackType.TcpOnly);
            break;

          default:
            throw new ArgumentOutOfRangeException();
        }
        ProvisioningDeviceClient provisioningClient = ProvisioningDeviceClient.Create(GlobalDeviceEndpoint, AzureEnabledNetworkDevice.AzureDeviceParameters.AzureScopeId, SecurityProvider, _transport);
        Logger.LogDebug($"Register device using {nameof(ProvisioningDeviceClient.RegisterAsync)} device.");
        DeviceRegistrationResult = await provisioningClient.RegisterAsync().ConfigureAwait(false);
      }
      finally
      {
        _transport.Dispose();
      }
      switch (DeviceRegistrationResult.Status)
      {
        case ProvisioningRegistrationStatusType.Unassigned:
          throw new ArgumentOutOfRangeException($"{nameof(DeviceRegistrationResult.Status)} = {nameof(ProvisioningRegistrationStatusType.Unassigned)}");
        case ProvisioningRegistrationStatusType.Assigning:
          throw new ArgumentOutOfRangeException($"{nameof(DeviceRegistrationResult.Status)} = {nameof(ProvisioningRegistrationStatusType.Assigning)}");
        case ProvisioningRegistrationStatusType.Assigned:
          return RegisterResult.Assigned;

        case ProvisioningRegistrationStatusType.Failed:
          Logger.LogWarning($"Failed to provision the device. {nameof(ProvisioningRegistrationStatusType.Failed)} - {DeviceRegistrationResult.ErrorMessage}.");
          return RegisterResult.Assigned;

        case ProvisioningRegistrationStatusType.Disabled:
          Logger.LogWarning($"Failed to provision the device. {nameof(ProvisioningRegistrationStatusType.Disabled)} - {DeviceRegistrationResult.ErrorMessage}.");
          return RegisterResult.Disabled;
      }
      return RegisterResult.Disabled;
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