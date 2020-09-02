//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  internal class DataTransfering : CommunicationContext.StateBase
  {
    public DataTransfering(IDTOProvider dataProvider, string repositoryGroup, CommunicationContext communicationContext) : base(communicationContext)
    {
      _dataProvider = dataProvider;
      _repositoryGroup = repositoryGroup;
      _timer = new Timer(Callback, null, TimeSpan.Zero, AzureEnabledNetworkDevice.PublishingInterval);
    }

    #region CommunicationContext.StateBase

    public override ProvisioningRegistrationStatusType GetProvisioningRegistrationStatusType => throw new NotImplementedException();

    public override Task<bool> Connect()
    {
      throw new ApplicationException($"The operation {nameof(Connect)} is not allowed in the {nameof(DataTransfering)}");
    }

    public override Task<bool> Register(IAzureEnabledNetworkDevice device)
    {
      throw new ApplicationException($"The operation {nameof(Connect)} is not allowed in the {nameof(DataTransfering)}");
    }

    public override void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      throw new ApplicationException($"The operation {nameof(Connect)} is not allowed in the {nameof(DataTransfering)}");
    }

    public override void DisconnectRequest()
    {
      _timer.Dispose();
      DeviceClient.CloseAsync().Wait();
      TransitionTo(new AssigneddState(_paretContext));
    }

    #endregion CommunicationContext.StateBase

    #region private

    private readonly IDTOProvider _dataProvider;
    private readonly string _repositoryGroup;
    private Timer _timer;

    private void Callback(object state)
    {
      try
      {
        Logger.LogDebug("Building payload.");
        string payload = JsonConvert.SerializeObject(_dataProvider.GetDTO(_repositoryGroup));
        DeviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes(payload))).Wait();
        Logger.LogDebug("Successfully published device state to Azure.");
      }
      catch (Exception e)
      {
        Logger.LogError(e, "Failed to publish device state.");
      }
    }

    #endregion private
  }
}