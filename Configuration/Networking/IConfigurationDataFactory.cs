//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking
{

  /// <summary>
  /// Interface IConfigurationDataFactory - creates an instance of <see cref="ConfigurationData"/>
  /// </summary>
  public interface IConfigurationDataFactory
  {
    /// <summary>
    /// Gets and instance of <see cref="ConfigurationData"/>.
    /// </summary>
    /// <returns>Returns an instance of <see cref="ConfigurationData"/>.</returns>
    ConfigurationData GetConfigurationData();
    /// <summary>
    /// Gets or sets the the delegate capturing functionality tha is executed when the configuration is changing.
    /// </summary>
    /// <value>The m_ on changed.</value>
    Action OnChanged { get; set; }
    /// <summary>
    /// Called when the configuration is loaded.
    /// </summary>
    void OnLoaded();
    /// <summary>
    /// Called before the saving the configuration.
    /// </summary>
    void OnSaving();

  }
}
