//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Interface IModelFactory defines an abstract implementation of the specific representation of th OPC UA Information Model.
  /// </summary>
  public interface IModelFactory : INodeContainer
  {

    /// <summary>
    /// Creates the namespace description for the provided uri. 
    /// </summary>
    /// <remarks>
    /// The set of objects that the OPC Unified Architecture server makes available to clients is referred to as its Address Space. The namespace is provided to make the BrowseName unique in the Address Space.
    /// </remarks>
    /// <param name="uri">The URI.</param>
    void CreateNamespace(string uri);

  }

}
