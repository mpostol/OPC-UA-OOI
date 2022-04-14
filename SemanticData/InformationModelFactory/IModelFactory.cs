//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IModelFactory defines an abstract definition of the specific representation of th OPC UA Information Model.
  /// </summary>
  public interface IModelFactory : INodeContainer
  {
    /// <summary>
    /// Creates the namespace description for the provided <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/>.</param>
    /// <param name="publicationDate">The publication <seealso cref="DateTime"/>- when the model was published. This value is used for comparisons if the Model is defined in multiple files.</param>
    /// <param name="version">The <seealso cref="Version"/> of the model. This is a human readable string and not intended for programmatic comparisons.</param>
    /// <remarks>The set of objects that the OPC Unified Architecture server makes available to clients is referred to as its Address Space. The namespace is provided to make the BrowseName unique in the Address Space.</remarks>
    void CreateNamespace(Uri uri, DateTime? publicationDate, Version version);
  }
}