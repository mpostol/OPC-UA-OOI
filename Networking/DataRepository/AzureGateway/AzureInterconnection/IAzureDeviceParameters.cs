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
  /// <summary>
  /// Interface defining device parameters for establishing azure connection.
  /// </summary>
  public class AzureDeviceParameters
  {
    #region constructor

    /// <summary>
    /// Parses the specified configuration if <paramref name="configuration"/> is not empty, otherwise parse it .
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>AzureDeviceParameters.</returns>
    /// <exception cref="NotImplementedException">parse string</exception>
    internal static AzureDeviceParameters Parse(string configuration)
    {
      AzureDeviceParameters ret = new AzureDeviceParameters();
      if (!String.IsNullOrEmpty(configuration))
        throw new NotImplementedException("parse string");
      return ret;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureDeviceParameters"/> class.
    /// </summary>
    /// <remarks> Must be protected to be used by Mock in UT</remarks>
    protected AzureDeviceParameters()
    {
    }

    #endregion constructor

    #region API

    /// <summary>
    /// Gets the transport type used for this device.
    /// </summary>
    internal TransportType TransportType { get; } = default(TransportType);

    /// <summary>
    /// Gets the Id corresponding to Azure device id.
    /// </summary>
    internal string AzureDeviceId { get; } = string.Empty;

    /// <summary>
    /// Gets the azure scope id in which given device resides.
    /// </summary>
    internal string AzureScopeId { get; } = string.Empty;

    /// <summary>
    /// Gets the Azure primary key.
    /// </summary>
    /// <value>The Azure primary key.</value>
    internal string AzurePrimaryKey { get; } = string.Empty;

    /// <summary>
    /// Gets the azure secondary key.
    /// </summary>
    /// <value>The azure secondary key.</value>
    internal string AzureSecondaryKey { get; } = string.Empty;

    /// <summary>
    /// Gets the time interval when to send device state to Azure.
    /// </summary>
    internal TimeSpan PublishingInterval { get; set; } = TimeSpan.FromSeconds(1.0);

    #endregion API
  }
}