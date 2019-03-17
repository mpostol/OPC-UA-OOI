//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal class InformationModelFactoryBase : NodesContainer, IModelFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="InformationModelFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public InformationModelFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <remarks>The set of objects that the OPC Unified Architecture server makes available to clients is referred to as its Address Space. The namespace is provided to make the BrowseName unique in the Address Space.</remarks>
    public void CreateNamespace(string uri) { }

  }

}
