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
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  /// <summary>
  /// Class CommunicationContext - implements Azure communication state machine.
  /// </summary>
  internal class CommunicationContext
  {
    #region constructor

    internal CommunicationContext(IDTOProvider dataProvider, string repositoryGroup, IAzureDeviceParameters azureDeviceParameters, ILogger<CommunicationContext> logger)
    {
      _dataProvider = dataProvider ?? throw new ArgumentNullException($"{nameof(dataProvider)}"); ;
      _repositoryGroup = repositoryGroup;
      _azureDeviceParameters = azureDeviceParameters ?? throw new ArgumentNullException($"{nameof(azureDeviceParameters)}");
      _Logger = logger ?? throw new ArgumentNullException($"{nameof(logger)}"); ;
    }

    #endregion constructor

    internal async Task Run(CancellationToken cancelation)
    {
      await Task.Run(async () => await TransitionLoop(), cancelation);
    }

    internal void DisconnectRequest()
    {
      _disconnectRequest = true;
    }

    #region private

    private const string _globalDeviceEndpoint = "global.azure-devices-provisioning.net";
    private const int _delayAfterFailure = 5000;

    private enum MachineState { UnassignedState, AssigneddState, DataTransferingState }

    private readonly IDTOProvider _dataProvider;
    private readonly string _repositoryGroup;
    private readonly IAzureDeviceParameters _azureDeviceParameters;
    private readonly ILogger<CommunicationContext> _Logger;
    private MachineState _currentState = MachineState.UnassignedState;
    private bool _disconnectRequest = false;

    private void TransitionTo(MachineState state)
    {
      _currentState = state;
    }

    private async Task<DeviceRegistrationResult> Register(SecurityProvider security)
    {
      _Logger.LogDebug($"Opening {nameof(Register)} operation. Obtaining security provider for the device.");
      ProvisioningTransportHandler _transport = null;
      try
      {
        switch (_azureDeviceParameters.TransportType)
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
        ProvisioningDeviceClient provisioningClient = ProvisioningDeviceClient.Create(_globalDeviceEndpoint, _azureDeviceParameters.AzureScopeId, security, _transport);
        _Logger.LogDebug($"Register device using {nameof(ProvisioningDeviceClient.RegisterAsync)} device.");
        return await provisioningClient.RegisterAsync().ConfigureAwait(false);
      }
      finally
      {
        _transport.Dispose();
      }
    }

    private async Task<DeviceClient> Connect(string assignedHub, SecurityProvider security)
    {
      DeviceClient deviceClient;
      try
      {
        _Logger.LogDebug("Successfully provisioned device. Creating client.");
        IAuthenticationMethod authenticationMethod;
        switch (security)
        {
          case SecurityProviderTpm tpmSecurity:
            authenticationMethod = new DeviceAuthenticationWithTpm(_azureDeviceParameters.AzureDeviceId, tpmSecurity);
            break;

          case SecurityProviderX509 certificateSecurity:
            authenticationMethod = new DeviceAuthenticationWithX509Certificate(_azureDeviceParameters.AzureDeviceId, certificateSecurity.GetAuthenticationCertificate());
            break;

          case SecurityProviderSymmetricKey symmetricKeySecurity:
            authenticationMethod = new DeviceAuthenticationWithRegistrySymmetricKey(_azureDeviceParameters.AzureDeviceId, symmetricKeySecurity.GetPrimaryKey());
            break;

          default:
            _Logger.LogError("Specified security provider is unknown.");
            throw new NotSupportedException("Unknown authentication type.");
        }
        deviceClient = DeviceClient.Create(assignedHub, authenticationMethod, _azureDeviceParameters.TransportType);
        await deviceClient.OpenAsync().ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        _Logger.LogError($"Operation {nameof(Connect)} failed because of error {ex.Message}.");
        return await Task.FromResult<DeviceClient>(null);
      }
      return await Task.FromResult<DeviceClient>(deviceClient);
    }

    private async Task DataTransfer(DeviceClient deviceClient)
    {
      try
      {
        _Logger.LogDebug("Building payload.");
        string payload = JsonConvert.SerializeObject(_dataProvider.GetDTO(_repositoryGroup));
        await deviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes(payload)));
        _Logger.LogDebug("Successfully published device state to Azure.");
      }
      catch (Exception e)
      {
        _Logger.LogError(e, "Failed to publish device state.");
      }
    }

    private async Task TransitionLoop()
    {
      SecurityProvider security = new SecurityProviderSymmetricKey(_azureDeviceParameters.AzureDeviceId, _azureDeviceParameters.AzurePrimaryKey, _azureDeviceParameters.AzureSecondaryKey);
      DeviceClient deviceClient = null;
      string assignedHub = String.Empty;
      try
      {
        while (!_disconnectRequest)
        {
          switch (_currentState)
          {
            case MachineState.UnassignedState:
              _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.UnassignedState)}");
              DeviceRegistrationResult provisioningResult = await Register(security);
              switch (provisioningResult.Status)
              {
                case ProvisioningRegistrationStatusType.Unassigned:
                  _Logger.LogDebug($"Unexpected result from {nameof(provisioningResult.Status)}:  {nameof(ProvisioningRegistrationStatusType.Unassigned)}");
                  await Task.Delay(_delayAfterFailure); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Assigning:
                  _Logger.LogDebug($"Unexpected result from {nameof(provisioningResult.Status)}:  {nameof(ProvisioningRegistrationStatusType.Assigning)}");
                  await Task.Delay(_delayAfterFailure); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Assigned:
                  assignedHub = provisioningResult.AssignedHub;
                  TransitionTo(MachineState.AssigneddState);
                  _Logger.LogDebug("Successfully provisioned the device.");
                  break;

                case ProvisioningRegistrationStatusType.Failed:
                  _Logger.LogWarning($"Failed to provision the device. {nameof(ProvisioningRegistrationStatusType.Failed)} - {provisioningResult.ErrorMessage}.");
                  await Task.Delay(_delayAfterFailure); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Disabled:
                  _Logger.LogWarning($"Failed to provision the device. {nameof(ProvisioningRegistrationStatusType.Disabled)} - {provisioningResult.ErrorMessage}.");
                  await Task.Delay(_delayAfterFailure); //No transition
                  break;
              }

              break;

            case MachineState.AssigneddState:
              _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.AssigneddState)}");
              deviceClient = await Connect(assignedHub, security);
              if (deviceClient != null)
              {
                security.Dispose();
                security = null;
                TransitionTo(MachineState.DataTransferingState);
              }
              else
              {
                _Logger.LogWarning($"Failed to connect.");
                await Task.Delay(5000);
              }
              break;

            case MachineState.DataTransferingState:
              _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.UnassignedState)}");
              await Task.Delay(_azureDeviceParameters.PublishingInterval);
              await DataTransfer(deviceClient);
              break;

          }
        }
      }
      catch (Exception ex)
      {
        _Logger.LogError($"{nameof(CommunicationContext.TransitionLoop)} - an Exception has been thrown: {ex.Message}. The device {_repositoryGroup} has been disconnected.");
      }
      finally
      {
        if (deviceClient != null)
          await deviceClient.CloseAsync();
        if (security != null)
          security.Dispose();
      }
    }

    #endregion private
  }
}