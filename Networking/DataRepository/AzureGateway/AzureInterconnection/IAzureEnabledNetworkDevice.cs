//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using System;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  internal interface IAzureEnabledNetworkDevice
  {
    /// <summary>
    /// Gets required connection parameters for establishing azure connection.
    /// </summary>
    IAzureDeviceParameters AzureDeviceParameters { get; }

    /// <summary>
    /// Gets the time interval when to send device state to Azure.
    /// </summary>
    TimeSpan PublishingInterval { get; }

    /// <summary>
    /// Returns JSON payload which will be passed to Azure.
    /// </summary>
    string CreateMessagePayload();
  }

}
