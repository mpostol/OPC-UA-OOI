//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Xml;

//TODO Define independent Address Space API #645 - this dependency must be removed
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// The UANodeSet is the root element of the XML document.
  /// </summary>
  /// <remarks>
  /// Dependency on UANodeSet must be removed form the Address Space API
  /// </remarks>
  public interface IUANodeSet
  {
    /// <summary>
    /// Parse the model expressed as the UANodeset XML file compliant with the UANodeset schema.
    /// </summary>
    /// <param name="addressSpaceContext"></param>
    /// <param name="traceEvent"></param>
    /// <returns></returns>
    Uri ParseUAModelContext(INamespaceTable addressSpaceContext, Action<TraceMessage> traceEvent);

    /// <summary>
    /// A list of namespaces represented by URI referred in the document. External references are allowed.
    /// </summary>
    string[] NamespaceUris { get; set; }

    /// <summary>
    /// list of ServerUri entries used in the UANodeSet document
    /// </summary>
    string[] ServerUris { get; set; }

    /// <summary>
    /// An element containing any vendor defined extensions to the UANodeSet.
    /// </summary>
    XmlElement[] Extensions { get; set; }

    /// <summary>
    /// The Address Space nodes collection representation in the UANodeSet.
    /// </summary>
    IUANode[] Items { get; }

    /// <summary>
    /// The last time a document was modified.
    /// </summary>
    DateTime LastModified { get; set; }

    /// <summary>
    /// Determines if the <see cref="IUANodeSet.LastModified"/> is specified.
    /// </summary>
    bool LastModifiedSpecified { get; set; }
  }
}