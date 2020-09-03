//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;

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
    /// Gets the Azure primary key.
    /// </summary>
    /// <value>The Azure primary key.</value>
    string AzurePrimaryKey { get; }

    /// <summary>
    /// Gets the azure secondary key.
    /// </summary>
    /// <value>The azure secondary key.</value>
    string AzureSecondaryKey { get; }
  }
}