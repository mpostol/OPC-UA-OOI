//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommandLine;
using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  /// <summary>
  /// Interface defining device parameters for establishing azure connection.
  /// </summary>
  public class AzureDeviceParameters
  {
    #region constructor

    /// <summary>
    /// Parses the specified configuration if <paramref name="repositoryGroup"/> is not empty, otherwise parse it .
    /// </summary>
    /// <param name="repositoryGroup">The configuration.</param>
    /// <returns>AzureDeviceParameters.</returns>
    /// <exception cref="NotImplementedException">parse string</exception>
    internal static AzureDeviceParameters ParseRepositoryGroup(string repositoryGroup)
    {
      AzureDeviceParameters ret = new AzureDeviceParameters();
      if (String.IsNullOrEmpty(repositoryGroup))
        return ret;
      string[] args = repositoryGroup.Split(' ');
      using (Parser parserInstance = new Parser(x => { x.AutoHelp = false; x.AutoVersion = false; x.HelpWriter = null; }))
        parserInstance.ParseArguments<AzureDeviceParameters>(args).WithParsed<AzureDeviceParameters>(opts => ret = opts).WithNotParsed<AzureDeviceParameters>(X => ReportErrors(X));
      return ret;
    }

    private static void ReportErrors(IEnumerable<Error> errors)
    {
      List<ArgumentOutOfRangeException> errorsList = new List<ArgumentOutOfRangeException>();
      foreach (Error e in errors)
        errorsList.Add(new ArgumentOutOfRangeException($"{e.Tag} with stop processing = {e.StopsProcessing}"));
      throw new AggregateException(errorsList);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureDeviceParameters"/> class.
    /// </summary>
    /// <remarks> Must be public to be used by the command line parser</remarks>
    public AzureDeviceParameters()
    {
    }

    #endregion constructor

    #region API

    /// <summary>
    /// Gets or sets the name of the resource group.
    /// </summary>
    /// <value>The name of the resource group.</value>
    [Value(0, HelpText = "Resource Group Name retrieved as the first identifier", Required = true)]
    public string ResourceGroupName { get; set; }

    /// <summary>
    /// Gets the transport type used for this device.
    /// </summary>
    [Option('t', "transport", HelpText = "TransportType", Default = default(TransportType), Required = false)]
    public TransportType TransportType { get; set; }

    /// <summary>
    /// Gets the Id corresponding to Azure device id.
    /// </summary>
    [Option('d', "DeviceId", HelpText = "a string representing AzureDeviceId", Required = true)]
    public string AzureDeviceId { get; set; }

    /// <summary>
    /// Gets the azure scope id in which given device resides.
    /// </summary>
    [Option('s', "ScopeId", HelpText = "a string representing AzureScopeId", Required = true)]
    public string AzureScopeId { get; set; }

    /// <summary>
    /// Gets the Azure primary key.
    /// </summary>
    /// <value>The Azure primary key.</value>
    [Option('p', "PrimaryKey", HelpText = "a string representing AzurePrimaryKey", Required = true)]
    public string AzurePrimaryKey { get; set; }

    /// <summary>
    /// Gets the azure secondary key.
    /// </summary>
    /// <value>The azure secondary key.</value>
    [Option('k', "SecondaryKey", HelpText = "a string representing AzureSecondaryKey", Required = true)]
    public string AzureSecondaryKey { get; set; }

    /// <summary>
    /// Gets the time interval when to send device state to Azure.
    /// </summary>
    [Option('i', "Interval", HelpText = "an integer representing PublishingInterval", Required = false)]
    public int PublishingIntervalMS { get; set; }

    /// <summary>
    /// Calculates the time interval when to send device state to Azure.
    /// </summary>
    internal TimeSpan PublishingInterval()
    {
      return TimeSpan.FromMilliseconds(Math.Max(1000, PublishingIntervalMS));
    }

    #endregion API
  }
}