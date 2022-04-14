//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
    /// Creates the namespace description for the provided <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/>.</param>
    /// <param name="publicationDate">The publication <seealso cref="DateTime"/>- when the model was published. This value is used for comparisons if the Model is defined in multiple files.</param>
    /// <param name="version">The <seealso cref="Version"/> of the model. This is a human readable string and not intended for programmatic comparisons.</param>
    /// <remarks>The set of objects that the OPC Unified Architecture server makes available to clients is referred to as its Address Space. The namespace is provided to make the BrowseName unique in the Address Space.</remarks>
    void IModelFactory.CreateNamespace(Uri uri, DateTime? publicationDate, Version version)
    {
      string uriString = uri == null ? string.Empty : uri.ToString();
      Namespace _new = new Namespace()
      {
        FilePath = string.Empty,
        InternalPrefix = uriString,
        Name = string.Format("Name{0}", m_Count),
        Prefix = string.Format("Prefix{0}", m_Count++),
        Value = uriString,
        XmlNamespace = uriString,
        XmlPrefix = string.Format("Prefix{0}", m_Count++),
        PublicationDate = publicationDate.HasValue ? XmlConvert.ToString(publicationDate.Value, XmlDateTimeSerializationMode.Utc): null,
        Version = version == null ? String.Empty : version.ToString()
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