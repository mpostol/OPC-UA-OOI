//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  internal interface IDTOProvider: IEnumerable<string>
  {
    /// <summary>
    /// Gets the Data Transfer Object to be transmitted to Azure.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <returns>dynamic.</returns>
    dynamic GetDTO(string repositoryGroup);
  }
}