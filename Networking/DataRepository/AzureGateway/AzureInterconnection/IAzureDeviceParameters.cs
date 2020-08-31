//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  /// <summary>
  /// Interface defining device parameters for establishing azure connection.
  /// </summary>
  public interface IAzureDeviceParameters
  {
    /// <summary>
    /// Gets the transport type used for this device.
    /// </summary>
    TransportType TransportType { get; }

    /// <summary>
    /// Gets the Id corresponding to Azure device id.
    /// </summary>
    string AzureDeviceId { get; }

    /// <summary>
    /// Gets the azure scope id in which given device resides.
    /// </summary>
    string AzureScopeId { get; }

    /// <summary>
    /// Creates security client which will be used for device provisioning.
    /// </summary>
    Task<SecurityProvider> GetSecurityProviderAsync();
  }
}