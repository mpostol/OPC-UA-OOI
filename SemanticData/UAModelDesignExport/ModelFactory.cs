//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;
using TraceMessage = UAOOI.SemanticData.BuildingErrorsHandling.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Class ModelFactory.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.NodesContainer" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IModelFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.NodesContainer" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IModelFactory" />
  internal class ModelFactory : NodesContainer, IModelFactory
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelFactory"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public ModelFactory(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    //IModelFactory
    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="publicationDate">The publication date - when the model was published. This value is used for comparisons if the Model is defined in multiple UANodeSet files.</param>
    /// <param name="version">The version of the model defined in the UANodeSet. This is a human readable string and not intended for programmatic comparisons.</param>
    /// <remarks>The set of objects that the OPC Unified Architecture server makes available to clients is referred to as its Address Space. The namespace is provided to make the BrowseName unique in the Address Space.</remarks>
    void IModelFactory.CreateNamespace(string uri, string publicationDate, string version)
    {
      Namespace _new = new Namespace()
      {
        FilePath = string.Empty,
        InternalPrefix = uri,
        Name = string.Format("Name{0}", m_Count),
        Prefix = string.Format("Prefix{0}", m_Count++),
        Value = uri,
        XmlNamespace = uri,
        XmlPrefix = string.Format("Prefix{0}", m_Count++),
        PublicationDate = publicationDate,
        Version = version
      };
      m_Namespaces.Add(_new);
    }

    //internal  API
    /// <summary>
    /// Exports this instance.
    /// </summary>
    /// <returns>ModelDesign.</returns>
    internal ModelDesign Export()
    {
      List<NodeDesign> _mdNodes = new List<NodeDesign>();
      List<string> path = new List<string>();
      base.ExportNodes(_mdNodes, path, (x, y) => CreateInstanceType(x, y, _mdNodes));
      return new ModelDesign()
      {
        Items = _mdNodes.ToArray<NodeDesign>(),
        Namespaces = m_Namespaces.ToArray<Namespace>(),
        TargetNamespace = m_Namespaces[1].Value,
        AnyAttr = null,
        DefaultLocale = null,
        TargetPublicationDate = DateTime.Today,
        TargetPublicationDateSpecified = true,
        TargetVersion = string.Empty,
        TargetXmlNamespace = null
      };
    }

    //private
    private List<Namespace> m_Namespaces = new List<Namespace>();

    private static int m_Count = 0;

    private void CreateInstanceType(InstanceDesign instance, List<string> browsePath, List<NodeDesign> mdNodes)
    {
      return;
    }
  }
}