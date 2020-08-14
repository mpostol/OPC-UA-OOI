//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  internal class PartConfigurationFactory : IConfigurationFactory
  {
    private readonly string _configurationFileName;

    public PartConfigurationFactory(string configurationFileName)
    {
      _configurationFileName = configurationFileName;
    }

    #region IConfigurationFactory

    public event EventHandler<EventArgs> OnAssociationConfigurationChange;

    public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

    public ConfigurationData GetConfiguration()
    {
      throw new NotImplementedException();
    }

    #endregion IConfigurationFactory
  }
}
