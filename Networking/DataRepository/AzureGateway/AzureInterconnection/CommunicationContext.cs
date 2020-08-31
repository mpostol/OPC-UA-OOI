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

    internal CommunicationContext(StateBase state, ILogger<CommunicationContext> logger)
    {
      _Logger = logger;
      SetContext(state);
    }

    #endregion constructor

    #region IStateBase

    public async Task<bool> Register()
    {
      return await _currenyState.Register();
    }

    public async Task<bool> Connect()
    {
      return await _currenyState.Connect();
    }

    public void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      _currenyState.TransferData(dataProvider, repositoryGroup);
    }

    public void DisconnectRequest()
    {
      _currenyState.DisconnectRequest();
    }

    public ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => _currenyState.GetProvisioningRegistrationStatusType;

    #endregion IStateBase

    #region private

    internal abstract class StateBase : IStateBase
    {
      #region constructors

      public StateBase(CommunicationContext communicationContext)
      {
        _context = communicationContext;
        SecurityProvider = null;
      }

      #endregion constructors

      #region IStateBase

      public abstract Task<bool> Register();

      public abstract Task<bool> Connect();

      public abstract void TransferData(IDTOProvider dataProvider, string repositoryGroup);

      public abstract void DisconnectRequest();

      public abstract ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType { get; }

      #endregion IStateBase

      protected void TransitionTo(StateBase state)
      {
        _context.SetContext(this);
      }

      protected CommunicationContext _context;
      protected ILogger<CommunicationContext> Logger => _context._Logger;
      protected SecurityProvider SecurityProvider { get => _context._security; set => _context._security = value; }
      protected IAzureEnabledNetworkDevice AzureEnabledNetworkDevice { get => _context._device; set => _context._device = value; }
      protected DeviceClient DeviceClient { get => _context._deviceClient; set => _context._deviceClient = value; }
      protected DeviceRegistrationResult DeviceRegistrationResult { get => _context._provisioningResult; set => _context._provisioningResult = value; }
    }

    private void SetContext(StateBase stateBase)
    {
      _currenyState = stateBase;
    }

    private StateBase _currenyState = null;
    private SecurityProvider _security;
    private readonly ILogger<CommunicationContext> _Logger;
    private IAzureEnabledNetworkDevice _device;
    private DeviceClient _deviceClient;
    private DeviceRegistrationResult _provisioningResult;

    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
          _security?.Dispose();
          if (_deviceClient != null)
          {
            try
            {
              Task _await = _deviceClient.CloseAsync();
              _await.Wait();
              _Logger.LogInformation($"Disposed azure connection.");
            }
            catch (Exception e)
            {
              _Logger.LogInformation($"Azure connection already disposed.");
            }
            _deviceClient.Dispose();
          }
          _device.DeviceClient = null;
        }
        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below. Set large fields to null.
        disposedValue = true;
      }
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