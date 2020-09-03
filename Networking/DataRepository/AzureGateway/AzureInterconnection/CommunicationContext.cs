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
  /// <summary>
  /// Class CommunicationContext - implements Azure communication state machine.
  /// Implements the <see cref="IStateBase" />
  /// </summary>
  /// <seealso cref="IStateBase" />
  internal class CommunicationContext : IStateBase, IDisposable
  {
    #region constructor

    internal CommunicationContext(IAzureEnabledNetworkDevice device, ILogger<CommunicationContext> logger)
    {
      _device = device ?? throw new ArgumentNullException($"{nameof(device)}");
      _Logger = logger;
      _currentState = new UnassignedState(this);
    }

    #endregion constructor



    #region IStateBase

    public async Task<RegisterResult> Register()
    {
      return await _currentState.Register();
    }

    public async Task<bool> Connect()
    {
      return await _currentState.Connect();
    }

    public void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      _currentState.TransferData(dataProvider, repositoryGroup);
    }

    public void DisconnectRequest()
    {
      _currentState.DisconnectRequest();
    }

    public ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => _currentState.GetProvisioningRegistrationStatusType;

    #endregion IStateBase

    #region private

    internal abstract class StateBase : IStateBase
    {
      #region constructors

      public StateBase(CommunicationContext communicationContext)
      {
        _paretContext = communicationContext ?? throw new ArgumentNullException($"{nameof(communicationContext)}");
      }

      #endregion constructors

      #region IStateBase

      public abstract Task<RegisterResult> Register();

      public abstract Task<bool> Connect();

      public abstract void TransferData(IDTOProvider dataProvider, string repositoryGroup);

      public abstract void DisconnectRequest();

      public abstract ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType { get; }

      #endregion IStateBase

      protected void TransitionTo(StateBase state)
      {
        _paretContext.TransitionTo(state);
      }

      protected CommunicationContext _paretContext;
      protected ILogger<CommunicationContext> Logger => _paretContext._Logger;
      protected SecurityProvider SecurityProvider { get => _paretContext._security; set => _paretContext._security = value; }
      protected IAzureEnabledNetworkDevice AzureEnabledNetworkDevice { get => _paretContext._device; set => _paretContext._device = value; }
      protected DeviceClient DeviceClient { get => _paretContext._deviceClient; set => _paretContext._deviceClient = value; }
      protected DeviceRegistrationResult DeviceRegistrationResult { get => _paretContext._provisioningResult; set => _paretContext._provisioningResult = value; }
    }

    private StateBase _currentState = null;
    private SecurityProvider _security;
    private readonly ILogger<CommunicationContext> _Logger;
    private IAzureEnabledNetworkDevice _device;
    private DeviceClient _deviceClient;
    private DeviceRegistrationResult _provisioningResult;

    private void TransitionTo(StateBase stateBase)
    {
      _currentState = stateBase;
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