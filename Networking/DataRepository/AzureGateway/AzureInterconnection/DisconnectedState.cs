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
    public UnassignedState(IAzureEnabledNetworkDevice device, CommunicationContext communicationContext) : base(communicationContext)
    {
      AzureEnabledNetworkDevice = device;
    }

    #region CommunicationContext.StateBase

    public override ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => ProvisioningRegistrationStatusType.Unassigned;

    public override async Task<bool> Connect()
    {
      throw new ApplicationException($"operation {nameof(Connect)} is not allowed in the {nameof(UnassignedState)}");
    }

    public override void DisconnectRequest()
    {
      throw new NotImplementedException();
    }

    public override async Task<bool> Register()
    {
      Logger.LogDebug($"Opening {nameof(Connect)}. Obtaining security provider for the device.");
      SecurityProvider = await AzureEnabledNetworkDevice.AzureDeviceParameters.GetSecurityProviderAsync().ConfigureAwait(false);
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
      switch (DeviceRegistrationResult.Status)
      {
        case ProvisioningRegistrationStatusType.Unassigned:
          throw new ArgumentOutOfRangeException($"{nameof(DeviceRegistrationResult.Status)} = {nameof(ProvisioningRegistrationStatusType.Unassigned)}");
        case ProvisioningRegistrationStatusType.Assigning:
          throw new ArgumentOutOfRangeException($"{nameof(DeviceRegistrationResult.Status)} = {nameof(ProvisioningRegistrationStatusType.Assigning)}");
        case ProvisioningRegistrationStatusType.Assigned:
          TransitionTo(new AssigneddState(_context));
          return await _context.Connect();

        case ProvisioningRegistrationStatusType.Failed:
        case ProvisioningRegistrationStatusType.Disabled:
          break;
      }
      Logger.LogWarning($"Failed to provision the device. {DeviceRegistrationResult.Status} - {DeviceRegistrationResult.ErrorMessage}. Disposing.");
      await DisposeAsync();
      return false;
    }

    public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      throw new NotImplementedException();
    }

    #endregion CommunicationContext.StateBase

    #region private

    private const string GlobalDeviceEndpoint = "global.azure-devices-provisioning.net";

    private ProvisioningTransportHandler _transport;

    private async ValueTask DisposeAsync()
    {
      _transport?.Dispose();
      //_timer?.DisposeAsync();

    }

    #endregion private
  }
}