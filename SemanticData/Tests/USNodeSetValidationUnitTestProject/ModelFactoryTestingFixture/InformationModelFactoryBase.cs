//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.ModelFactoryTestingFixture
{
  /// <summary>
  /// Class InformationModelFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.NodesContainer" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IModelFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.NodesContainer" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IModelFactory" />
  internal class InformationModelFactoryBase : NodesContainer, IModelFactory
  {
    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="publicationDate">The publication date - when the model was published. This value is used for comparisons if the Model is defined in multiple UANodeSet files.</param>
    /// <param name="version">The version of the model defined in the UANodeSet. This is a human readable string and not intended for programmatic comparisons.</param>
    /// <remarks>The set of objects that the OPC Unified Architecture server makes available to clients is referred to as its Address Space. The namespace is provided to make the BrowseName unique in the Address Space.</remarks>
    public void CreateNamespace(string uri, string publicationDate, string version) { }

    internal IEnumerable<NodeFactoryBase> Export()
    {
      List<NodeFactoryBase> nodes = new List<NodeFactoryBase>();
      base.Export(x => nodes.Add(x));
      return nodes;
    }


  }
}