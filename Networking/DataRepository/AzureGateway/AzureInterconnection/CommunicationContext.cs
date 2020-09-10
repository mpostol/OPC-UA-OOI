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

    internal CommunicationContext(IDTOProvider dataProvider, string repositoryGroup, AzureDeviceParameters azureDeviceParameters, ILogger<CommunicationContext> logger)
    {
      _dataProvider = dataProvider ?? throw new ArgumentNullException($"{nameof(dataProvider)}");
      _repositoryGroup = repositoryGroup;
      _azureDeviceParameters = azureDeviceParameters ?? throw new ArgumentNullException($"{nameof(azureDeviceParameters)}");
      _Logger = logger ?? throw new ArgumentNullException($"{nameof(logger)}");
    }

    #endregion constructor

    #region API

    /// <summary>
    /// Runs the communication machine.
    /// </summary>
    /// <param name="cancellation">The cancellation token.</param>
    /// <exception cref="ApplicationException">Only one instance of the task {nameof(Run)} is allowed.</exception>
    internal async void Run(CancellationToken cancellation)
    {
      _Logger.LogDebug($"Entering {nameof(Run)} operation");
      if (_running)
        throw new ApplicationException($"Only one instance of the task {nameof(Run)} is allowed.");
      _running = true;
      await TransitionLoopAsync(cancellation);
    }

    /// <summary>
    /// Disconnects the request.
    /// </summary>
    /// <exception cref="ApplicationException">Calling the {nameof(DisconnectRequest)} operation is allowed only in the running state of the communication machine.</exception>
    internal void DisconnectRequest()
    {
      if (!_running)
        throw new ApplicationException($"Calling the {nameof(DisconnectRequest)} operation is allowed only in the running state of the communication machine.");
      _disconnectRequest = true;
    }

    #endregion API

    #region private

    private const string _globalDeviceEndpoint = "global.azure-devices-provisioning.net";
    private const int _delayAfterFailure = 5000;

    private enum MachineState { UnassignedState, AssigneddState, DataTransferingState }

    private readonly IDTOProvider _dataProvider;
    private readonly string _repositoryGroup;
    private readonly AzureDeviceParameters _azureDeviceParameters;
    private readonly ILogger<CommunicationContext> _Logger;  //TODO Create and Register the EventSource #455
    private MachineState _currentState = MachineState.UnassignedState;
    private bool _disconnectRequest = false;
    private bool _running = false;

    private void TransitionTo(MachineState state)
    {
      _currentState = state;
    }

    private async Task<DeviceRegistrationResult> Register(SecurityProvider security, CancellationToken token)
    {
      _Logger.LogDebug($"Entering {nameof(Register)} operation");
      ProvisioningTransportHandler transport = null;
      try
      {
        switch (_azureDeviceParameters.TransportType)
        {
          case TransportType.Amqp:
            transport = new ProvisioningTransportHandlerAmqp();
            break;

          case TransportType.Http1:
            transport = new ProvisioningTransportHandlerHttp();
            break;

          case TransportType.Amqp_WebSocket_Only:
            transport = new ProvisioningTransportHandlerAmqp(TransportFallbackType.WebSocketOnly);
            break;

          case TransportType.Amqp_Tcp_Only:
            transport = new ProvisioningTransportHandlerAmqp(TransportFallbackType.TcpOnly);
            break;

          case TransportType.Mqtt:
            transport = new ProvisioningTransportHandlerMqtt();
            break;

          case TransportType.Mqtt_WebSocket_Only:
            transport = new ProvisioningTransportHandlerMqtt(TransportFallbackType.WebSocketOnly);
            break;

          case TransportType.Mqtt_Tcp_Only:
            transport = new ProvisioningTransportHandlerMqtt(TransportFallbackType.TcpOnly);
            break;

          default:
            throw new ArgumentOutOfRangeException();
        }
        ProvisioningDeviceClient provisioningClient = ProvisioningDeviceClient.Create(_globalDeviceEndpoint, _azureDeviceParameters.AzureScopeId, security, transport);
        _Logger.LogDebug($"Register device using {nameof(ProvisioningDeviceClient.RegisterAsync)} device.");
        return await provisioningClient.RegisterAsync(token);
      }
      finally
      {
        transport.Dispose();
      }
    }

    private async Task<DeviceClient> Connect(string assignedHub, SecurityProvider security, CancellationToken token)
    {
      _Logger.LogDebug($"Entering {nameof(Connect)} operation");
      DeviceClient deviceClient;
      try
      {
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
        await deviceClient.OpenAsync(token);
      }
      catch (Exception ex)
      {
        _Logger.LogError($"Operation {nameof(Connect)} failed because of error {ex.Message}.");
        return null;
      }
      return deviceClient;
    }

    private async Task DataTransfer(DeviceClient deviceClient, CancellationToken token)
    {
      try
      {
        _Logger.LogDebug($"Entering {nameof(DataTransfer)} operation");
        string payload = JsonConvert.SerializeObject(_dataProvider.GetDTO(_repositoryGroup));
        using (Message message = new Message(Encoding.UTF8.GetBytes(payload)))
          await deviceClient.SendEventAsync(message, token);
        _Logger.LogDebug("Successfully published device state to Azure.");
      }
      catch (Exception e)
      {
        _Logger.LogError(e, "Failed to publish device state.");
        throw;
      }
    }

    private async Task TransitionLoopAsync(CancellationToken token)
    {
      _Logger.LogDebug($"Entering {nameof(TransitionLoopAsync)} operation");
      SecurityProvider security = null;
      DeviceClient deviceClient = null;
      string assignedHub = String.Empty;
      try
      {
        security = new SecurityProviderSymmetricKey(_azureDeviceParameters.AzureDeviceId, _azureDeviceParameters.AzurePrimaryKey, _azureDeviceParameters.AzureSecondaryKey);
        while (!_disconnectRequest)
        {
          token.ThrowIfCancellationRequested();
          switch (_currentState)
          {
            case MachineState.UnassignedState:
              _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.UnassignedState)}");
              DeviceRegistrationResult provisioningResult = await Register(security, token);
              switch (provisioningResult.Status)
              {
                case ProvisioningRegistrationStatusType.Unassigned:
                  _Logger.LogWarning($"Unexpected result from {nameof(provisioningResult.Status)}:  {nameof(ProvisioningRegistrationStatusType.Unassigned)}");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Assigning:
                  _Logger.LogWarning($"Unexpected result from {nameof(provisioningResult.Status)}:  {nameof(ProvisioningRegistrationStatusType.Assigning)}");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Assigned:
                  assignedHub = provisioningResult.AssignedHub;
                  TransitionTo(MachineState.AssigneddState);
                  _Logger.LogDebug("Successfully provisioned the device.");
                  break;

                case ProvisioningRegistrationStatusType.Failed:
                  _Logger.LogInformation($"Failed to provision the device. The returned status: {nameof(ProvisioningRegistrationStatusType.Failed)}; reported error message: {provisioningResult.ErrorMessage}.");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Disabled:
                  _Logger.LogInformation($"Failed to provision the device. The returned status: {nameof(ProvisioningRegistrationStatusType.Disabled)}; reported error message: {provisioningResult.ErrorMessage}.");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;
              }

              break;

            case MachineState.AssigneddState:
              _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.AssigneddState)}");
              deviceClient = await Connect(assignedHub, security, token);
              if (deviceClient != null)
              {
                security.Dispose();
                security = null;
                TransitionTo(MachineState.DataTransferingState);
              }
              else
              {
                _Logger.LogWarning($"Failed to connect.");
                await Task.Delay(5000, token);
              }
              break;

            case MachineState.DataTransferingState:
              _Logger.LogDebug($"{nameof(CommunicationContext)} entering the state: {nameof(MachineState.UnassignedState)}");
              await Task.Delay(_azureDeviceParameters.PublishingInterval(), token);
              await DataTransfer(deviceClient, token);
              break;
          }
        }
      }
      catch (Exception ex)
      {
        _Logger.LogError($"{nameof(CommunicationContext.TransitionLoopAsync)} - an Exception has been thrown: {ex.Message}. The device {_repositoryGroup} has been disconnected.");
      }
      finally
      {
        if (deviceClient != null)
          await deviceClient.CloseAsync();
        if (security != null)
          security.Dispose();
        _disconnectRequest = false;
        _running = false;
      }
    }

    #endregion private
  }
}