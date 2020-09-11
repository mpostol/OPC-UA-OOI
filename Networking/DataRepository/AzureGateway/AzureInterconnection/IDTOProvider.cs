//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  /// <summary>
  /// Interface IDTOProvider - used to get access to data cached in the selected RepositoryGroup
  /// </summary>
  internal interface IDTOProvider
  {
    /// <summary>
    /// Gets the Data Transfer Object to be transmitted to Azure.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <returns>dynamic.</returns>
    dynamic GetDTO(string repositoryGroup);
  }
}