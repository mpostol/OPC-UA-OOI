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
  internal class CommunicationContext : IDisposable
  {
    #region constructor

    internal CommunicationContext(IDTOProvider dataProvider, string repositoryGroup, IAzureEnabledNetworkDevice device, ILogger<CommunicationContext> logger)
    {
      _device = device ?? throw new ArgumentNullException($"{nameof(device)}");
      _dataProvider = dataProvider ?? throw new ArgumentNullException($"{nameof(dataProvider)}"); ;
      _repositoryGroup = repositoryGroup;
      _Logger = logger;
    }

    #endregion constructor

    internal async Task Run(CancellationToken cancelation)
    {
      await Task.Run(async () => await TransitionLoop(), cancelation);
    }

    public enum MachineState { UnassignedState, AssigneddState, DataTransferingState, ConnectedState, DisconnectingState }

    #region private

    private const string _globalDeviceEndpoint = "global.azure-devices-provisioning.net";
    private const int _delayAfterFailure = 5000;

    private enum RegisterResult { Assigned, Failed, Disabled }

    private readonly IDTOProvider _dataProvider;
    private readonly string _repositoryGroup;
    private IAzureEnabledNetworkDevice _device;
    private MachineState _currentState = MachineState.UnassignedState;
    private SecurityProvider _security;
    private readonly ILogger<CommunicationContext> _Logger;
    private DeviceClient _deviceClient;
    private DeviceRegistrationResult _provisioningResult = null;

    private void TransitionTo(MachineState state)
    {
      _currentState = state;
    }

    private async Task<RegisterResult> Register()
    {
      _Logger.LogDebug($"Opening {nameof(Register)} operation. Obtaining security provider for the device.");
      _security = new SecurityProviderSymmetricKey(_device.AzureDeviceParameters.AzureDeviceId, _device.AzureDeviceParameters.AzurePrimaryKey, _device.AzureDeviceParameters.AzureSecondaryKey);
      ProvisioningTransportHandler _transport = null;
      try
      {
        switch (_device.AzureDeviceParameters.TransportType)
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
        ProvisioningDeviceClient provisioningClient = ProvisioningDeviceClient.Create(_globalDeviceEndpoint, _device.AzureDeviceParameters.AzureScopeId, _security, _transport);
        _Logger.LogDebug($"Register device using {nameof(ProvisioningDeviceClient.RegisterAsync)} device.");
        _provisioningResult = await provisioningClient.RegisterAsync().ConfigureAwait(false);
      }
      finally
      {
        _transport.Dispose();
      }
      switch (_provisioningResult.Status)
      {
        case ProvisioningRegistrationStatusType.Unassigned:
          throw new ArgumentOutOfRangeException($"{nameof(_provisioningResult.Status)} = {nameof(ProvisioningRegistrationStatusType.Unassigned)}");
        case ProvisioningRegistrationStatusType.Assigning:
          throw new ArgumentOutOfRangeException($"{nameof(_provisioningResult.Status)} = {nameof(ProvisioningRegistrationStatusType.Assigning)}");
        case ProvisioningRegistrationStatusType.Assigned:
          return RegisterResult.Assigned;

        case ProvisioningRegistrationStatusType.Failed:
          _Logger.LogWarning($"Failed to provision the device. {nameof(ProvisioningRegistrationStatusType.Failed)} - {_provisioningResult.ErrorMessage}.");
          return RegisterResult.Assigned;

        case ProvisioningRegistrationStatusType.Disabled:
          _Logger.LogWarning($"Failed to provision the device. {nameof(ProvisioningRegistrationStatusType.Disabled)} - {_provisioningResult.ErrorMessage}.");
          return RegisterResult.Disabled;
      }
      return RegisterResult.Disabled;
    }

    private async Task<bool> Connect()
    {
      try
      {
        _Logger.LogDebug("Successfully provisioned device. Creating client.");
        IAuthenticationMethod auth;
        switch (_security)
        {
          case SecurityProviderTpm tpmSecurity:
            auth = new DeviceAuthenticationWithTpm(_device.AzureDeviceParameters.AzureDeviceId, tpmSecurity);
            break;

          case SecurityProviderX509 certificateSecurity:
            auth = new DeviceAuthenticationWithX509Certificate(_device.AzureDeviceParameters.AzureDeviceId, certificateSecurity.GetAuthenticationCertificate());
            break;

          case SecurityProviderSymmetricKey symmetricKeySecurity:
            auth = new DeviceAuthenticationWithRegistrySymmetricKey(_device.AzureDeviceParameters.AzureDeviceId, symmetricKeySecurity.GetPrimaryKey());
            break;

          default:
            _Logger.LogError("Specified security provider is unknown.");
            throw new NotSupportedException("Unknown authentication type.");
        }
        _deviceClient = DeviceClient.Create(_provisioningResult.AssignedHub, auth, _device.AzureDeviceParameters.TransportType);
        await _deviceClient.OpenAsync().ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        _Logger.LogError($"Operation {nameof(Connect)} failed because of error {ex.Message}.");
        return false;
      }
      return true;
    }

    private async Task DataTransfer()
    {
      try
      {
        _Logger.LogDebug("Building payload.");
        string payload = JsonConvert.SerializeObject(_dataProvider.GetDTO(_repositoryGroup));
        await _deviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes(payload)));
        _Logger.LogDebug("Successfully published device state to Azure.");
      }
      catch (Exception e)
      {
        _Logger.LogError(e, "Failed to publish device state.");
      }
    }

    private async Task TransitionLoop()
    {
      while (true)
      {
        switch (_currentState)
        {
          case MachineState.UnassignedState:
            _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.UnassignedState)}");
            switch (await Register())
            {
              case RegisterResult.Assigned:
                TransitionTo(MachineState.AssigneddState);
                break;

              case RegisterResult.Failed:

                break;

              case RegisterResult.Disabled:
                break;

              default:
                break;
            }
            break;

          case MachineState.AssigneddState:
            _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.AssigneddState)}");
            if (await Connect())
              _currentState = MachineState.DataTransferingState;
            else
            {
              _Logger.LogWarning($"Failed to connect.");
              await Task.Delay(5000);
            }
            break;

          case MachineState.DataTransferingState:
            _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.UnassignedState)}");
            await Task.Delay(_device.PublishingInterval);
            await DataTransfer();
            break;

          case MachineState.ConnectedState:
            _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.UnassignedState)}");
            break;

          case MachineState.DisconnectingState:
            await _deviceClient.CloseAsync();
            _currentState = MachineState.AssigneddState;
            break;

          default:
            break;
        }
      }
    }


    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (disposedValue)
        return;
      if (disposing)
      {
        // TODO: dispose managed state (managed objects).
        _security?.Dispose();
        if (_deviceClient != null)
          try
          {
            Task _await = _deviceClient.CloseAsync();
            _await.Wait();
            _Logger.LogInformation($"Disposed azure connection.");
            _deviceClient.Dispose();
            _deviceClient = null;
          }
          catch (Exception e)
          {
            _Logger.LogError($"Error while disposing {nameof(CommunicationContext)}: {e.Message}");
          }
      }
      // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below. Set large fields to null.
      disposedValue = true;
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }

    #endregion IDisposable Support

    #endregion private
  }
}