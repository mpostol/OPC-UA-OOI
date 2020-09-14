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
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UAOOI.Networking.DataRepository.AzureGateway.Diagnostic;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  /// <summary>
  /// Class CommunicationContext - implements Azure communication state machine.
  /// </summary>
  internal class CommunicationContext
  {
    #region constructor

    internal CommunicationContext(IDTOProvider dataProvider, string repositoryGroup, AzureDeviceParameters azureDeviceParameters)
    {
      _Logger.EnteringMethodAzure(nameof(CommunicationContext));
      _dataProvider = dataProvider ?? throw new ArgumentNullException($"{nameof(dataProvider)}");
      _repositoryGroup = repositoryGroup;
      _azureDeviceParameters = azureDeviceParameters ?? throw new ArgumentNullException($"{nameof(azureDeviceParameters)}");
    }

    #endregion constructor

    #region API

    /// <summary>
    /// Runs the communication machine.
    /// </summary>
    /// <param name="cancellation">The cancellation token.</param>
    internal async void Run(CancellationToken cancellation)
    {
      _Logger.EnteringMethodAzure(nameof(CommunicationContext));
      if (_running)
        throw new ApplicationException($"Only one instance of the task {nameof(Run)} is allowed.");
      _running = true;
      await CommunicationEngineLoopAsync(cancellation);
    }

    /// <summary>
    /// Disconnects the request.
    /// </summary>
    /// <exception cref="ApplicationException">Calling the {nameof(DisconnectRequest)} operation is allowed only in the running state of the communication machine.</exception>
    internal void DisconnectRequest()
    {
      _Logger.EnteringMethodAzure(nameof(CommunicationContext));
      if (!_running)
      {
        _Logger.ProgramFailure(nameof(CommunicationContext), "This method cannot be called in the running state of the communication machine");
        throw new ApplicationException($"Calling the {nameof(DisconnectRequest)} operation is allowed only in the running state of the communication machine.");
      }
      _disconnectRequest = true;
    }

    #endregion API

    #region private

    private const string _globalDeviceEndpoint = "global.azure-devices-provisioning.net";
    private const int _delayAfterFailure = 5000;

    private enum MachineState { UnassignedState, AssigneddState, DataTransferingState }

    private readonly AzureGatewaySemanticEventSource _Logger = AzureGatewaySemanticEventSource.Log();
    private readonly IDTOProvider _dataProvider;
    private readonly string _repositoryGroup;
    private readonly AzureDeviceParameters _azureDeviceParameters;
    private MachineState _currentState = MachineState.UnassignedState;
    private bool _disconnectRequest = false;
    private bool _running = false;

    private void TransitionTo(MachineState state)
    {
      _currentState = state;
    }

    private async Task<DeviceRegistrationResult> RegisterAsync(SecurityProvider security, CancellationToken token)
    {
      _Logger.EnteringMethodAzure(nameof(CommunicationContext));
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
        _Logger.EnteringMethodAzure(nameof(ProvisioningDeviceClient), nameof(ProvisioningDeviceClient.RegisterAsync));
        return await provisioningClient.RegisterAsync(token);
      }
      finally
      {
        _Logger.EnteringMethodAzure(nameof(ProvisioningTransportHandler), nameof(ProvisioningTransportHandler.Dispose));
        transport.Dispose();
      }
    }

    private async Task<DeviceClient> ConnectAsync(string assignedHub, SecurityProvider security, CancellationToken token)
    {
      _Logger.EnteringMethodAzure(nameof(CommunicationContext));
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
            _Logger.ProgramFailure(nameof(CommunicationContext), "Specified authentication type is unknown.");
            throw new NotSupportedException("Unknown authentication type.");
        }
        _Logger.StartCreatingClient(nameof(DeviceClient), nameof(DeviceClient.Create), assignedHub, authenticationMethod.ToString(), _azureDeviceParameters.TransportType.ToString());
        deviceClient = DeviceClient.Create(assignedHub, authenticationMethod, _azureDeviceParameters.TransportType);
        _Logger.EnteringMethodAzure(nameof(DeviceClient), nameof(DeviceClient.OpenAsync));
        await deviceClient.OpenAsync(token);
      }
      catch (Exception ex)
      {
        _Logger.LogException(nameof(CommunicationContext), ex);
        return null;
      }
      return deviceClient;
    }

    private async Task DataTransfer(DeviceClient deviceClient, CancellationToken token)
    {
      try
      {
        _Logger.EnteringMethodAzure(nameof(CommunicationContext));
        string payload = _dataProvider.GetDTO(_repositoryGroup);
        using (Message message = new Message(Encoding.UTF8.GetBytes(payload)))
          await deviceClient.SendEventAsync(message, token);
        _Logger.SendEvenSuccided(payload.Substring(0, 80));
      }
      catch (Exception e)
      {
        _Logger.LogException(nameof(CommunicationContext), e);
        throw;
      }
    }

    private async Task CommunicationEngineLoopAsync(CancellationToken token)
    {
      _Logger.EnteringMethodAzure(nameof(CommunicationContext));
      SecurityProvider security = null;
      DeviceClient deviceClient = null;
      string assignedHub = String.Empty;
      try
      {
        security = new SecurityProviderSymmetricKey(_azureDeviceParameters.AzureDeviceId, _azureDeviceParameters.AzurePrimaryKey, _azureDeviceParameters.AzureSecondaryKey);
        while (!_disconnectRequest)
        {
          token.ThrowIfCancellationRequested();
          _Logger.EnteringState(_currentState.ToString());
          switch (_currentState)
          {
            case MachineState.UnassignedState:
              DeviceRegistrationResult provisioningResult = await RegisterAsync(security, token);
              switch (provisioningResult.Status)
              {
                case ProvisioningRegistrationStatusType.Unassigned:
                  _Logger.UnexpectedProvisioningResultStatus($"{nameof(ProvisioningRegistrationStatusType.Unassigned)}", $"{provisioningResult.ErrorMessage}");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Assigning:
                  _Logger.UnexpectedProvisioningResultStatus($"{nameof(ProvisioningRegistrationStatusType.Assigning)}", $"{provisioningResult.ErrorMessage}");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Assigned:
                  assignedHub = provisioningResult.AssignedHub;
                  TransitionTo(MachineState.AssigneddState);
                  break;

                case ProvisioningRegistrationStatusType.Failed:
                  _Logger.UnexpectedProvisioningResultStatus($"{nameof(ProvisioningRegistrationStatusType.Failed)}", $"{provisioningResult.ErrorMessage}");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;

                case ProvisioningRegistrationStatusType.Disabled:
                  _Logger.UnexpectedProvisioningResultStatus($"{nameof(ProvisioningRegistrationStatusType.Disabled)}", $"{provisioningResult.ErrorMessage}");
                  await Task.Delay(_delayAfterFailure, token); //No transition
                  break;
              }

              break;

            case MachineState.AssigneddState:
              deviceClient = await ConnectAsync(assignedHub, security, token);
              if (deviceClient != null)
              {
                security.Dispose();
                security = null;
                TransitionTo(MachineState.DataTransferingState);
              }
              else
              {
                _Logger.AzureConnectionFailure(nameof(CommunicationContext), nameof(CommunicationEngineLoopAsync), $"Failed to connect.");
                await Task.Delay(_delayAfterFailure, token);
              }
              break;

            case MachineState.DataTransferingState:
              _Logger.StartingTimeDelay(_azureDeviceParameters.PublishingInterval().ToString());
              await Task.Delay(_azureDeviceParameters.PublishingInterval(), token);
              await DataTransfer(deviceClient, token);
              break;
          }
        }
      }
      catch (Exception ex)
      {
        _Logger.LogException(nameof(CommunicationContext), ex);
      }
      finally
      {
        _Logger.DisposingObject(nameof(DeviceClient), nameof(DeviceClient.CloseAsync));
        deviceClient?.CloseAsync();
        _Logger.DisposingObject(nameof(SecurityProvider), nameof(SecurityProvider.Dispose));
        security?.Dispose();
        _disconnectRequest = false;
        _running = false;
      }
    }

    #endregion private
  }
}